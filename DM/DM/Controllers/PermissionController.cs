﻿using DM.Domain.Interfaces;
using DM.Domain.Models;
using DM.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/permission")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [Authorize(RoleConst.SuperAdmin)]
        [HttpGet]
        public IActionResult GetAllPermissionsOfUser(long userId)
        {
            var permissions = _permissionService.GetAllPermissionsOfUser(userId);

            return Ok(permissions);
        }

        [Authorize(RoleConst.SuperAdmin)]
        [HttpPost]
        public IActionResult AddPermissionToUser(PermissionModel permissionModel)
        {
            var permissions = _permissionService.AddPermissionToUser(permissionModel);

            return Ok(true);
        }

        [Authorize(RoleConst.SuperAdmin)]
        [HttpDelete]
        public IActionResult DeletePermissionOfUser(PermissionModel permissionModel)
        {
            var result = _permissionService.RemovePermissionFromUser(permissionModel);

            return Ok(result);
        }
    }
}
