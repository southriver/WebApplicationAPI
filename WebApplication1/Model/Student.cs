using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    [Table("Student")]
    [Index(nameof(TCKimlik), IsUnique = true)]
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(11), MinLength(11)]
        public String StudentId { get; set; }
        [Required]
        [MaxLength(11), MinLength(11)]
        public String TCKimlik{ get; set; }
        [Required]
        [MaxLength(100)]
        public String FullName { get; set; }
        [Required]
        [StringLength(20)]
        public String StudentStatus { get; set; }

    }
}
