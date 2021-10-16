using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.ViewModels
{
    public class LeaveAllocationVM
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }

        public EmployeeVM Employee { get; set; }

        public string EmployeeId { get; set; }

        public DetailLeaveTypeVM LeaveType { get; set; }

        public int LeaveTypeId { get; set; }

        public IEnumerable<SelectListItem> EmployeeList { get; set; }

        public IEnumerable<SelectListItem> LeaveTypeList { get; set; }

    }
}
