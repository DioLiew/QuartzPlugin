namespace QuartzPlugin.Settings;

public sealed record class Schedule(bool IgnoreDuplicates, int MaxConcurrency, TimeSpan MaxRunTime, bool OverWriteExistingData, IList<Job> Jobs) {
   public Schedule() : this(IgnoreDuplicates: default, MaxConcurrency: default, MaxRunTime: default, OverWriteExistingData: default, Jobs: []) { }
}