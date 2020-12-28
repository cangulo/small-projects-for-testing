using AWSTestSQS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWSTestSQS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IEnqueueMessageSQS _enqueueMessageSQS;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IEnqueueMessageSQS enqueueMessageSQS, ILogger<WeatherForecastController> logger)
        {
            _enqueueMessageSQS = enqueueMessageSQS ?? throw new ArgumentNullException(nameof(enqueueMessageSQS));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetAsync()
        {
            return await _enqueueMessageSQS.GetQueues();
        }
    }
}
