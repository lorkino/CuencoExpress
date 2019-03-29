using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Film.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace Film.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]    
        public async Task<JsonResult> Profile([FromBody]  UserDates userDates)
        {
            var userName = User.Identity.Name;
            User user = await _context.Users.Where(a=>a.Email== userName).Include(a=>a.UserDates).FirstOrDefaultAsync();
            user.UserDates = userDates;
     
            await _context.SaveChangesAsync();
            return Json("Ok");


        }
        [HttpPost]
        [Route("saveKnowledges")]
        public async Task<JsonResult> SaveKnowledges([FromBody] List<Knowledges> knowledges )
        {
            try
            {
                var userName = User.Identity.Name;
                User user = await _context.Users.Where(a => a.Email == userName).Include(a => a.UserKnowledges).FirstOrDefaultAsync();
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
                List<UserKnowledges> ListuserKnowledges = new List<UserKnowledges>();
                knowledgesToInsert.ForEach(delegate (Knowledges element)
                {
                    UserKnowledges userKnowledges = new UserKnowledges();
                    userKnowledges.User = user;
                    userKnowledges.Knowledges = element;
                    ListuserKnowledges.Add(userKnowledges);
                });
                user.UserKnowledges = ListuserKnowledges;
                await _context.SaveChangesAsync();
                var asyncIndexResponse = await MyGlobals.elasticClient.IndexDocumentAsync(user);
            }
            catch (Exception e) {
                Debug.Print(e.ToString());

            }
            return Json("Ok");

        }
        [HttpGet]
        public async Task<JsonResult> Profile()
        {
            var userName = User.Identity.Name;
            User user = await _context.Users.Where(a => a.Email == userName).Include(a => a.UserDates).FirstOrDefaultAsync();
            await _context.SaveChangesAsync();
            UserDates userDates = user.UserDates;
            if(userDates!=null)
            userDates.ProfileImgString = Convert.ToBase64String(userDates?.ProfileImg);

            var searchRequest = await MyGlobals.elasticClient.SearchAsync<User>(s =>
            s.Query(q => q
                .Match(c=>c.Field(p=>p.UserKnowledges.First().Knowledges.Value).Query("Arquímedes"))
                ).AllTypes().Index("people"));
            return Json(userDates);


        }
    }
}