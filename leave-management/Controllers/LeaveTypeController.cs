using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contract;
using leave_management.Data.Models;
using leave_management.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace leave_management.Controllers
{
    [Authorize(Roles ="Admin")]
    public class LeaveTypeController : Controller
    {
        private readonly ILeaveTypeRepository _repo;
        private readonly IMapper _mapper;

        public LeaveTypeController(ILeaveTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        // GET: LeaveTypeController
        public async Task<ActionResult> Index()
        {
            var leaveTypes = await _repo.FindAll();
            var model = _mapper.Map<List<LeaveType>,List<DetailLeaveTypeVM>>(leaveTypes.ToList());
            return View(model);
        }

        // GET: LeaveTypeController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var details = await _repo.FindById(id);
            var VMData = _mapper.Map<DetailLeaveTypeVM>(details);
            return View(VMData);
        }

        // GET: LeaveTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Name,DefaultDays")]DetailLeaveTypeVM model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                    var leaveType = _mapper.Map<LeaveType>(model);
                    leaveType.DateCreated = DateTime.Now;
                    var success = await _repo.Create(leaveType);
                    if(!success)
                    {
                        ModelState.AddModelError("", "Something Went wrong");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something Went wrong");
                return View(model);
            }
        }

        // GET: LeaveTypeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var leaveData = await _repo.FindById(id);
            var modealData = _mapper.Map<DetailLeaveTypeVM>(leaveData);
            return View(modealData);
        }

        // POST: LeaveTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, DetailLeaveTypeVM model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                    var leaveData = _mapper.Map<LeaveType>(model);
                    leaveData.DateCreated = DateTime.Now;
                    var success = await _repo.Update(leaveData);
                    if(!success)
                    {
                        ModelState.AddModelError("", "Something went wrong");
                        return View(model);
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: LeaveTypeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var details = await _repo.FindById(id);
            var VMData = _mapper.Map<DetailLeaveTypeVM>(details);
            return View(VMData);

        }

        // POST: LeaveTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, DetailLeaveTypeVM model)
        {
            try
            {
                var modelData = _mapper.Map<LeaveType>(model);
                var success = await _repo.Delete(modelData);
                if(!success)
                {
                    ModelState.AddModelError("", "Something Went Wrong ");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}
