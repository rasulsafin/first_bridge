﻿using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using DM.Domain.Interfaces;
using DM.Domain.Implementations;
using DM.Domain.Helpers;
using DM.Domain.Models;

using DM.DAL.Entities;
using DM.DAL;

using DM.Helpers;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/record")]
    public class RecordController : ControllerBase
    {
        private readonly DmDbContext _context;
        private readonly UserEntity _currentUser;

        private readonly IRecordService _recordService;

        public RecordController(IRecordService recordService, DmDbContext context, CurrentUserService userService)
        {
            _context = context;
            _recordService = recordService;
            _currentUser = userService.CurrentUser;
        }

        [Authorize(new string[] { RoleConst.Admin, RoleConst.Owner })]
        [HttpGet]
        public IActionResult GetAll()
        {
            var records = _recordService.GetAll();

            return Ok(records);
        }

        [Authorize(new string[] { RoleConst.Admin, RoleConst.Owner })]
        [HttpGet("{recordId}")]
        public IActionResult GetById(long recordId)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsById(_context, _currentUser, PermissionType.Record);

            if (!permission)
            {
                return StatusCode(403);
            }

            var record = _recordService.GetById(recordId);

            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [Authorize(new string[] { RoleConst.Admin, RoleConst.Owner })]
        [HttpPost]
        public async Task<IActionResult> Create(RecordModel recordModel)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForCreate(_context, _currentUser, PermissionType.Record);

            if (!permission)
            {
                return StatusCode(403);
            }

            var id = await _recordService.Create(recordModel);

            if (id == 0)
            {
                return BadRequest();
            }

            return Ok(id);
        }

        [Authorize(new string[] { RoleConst.Admin, RoleConst.Owner })]
        [HttpPut]
        public async Task<IActionResult> Update(RecordModel recordModel)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForUpdate(_context, _currentUser, PermissionType.Record);

            if (!permission)
            {
                return StatusCode(403);
            }

            var checker = await _recordService.Update(recordModel);

            if (!checker)
            {
                return BadRequest("the fields must not contain invalid characters");
            }

            return Ok(checker);
        }


        [Authorize(new string[] { RoleConst.Admin, RoleConst.Owner })]
        [HttpDelete]
        public async Task<IActionResult> Delete(long recordId)
        {
            var permission = AuthorizationHelper.CheckUserPermissionsForDelete(_context, _currentUser, PermissionType.Record);

            if (!permission)
            {
                return StatusCode(403);
            }

            var checker = await _recordService.Delete(recordId);

            if (!checker)
            {
                return NotFound();
            }

            return Ok(checker);
        }
    }
}
