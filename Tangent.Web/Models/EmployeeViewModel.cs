using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tangent.Web.Models
{
    public class EmployeeViewModel
    {
        public SelectList Gender { get; set; }

        public SelectList Race { get; set; }

        public SelectList Position { get; set; }

        [Display(Name = "Race")]
        public string RaceSelection { get; set; }

        [Display(Name = "Position")]
        public string PositionSelection { get; set; }

        [Display(Name = "Gender")]
        public string GenderSelection { get; set; }

        public string Email { get; set; }
    }
}
