using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Student
    {
        [Column("Roll No")]
        [Key] //Validations
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(25)")]
        public string userName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(25)")]
        public string firstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(25)")]
        public string lastName { get; set; }

        [Column(TypeName = "nvarchar(25)")]
        public string email { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string phoneNumber { get; set; }

        [Column(TypeName = "nvarchar(6)")]
        public string gender { get; set; }
    }
}