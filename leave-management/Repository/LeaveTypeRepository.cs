using leave_management.Contract;
using leave_management.Data;
using leave_management.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace leave_management.Repository
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public LeaveTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(LeaveType entity)
        {
            _context.LeaveTypes.Add(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveType entity)
        {
            _context.LeaveTypes.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<LeaveType>> FindAll()
        {
            var result = await _context.LeaveTypes.ToListAsync();
            return result;
        }

        public async Task<LeaveType> FindById(int id)
        {
            var result = await _context.LeaveTypes.FirstOrDefaultAsync(m => m.Id == id);
            return result;
        }

        public async Task<bool> Save()
        {
            var result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        public async Task<bool> Update(LeaveType entity)
        {
            _context.LeaveTypes.Update(entity);
            return await Save();
        }
    }
}
