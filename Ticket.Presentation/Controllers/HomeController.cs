using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.Presentation.Helpers;
using Ticket.Presentation.Models;

namespace Ticket.Presentation.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {

        private readonly string[] colors = { "fc-event-success", "fc-event-primary", "fc-event-danger" };


        public async Task<IActionResult> Index(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = User.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            }


            ViewData["Events"] = await GetEvents(new List<int>() { Convert.ToInt32(id) });
            ViewData["SelectedUserId"] = id;

            return View();
        }

        public async Task<List<CalendarEventModel>> GetEvents(List<int> userId)
        {
            var repositories = await HttpClientHelper.SendPostRequest<List<int>, IEnumerable<CalendarModel>>(userId, "Calendar/get-all-by-userId", CookieHelper.GetToken(Request, "oaut.Cookie")); ;

            Random rnd = new Random();

            List<CalendarEventModel> result = new List<CalendarEventModel>();
            foreach (var repo in repositories)
            {
                result.Add(new CalendarEventModel()
                {
                    id = repo.Id,
                    start = repo.StartDate.ToString("yyyy-MM-dd HH:mm").Replace(" ", "T"),
                    end = repo.EndDate.ToString("yyyy-MM-dd HH:mm").Replace(" ", "T"),
                    description = repo.Description,
                    title = repo.Title +" - "+repo.NameSurname,
                    className = colors[rnd.Next(0, 2)]
                });
            }

            return result;
        }


        [HttpPost]
        public async Task<IActionResult> AddNewApp(CalendarModel model)
        {

            string userId = User.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            model.UserCode = Convert.ToInt32(userId);

            string userName=User.Claims.First(x => x.Type == ClaimTypes.GivenName).Value;

            if (model.Id == 0)
            {
                var result = await HttpClientHelper.SendPostRequest(model, "Calendar/create-appointment", CookieHelper.GetToken(Request, "oaut.Cookie"));
            }
            else
            {
                var result = await HttpClientHelper.SendPostRequest(model, "Calendar/update-appointment", CookieHelper.GetToken(Request, "oaut.Cookie"));
            }

            Random rnd = new Random();

            CalendarEventModel retval = new CalendarEventModel()
            {
                id = model.Id,
                start = model.StartDate.ToString("yyyy-MM-dd HH:mm").Replace(" ", "T"),
                end = model.EndDate.ToString("yyyy-MM-dd HH:mm").Replace(" ", "T"),
                description = model.Description,
                title = model.Title +" - "+ userName,
                className = colors[rnd.Next(0, 2)],
                isUpdate = model.Id > 0
            };

            return Ok(retval);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateApp(CalendarModel model)
        {
            var existedModel = await HttpClientHelper.SendGetRequest<CalendarModel>("Calendar/get-by-id?id=" + model.Id, CookieHelper.GetToken(Request, "oaut.Cookie"));

            existedModel.StartDate = model.StartDate;
            existedModel.EndDate = model.EndDate;

            var result = await HttpClientHelper.SendPostRequest(existedModel, "Calendar/update-appointment", CookieHelper.GetToken(Request, "oaut.Cookie"));

            return Ok("Ok");
        }


        [HttpGet]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var model = await HttpClientHelper.SendGetRequest<CalendarModel>("Calendar/get-by-id?id=" + id, CookieHelper.GetToken(Request, "oaut.Cookie"));

            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvent(int userId)
        {
            var result = await GetEvents(new List<int>() { Convert.ToInt32(userId) });

            return Ok(result);
        }


        public IActionResult Ticket()
        {
            return View();
        }

    }
}
