using System.ComponentModel.DataAnnotations;

namespace testAPI.Models
{
    public class User
    {
        [Required(ErrorMessage="โปรดใส่ userName ก่อน")]
        [StringLength(10 ,MinimumLength=1, ErrorMessage="Error wa {0} {1} {2}")]
        public string firstName { get; set; }
        [Required(ErrorMessage="โปรดใส่ lastName ก่อน")]
        public string lastName { get; set; }
    }
}