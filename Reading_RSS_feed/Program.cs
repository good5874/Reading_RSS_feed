using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Linq;
using System.Xml;
using Reading_RSS_feed.Models;
using Quartz;
using System.Configuration;
using Quartz.Impl;

namespace Reading_RSS_feed
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("Service start. Time: {0}", DateTime.Now.ToString("HH:mm:ss"));

            IJobDetail parseJobDetail = JobBuilder.Create<ParseJob>()
                .WithIdentity("ParserJob")
                .Build();
            ITrigger parseTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(1)
                    .RepeatForever())
                .Build();

            IScheduler scheduler = new StdSchedulerFactory().GetScheduler().Result;

            scheduler.ScheduleJob(parseJobDetail, parseTrigger);

            scheduler.Start();

            Console.ReadLine();
        }
    }
}
