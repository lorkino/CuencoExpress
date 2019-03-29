using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Film.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;
using Job = Film.Models.Job;

namespace Film.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class JobController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SaveKnowledges([FromBody] List<Knowledges> knowledges)
        {
            try
            {
                var userName = User.Identity.Name;
                User user = await _context.Users.Where(a => a.Email == userName).Include(a => a.UserKnowledges).FirstOrDefaultAsync();

                Job job = await _context.Job.Where(a => a.UserCreator.UserName == user.UserName).OrderByDescending(a=>a.CreatedDate).Include(a => a.JobKnowledges).FirstOrDefaultAsync();
                MyGlobals.SearchByTags(knowledges.Where(a=>a.Value!=null).ToList());
                //conocimientos existentes
                var eKnowledges = _context.Knowledges.ToList();
                var newItems = knowledges.Where(x => !eKnowledges.Any(y => x.Value == y.Value)).ToList();
                //añadimos solo los conocimientos que ya no esten en BD
                _context.Knowledges.AddRange(newItems);
                //guardamos los cambios
                await _context.SaveChangesAsync();
                //volvemos a tomar los conocimientos existentes ya actualizados previamente
                eKnowledges = _context.Knowledges.ToList();
                //filtramos los conocimientos de la lista con los que vamos a insertar
                var knowledgesToInsert = eKnowledges.Where(x => knowledges.Any(y => x.Value == y.Value)).ToList();
                List<JobKnowledges> ListjobKnowledges = new List<JobKnowledges>();
                knowledgesToInsert.ForEach(delegate (Knowledges element)
                {
                    JobKnowledges jobKnowledges = new JobKnowledges();
                    jobKnowledges.Job = job;
                    jobKnowledges.Knowledges = element;
                    ListjobKnowledges.Add(jobKnowledges);
                });
                job.JobKnowledges = ListjobKnowledges;
                await _context.SaveChangesAsync();
                
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());

            }
            return Ok("Ok");

        }

        [HttpPost]
        public async Task<IActionResult> CreateJob([FromBody]  Job job)
        {
            var body = new StreamReader(Request.Body);
            //The modelbinder has already read the stream and need to reset the stream index
            body.BaseStream.Seek(0, SeekOrigin.Begin);
            var requestBody = body.ReadToEnd();
            //etc, we use this for an audit trail
            var userName = User.Identity.Name;
            User user = await _context.Users.Where(a => a.Email == userName).Include(a => a.UserDates).FirstOrDefaultAsync();

            job.UserCreator = user;
            _context.Job.Add(job);
            await _context.SaveChangesAsync();
            return Ok();


        }
       
     
    }
}