using Microsoft.AspNetCore.Mvc;
using Quartz;

namespace QuartzJob_Scheduler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ISchedulerFactory schedulerFactory;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ISchedulerFactory schedulerFactory)
        {
            _logger = logger;
            this.schedulerFactory = schedulerFactory;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var job = JobBuilder.Create<BackgroundJob>()
            .WithIdentity(name: "BackgroundJob", group: "JobGroup")
            .UsingJobData("ConsoleOutput", "Executing background job using JobDataMap")
            .UsingJobData("UseJobDataMapConsoleOutput", true)
            .Build();

            var trigger = TriggerBuilder.Create()
            .WithIdentity(name: "RepeatingTrigger", group: "TriggerGroup")
            .WithSimpleSchedule(o => o
                .RepeatForever()
                .WithIntervalInSeconds(1))
            .Build();

            var scheduler = await this.schedulerFactory.GetScheduler().ConfigureAwait(false);
            await scheduler.ScheduleJob(job, trigger);

            return Ok();
        }
    }
}