using System.ComponentModel.DataAnnotations;

namespace testAPI.Models
{
    public struct User
    {
        [Required(ErrorMessage="โปรดใส่ userName ก่อน")]
        [StringLength(10 ,MinimumLength=1, ErrorMessage="Error wa {0} {1} {2}")]
        public string firstName { get; set; }
        [Required(ErrorMessage="โปรดใส่ lastName ก่อน")]
        public string lastName { get; set; }
    }

    public class Position
    {
        public string Latitude { get; set; } 
        public string Longitude { get; set; } 

    }
}