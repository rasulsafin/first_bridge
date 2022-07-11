﻿using DM.Domain.Interfaces;
using DM.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DM.Controllers
{
    [ApiController]
    [Route("api/record")]
    public class RecordController : ControllerBase
    {
        public readonly IRecordService _recordService;

        public RecordController(IRecordService recordService)
        {
            _recordService = recordService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _recordService.GetAll();

            return Ok(users);
        }

        [HttpGet("{recordId}")]
        public IActionResult GetById(long recordId)
        {
            var record = _recordService.GetById(recordId);

            return Ok(record);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecordModel recordModel)
        {
            var id = await _recordService.Create(recordModel);

            return Ok(id);
        }
    }
}
