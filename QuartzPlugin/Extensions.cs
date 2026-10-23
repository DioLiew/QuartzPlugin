using Microsoft.Extensions.DependencyInjection;
using Quartz;
using QuartzPlugin.Settings;

namespace QuartzPlugin;

public static class Extensions {
   extension(IServiceCollection self) {
      public IServiceCollection AddQuartz(Func<Schedule> f, Action<IQuartzBuilder, IList<Job>> scheduleJob) => self.AddQuartz(
         c => {
            var settings = f();
            self.Configure<QuartzOptions>(
               o => {
                  o.Scheduling.IgnoreDuplicates = settings.IgnoreDuplicates;
                  o.Scheduling.OverwriteExistingData = settings.OverWriteExistingData;
               }
            );
            c.UseTimeZoneConverter();
            c.AddJobTimeout(settings.MaxRunTime);
            c.UseDefaultThreadPool(x => x.MaxConcurrency = settings.MaxConcurrency);
            scheduleJob(c, settings.Jobs);
         }
      )
      .AddQuartzHostedService(x => x.WaitForJobsToComplete = true);
   }
}