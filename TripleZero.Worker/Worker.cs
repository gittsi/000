using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using TripleZero.Worker.Helper;

namespace TripleZero.Worker
{
    public class Worker
    {
        private static BackgroundWorker worker = new BackgroundWorker();

        static void Main(string[] args)
             => new Worker().MainAsync().GetAwaiter().GetResult();
        public async Task MainAsync()
        {
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            Console.WriteLine("Starting Application...");

            worker.RunWorkerAsync();


            Console.ReadKey();
        }

        static void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine(e.ProgressPercentage.ToString());
        }

        static async void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("Starting to do some work now...");
            int i = 1;

            var delay = 100;

            while(i<20)
            {
                try
                {                    
                    Thread.Sleep(delay);                  

                    if (i > 11 && delay<1000) throw new Exception("test ex");
                    worker.ReportProgress(Convert.ToInt32((100.0 * i) / 10));

                    i += 1;
                    delay = 100;                    
                }
                catch (Exception ex)
                {
                    
                    delay = delay/2 + delay;
                    Consoler.WriteLineInColor(ex.Message, ConsoleColor.Red);
                    Thread.Sleep(delay);
                }                
            }

            e.Result = i;
        }

        static void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Value Of i = " + e.Result.ToString());
            Console.WriteLine("Done now...");

            worker.RunWorkerAsync();
        }
    }
}
