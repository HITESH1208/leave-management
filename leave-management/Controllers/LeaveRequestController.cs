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
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveHistoryRepository _leaveRequestRepo;
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly UserManager<Employee> _userManager;
        private readonly ILeaveTypeRepository _leaveTypeRepo;

        public LeaveRequestController(ILeaveHistoryRepository leaveRequestRepo,IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository, UserManager<Employee> userManager, ILeaveTypeRepository leaveTypeRepo)
        {
            _leaveRequestRepo = leaveRequestRepo;
            _mapper = mapper;
            _leaveAllocationRepository = leaveAllocationRepository;
            _userManager = userManager;
            _leaveTypeRepo = leaveTypeRepo;
        }


        // GET: LeaveRequestController
        public async Task<ActionResult> Index()
        {
            var employee = _userManager.GetUserAsync(User).Result;
            var mappingModel = _mapper.Map<EmployeeVM>(employee);
            var allocationsData = _mapper.Map<List<LeaveAllocationVM>>(await _leaveAllocationRepository.FindLeaveAllocationByEmpId(employee.Id));

            var model = new LeaveDetailsVM
            {
                EmployeeDetails = mappingModel,
                LeaveAllocations = allocationsData
            };
            return View(model);
        }

        
        public async Task<ActionResult> ApplyLeave(int id)
        {
            var employee =  _mapper.Map<EmployeeVM>(_userManager.GetUserAsync(User).Result);
            var leaveDetails = _mapper.Map<DetailLeaveTypeVM>(await _leaveTypeRepo.FindById(id));
            var model = new CreateLeaveRequestVM
            {
                RequestingEmployee = employee,
                LeaveType = leaveDetails,
                DateRequested = DateTime.Now.Date,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ApplyLeave(CreateLeaveRequestVM model)
        {
            if(!ModelState.IsValid)
            { 
                return View(model);
            }
            else
            {
                var createLeaveRequest = new CreateLeaveRequestVM
                {
                    RequestingEmployeeId = model.RequestingEmployee.Id,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Approved = false,
                    LeaveTypeId = model.LeaveType.Id,
                    DateRequested = model.DateRequested
               };

                bool result =  await _leaveRequestRepo.Create(_mapper.Map<LeaveRequest>(createLeaveRequest));
                if (!result)
                { 
                    ModelState.AddModelError("", "Duplicate Leave Request");
                    return View(model);
                }
                else
                {
                   await _leaveRequestRepo.UpdatePendingLeaves(_mapper.Map < LeaveRequest >(createLeaveRequest));
                }
            }
            
            return RedirectToAction(nameof(Index));
        }


        // GET: LeaveRequestController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LeaveRequestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveRequestController/Create
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

        // GET: LeaveRequestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveRequestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: LeaveRequestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveRequestController/Delete/5
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
