using leave_management.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contract
{
    public interface ILeaveHistoryRepository : IRepositoryBase<LeaveRequest>
    {
        Task<bool> UpdatePendingLeaves(LeaveRequest entity);
    }
}
