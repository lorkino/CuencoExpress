using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Film.Models;
using Film.ViewModels;
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

        [HttpGet]
        [Route("getMe")]
        public async Task<JsonResult> GetMe()
        {
            var userName = User.Identity.Name;
            User userComplete = await _context.Users.Where(a => a.Email == userName).Include(a => a.UserDates).Include(b => b.UserKnowledges).ThenInclude(post => post.Knowledges).Include(a=>a.Notifications).FirstOrDefaultAsync();

            //llamamos al token de acceso
            Tuple<string, DateTime> token = Film.Controllers.Account.BuildToken(userComplete);
            userComplete.Token = token.Item1;
            userComplete.TokenExpiration = token.Item2;
            ViewUser userSecure = userComplete;
            return Json(userSecure);
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

                //  if (MyGlobals.elasticClient.Indices.in("people").Exists)
                //       MyGlobals.elasticClient.CreateIndex("people");

                //var createIndexResponse = MyGlobals.elasticClient.Indices.Create("people", c => c
                //    .Map<User>(m => m
                //        .AutoMap()
        
                //    )
                //);
                //   if (MyGlobals.elasticClient.IndexExists("people").Exists)
                //       MyGlobals.elasticClient.CreateIndex("people");
                //    var createIndexResponse = MyGlobals.elasticClient.CreateIndex("people", c => c
                //    .Mappings(m => m
                //        .Map<User>(mm => mm
                //            .AutoMap()
                //            .Properties(p => p
                //                .GeoPoint(g => g
                //                    .Name(n => n.UserDates.Location)
                //                )
                //            )
                //        )
                //    )
                //);
                var asyncIndexResponse = await MyGlobals.elasticClient.IndexDocumentAsync(user);
            }
            catch (Exception e) {
                Debug.Print(e.ToString());

            }
            return Json("Su perfil se ha actualizado correctamente");

        }
        [HttpGet]
        public async Task<JsonResult> Profile()
        {
            var userName = User.Identity.Name;
            User user = await _context.Users.Where(a => a.Email == userName).Include(a => a.UserDates).FirstOrDefaultAsync();
            await _context.SaveChangesAsync();
           // UserDates userDates = user.UserDates;
            //if(userDates!=null)
            //userDates.ProfileImgString = Convert.ToBase64String(userDates?.ProfileImg);

            var searchRequest = await MyGlobals.elasticClient.SearchAsync<User>(s =>
            s.Query(q => q
                .Match(c=>c.Field(p=>p.UserKnowledges.First().Knowledges.Value).Query("Arquímedes"))
                ).Index("people"));
            return Json(user.UserDates);


        }
    }
}