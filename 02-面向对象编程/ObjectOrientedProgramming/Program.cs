using System;
using System.Collections.Generic;

namespace ObjectOrientedProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== C# 面向对象编程学习 Demo ===\n");

            // 1. 类和对象
            ClassesAndObjects();

            // 2. 继承和多态
            InheritanceAndPolymorphism();

            // 3. 接口和抽象类
            InterfacesAndAbstractClasses();

            // 4. 封装和访问修饰符
            EncapsulationDemo();

            // 5. 属性和索引器
            PropertiesAndIndexers();

            Console.WriteLine("\n按任意键退出...");
            Console.ReadKey();
        }

        static void ClassesAndObjects()
        {
            Console.WriteLine("1. 类和对象:");

            // 创建Person对象
            Person person1 = new Person("张三", 25);
            Person person2 = new Person("李四", 30);

            person1.Introduce();
            person2.Introduce();

            // 调用方法
            person1.CelebrateBirthday();
            person1.Introduce();

            Console.WriteLine();
        }

        static void InheritanceAndPolymorphism()
        {
            Console.WriteLine("2. 继承和多态:");

            // 创建不同形状的对象
            Shape[] shapes = {
                new Circle(5.0),
                new Rectangle(4.0, 6.0),
                new Triangle(3.0, 4.0, 5.0)
            };

            foreach (Shape shape in shapes)
            {
                Console.WriteLine($"{shape.GetType().Name}: 面积 = {shape.CalculateArea():F2}");
            }

            // 多态示例
            Animal[] animals = {
                new Dog("旺财"),
                new Cat("咪咪"),
                new Bird("小黄")
            };

            foreach (Animal animal in animals)
            {
                animal.MakeSound();
            }

            Console.WriteLine();
        }

        static void InterfacesAndAbstractClasses()
        {
            Console.WriteLine("3. 接口和抽象类:");

            // 使用接口
            IPlayable[] playables = {
                new MusicPlayer(),
                new VideoPlayer()
            };

            foreach (IPlayable playable in playables)
            {
                playable.Play();
                playable.Pause();
                playable.Stop();
                Console.WriteLine();
            }

            // 使用抽象类
            Vehicle[] vehicles = {
                new Car("丰田", "凯美瑞"),
                new Motorcycle("本田", "CBR")
            };

            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.Start();
                vehicle.Drive();
                vehicle.Stop();
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        static void EncapsulationDemo()
        {
            Console.WriteLine("4. 封装和访问修饰符:");

            BankAccount account = new BankAccount("123456789", 1000);
            
            Console.WriteLine($"账户余额: {account.GetBalance()}");
            
            account.Deposit(500);
            Console.WriteLine($"存款500后余额: {account.GetBalance()}");
            
            if (account.Withdraw(200))
            {
                Console.WriteLine($"取款200后余额: {account.GetBalance()}");
            }
            else
            {
                Console.WriteLine("取款失败，余额不足");
            }

            // 尝试直接访问私有字段（编译错误）
            // account.balance = 10000; // 这行代码会编译错误

            Console.WriteLine();
        }

        static void PropertiesAndIndexers()
        {
            Console.WriteLine("5. 属性和索引器:");

            // 使用属性
            Student student = new Student();
            student.Name = "王五";
            student.Age = 20;
            student.Grade = 85;

            Console.WriteLine($"学生: {student.Name}, 年龄: {student.Age}, 成绩: {student.Grade}");
            Console.WriteLine($"等级: {student.Level}");

            // 使用索引器
            GradeBook gradeBook = new GradeBook();
            gradeBook["数学"] = 90;
            gradeBook["英语"] = 85;
            gradeBook["物理"] = 88;

            Console.WriteLine($"数学成绩: {gradeBook["数学"]}");
            Console.WriteLine($"英语成绩: {gradeBook["英语"]}");
            Console.WriteLine($"物理成绩: {gradeBook["物理"]}");

            Console.WriteLine();
        }
    }

    // 基础类示例
    public class Person
    {
        // 字段
        private string name;
        private int age;

        // 构造函数
        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        // 方法
        public void Introduce()
        {
            Console.WriteLine($"大家好，我是{name}，今年{age}岁");
        }

        public void CelebrateBirthday()
        {
            age++;
            Console.WriteLine($"{name}过生日了！现在{age}岁");
        }

        // 属性
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }
    }

    // 继承示例 - 抽象类
    public abstract class Shape
    {
        public abstract double CalculateArea();
    }

    public class Circle : Shape
    {
        private double radius;

        public Circle(double radius)
        {
            this.radius = radius;
        }

        public override double CalculateArea()
        {
            return Math.PI * radius * radius;
        }
    }

    public class Rectangle : Shape
    {
        private double width;
        private double height;

        public Rectangle(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        public override double CalculateArea()
        {
            return width * height;
        }
    }

    public class Triangle : Shape
    {
        private double a, b, c;

        public Triangle(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public override double CalculateArea()
        {
            // 海伦公式
            double s = (a + b + c) / 2;
            return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
        }
    }

    // 多态示例
    public abstract class Animal
    {
        protected string name;

        public Animal(string name)
        {
            this.name = name;
        }

        public abstract void MakeSound();
    }

    public class Dog : Animal
    {
        public Dog(string name) : base(name) { }

        public override void MakeSound()
        {
            Console.WriteLine($"{name} 汪汪叫");
        }
    }

    public class Cat : Animal
    {
        public Cat(string name) : base(name) { }

        public override void MakeSound()
        {
            Console.WriteLine($"{name} 喵喵叫");
        }
    }

    public class Bird : Animal
    {
        public Bird(string name) : base(name) { }

        public override void MakeSound()
        {
            Console.WriteLine($"{name} 叽叽喳喳");
        }
    }

    // 接口示例
    public interface IPlayable
    {
        void Play();
        void Pause();
        void Stop();
    }

    public class MusicPlayer : IPlayable
    {
        public void Play()
        {
            Console.WriteLine("开始播放音乐");
        }

        public void Pause()
        {
            Console.WriteLine("暂停音乐");
        }

        public void Stop()
        {
            Console.WriteLine("停止音乐");
        }
    }

    public class VideoPlayer : IPlayable
    {
        public void Play()
        {
            Console.WriteLine("开始播放视频");
        }

        public void Pause()
        {
            Console.WriteLine("暂停视频");
        }

        public void Stop()
        {
            Console.WriteLine("停止视频");
        }
    }

    // 抽象类示例
    public abstract class Vehicle
    {
        protected string brand;
        protected string model;

        public Vehicle(string brand, string model)
        {
            this.brand = brand;
            this.model = model;
        }

        public virtual void Start()
        {
            Console.WriteLine($"{brand} {model} 启动");
        }

        public abstract void Drive();

        public virtual void Stop()
        {
            Console.WriteLine($"{brand} {model} 停止");
        }
    }

    public class Car : Vehicle
    {
        public Car(string brand, string model) : base(brand, model) { }

        public override void Drive()
        {
            Console.WriteLine($"{brand} {model} 在路上行驶");
        }
    }

    public class Motorcycle : Vehicle
    {
        public Motorcycle(string brand, string model) : base(brand, model) { }

        public override void Drive()
        {
            Console.WriteLine($"{brand} {model} 在街道上飞驰");
        }
    }

    // 封装示例
    public class BankAccount
    {
        private string accountNumber;
        private decimal balance;

        public BankAccount(string accountNumber, decimal initialBalance)
        {
            this.accountNumber = accountNumber;
            this.balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                balance += amount;
                Console.WriteLine($"存款 {amount:C} 成功");
            }
        }

        public bool Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= balance)
            {
                balance -= amount;
                Console.WriteLine($"取款 {amount:C} 成功");
                return true;
            }
            return false;
        }

        public decimal GetBalance()
        {
            return balance;
        }
    }

    // 属性示例
    public class Student
    {
        private string name;
        private int age;
        private int grade;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public int Grade
        {
            get { return grade; }
            set { grade = value; }
        }

        // 只读属性
        public string Level
        {
            get
            {
                if (grade >= 90) return "优秀";
                else if (grade >= 80) return "良好";
                else if (grade >= 70) return "中等";
                else return "需要努力";
            }
        }
    }

    // 索引器示例
    public class GradeBook
    {
        private Dictionary<string, int> grades = new Dictionary<string, int>();

        public int this[string subject]
        {
            get
            {
                return grades.ContainsKey(subject) ? grades[subject] : 0;
            }
            set
            {
                grades[subject] = value;
            }
        }
    }
}
