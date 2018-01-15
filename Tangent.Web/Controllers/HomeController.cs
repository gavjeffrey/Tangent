using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tangent.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Tangent.Web.Infrastructure;
using Newtonsoft.Json;
using Tangent.Web.Extensions;

namespace Tangent.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHttpClient httpClient;

        public HomeController(IHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            //get all employees to do some analysis on
            var response = await httpClient.ReadAsStringAsync("employee");

            var employees = JsonConvert.DeserializeObject<List<Employee>>(response.Content);

            //initialize view model with basic metrics
            DashboardViewModel viewModel = new DashboardViewModel
            {
                NumberOfEmployees = employees.Count(),
                BirthdaysThisMonth = employees.Where(x => x.Birth_date.Month == DateTime.Now.Month).Count()
            };

            //Get count of different races to display in bar chart
            var ethnicityGroups = employees.GroupBy(x => x.Race.GetEnumValueFromEnumMemberAttribute())
                                    .Select(x => new { xAxis = x.Key, yAxis = x.Count() })
                                    .ToArray();

            viewModel.EthnicityGroups = JsonConvert.SerializeObject(ethnicityGroups);

            //Get count of people per position for donut chart
            var positions = employees.GroupBy(x => x.Position.Name,
                x => x.Position.Name, (key, g) => new { label = key, value = g.Count() });

            viewModel.PositionGroups = JsonConvert.SerializeObject(positions);

            //get count of male vs female employees for gender donut
            var genders = employees.GroupBy(x => x.Gender.GetEnumValueFromEnumMemberAttribute())
                            .Select(x => new { label = x.Key, value = x.Count() })
                            .ToArray();

            viewModel.GenderGroups = JsonConvert.SerializeObject(genders);

            //Populate data for line chart to show employee count over the years
            var employeeCountOverYears = employees.GroupBy(x => x.Years_worked)
                .Select(x => new GraphPoint { xAxis = DateTime.Now.AddYears(-x.Key).Year, aSeries = x.Count() })
                .ToList();

            var minYear = employeeCountOverYears.Min(x => x.xAxis);
            var maxYear = employeeCountOverYears.Max(x => x.xAxis);

            var previousYear = 0;
            for (int x = minYear; x <= maxYear; x++) //fill in missing years to show continuous line graph
            {
                var empCountForYear = employeeCountOverYears.Where(i => i.xAxis == x).FirstOrDefault();
                var previousYearItem = employeeCountOverYears.FirstOrDefault(i => i.xAxis == previousYear);

                if (empCountForYear == null)
                {
                    employeeCountOverYears.Add(new GraphPoint { xAxis = x, aSeries = previousYearItem.aSeries });
                }
                else
                {
                    //if an employee was at the company in the previous year then the employ count should add to the previous value                   
                    if (previousYearItem != null)
                    {
                        empCountForYear.aSeries += previousYearItem.aSeries;
                    }
                }

                previousYear = x;
            }

            viewModel.EmployeeCountOverYears = JsonConvert.SerializeObject(employeeCountOverYears.OrderBy(x => x.xAxis).Select(x => new { xAxis = x.xAxis.ToString(), aSeries = x.aSeries }).OrderBy(x => x.xAxis));

            //Get count of outstanding reviews for simple metric
            response = await httpClient.ReadAsStringAsync("review");

            var reviews = JsonConvert.DeserializeObject<List<Review>>(response.Content);
            viewModel.PendingReviews = reviews.Count();

            return View(viewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
