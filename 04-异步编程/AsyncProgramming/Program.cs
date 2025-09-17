using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProgramming
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== C# 异步编程学习 Demo ===\n");

            // 1. 基础异步编程
            await BasicAsyncProgramming();

            // 2. Task和async/await
            await TaskAndAsyncAwait();

            // 3. 并行编程
            await ParallelProgramming();

            // 4. 异步I/O操作
            await AsyncIOOperations();

            // 5. 取消令牌
            await CancellationTokens();

            Console.WriteLine("\n按任意键退出...");
            Console.ReadKey();
        }

        static async Task BasicAsyncProgramming()
        {
            Console.WriteLine("1. 基础异步编程:");

            // 同步方法
            Console.WriteLine("开始同步任务...");
            var syncResult = DoWorkSync("同步任务", 2000);
            Console.WriteLine($"同步结果: {syncResult}");

            // 异步方法
            Console.WriteLine("开始异步任务...");
            var asyncResult = await DoWorkAsync("异步任务", 2000);
            Console.WriteLine($"异步结果: {asyncResult}");

            Console.WriteLine();
        }

        static async Task TaskAndAsyncAwait()
        {
            Console.WriteLine("2. Task和async/await:");

            // 创建多个异步任务
            var task1 = DoWorkAsync("任务1", 1000);
            var task2 = DoWorkAsync("任务2", 1500);
            var task3 = DoWorkAsync("任务3", 800);

            Console.WriteLine("所有任务已启动，等待完成...");

            // 等待所有任务完成
            var results = await Task.WhenAll(task1, task2, task3);
            Console.WriteLine("所有任务完成:");
            foreach (var result in results)
            {
                Console.WriteLine($"  {result}");
            }

            // 等待任意一个任务完成
            var tasks = new[]
            {
                DoWorkAsync("快速任务", 500),
                DoWorkAsync("慢速任务", 3000),
                DoWorkAsync("中等任务", 1500)
            };

            Console.WriteLine("\n等待任意一个任务完成...");
            var completedTask = await Task.WhenAny(tasks);
            var completedResult = await completedTask;
            Console.WriteLine($"第一个完成的任务: {completedResult}");

            Console.WriteLine();
        }

        static async Task ParallelProgramming()
        {
            Console.WriteLine("3. 并行编程:");

            // 使用Parallel.For
            Console.WriteLine("使用Parallel.For:");
            var stopwatch = Stopwatch.StartNew();
            Parallel.For(1, 6, i =>
            {
                Thread.Sleep(1000); // 模拟工作
                Console.WriteLine($"  Parallel.For 任务 {i} 完成");
            });
            stopwatch.Stop();
            Console.WriteLine($"Parallel.For 总耗时: {stopwatch.ElapsedMilliseconds}ms");

            // 使用Parallel.ForEach
            Console.WriteLine("\n使用Parallel.ForEach:");
            var numbers = new[] { 1, 2, 3, 4, 5 };
            stopwatch.Restart();
            Parallel.ForEach(numbers, num =>
            {
                Thread.Sleep(1000); // 模拟工作
                Console.WriteLine($"  Parallel.ForEach 处理 {num} 完成");
            });
            stopwatch.Stop();
            Console.WriteLine($"Parallel.ForEach 总耗时: {stopwatch.ElapsedMilliseconds}ms");

            // 使用Task.Run
            Console.WriteLine("\n使用Task.Run:");
            stopwatch.Restart();
            var tasks = new List<Task>();
            for (int i = 1; i <= 5; i++)
            {
                int taskId = i; // 捕获循环变量
                tasks.Add(Task.Run(() =>
                {
                    Thread.Sleep(1000); // 模拟工作
                    Console.WriteLine($"  Task.Run 任务 {taskId} 完成");
                }));
            }
            await Task.WhenAll(tasks);
            stopwatch.Stop();
            Console.WriteLine($"Task.Run 总耗时: {stopwatch.ElapsedMilliseconds}ms");

            Console.WriteLine();
        }

        static async Task AsyncIOOperations()
        {
            Console.WriteLine("4. 异步I/O操作:");

            // 模拟文件读取
            Console.WriteLine("模拟异步文件读取:");
            var fileContent = await ReadFileAsync("sample.txt");
            Console.WriteLine($"文件内容: {fileContent}");

            // 模拟网络请求
            Console.WriteLine("\n模拟异步网络请求:");
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync("https://httpbin.org/delay/1");
                Console.WriteLine($"网络请求成功，响应长度: {response.Length}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"网络请求失败: {ex.Message}");
            }

            // 模拟数据库操作
            Console.WriteLine("\n模拟异步数据库操作:");
            var users = await GetUsersAsync();
            Console.WriteLine($"获取到 {users.Count} 个用户:");
            foreach (var user in users)
            {
                Console.WriteLine($"  {user.Name} - {user.Email}");
            }

            Console.WriteLine();
        }

        static async Task CancellationTokens()
        {
            Console.WriteLine("5. 取消令牌:");

            // 创建取消令牌源
            using var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            // 启动一个长时间运行的任务
            var longRunningTask = LongRunningTaskAsync("长时间任务", 5000, cancellationToken);

            // 3秒后取消任务
            _ = Task.Run(async () =>
            {
                await Task.Delay(3000);
                Console.WriteLine("3秒后取消任务...");
                cts.Cancel();
            });

            try
            {
                var result = await longRunningTask;
                Console.WriteLine($"任务完成: {result}");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("任务被取消");
            }

            // 超时取消示例
            Console.WriteLine("\n超时取消示例:");
            using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(2));
            try
            {
                var timeoutTask = DoWorkAsync("超时任务", 5000, timeoutCts.Token);
                var timeoutResult = await timeoutTask;
                Console.WriteLine($"超时任务完成: {timeoutResult}");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("任务因超时被取消");
            }

            Console.WriteLine();
        }

        // 同步工作方法
        static string DoWorkSync(string taskName, int delayMs)
        {
            Console.WriteLine($"  开始 {taskName}...");
            Thread.Sleep(delayMs);
            Console.WriteLine($"  {taskName} 完成");
            return $"{taskName} 结果";
        }

        // 异步工作方法
        static async Task<string> DoWorkAsync(string taskName, int delayMs, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"  开始 {taskName}...");
            await Task.Delay(delayMs, cancellationToken);
            Console.WriteLine($"  {taskName} 完成");
            return $"{taskName} 结果";
        }

        // 模拟异步文件读取
        static async Task<string> ReadFileAsync(string fileName)
        {
            Console.WriteLine($"  开始读取文件 {fileName}...");
            await Task.Delay(1000); // 模拟I/O延迟
            Console.WriteLine($"  文件 {fileName} 读取完成");
            return $"这是 {fileName} 的内容";
        }

        // 模拟异步数据库操作
        static async Task<List<User>> GetUsersAsync()
        {
            Console.WriteLine("  开始查询数据库...");
            await Task.Delay(1500); // 模拟数据库查询延迟
            Console.WriteLine("  数据库查询完成");

            return new List<User>
            {
                new User { Name = "张三", Email = "zhangsan@example.com" },
                new User { Name = "李四", Email = "lisi@example.com" },
                new User { Name = "王五", Email = "wangwu@example.com" }
            };
        }

        // 长时间运行的任务
        static async Task<string> LongRunningTaskAsync(string taskName, int delayMs, CancellationToken cancellationToken)
        {
            Console.WriteLine($"  开始 {taskName}...");
            
            // 模拟工作，定期检查取消令牌
            for (int i = 0; i < delayMs / 1000; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(1000, cancellationToken);
                Console.WriteLine($"  {taskName} 进度: {i + 1}s");
            }
            
            Console.WriteLine($"  {taskName} 完成");
            return $"{taskName} 结果";
        }
    }

    // 用户类
    public class User
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
