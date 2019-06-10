using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Film.Models;
using Film.ViewModels;
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
        //añadimos nuevos conocieminetos a la base de datos
        //Añadimos al trabajo sus conocimientos
        [HttpPost]
        public async Task<IActionResult> SaveKnowledges([FromBody] List<Knowledges> knowledges)
        {
            try
            {
                var userName = User.Identity.Name;
                User user = await _context.Users.Where(a => a.Email == userName).Include(a => a.UserKnowledges).FirstOrDefaultAsync();

               
                     Job job = await _context.Job.Where(a => a.UserCreator.UserName == user.UserName).OrderByDescending(a => a.CreatedDate).Include(a => a.JobKnowledges).Include(a=>a.UserPreWorker).FirstOrDefaultAsync();
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
                    JobKnowledges jobKnowledges = new JobKnowledges
                    {
                        Job = job,
                        Knowledges = element
                    };
                    ListjobKnowledges.Add(jobKnowledges);
                });
               
                job.JobKnowledges = ListjobKnowledges;
                _context.Entry<Job>(job).State = EntityState.Detached;
                await _context.SaveChangesAsync();
                //conectamos con los usuarios para preasignarles un aviso de trabajo
                await AsignJob(knowledges.Where(a => a.Value != null).ToList(),job);


            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
                return BadRequest(e.ToString());

            }
            return Ok("Ok");

        }
        public class MyLogger
        {
            public void Log(string component, string message)
            {
                Console.WriteLine("Component: {0} Message: {1} ", component, message);
            }
        }

        public  async Task<bool> AsignJob(List<Knowledges> knowledges, Job job) {
            try
            {
                var userName = User.Identity.Name;
                User usercreator = await _context.Users.Where(a => a.Email == userName).Include(a => a.JobsPreworker).FirstOrDefaultAsync();

                List<User> usersPreWorkers = MyGlobals.SearchByTags(knowledges.ToList());
                usersPreWorkers.RemoveAll(a => a.Email == userName);
                List<JobPreWorker> jobPreWorkers = new List<JobPreWorker>();
                usersPreWorkers.ForEach(delegate (User element)
                {
                    JobPreWorker jobPreWorker = new JobPreWorker
                    {
                        Job = job,
                        UserPreWorker = element
                    };
                   
                    jobPreWorkers.Add(jobPreWorker);
                });
                usersPreWorkers.ForEach(delegate (User element)
                {
                    
                    element.JobsPreworker = jobPreWorkers;
                    
                });
                var logger = new MyLogger();

                var log = _context.FirstOrDefault(o => o.Id > 0);
                Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                usercreator.JobsPreworker = jobPreWorkers;
                job.UserPreWorker = jobPreWorkers;
                var results= await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e) {
                throw e;
            }
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
        [HttpGet]
        public async Task<JsonResult> Offers()
        {
            var userName = User.Identity.Name;



            List<JobPreWorker> user = await _context.JobPreWorker.Where(a => a.UserPreWorker.Email == userName).Include(a=>a.UserPreWorker).Include(a => a.Job).ToListAsync();
            List<Job> jobs = user.Select(a=>a.Job).ToList();
            // List<Job> jobs = await _context.Job.Include(b=>b.UserPreWorker).Where(c=>c.UserPreWorker.First().).OrderByDescending(a => a.CreatedDate).Include(a => a.JobKnowledges).ThenInclude(post => post.Knowledges).Include(a => a.UserWorker).ToListAsync();
           // List <Job> jobs = await _context.Job.Include(b => b.UserPreWorker).ThenInclude(b=>b.UserPreWorker).OrderByDescending(a => a.CreatedDate).ToListAsync();
            List<ViewJob> job = jobs.Select(a => (ViewJob)a).ToList();
            return Json(job);
        }
        //cantidad de jobs
        [HttpGet]
        public async Task<JsonResult> OffersNumber()
        {
            var userName = User.Identity.Name;
            User user = await _context.Users.Where(a => a.Email == userName).FirstOrDefaultAsync();
            //int jobs = await _context.Job.Where(a => a.UserPreWorker.Contains(user)).CountAsync();
            return Json(1);
        }
        [HttpGet]
        public async Task<JsonResult> Jobs()
        {
            var userName = User.Identity.Name;
            User user = await _context.Users.Where(a => a.Email == userName).FirstOrDefaultAsync();
            List<Job> jobs =  await _context.Job.Where(a => a.UserCreator == user).OrderByDescending(a => a.CreatedDate).Include(a=>a.JobKnowledges).ThenInclude(post => post.Knowledges).Include(a=>a.UserWorker).ToListAsync();
            List<ViewJob> job = jobs.Select(a => (ViewJob)a).ToList();
            return Json(job);
        }
        //cantidad de jobs
        [HttpGet]
        public async Task<JsonResult> JobsNumber()
        {
            var userName = User.Identity.Name;
            User user = await _context.Users.Where(a => a.Email == userName).FirstOrDefaultAsync();
            int jobs = await _context.Job.Where(a => a.UserCreator == user).CountAsync();
            return Json(jobs);
        }

    }
}