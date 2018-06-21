using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;

namespace testAPI.Models
{
    public struct User
    {
        [Required(ErrorMessage = "โปรดใส่ userName ก่อน")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Error wa {0} {1} {2}")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "โปรดใส่ lastName ก่อน")]
        public string lastName { get; set; }
    }

    public class Position
    {
        [JsonIgnore]
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public void CalculateLocation()
        {
            Console.WriteLine(Latitude + Longitude);
        }

    }

    /*public static class TrimStrings
    {
        public static T TrimStringProperties<T>(ref T input)
        {
            var stringProperties = input.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));

            foreach (var stringProperty in stringProperties)
            {
                string currentValue = (string)stringProperty.GetValue(input, null);
                if (currentValue != null)
                    stringProperty.SetValue(input, currentValue.Trim(), null);
            }
            return input;
        }

    }*/
}