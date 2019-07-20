using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Film.Models;
using Film.SignalR;
using Film.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Nest;
using WebPush;
using Job = Film.Models.Job;

namespace Film.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class JobController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<NotificationsHub> _hubContext;
        public JobController(ApplicationDbContext context, IHubContext<NotificationsHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
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

               
                     Job job = await _context.Job.Where(a => a.UserCreator.UserName == user.UserName).OrderByDescending(a => a.CreatedDate).Include(a => a.JobKnowledges).FirstOrDefaultAsync();
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
                //_context.Entry<Job>(job).State = EntityState.Detached;
                await _context.SaveChangesAsync();
                //conectamos con los usuarios para preasignarles un aviso de trabajo
                await AsignJob(knowledges.Where(a => a.Value != null).ToList(),job);
               

            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
                return BadRequest(e.ToString());

            }
            return Ok(Json("Se ha publicado el trabajo correctamente"));

        }
        public class MyLogger
        {
            public void Log(string component, string message)
            {
                Console.WriteLine("Component: {0} Message: {1} ", component, message);
            }
        }

        protected  async Task<bool> AsignJob(List<Knowledges> knowledges, Job job) {
            try
            {
                var userName = User.Identity.Name;
                User usercreator = await _context.Users.Where(a => a.Email == userName).FirstOrDefaultAsync();

                List<User> usersPreWorkers = MyGlobals.SearchByTags(knowledges.ToList());
                usersPreWorkers.RemoveAll(a => a.Email == userName);
                List<User> usersNotifications = _context.Users.Where(a=> usersPreWorkers.Any(b=>a.Email==b.Email)).Include(a=>a.UserDates).Include(a=>a.Suscription).ToList();
                

                
                List<JobPreWorker> jobPreWorkers = new List<JobPreWorker>();
                usersPreWorkers.ForEach(delegate (User element)
                {
                    User user = _context.Users.Where(a => a.Email == element.Email).FirstOrDefault();

                    JobPreWorker jobPreWorker = new JobPreWorker
                    {
                        Job = job,
                        UserPreWorker = user
                    };
                    jobPreWorkers.Add(jobPreWorker);
                }); 
               
                //List<JobPreWorker> jobPreWorkers = new List<JobPreWorker>();
                job.UserPreWorker = jobPreWorkers;
                


                var results = await _context.SaveChangesAsync();
                NotificationModel message = new NotificationModel()
                {
                    Message = "Una nueva oferta de trabajo",
                    Title = "Oferta!",
                    Url = this.Request.Host.ToString()
                };

                  NotificationsController.Broadcast(message, usersNotifications);
                NotificationsController NC = new NotificationsController(_context,_hubContext);
                await NC.AddNotifications(usersNotifications, 0,_context);
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
        [HttpGet("{i}")]
        public async Task<JsonResult> Offers(int i)
        {
            var userName = User.Identity.Name;



            List<JobPreWorker> user = await _context.JobPreWorkers.Where(a => a.UserPreWorker.Email == userName).Include(a => a.Job).Skip((i - 1) * 5).Take(5).ToListAsync();
   //List<Job> offers = await _context.Job.Where(a => a.UserPreWorker.Any(b => b.UserName.Contains(userName))).ToListAsync();
            List<ViewJob> offers = user.Select(a=> (ViewJob)a.Job).ToList();

            // List<Job> jobs = await _context.Job.Include(b=>b.UserPreWorker).Where(c=>c.UserPreWorker.First().).OrderByDescending(a => a.CreatedDate).Include(a => a.JobKnowledges).ThenInclude(post => post.Knowledges).Include(a => a.UserWorker).ToListAsync();
            // List <Job> jobs = await _context.Job.Include(b => b.UserPreWorker).ThenInclude(b=>b.UserPreWorker).OrderByDescending(a => a.CreatedDate).ToListAsync();

            //List<ViewJob> job = jobs.Select(a => (ViewJob)a).ToList();
            return Json(offers);
        }
        //cantidad de jobs
        [HttpGet]
        public async Task<JsonResult> OffersNumber()
        {
            var userName = User.Identity.Name;

            List<JobPreWorker> user = await _context.JobPreWorkers.Where(a => a.UserPreWorker.Email == userName).Include(a => a.Job).ToListAsync();
            //List<Job> offers = await _context.Job.Where(a => a.UserPreWorker.Any(b => b.UserName.Contains(userName))).ToListAsync();
            int offers = user.Select(a => (ViewJob)a.Job).ToList().Count();
            //int jobs = await _context.Job.Where(a => a.UserPreWorker.Contains(user)).CountAsync();
            return Json(offers);
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