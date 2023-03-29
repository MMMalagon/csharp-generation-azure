﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Demo
{
    public class Program
    {
        /*
        public static void Main(string[] args)
        {
            var student = new Student()
            {
                Name = "Ana",
                Surname = "Ruíz",
                Age = 35
            };

            Console.WriteLine(student.FullName);

            // student.Sum(110, 115);

            int num1 = 10;  // It can be Int32 and has no effect whatsoever when
            int num2 = 20;  // passed as a param (it's still num type, not ref).
            var demo = new Demo();

            Console.WriteLine(num1);
            Console.WriteLine(num2);
            demo.Transform(ref num1, out num2);
            Console.WriteLine(num1);
            Console.WriteLine(num2);

            var person1 = new PersonClass()
            {
                Name = "Ana",
                Age = 35
            };

            Console.WriteLine(person1.ToString());
            demo.Transform(person1);
            Console.WriteLine(person1.ToString());

            var person2 = new PersonStruct()
            {
                Name = "Ana",
                Age = 35
            };

            Console.WriteLine(person2.ToString());
            demo.Transform(ref person2);
            Console.WriteLine(person2.ToString());

            Environment.Exit(0);
        }
        */

        public static void Main(string[] args)
        {
            ArraysDemo();
            CollectionsDemo();
        }

        public static void CollectionsDemo()
        {
            ArrayList array = new ArrayList();  // Object list
            Hashtable ht = new Hashtable();  // Object: Object dictionary (key: value)
            // var ht = new Hashtable();

            List<Student> students = new List<Student>();
            List<object> list = new List<object>();  // Strongly-typed list | Worse performance than ArrayList
            Dictionary<int, string> dic = new Dictionary<int, string>();
        }

        public static void ArraysDemo()
        {
            int[] array1 = new int[5];

            Student[] array2 = new Student[5];

            int[] array3a = new int[] { 1, 2, 3 };
            int[] array3b = { 1, 2, 3 };

            int[,] array4a = new int[2, 5];  // bidimensional (2 by 5)
            array4a[0, 0] = 0;
            array4a[0, 1] = 1;
            array4a[1, 4] = 5;

            int[,] array4b = { { 1, 62 }, { 2, 89 }, { 55, 0 }, { 789, -3 }, { -984, 6541 } };  // bidimensional (5 by 2)
            Console.WriteLine(array4b[2, 1]);  // prints: 0 ↵

            int[][] array5 = new int[5][];  // Jagged array (arrays of arrays, allowing rows with different lengths)
            // int[][] array5 = new int[5][5];  // Compilation error (could be cool, tbh, but that would be a bidimensional array)
            array5[0] = new int[] { 561, 56, 33 };
            array5[0] = new int[] { 1, 2, 3, 4, 5, 6 };
            array5[0] = new int[] { 5461, 946125 };

            // We can scale this with multiple dimensions, both traditional multidimensional matrixes and jagged arrays
            // int [,,] tripleArray = new int[3, 2, 4];
            // int[][][] tripleJaggedArray = new int[3][][];

            // Delete every element of the array
            int[] numbers = new int[] { 15, -3, 577, 82, 19, 33, 78, 1000, -63 };
            Array.Clear(numbers, 0, numbers.Length);

            // Add new elements (with resize)
            string[] fruits = new string[] { "orange", "lemon", "grapefruit", "lime" };
            Array.Resize(ref fruits, 5);
            fruits[4] = "apple";

            // Get number of elements (lenght)
            Console.WriteLine($"Items lenght: {fruits.Length}");

            // Loop through collection
            // Option 1: foreach
            foreach (var item in fruits)
            {
                Console.WriteLine("Item: {0}", item);
            }
            Console.WriteLine(Environment.NewLine);

            // Loop through collection
            // Option 2: for
            for (var i = 0; i < fruits.Length; i++)
            {
                Console.WriteLine("Item: {0}", fruits[i]);
            }

            var student = new Student();

            Student[] students1 = new Student[] { student, student, student };
            students1[0].Name = "Borja";
            students1[1].Name = "Ana";
            students1[2].Name = "María";

            // Using the same student object multiple times, so its the same Name
            Console.WriteLine(students1[0].Name);
            Console.WriteLine(students1[1].Name);
            Console.WriteLine(students1[2].Name);

            Student[] students2 = new Student[] { new Student(), new Student(), new Student() };
            students2[0].Name = "Borja";
            students2[1].Name = "Ana";
            students2[2].Name = "María";

            // Declaring and assigning the proper way (different students)
            Console.WriteLine(students2[0].Name);
            Console.WriteLine(students2[1].Name);
            Console.WriteLine(students2[2].Name);
        }
    }

    public struct PersonStruct
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString() { return this.Name + $" ({this.Age} yrs)"; }
    }

    public class PersonClass
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString() { return this.Name + $" ({this.Age} yrs)"; }
    }

    public class Demo
    {
        public bool Transform(ref int a, out int b)
        {
            b = 0;
            try
            {
                Console.WriteLine("Transforming numbers");
                a *= 10;
                b = a * 10;
                Console.WriteLine(a);
                Console.WriteLine(b);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Transform(PersonClass person)
        {
            try
            {
                person.Age += 1;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Transform(ref PersonStruct person)
        {
            try
            {
                person.Age += 1;
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

    public class Student
    {
        // Local vars
        private string name;
        private string surname;
        private int age;

        // Encapsulation of local var as a property with several conditions/manipulations
        public string Name
        {
            get { return this.name.Trim().ToLower(); }
            set { this.name = (value.Length < 3) ? string.Empty : value; }
        }

        // Property with redundant get; set; of a local var (option 1)
        public string Surname
        {
            get => this.surname;
            set => this.surname = value;
        }

        // Property with redundant get; set; of a local var (option 2)
        public int Age
        {
            get { return this.age; }
            set { this.age = value; }
        }

        // No need to declare local var with short get; set; syntax (auto-implemented properties)
        // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/auto-implemented-properties
        // Permission modifiers can also be applied (Eg: get is public but not set, which can only be done inside class)
        public string Course { get; private set; }

        // Read-only property
        // public string Name { get; }
        public string FullName
        {
            get => $"{this.name} {this.surname}";
        }

        // Another way (way shorter, btw) | https://stackoverflow.com/q/40282424
        // public string FullName => $"{this.name} {this.surname}";

        // Even with functions!
        // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members
        // public bool Status => EvaluateStatus();

        // Write-only properties are also possible
        // public string WhateverThisIs { set; }

        // Constructors (there are several other ways, even using a Builder pattern, which is better when using default vars, I guess)
        // Method overloading, btw (this is declaring methods using the same name but changing params both types and amount)
        // https://stackoverflow.com/q/4009013 | https://stackoverflow.com/q/48205792 | https://stackoverflow.com/q/6724133
        public Student() : this(string.Empty, string.Empty, 0) { }

        public Student(string name, int age) : this(name, string.Empty, age) { }

        public Student(string name, string surname, int age) { this.name = name; this.surname = surname; this.age = age; }

        // Destructor (interesing if you want to trigger something when deleting, debugging...)
        ~Student()
        {
            System.Diagnostics.Debug.WriteLine("Student object destructor");
            this.name = null;
            this.surname = null;
        }

        public void MethodOne() { }

        public int MethodTwo()
        {
            name = "Manuel";
            // this.Name = "Manuel";  // C# naming convention is PascalCase for properties and camelCase for local vars and params
            // Name = "Manuel";  // This differs from Java with a more widely used camelCase, but still I prefer to use this keyword (Eg: this.Name)
            return 0;
        }

        public int MethodThree(int param1, string param2)
        {
            string name;

            name = param2;
            // this.Name = param2;
            this.name = param2;

            return param1;
        }

        public int Sum(int param1, int param2 = 35)
        {
            return param1 + param2;
        }
    }
}
