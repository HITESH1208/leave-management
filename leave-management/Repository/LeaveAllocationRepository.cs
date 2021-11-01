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

        public bool CheckAllocation(int leaveTypeId, string employeeId)
        {
            var year = DateTime.Now.Year;
            return FindAll().Where(q => q.EmployeeId == employeeId && q.LeaveTypeId == leaveTypeId && q.Year==year).Any();
        }

        public bool Create(LeaveAllocation entity)
        {
            _context.LeaveAllocations.Add(entity);
            return Save();
        }

        public bool Delete(LeaveAllocation entity)
        {
            _context.LeaveAllocations.Remove(entity);
            return Save();
        }

        public ICollection<LeaveAllocation> FindAll()
        {
            return _context.LeaveAllocations
                  .Include(q=>q.LeaveType)
                  .ToList();
        }

        public LeaveAllocation FindById(int id)
        {
            return _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .Include(q => q.Employee)
                .FirstOrDefault(q => q.Id == id);
                
        }

        public IEnumerable<LeaveAllocation> FindLeaveAllocationByEmpId(string empId)
        {
            var period = DateTime.Now.Year;
            return FindAll().Where(o => o.EmployeeId == empId && o.Year == period).ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool Update(LeaveAllocation entity)
        {
            _context.LeaveAllocations.Update(entity);
            return Save();
        }
    }
}
