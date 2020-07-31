using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using LiteDB;
using LiteDB3App.Entities;

namespace LiteDB3App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string fileName = @"d:\work835\Temp\MyData.v3.db";
            File.Delete(fileName);

            using (var db = new LiteDatabase(fileName))
            {
                var sw = new Stopwatch();
                sw.Start();

                var flows = CreateFlows();
                var col = db.GetCollection<Flow>("flows");
                col.Insert(flows);

                sw.Stop();
                Console.WriteLine($@"Elapsed: {sw.Elapsed}");
            }

            Console.WriteLine(@"Done!");
            Console.ReadLine();
        }

        private static List<Flow> CreateFlows()
        {
            var flows = new List<Flow>();

            for (int i = 0; i < 10000; i++)
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
                
                flows.Add(flow);
            }

            return flows;
        }
    }
}
