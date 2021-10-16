using leave_management.Contract;
using leave_management.Data;
using leave_management.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveHistoryRepository : ILeaveHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public LeaveHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(LeaveHistory entity)
        {
            _context.LeaveHistories.Add(entity);
            return Save();
        }

        public bool Delete(LeaveHistory entity)
        {
            _context.LeaveHistories.Remove(entity);
            return Save();
        }

        public ICollection<LeaveHistory> FindAll()
        {
            return _context.LeaveHistories.ToList();
        }

        public LeaveHistory FindById(int id)
        {
            return _context.LeaveHistories.FirstOrDefault(m => m.LeaveTypeId == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool Update(LeaveHistory entity)
        {
            _context.LeaveHistories.Update(entity);
            return Save();
        }
    }
}
