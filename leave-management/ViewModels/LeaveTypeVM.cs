using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.ViewModels
{
    public class DetailLeaveTypeVM
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name="Leave Type")]
        public string Name { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Required]
        [Range(1,25,ErrorMessage ="Please enter a valid number.")]
        [Display(Name="Default Number of Days")]
        public int DefaultDays { get; set; }
    }

    public class CreateLeaveTypeVM
    {
        [Required]
        [Display(Name = "Leave Type")]
        public string Name { get; set; }
    }

}
