using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.ViewModels
{
    public class LeaveRequestVM
    {
        [Key]
        public int Id { get; set; }

        public EmployeeVM RequestingEmployee { get; set; }

        public string RequestingEmployeeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
      
        public DetailLeaveTypeVM LeaveType { get; set; }

        public int LeaveTypeId { get; set; }

        public DateTime DateRequested { get; set; }

        public DateTime DateActioned { get; set; }

        public bool? Approved { get; set; }

        public EmployeeVM ApprovedBy { get; set; }

        public string ApprovedById { get; set; }
    }

    public class LeaveDetailsVM
    {
        public int Id { get; set; }
        public EmployeeVM EmployeeDetails { get; set; }
        public List<LeaveAllocationVM> LeaveAllocations { get; set; }
    }

    public class CreateLeaveRequestVM
    {
        public int Id { get; set; }
        public EmployeeVM RequestingEmployee { get; set; }
        public string RequestingEmployeeId { get; set; }
        [Required]
        [BindProperty, DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [BindProperty, DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public DetailLeaveTypeVM LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime DateRequested { get; set; }
        public bool? Approved { get; set; }
    }
}
