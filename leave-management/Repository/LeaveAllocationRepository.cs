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
    public class LeaveAllocationRepository: ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _context;
        public LeaveAllocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckAllocation(int leaveTypeId, string employeeId)
        {
            var year = DateTime.Now.Year;
            var result = await FindAll();
            return result.Where(q => q.EmployeeId == employeeId && q.LeaveTypeId == leaveTypeId && q.Year == year).Any();
        }

        public async Task<bool> Create(LeaveAllocation entity)
        {
            await _context.LeaveAllocations.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveAllocation entity)
        {
            _context.LeaveAllocations.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<LeaveAllocation>> FindAll()
        {
            var result = await _context.LeaveAllocations
                  .Include(q => q.LeaveType).ToListAsync();
            return result;
        }

        public async Task<LeaveAllocation> FindById(int id)
        {
            var result = await _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .Include(q => q.Employee)
                .FirstOrDefaultAsync(q => q.Id == id);
            return result;
                
        }

        public async Task<IEnumerable<LeaveAllocation>> FindLeaveAllocationByEmpId(string empId)
        {
            var period = DateTime.Now.Year;
            var result = await FindAll();
            return result.Where(o => o.EmployeeId == empId && o.Year == period).ToList();
        }

        public async Task<bool> Save()
        {
            var result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        public async Task<bool> Update(LeaveAllocation entity)
        {
            _context.LeaveAllocations.Update(entity);
            return await Save();
        }
    }
}
