using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tangent.Web.Infrastructure;
using Newtonsoft.Json;
using Tangent.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Tangent.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace Tangent.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IHttpClient httpClient;

        public EmployeeController(IHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            EmployeeViewModel viewModel = new EmployeeViewModel
            {
                Race = CreateSelectListForEnum<Race>(),

                Gender = CreateSelectListForEnum<Gender>()
            };
            
            //query employee service to get possible positions
            var response = await httpClient.ReadAsStringAsync("employee");
            var employees = JsonConvert.DeserializeObject<List<Employee>>(response.Content);

            List<SelectListItem> positions = new List<SelectListItem>
            {
                new SelectListItem { Text = "", Value = "" }
            };

            positions.AddRange(employees.Select(x => x.Position)
                                    .GroupBy(x => new { x.Id, x.Name })
                                    .Select(x => new SelectListItem { Text = x.Key.Name, Value = x.Key.Id.ToString() }));

            viewModel.Position = new SelectList(positions, "Value", "Text");

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SearchEmployee([FromBody]EmployeeViewModel EmployeeSearch)
        {
            //it is actually a lot of unneccessary queries to the employee service, 
            //in reality I might just cache the results I got from querying for position data

            StringBuilder queryUrl = new StringBuilder();

            queryUrl.Append("employee/?");

            if (!string.IsNullOrWhiteSpace(EmployeeSearch.PositionSelection))
            {
                queryUrl.Append("position=");
                queryUrl.Append(EmployeeSearch.PositionSelection);
                queryUrl.Append("&");
            }
            if (!string.IsNullOrWhiteSpace(EmployeeSearch.RaceSelection))
            {
                queryUrl.Append("race=");
                queryUrl.Append(EmployeeSearch.RaceSelection);
                queryUrl.Append("&");
            }
            if (!string.IsNullOrWhiteSpace(EmployeeSearch.GenderSelection))
            {
                queryUrl.Append("gender=");
                queryUrl.Append(EmployeeSearch.GenderSelection);
                queryUrl.Append("&");
            }
            if (!string.IsNullOrWhiteSpace(EmployeeSearch.Email))
            {
                queryUrl.Append("email__contains=");
                queryUrl.Append(EmployeeSearch.Email);                
            }

            queryUrl.Length--; //will remove & or ? 
            
            var response = await httpClient.ReadAsStringAsync(queryUrl.ToString());

            var employees = JsonConvert.DeserializeObject<List<Employee>>(response.Content);

            return PartialView("_PartialEmployeeResult", employees);
        }

        private SelectList CreateSelectListForEnum<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            List<SelectListItem> list = new List<SelectListItem>
            {
                new SelectListItem { Text = "", Value = "" }
            };

            foreach (var item in Enum.GetValues(typeof(T)))
            {
                Enum currentEnumItem = (Enum)Enum.Parse(typeof(T), item.ToString(), false);

                list.Add(new SelectListItem { Text = currentEnumItem.GetEnumValueFromEnumMemberAttribute(), Value = currentEnumItem.GetName() });
            }

            return new SelectList(list, "Value", "Text");
        }
    }

}