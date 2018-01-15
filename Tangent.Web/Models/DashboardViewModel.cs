using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tangent.Web.Models
{
    public class DashboardViewModel
    {
        public int NumberOfEmployees { get; set; }

        public int BirthdaysThisMonth { get; set; }

        public int PendingReviews { get; set; }

        public string EthnicityGroups { get; set; }

        public string GenderGroups { get; set; }

        public string PositionGroups { get; set; }

        public string EmployeeCountOverYears { get; set; }
    }
}
