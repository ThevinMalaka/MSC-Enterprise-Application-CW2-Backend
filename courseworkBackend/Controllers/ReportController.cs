using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using courseworkBackend.DTO;
using courseworkBackend.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace courseworkBackend.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        //get report by user id
        [HttpGet("User/{userId}")]
        public async Task<List<ReportDTO>> Get(int userId)
        {
            //get all reports from the database
            return await _reportService.GetReportByUserIdAsync(userId);
        }

    }
}

