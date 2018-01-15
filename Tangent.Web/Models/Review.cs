using Newtonsoft.Json;
using System;

namespace Tangent.Web.Models
{
    [JsonConverter(typeof(ReviewConverter))]
    public class Review
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Salary { get; set; }
        public ReviewType Type { get; set; }
        public int Employee { get; set; }
        public Position Position { get; set; }
    }
}
