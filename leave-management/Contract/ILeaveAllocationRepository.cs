using leave_management.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contract
{
    public interface ILeaveAllocationRepository: IRepositoryBase<LeaveAllocation>
    {
        Task<bool> CheckAllocation(int leaveTypeId,string employeeId);
        Task<IEnumerable<LeaveAllocation>> FindLeaveAllocationByEmpId(string empId);
    }
}
