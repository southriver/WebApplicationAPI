using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace WebApplication1.Model
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(2), MinLength(2)]
        public String DeptCode { get; set; }
        [Required]
        [MaxLength(4), MinLength(4)]
        public String CourseCode { get; set; }
        [Required]
        [StringLength(20)]
        public String CourseDesc { get; set; }

    }
}
