using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionsAndLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== C# 集合和LINQ学习 Demo ===\n");

            // 1. 集合基础
            CollectionsBasics();

            // 2. 泛型集合
            GenericCollections();

            // 3. LINQ查询
            LINQQueries();

            // 4. 委托和事件
            DelegatesAndEvents();

            // 5. 异常处理
            ExceptionHandling();

            Console.WriteLine("\n按任意键退出...");
            Console.ReadKey();
        }

        static void CollectionsBasics()
        {
            Console.WriteLine("1. 集合基础:");

            // ArrayList (非泛型，不推荐使用)
            var arrayList = new System.Collections.ArrayList();
            arrayList.Add("Hello");
            arrayList.Add(123);
            arrayList.Add(true);

            Console.WriteLine("ArrayList内容:");
            foreach (var item in arrayList)
            {
                Console.WriteLine($"  {item} ({item.GetType().Name})");
            }

            // Hashtable (非泛型)
            var hashtable = new System.Collections.Hashtable();
            hashtable["name"] = "张三";
            hashtable["age"] = 25;
            hashtable["city"] = "北京";

            Console.WriteLine("\nHashtable内容:");
            foreach (System.Collections.DictionaryEntry entry in hashtable)
            {
                Console.WriteLine($"  {entry.Key}: {entry.Value}");
            }

            Console.WriteLine();
        }

        static void GenericCollections()
        {
            Console.WriteLine("2. 泛型集合:");

            // List<T>
            var numbers = new List<int> { 1, 2, 3, 4, 5 };
            numbers.Add(6);
            numbers.AddRange(new[] { 7, 8, 9 });

            Console.WriteLine("List<int>内容:");
            foreach (var num in numbers)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine();

            // Dictionary<TKey, TValue>
            var students = new Dictionary<string, int>
            {
                ["张三"] = 85,
                ["李四"] = 92,
                ["王五"] = 78
            };
            students["赵六"] = 88;

            Console.WriteLine("\nDictionary<string, int>内容:");
            foreach (var kvp in students)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }

            // HashSet<T>
            var uniqueNumbers = new HashSet<int> { 1, 2, 2, 3, 3, 4, 5 };
            Console.WriteLine("\nHashSet<int>内容 (自动去重):");
            foreach (var num in uniqueNumbers)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine();

            // Queue<T>
            var queue = new Queue<string>();
            queue.Enqueue("第一个");
            queue.Enqueue("第二个");
            queue.Enqueue("第三个");

            Console.WriteLine("\nQueue<string>内容 (FIFO):");
            while (queue.Count > 0)
            {
                Console.WriteLine($"  出队: {queue.Dequeue()}");
            }

            // Stack<T>
            var stack = new Stack<string>();
            stack.Push("底部");
            stack.Push("中间");
            stack.Push("顶部");

            Console.WriteLine("\nStack<string>内容 (LIFO):");
            while (stack.Count > 0)
            {
                Console.WriteLine($"  出栈: {stack.Pop()}");
            }

            Console.WriteLine();
        }

        static void LINQQueries()
        {
            Console.WriteLine("3. LINQ查询:");

            // 准备数据
            var students = new List<Student>
            {
                new Student { Name = "张三", Age = 20, Grade = 85, Department = "计算机" },
                new Student { Name = "李四", Age = 22, Grade = 92, Department = "数学" },
                new Student { Name = "王五", Age = 19, Grade = 78, Department = "计算机" },
                new Student { Name = "赵六", Age = 21, Grade = 88, Department = "物理" },
                new Student { Name = "钱七", Age = 23, Grade = 95, Department = "数学" }
            };

            // 基本查询
            Console.WriteLine("所有学生:");
            var allStudents = from s in students select s;
            foreach (var student in allStudents)
            {
                Console.WriteLine($"  {student.Name}, {student.Age}岁, {student.Grade}分, {student.Department}");
            }

            // 条件查询
            Console.WriteLine("\n成绩大于80的学生:");
            var highGrades = from s in students where s.Grade > 80 select s;
            foreach (var student in highGrades)
            {
                Console.WriteLine($"  {student.Name}: {student.Grade}分");
            }

            // 排序
            Console.WriteLine("\n按成绩降序排列:");
            var sortedByGrade = from s in students orderby s.Grade descending select s;
            foreach (var student in sortedByGrade)
            {
                Console.WriteLine($"  {student.Name}: {student.Grade}分");
            }

            // 分组
            Console.WriteLine("\n按专业分组:");
            var groupedByDept = from s in students group s by s.Department;
            foreach (var group in groupedByDept)
            {
                Console.WriteLine($"  {group.Key}专业:");
                foreach (var student in group)
                {
                    Console.WriteLine($"    {student.Name}: {student.Grade}分");
                }
            }

            // 聚合操作
            Console.WriteLine("\n统计信息:");
            Console.WriteLine($"  平均成绩: {students.Average(s => s.Grade):F2}");
            Console.WriteLine($"  最高成绩: {students.Max(s => s.Grade)}");
            Console.WriteLine($"  最低成绩: {students.Min(s => s.Grade)}");
            Console.WriteLine($"  总人数: {students.Count}");

            // 投影
            Console.WriteLine("\n学生姓名和等级:");
            var studentLevels = from s in students
                               select new { s.Name, Level = s.Grade >= 90 ? "优秀" : s.Grade >= 80 ? "良好" : "一般" };
            foreach (var item in studentLevels)
            {
                Console.WriteLine($"  {item.Name}: {item.Level}");
            }

            // 方法语法
            Console.WriteLine("\n使用方法语法的查询:");
            var computerStudents = students.Where(s => s.Department == "计算机")
                                          .OrderBy(s => s.Name)
                                          .Select(s => s.Name);
            foreach (var name in computerStudents)
            {
                Console.WriteLine($"  {name}");
            }

            Console.WriteLine();
        }

        static void DelegatesAndEvents()
        {
            Console.WriteLine("4. 委托和事件:");

            // 委托示例
            Calculator calc = new Calculator();
            
            // 使用委托
            Calculator.CalculateDelegate addDelegate = calc.Add;
            Calculator.CalculateDelegate multiplyDelegate = calc.Multiply;

            int result1 = addDelegate(5, 3);
            int result2 = multiplyDelegate(4, 6);

            Console.WriteLine($"5 + 3 = {result1}");
            Console.WriteLine($"4 * 6 = {result2}");

            // 多播委托
            Calculator.CalculateDelegate multiDelegate = addDelegate + multiplyDelegate;
            // 注意：多播委托只返回最后一个方法的结果
            int result3 = multiDelegate(2, 3);
            Console.WriteLine($"多播委托结果: {result3}");

            // 事件示例
            Button button = new Button();
            button.Click += OnButtonClick;
            button.Click += OnButtonClick2;

            Console.WriteLine("\n模拟按钮点击:");
            button.SimulateClick();

            Console.WriteLine();
        }

        static void ExceptionHandling()
        {
            Console.WriteLine("5. 异常处理:");

            // 基本异常处理
            try
            {
                int result = Divide(10, 0);
                Console.WriteLine($"结果: {result}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"捕获到除零异常: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"捕获到其他异常: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("finally块总是执行");
            }

            // 自定义异常
            try
            {
                ValidateAge(-5);
            }
            catch (InvalidAgeException ex)
            {
                Console.WriteLine($"捕获到自定义异常: {ex.Message}");
            }

            // 使用using语句（自动资源管理）
            using (var resource = new ManagedResource())
            {
                resource.DoSomething();
            }
            Console.WriteLine("资源已自动释放");

            Console.WriteLine();
        }

        // 事件处理方法
        static void OnButtonClick(object sender, EventArgs e)
        {
            Console.WriteLine("  按钮被点击了！");
        }

        static void OnButtonClick2(object sender, EventArgs e)
        {
            Console.WriteLine("  另一个事件处理器也响应了！");
        }

        // 异常处理方法
        static int Divide(int a, int b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("除数不能为零");
            }
            return a / b;
        }

        static void ValidateAge(int age)
        {
            if (age < 0)
            {
                throw new InvalidAgeException("年龄不能为负数");
            }
        }
    }

    // 学生类
    public class Student
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public int Grade { get; set; }
        public string Department { get; set; } = string.Empty;
    }

    // 计算器类
    public class Calculator
    {
        public delegate int CalculateDelegate(int a, int b);

        public int Add(int a, int b)
        {
            Console.WriteLine($"  执行加法: {a} + {b}");
            return a + b;
        }

        public int Multiply(int a, int b)
        {
            Console.WriteLine($"  执行乘法: {a} * {b}");
            return a * b;
        }
    }

    // 按钮类
    public class Button
    {
        public event EventHandler Click;

        public void SimulateClick()
        {
            Click?.Invoke(this, EventArgs.Empty);
        }
    }

    // 自定义异常类
    public class InvalidAgeException : Exception
    {
        public InvalidAgeException(string message) : base(message) { }
    }

    // 资源管理类
    public class ManagedResource : IDisposable
    {
        public void DoSomething()
        {
            Console.WriteLine("  使用资源进行某些操作");
        }

        public void Dispose()
        {
            Console.WriteLine("  释放资源");
        }
    }
}
