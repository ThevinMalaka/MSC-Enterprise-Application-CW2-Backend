using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using workoutService.DTO;
using workoutService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace workoutService.Controllers
{
    [Route("[controller]")]
    public class ReportController : Controller
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        //get report by user id
        [HttpGet("{userId}")]
        [Authorize]
        public async Task<List<ReportDTO>> Get(int userId)
        {
            //get all reports from the database
            return await _reportService.GetReportByUserIdAsync(userId);
        }
    }
}

