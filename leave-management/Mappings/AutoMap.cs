using AutoMapper;
using leave_management.Data.Models;
using leave_management.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Mappings
{
    public class AutoMap : Profile
    {
        public AutoMap()
        {
            CreateMap<LeaveType, DetailLeaveTypeVM>().ReverseMap();
            CreateMap<Employee, EmployeeVM>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationVM>().ReverseMap();
            CreateMap<LeaveHistory, LeaveHistoryVM>().ReverseMap();
            CreateMap<EditLeaveAllocationVM, LeaveAllocation>().ReverseMap();
        }
    }
}
