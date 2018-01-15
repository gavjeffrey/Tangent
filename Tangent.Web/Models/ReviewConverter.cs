using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Tangent.Web.Models
{
    public class ReviewConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Review));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);

            Review review = new Review
            {
                Id = jo["id"].Value<int>(),
                Date = jo["date"].Value<DateTime>(),
                Salary = jo["salary"].Value<decimal>(),
                Type = (ReviewType)Enum.Parse(typeof(ReviewType), jo["type"].Value<string>(), true),
                Employee = jo["employee"].Value<int>(),
                Position = new Position { Id = jo["position"].Value<int>() }
            };
            return review;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
