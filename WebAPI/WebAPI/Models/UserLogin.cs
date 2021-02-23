using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class UserLogin
    {
        [Key] //Validations
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(25)")]
        public string userName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(25)")]
        public string password { get; set; }

        public string userType { get; set; }
    }
}
