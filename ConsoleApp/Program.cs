using System;
using System.Collections.Generic;
using System.Diagnostics;
using ConsoleApp.DataAccess;
using ConsoleApp.Entities;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var dbContext = new IpaDbContext())
            {
                var sw = new Stopwatch();
                sw.Start();

                for (int i = 0; i < 50; i++)
                {
                    var flow = new Flow
                    {
                        NetElementId = Guid.NewGuid().ToString(),
                        FlowDirection = FlowDirection.In,
                        FlowStates = new List<FlowState>()
                    };

                    for (int j = 0; j < 10; j++)
                    {
                        var oilFlowState = new OilFlowState();
                        flow.FlowStates.Add(oilFlowState);
                    }
                
                    dbContext.Flows.Add(flow);
                }

                dbContext.SaveChanges();

                sw.Stop();
                Console.WriteLine($"Elapsed: {sw.Elapsed}");
            }

            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
