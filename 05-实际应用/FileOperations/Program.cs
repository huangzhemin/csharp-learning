using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FileOperations
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== C# 文件操作学习 Demo ===\n");

            // 1. 基本文件操作
            BasicFileOperations();

            // 2. 目录操作
            DirectoryOperations();

            // 3. 流操作
            StreamOperations();

            // 4. 异步文件操作
            await AsyncFileOperations();

            // 5. JSON文件处理
            await JSONFileOperations();

            // 6. 文件监控
            FileMonitoring();

            Console.WriteLine("\n按任意键退出...");
            Console.ReadKey();
        }

        static void BasicFileOperations()
        {
            Console.WriteLine("1. 基本文件操作:");

            string fileName = "test.txt";
            string content = "Hello, C# 文件操作!\n这是第二行内容。\n包含中文字符。";

            // 写入文件
            File.WriteAllText(fileName, content, Encoding.UTF8);
            Console.WriteLine($"文件 {fileName} 已创建");

            // 读取文件
            string readContent = File.ReadAllText(fileName, Encoding.UTF8);
            Console.WriteLine($"文件内容:\n{readContent}");

            // 检查文件是否存在
            if (File.Exists(fileName))
            {
                Console.WriteLine($"文件 {fileName} 存在");
                
                // 获取文件信息
                FileInfo fileInfo = new FileInfo(fileName);
                Console.WriteLine($"文件大小: {fileInfo.Length} 字节");
                Console.WriteLine($"创建时间: {fileInfo.CreationTime}");
                Console.WriteLine($"最后修改时间: {fileInfo.LastWriteTime}");
            }

            // 复制文件
            string copyFileName = "test_copy.txt";
            File.Copy(fileName, copyFileName);
            Console.WriteLine($"文件已复制到 {copyFileName}");

            // 移动文件
            string moveFileName = "test_moved.txt";
            File.Move(copyFileName, moveFileName);
            Console.WriteLine($"文件已移动到 {moveFileName}");

            // 删除文件
            File.Delete(moveFileName);
            Console.WriteLine($"文件 {moveFileName} 已删除");

            Console.WriteLine();
        }

        static void DirectoryOperations()
        {
            Console.WriteLine("2. 目录操作:");

            string baseDir = "TestDirectory";
            string subDir1 = Path.Combine(baseDir, "SubDir1");
            string subDir2 = Path.Combine(baseDir, "SubDir2");

            // 创建目录
            Directory.CreateDirectory(subDir1);
            Directory.CreateDirectory(subDir2);
            Console.WriteLine($"目录 {baseDir} 及其子目录已创建");

            // 创建一些文件
            File.WriteAllText(Path.Combine(subDir1, "file1.txt"), "文件1内容");
            File.WriteAllText(Path.Combine(subDir1, "file2.txt"), "文件2内容");
            File.WriteAllText(Path.Combine(subDir2, "file3.txt"), "文件3内容");

            // 列出目录内容
            Console.WriteLine($"\n{baseDir} 目录内容:");
            string[] entries = Directory.GetFileSystemEntries(baseDir, "*", SearchOption.AllDirectories);
            foreach (string entry in entries)
            {
                if (File.Exists(entry))
                {
                    Console.WriteLine($"  文件: {entry}");
                }
                else if (Directory.Exists(entry))
                {
                    Console.WriteLine($"  目录: {entry}");
                }
            }

            // 获取目录信息
            DirectoryInfo dirInfo = new DirectoryInfo(baseDir);
            Console.WriteLine($"\n目录信息:");
            Console.WriteLine($"  名称: {dirInfo.Name}");
            Console.WriteLine($"  完整路径: {dirInfo.FullName}");
            Console.WriteLine($"  父目录: {dirInfo.Parent?.Name}");
            Console.WriteLine($"  创建时间: {dirInfo.CreationTime}");

            // 删除目录
            Directory.Delete(baseDir, true);
            Console.WriteLine($"\n目录 {baseDir} 已删除");

            Console.WriteLine();
        }

        static void StreamOperations()
        {
            Console.WriteLine("3. 流操作:");

            string fileName = "stream_test.txt";

            // 使用FileStream写入
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
            {
                writer.WriteLine("第一行数据");
                writer.WriteLine("第二行数据");
                writer.WriteLine("第三行数据");
                writer.Flush();
            }
            Console.WriteLine("使用FileStream写入完成");

            // 使用FileStream读取
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
            {
                Console.WriteLine("使用FileStream读取:");
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine($"  {line}");
                }
            }

            // 使用MemoryStream
            using (MemoryStream ms = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(ms, Encoding.UTF8))
            {
                writer.WriteLine("MemoryStream数据");
                writer.Flush();

                // 重置位置到开始
                ms.Position = 0;

                using (StreamReader reader = new StreamReader(ms, Encoding.UTF8))
                {
                    Console.WriteLine("\nMemoryStream内容:");
                    Console.WriteLine($"  {reader.ReadToEnd()}");
                }
            }

            // 清理
            File.Delete(fileName);

            Console.WriteLine();
        }

        static async Task AsyncFileOperations()
        {
            Console.WriteLine("4. 异步文件操作:");

            string fileName = "async_test.txt";
            string content = "这是异步文件操作的内容\n包含多行数据\n用于演示异步I/O";

            // 异步写入
            await File.WriteAllTextAsync(fileName, content, Encoding.UTF8);
            Console.WriteLine("异步写入完成");

            // 异步读取
            string readContent = await File.ReadAllTextAsync(fileName, Encoding.UTF8);
            Console.WriteLine("异步读取内容:");
            Console.WriteLine(readContent);

            // 异步读取所有行
            string[] lines = await File.ReadAllLinesAsync(fileName, Encoding.UTF8);
            Console.WriteLine("\n异步读取所有行:");
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine($"  行 {i + 1}: {lines[i]}");
            }

            // 异步追加内容
            await File.AppendAllTextAsync(fileName, "\n这是追加的内容", Encoding.UTF8);
            Console.WriteLine("\n异步追加完成");

            // 再次读取验证
            string finalContent = await File.ReadAllTextAsync(fileName, Encoding.UTF8);
            Console.WriteLine("最终内容:");
            Console.WriteLine(finalContent);

            // 清理
            File.Delete(fileName);

            Console.WriteLine();
        }

        static async Task JSONFileOperations()
        {
            Console.WriteLine("5. JSON文件处理:");

            // 创建一些数据
            var person = new Person
            {
                Name = "张三",
                Age = 25,
                Email = "zhangsan@example.com",
                Hobbies = new List<string> { "编程", "阅读", "运动" }
            };

            string jsonFileName = "person.json";

            // 序列化并保存到文件
            string jsonString = JsonSerializer.Serialize(person, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            await File.WriteAllTextAsync(jsonFileName, jsonString, Encoding.UTF8);
            Console.WriteLine("JSON文件已保存");

            // 从文件读取并反序列化
            string readJson = await File.ReadAllTextAsync(jsonFileName, Encoding.UTF8);
            Person deserializedPerson = JsonSerializer.Deserialize<Person>(readJson);
            
            Console.WriteLine("从JSON文件读取的数据:");
            Console.WriteLine($"  姓名: {deserializedPerson.Name}");
            Console.WriteLine($"  年龄: {deserializedPerson.Age}");
            Console.WriteLine($"  邮箱: {deserializedPerson.Email}");
            Console.WriteLine($"  爱好: {string.Join(", ", deserializedPerson.Hobbies)}");

            // 处理多个对象的JSON数组
            var people = new List<Person>
            {
                new Person { Name = "张三", Age = 25, Email = "zhangsan@example.com", Hobbies = new List<string> { "编程" } },
                new Person { Name = "李四", Age = 30, Email = "lisi@example.com", Hobbies = new List<string> { "阅读", "音乐" } }
            };

            string jsonArrayFileName = "people.json";
            string jsonArrayString = JsonSerializer.Serialize(people, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            await File.WriteAllTextAsync(jsonArrayFileName, jsonArrayString, Encoding.UTF8);
            Console.WriteLine("\nJSON数组文件已保存");

            // 读取JSON数组
            string readJsonArray = await File.ReadAllTextAsync(jsonArrayFileName, Encoding.UTF8);
            List<Person> deserializedPeople = JsonSerializer.Deserialize<List<Person>>(readJsonArray);
            
            Console.WriteLine("从JSON数组文件读取的数据:");
            foreach (var p in deserializedPeople)
            {
                Console.WriteLine($"  {p.Name}, {p.Age}岁, {p.Email}");
            }

            // 清理
            File.Delete(jsonFileName);
            File.Delete(jsonArrayFileName);

            Console.WriteLine();
        }

        static void FileMonitoring()
        {
            Console.WriteLine("6. 文件监控:");

            string watchDir = "WatchDirectory";
            string watchFile = Path.Combine(watchDir, "watch.txt");

            // 创建监控目录
            Directory.CreateDirectory(watchDir);

            // 创建FileSystemWatcher
            using (FileSystemWatcher watcher = new FileSystemWatcher(watchDir))
            {
                watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                watcher.Filter = "*.txt";

                // 添加事件处理器
                watcher.Changed += OnFileChanged;
                watcher.Created += OnFileCreated;
                watcher.Deleted += OnFileDeleted;
                watcher.Renamed += OnFileRenamed;

                // 开始监控
                watcher.EnableRaisingEvents = true;
                Console.WriteLine($"开始监控目录: {watchDir}");

                // 模拟文件操作
                Console.WriteLine("创建文件...");
                File.WriteAllText(watchFile, "初始内容");

                Console.WriteLine("修改文件...");
                File.WriteAllText(watchFile, "修改后的内容");

                Console.WriteLine("重命名文件...");
                string newFileName = Path.Combine(watchDir, "renamed.txt");
                File.Move(watchFile, newFileName);

                Console.WriteLine("删除文件...");
                File.Delete(newFileName);

                // 等待事件处理
                System.Threading.Thread.Sleep(1000);
            }

            // 清理
            Directory.Delete(watchDir, true);

            Console.WriteLine();
        }

        // 文件监控事件处理器
        static void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"  文件已修改: {e.FullPath}");
        }

        static void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"  文件已创建: {e.FullPath}");
        }

        static void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"  文件已删除: {e.FullPath}");
        }

        static void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"  文件已重命名: {e.OldFullPath} -> {e.FullPath}");
        }
    }

    // 用于JSON序列化的类
    public class Person
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public List<string> Hobbies { get; set; } = new List<string>();
    }
}
