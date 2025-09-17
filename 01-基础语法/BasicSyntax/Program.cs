using System;

namespace BasicSyntax
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== C# 基础语法学习 Demo ===\n");

            // 1. 变量和数据类型
            VariablesAndDataTypes();

            // 2. 运算符和表达式
            OperatorsAndExpressions();

            // 3. 控制流程
            ControlFlow();

            // 4. 方法定义和调用
            MethodsDemo();

            // 5. 数组和字符串操作
            ArraysAndStrings();

            Console.WriteLine("\n按任意键退出...");
            Console.ReadKey();
        }

        static void VariablesAndDataTypes()
        {
            Console.WriteLine("1. 变量和数据类型:");
            
            // 基本数据类型
            int age = 25;
            double height = 175.5;
            bool isStudent = true;
            char grade = 'A';
            string name = "张三";

            // 使用var关键字（类型推断）
            var salary = 5000.50m; // decimal类型
            var birthYear = 1998;  // int类型

            Console.WriteLine($"姓名: {name}, 年龄: {age}, 身高: {height}cm");
            Console.WriteLine($"是否为学生: {isStudent}, 等级: {grade}");
            Console.WriteLine($"薪资: {salary:C}, 出生年份: {birthYear}");

            // 常量
            const double PI = 3.14159;
            Console.WriteLine($"圆周率: {PI}");

            Console.WriteLine();
        }

        static void OperatorsAndExpressions()
        {
            Console.WriteLine("2. 运算符和表达式:");

            int a = 10, b = 3;
            
            // 算术运算符
            Console.WriteLine($"a = {a}, b = {b}");
            Console.WriteLine($"a + b = {a + b}");
            Console.WriteLine($"a - b = {a - b}");
            Console.WriteLine($"a * b = {a * b}");
            Console.WriteLine($"a / b = {a / b}");
            Console.WriteLine($"a % b = {a % b}");

            // 比较运算符
            Console.WriteLine($"a > b: {a > b}");
            Console.WriteLine($"a == b: {a == b}");
            Console.WriteLine($"a != b: {a != b}");

            // 逻辑运算符
            bool x = true, y = false;
            Console.WriteLine($"x && y: {x && y}");
            Console.WriteLine($"x || y: {x || y}");
            Console.WriteLine($"!x: {!x}");

            // 赋值运算符
            int c = 5;
            c += 3; // 等同于 c = c + 3
            Console.WriteLine($"c += 3 后: {c}");

            Console.WriteLine();
        }

        static void ControlFlow()
        {
            Console.WriteLine("3. 控制流程:");

            // if-else语句
            int score = 85;
            if (score >= 90)
            {
                Console.WriteLine("优秀");
            }
            else if (score >= 80)
            {
                Console.WriteLine("良好");
            }
            else if (score >= 70)
            {
                Console.WriteLine("中等");
            }
            else
            {
                Console.WriteLine("需要努力");
            }

            // switch语句
            char operation = '+';
            switch (operation)
            {
                case '+':
                    Console.WriteLine("执行加法操作");
                    break;
                case '-':
                    Console.WriteLine("执行减法操作");
                    break;
                case '*':
                    Console.WriteLine("执行乘法操作");
                    break;
                case '/':
                    Console.WriteLine("执行除法操作");
                    break;
                default:
                    Console.WriteLine("未知操作");
                    break;
            }

            // for循环
            Console.Write("for循环: ");
            for (int i = 1; i <= 5; i++)
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine();

            // while循环
            Console.Write("while循环: ");
            int j = 1;
            while (j <= 3)
            {
                Console.Write($"{j} ");
                j++;
            }
            Console.WriteLine();

            // foreach循环
            int[] numbers = { 10, 20, 30, 40, 50 };
            Console.Write("foreach循环: ");
            foreach (int num in numbers)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine();

            Console.WriteLine();
        }

        static void MethodsDemo()
        {
            Console.WriteLine("4. 方法定义和调用:");

            // 调用无返回值方法
            SayHello();

            // 调用有返回值方法
            int sum = Add(5, 3);
            Console.WriteLine($"5 + 3 = {sum}");

            // 调用有多个参数的方法
            double result = CalculateArea(5.0, 3.0);
            Console.WriteLine($"矩形面积 (5.0 x 3.0) = {result}");

            // 方法重载示例
            Console.WriteLine($"Add(1, 2) = {Add(1, 2)}");
            Console.WriteLine($"Add(1.5, 2.5) = {Add(1.5, 2.5)}");
            Console.WriteLine($"Add(1, 2, 3) = {Add(1, 2, 3)}");

            Console.WriteLine();
        }

        static void ArraysAndStrings()
        {
            Console.WriteLine("5. 数组和字符串操作:");

            // 数组操作
            int[] scores = { 85, 92, 78, 96, 88 };
            Console.WriteLine($"成绩数组: [{string.Join(", ", scores)}]");
            Console.WriteLine($"最高分: {scores.Max()}");
            Console.WriteLine($"平均分: {scores.Average():F2}");

            // 字符串操作
            string firstName = "张";
            string lastName = "三";
            string fullName = firstName + lastName;
            Console.WriteLine($"全名: {fullName}");
            Console.WriteLine($"姓名长度: {fullName.Length}");
            Console.WriteLine($"是否包含'张': {fullName.Contains("张")}");

            // 字符串格式化
            string message = string.Format("姓名: {0}, 年龄: {1}", fullName, 25);
            Console.WriteLine($"格式化字符串: {message}");

            // 字符串插值（推荐方式）
            string interpolatedMessage = $"姓名: {fullName}, 年龄: {25}";
            Console.WriteLine($"插值字符串: {interpolatedMessage}");

            Console.WriteLine();
        }

        // 方法定义示例
        static void SayHello()
        {
            Console.WriteLine("Hello, C#!");
        }

        static int Add(int a, int b)
        {
            return a + b;
        }

        static double Add(double a, double b)
        {
            return a + b;
        }

        static int Add(int a, int b, int c)
        {
            return a + b + c;
        }

        static double CalculateArea(double length, double width)
        {
            return length * width;
        }
    }
}
