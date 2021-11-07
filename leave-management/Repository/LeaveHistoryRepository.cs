using leave_management.Contract;
using leave_management.Data;
using leave_management.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveHistoryRepository : ILeaveHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        public LeaveHistoryRepository(ApplicationDbContext context, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _context = context;
            _leaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task<bool> Create(LeaveRequest entity)
        {
            var ExistingLeaveRequests = _context.LeaveRequests.Where(p => p.LeaveTypeId == entity.LeaveTypeId &&
            p.RequestingEmployeeId == entity.RequestingEmployeeId).ToList();
            
            foreach(var item in ExistingLeaveRequests)
            {
                if(entity.StartDate <= item.EndDate && entity.EndDate>=item.EndDate ||
                   entity.StartDate <= item.EndDate && entity.EndDate <= item.EndDate)
                {
                    return false;
                }
            }

            _context.LeaveRequests.Add(entity);
            return await Save();
        }


        public async Task<bool> UpdatePendingLeaves(LeaveRequest entity)
        {
            var leaveAllocationData = await _context.LeaveAllocations
                                 .Include(q => q.LeaveType)
                                 .Include(q => q.Employee)
                                 .FirstOrDefaultAsync(o => o.EmployeeId == entity.RequestingEmployeeId && o.LeaveTypeId == entity.LeaveTypeId && o.Year==DateTime.Now.Year);
            if (leaveAllocationData != null && leaveAllocationData.NumberOfDays > 0)
            {
                var dateDifference = (entity.EndDate - entity.StartDate).Days;
                leaveAllocationData.NumberOfDays = leaveAllocationData.NumberOfDays - dateDifference;
                _context.LeaveAllocations.Update(leaveAllocationData);
                return await Save();
            }

            return false;
        }


        public async Task<bool> Delete(LeaveRequest entity)
        {
            _context.LeaveRequests.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<LeaveRequest>> FindAll()
        {
            var result = await _context.LeaveRequests.ToListAsync();
            return result;
        }

        public async Task<LeaveRequest> FindById(int id)
        {
            var result = await _context.LeaveRequests.FirstOrDefaultAsync(m => m.LeaveTypeId == id);
            return result;
        }

        public async Task<bool> Save()
        {
            var result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        public async Task<bool> Update(LeaveRequest entity)
        {
            _context.LeaveRequests.Update(entity);
            return await Save();
        }
    }
}
