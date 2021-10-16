using leave_management.Contract;
using leave_management.Data;
using leave_management.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public LeaveTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(LeaveType entity)
        {
            _context.LeaveTypes.Add(entity);
            return Save();
        }

        public bool Delete(LeaveType entity)
        {
            _context.LeaveTypes.Remove(entity);
            return Save();
        }

        public ICollection<LeaveType> FindAll()
        {
            return _context.LeaveTypes.ToList();
        }

        public LeaveType FindById(int id)
        {
            return _context.LeaveTypes.FirstOrDefault(m => m.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool Update(LeaveType entity)
        {
            _context.LeaveTypes.Update(entity);
            return Save();
        }
    }
}
