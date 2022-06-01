using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieSuggestion.Job.Jobs;

namespace MovieSuggestion.Job.Hangfire
{
    public class RecurringJobs
    {
        public static void TaskOperations()
        {
            /*  RemoveIfExists yöntemini çağırarak var olan yinelenen bir işi kaldırabilirsiniz. 
                Böyle tekrar eden bir iş olmadığında bir istisna oluşturmaz */

            // https://crontab.guru/ cron örnekleri
            // !!! Silinen job ı dashboard dan da kaldır. Ya da RemoveIfExists koduyla sil. 
            // RecurringJob.RemoveIfExists(nameof(ErrorLogJob));
            // RecurringJob.AddOrUpdate<ErrorLogJob>(nameof(ErrorLogJob), job => job.Process(), Cron.Minutely, TimeZoneInfo.Local);
            // string windowsZoneId = TZConvert.RailsToWindows("Istanbul");
            //var timeZone = TimeZoneInfo.FindSystemTimeZoneById(windowsZoneId);

           

            RecurringJob.RemoveIfExists(nameof(SuccessLogJob));
            RecurringJob.AddOrUpdate<SuccessLogJob>(nameof(SuccessLogJob), job => job.Process(), Cron.Daily);

            RecurringJob.RemoveIfExists(nameof(MovieListJob));
            RecurringJob.AddOrUpdate<MovieListJob>(nameof(MovieListJob), job => job.Process(), Cron.Hourly);


        }
    }
}
