using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contract;
using leave_management.Data.Models;
using leave_management.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace leave_management.Controllers
{
    public class LeaveAllocationController : Controller
    {
        private readonly ILeaveTypeRepository _leaveTypeRepo;
        private readonly ILeaveAllocationRepository _leaveAllocationRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;

        public LeaveAllocationController(ILeaveTypeRepository leavetypeRepo,ILeaveAllocationRepository leaveAllocationRepo,IMapper mapper, UserManager<Employee> userManager)
        {
            _leaveTypeRepo = leavetypeRepo;
            _leaveAllocationRepo = leaveAllocationRepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: LeaveAllocationController
        public ActionResult Index()
        {
           var leaveTypes =  _leaveTypeRepo.FindAll();
           var leaveTypeModel = _mapper.Map<List<DetailLeaveTypeVM>>(leaveTypes);
            var model = new CreateLeaveAllocationVM
            {
                LeaveTypes = leaveTypeModel,
                NumberOfUpdate = 0
            };
            return View(model);
        }

        public ActionResult SetLeave(int id)
        {
            var leaveType = _leaveTypeRepo.FindById(id);
            var employees = _userManager.GetUsersInRoleAsync("Employee").Result;
            foreach (var emp in employees)
            {
                if (!_leaveAllocationRepo.CheckAllocation(id, emp.Id))
                {
                    var allocation = new LeaveAllocationVM
                    {
                        DateCreated = DateTime.Now,
                        EmployeeId = emp.Id,
                        LeaveTypeId = id,
                        NumberOfDays = leaveType.DefaultDays,
                        Year = DateTime.Now.Year
                    };

                    var leaveAllocation = _mapper.Map<LeaveAllocation>(allocation);
                    _leaveAllocationRepo.Create(leaveAllocation);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult ListEmployees()
        {
            var empList = _userManager.GetUsersInRoleAsync("Employee").Result;
            var model = _mapper.Map<List<EmployeeVM>>(empList);
            return View(model);
        }



        // GET: LeaveAllocationController/Details/5
        public ActionResult Details(string id)
        {
            var employee = _userManager.FindByIdAsync(id).Result;
            var mappingModel = _mapper.Map<EmployeeVM>(employee);
            var allocationsData = _mapper.Map<List<LeaveAllocationVM>>(_leaveAllocationRepo.FindLeaveAllocationByEmpId(id));

            var model = new ViewAllocationVM
            {
                Employee = mappingModel,
                LeaveAllocations = allocationsData
            };

            return View(model);
        }

        // GET: LeaveAllocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveAllocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAllocationController/Edit/5
        public ActionResult Edit(int id)
        {
            var leaveAllocation = _leaveAllocationRepo.FindById(id);
            var model = _mapper.Map<EditLeaveAllocationVM>(leaveAllocation);
            return View(model);
        }

        // POST: LeaveAllocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditLeaveAllocationVM model)
        {
            try
            {
               if(!ModelState.IsValid)
                {
                    return View(model);
                }
               else
                {
                    var originalData = _leaveAllocationRepo.FindById(model.Id);
                    originalData.NumberOfDays = model.NumberOfDays;
                    var allocation = _mapper.Map<LeaveAllocation>(originalData);
                    var success = _leaveAllocationRepo.Update(allocation);
                    if (!success)
                    {
                        ModelState.AddModelError("", "Something went wrong while saving");
                        return View(model);
                    }
                }
                return RedirectToAction(nameof(Details),new {id=model.EmployeeId });
            }
            catch(Exception ex)
            {
                return View(model);
            }
        }

        // GET: LeaveAllocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveAllocationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
