using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.DataAccess;
using ConsoleApp1.Entities;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var dbContext = new IpaDbContext())
            {
                var sw = new Stopwatch();
                sw.Start();

                for (int i = 0; i < 5000; i++)
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
