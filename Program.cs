using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DotnetAsync3
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //Example1(watch);
            //Example2(watch);

            try
            {
                var task1 = RunAsync3s(watch);
                var task2 = RunAsync7s(watch);
                var task3 = RunAsync10s(watch);
                Task.WaitAll(task1, task2, task3);
            }
            catch (AggregateException ae)
            {
                string errors = string.Empty;

                foreach (var e in ae.InnerExceptions)
                {
                    errors += e.Message;
                }

                //Console.WriteLine($"ERRORS! {errors}");
                throw new Exception($"Error in Tasks! {errors}");
            }

            watch.Stop();
            Console.Read();
        }

        #region ExampleRuns
        public static void Example1(Stopwatch watch)
        {
            Console.WriteLine($"Example2 Start!: {watch.Elapsed}");
            var result1 = RunAsync10s(watch).Result;
            var result2 = RunAsync7s(watch).Result;
            var result3 = RunAsync3s(watch).Result;

            Console.WriteLine($"Example2 Done!: {watch.Elapsed}");
            Console.WriteLine($"Result: {result1}, {result2}, {result3} ");
        }

        public static void Example2(Stopwatch watch)
        {
            Console.WriteLine($"Example3 Start!: {watch.Elapsed}");

            Console.WriteLine("PRESTART");
            var task4 = RunAsync3s(watch);
            Task.WaitAll(task4);
            Console.WriteLine("PREEND " + task4.Result);

            var task1 = RunAsync10s(watch);
            var task2 = RunAsync7s(watch);
            var task3 = RunAsync3s(watch);
            Task.WaitAll(task1, task2, task3);
            
            var result1 = task1.Result;
            var result2 = task2.Result;
            var result3 = task3.Result;

            Console.WriteLine($"Example3 Done!: {watch.Elapsed}");
            Console.WriteLine($"Result: {result1}, {result2}, {result3} ");
        }
        #endregion

        #region AsyncMethods
        public static async Task<int> RunAsync10s(Stopwatch watch)
        {
            Console.WriteLine($"RunAsync10s Start!: {watch.Elapsed}");
            await Task.Delay(10000);
            Console.WriteLine($"RunAsync10s Done!: {watch.Elapsed}");
            return 10;
        }

        public static async Task<string> RunAsync7s(Stopwatch watch)
        {
            Console.WriteLine($"RunAsync7s Start!: {watch.Elapsed}");

            // await Task.Delay(7000);
            // Simulate exception here
            await Task.Delay(1000);
            throw new Exception("This is a test!");

            Console.WriteLine($"RunAsync7s Done!: {watch.Elapsed}");
            return "7s Done!";
        }

        public static async Task<bool> RunAsync3s(Stopwatch watch)
        {
            Console.WriteLine($"RunAsync3s Start!: {watch.Elapsed}");
            await Task.Delay(3000);
            Console.WriteLine($"RunAsync3s Done!: {watch.Elapsed}");
            return false;
        }
        #endregion
    }
}
