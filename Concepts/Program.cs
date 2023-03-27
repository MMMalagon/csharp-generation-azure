using System;
using System.Linq;
using Concepts.Models;

// https://stackoverflow.com/a/212286
namespace Concepts
{
    class Program
    {
        // static ConsoleColor DefaultBackgroundColor;
        static ConsoleColor DefaultForegroundColor { get; }  // read-only(?) https://stackoverflow.com/a/4948846

        static Program()
        {
            // DefaultBackgroundColor = Console.BackgroundColor;
            DefaultForegroundColor = Console.ForegroundColor;
        }

        static void Main(string[] args)
        {
            // int option;

            while (true)
            {
                PrintMenu();

                // option = Convert.ToInt32(Console.ReadLine());  // This can throw an exception
                int.TryParse(Console.ReadLine(), out int option);

                switch (option)
                {
                    case 0:
                        RestoreTerminal();
                        return;

                    case 1:
                        VariablesDeclaration();
                        break;

                    case 2:
                        VariablesConversion();
                        break;

                    case 3:
                        NullableNumericVariables();
                        break;

                    case 4:
                        SelectionStatements();
                        break;

                    case 5:
                        IterationStatements();
                        break;

                    default:
                        PrintOptionEror(option);
                        PressAnyKeyToContinue();
                        break;
                }
            }

            // Environment.Exit(0);
        }

        /// <summary>
        /// Variables declaration demostration
        /// </summary>
        static void VariablesDeclaration()
        {
            ///////////////////////////////////////////////////////////////
            //
            //  Variables declaration
            //  [type] variableName = [initialValue]
            //
            //  Default vaule for value type: 0
            //  Default vaule for reference type: null
            //
            ///////////////////////////////////////////////////////////////

            string text1 = "Hello, World!";
            string text2;
            int number1 = 10;
            System.Int32 number2 = 10;
            int number3;
            decimal a, b, c;


            ///////////////////////////////////////////////////////////////
            //
            //  Variables declaration reference type
            //  [type] variableName = [new ClassConstructor(params)]
            //
            ///////////////////////////////////////////////////////////////

            // Instantiate a class object and modify its properties
            Student student = new Student()
            {
                Name = "Julián",
                Surname = "Sánchez",
                Age = 24
            };

            // Instantiate a class object and modify its properties
            Student student1 = new Student();
            student1.Name = "Julián";
            student1.Name = "Sánchez";
            student1.Age = 24;

            // Instantiate a class object with VAR, OBJECT and DYNAMIC
            var student2 = new Student();  // type is determined during compilation, no casting needed
            Object student3 = new Student();  // inheritance, so casting is needed
            dynamic student4 = new Student();  // type is determined in runtime, no casting needed

            Console.WriteLine("Var type 1: " + student1.GetType());
            Console.WriteLine("Name: {0}", student1.Name);

            Console.WriteLine("Var type 2: {0}", student2.GetType());
            Console.WriteLine("Name: {0}", student2.Name);

            Console.WriteLine($"Var type 3: {student3.GetType()}");
            // Console.WriteLine("Name: {0}", student3.Name));  // Need to cast, otherwise an exception is thrown
            Console.WriteLine("Name: {0}", ((Student)student3).Name);

            Console.WriteLine("Var type 4: " + student4.GetType());
            // Console.WriteLine("Name: {0}", student4.DoesNotExist);  // If property doesn't exist, an exception is thrown
            Console.WriteLine("Name: {0}", student4.Name);
            Console.WriteLine("Name: {0}", ((Student)student4).Name);


            ///////////////////////////////////////////////////////////////
            //
            //  Array declaration
            //  [type][] varName = [initialValue]
            //
            ///////////////////////////////////////////////////////////////

            int[] numbers1 = new int[10];
            int[] numbers2 = { 10, 5, 345, 55, 13, 1000, 83 };

            numbers1[7] = 32;
            Console.WriteLine(numbers2[5]);

            Student[] students = new Student[20];
            students[0] = new Student();
            students[1] = new Student();

            Student[] students2 = {
                new Student() { Name = "Julián", Surname = "Sánchez", Age = 24 },
                new Student(),
                new Student()
            };

            Student[] students3 = { new Student(), new Student(), new Student() };

            students3[1].Name = "Ana María";
            students3[1].Surname = "Sánchez";
            students3[1].Age = 24;
            Console.WriteLine(students3[1].Name);

            PressAnyKeyToContinue();
        }

        /// <summary>
        /// Variables conversion demostration
        /// </summary>
        static void VariablesConversion()
        {
            ///////////////////////////////////////////////////////////////
            //
            //  Variables conversion
            //
            ///////////////////////////////////////////////////////////////

            byte num1 = 10;        // 8-bit (unsigned)
            int num2 = 32;        // 32-bit (signed)
            string num3 = "42";

            Console.WriteLine("A: {0} - B: {1}", num1, num2);

            // Implicit conversion: a = b (a size >= b size).
            // It's possible because receiver var size is bigger than assigned var size.
            num2 = num1;

            // Implicit conversion: a = b (a size < b size).
            // It's not possible because receiver var size is smaller than assigned var size (throws exception).
            // num1 = num2;

            // Explicit conversion using casting
            // No exception is thrown but stored value may be truncated
            // Eg: int a = 320;       (0000 0000 0000 0000 0000 0001 0100 0000) -> 320
            //     byte b = (byte)a;                                (0100 0000) ->  64
            num1 = (byte)num2;

            // Explicit conversion using CONVERT
            // Throws an exception if value is outside the max/min value allowed by the output type allows
            // or if var type isn't supported
            num1 = Convert.ToByte(num2);
            num1 = Convert.ToByte(num3);

            // Explicit conversion using PARSE
            // Throws an exception if value is outside the max/min value allowed by the output type allows
            // or if var type isn't supported
            num1 = byte.Parse(num3);

            // Explicit conversion using TRYPARSE
            // Does not throw an exception. Instead, it returns a boolean value as a result of the parsing task
            // The parsed value is stored on a given var as a parameter (using the out modifier)
            byte.TryParse(num3, out num1);

            int.TryParse(num3, out int num4);
            var num5 = int.TryParse(num3, out _);  // https://stackoverflow.com/questions/48801040/out-var-and-out-difference

            Console.WriteLine("A: {0} - B: {1}", num1, num2);

            PressAnyKeyToContinue();
        }

        /// <summary>
        /// Null-able numeric values demostration
        /// </summary>
        static void NullableNumericVariables()
        {
            ///////////////////////////////////////////////////////////////
            //
            //  Null-able numeric values
            //
            ///////////////////////////////////////////////////////////////

            // Numeric/value variables cannot be initialized with NULL 
            int n1 = 10;
            // int n2 = null;  // compilation error

            // When we add a question mark (?) to the var type, we transform it into a nullable variable and it can contain NULL
            int? n2 = null;

            int r1;

            // The type of null-able variables are different from the non-null-able ones.
            // Eg: int var type is different to int? var
            r1 = n1;
            // r1 = n2;  // compilation error
            r1 = Convert.ToInt32(n2);

            // Conversion validation by means of an IF control statement
            if (n2 == null) r1 = 0;
            else r1 = Convert.ToInt32(n2);

            // Conversion validation by means of a (ternary) conditional operator
            // Ternary conditional operators can be concatenated
            // Eg: var result = (n1 != n2) ? ((n1 > n2) ? "greater" : "lower") : "equals";
            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator
            r1 = (n2 == null) ? r1 = 0 : r1 = Convert.ToInt32(n2);

            // Conversion validation by means of a null-coalescing operator
            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator
            r1 = Convert.ToInt32(n2 ?? 0);

            PressAnyKeyToContinue();
        }

        /// <summary>
        /// Control statements. Selection statements (IF, ELSE, SWITCH, CASE) demostration
        /// </summary>
        static void SelectionStatements()
        {
            ///////////////////////////////////////////////////////////////
            //
            //  Control statements. Selection statements
            //  (IF, ELSE, SWITCH, CASE)
            //
            ///////////////////////////////////////////////////////////////

            Booking booking = new Booking();

            Console.Write("Booking ID: ");
            booking.BookingId = Console.ReadLine();

            Console.Write("Client name: ");
            booking.Client = Console.ReadLine();

            // 100: Individual; 200: Double; 300: Junior; 400: Suite
            Console.Write("Room type: ");
            string answer = Console.ReadLine();
            int.TryParse(answer, out booking.RoomType);  // Parsing from string to int

            // Ask if client is a smoker
            Console.Write("Smoker (y/n)? ");
            string smoker = Console.ReadLine();

            // Option 1: using IF/ELSE
            if (smoker.ToLower().Trim() == "y")
            {
                booking.Smoker = true;
            }
            else
            {
                booking.Smoker = false;
            }

            // Option 2: using IF/ELSE
            if (smoker.ToLower().Trim() == "y") booking.Smoker = true;
            else booking.Smoker = false;

            // Option 3, using (ternary) conditional operator
            booking.Smoker = (smoker.ToLower().Trim() == "y") ? true : false;

            //Opción 4, tulizando SWITCH
            switch (smoker.ToLower().Trim())
            {
                case "y":
                    booking.Smoker = true;
                    break;

                default:
                    booking.Smoker = false;
                    break;
            }

            Console.Clear();

            // Printing booking ID
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Booking ID:".PadRight(20, ' '));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(booking.BookingId);

            // Printing client (name)
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Client:".PadRight(20, ' '));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(booking.Client);

            // Printing booking type using SWITCH/CASE
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Room type:".PadRight(20, ' '));
            Console.ForegroundColor = ConsoleColor.Yellow;

            switch (booking.RoomType)
            {
                case 100:
                    Console.WriteLine("Individual room");
                    break;
                case 200:
                    Console.WriteLine("Double room");
                    break;
                case 300:
                    Console.WriteLine("Junior room");
                    break;
                case 400:
                    Console.WriteLine("Suite room");
                    break;
                default:
                    Console.WriteLine($"Unknown room ({booking.RoomType})");
                    break;
            }

            // Printing booking type using IF/ELSE
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Room type:".PadRight(20, ' '));
            Console.ForegroundColor = ConsoleColor.Yellow;

            if (booking.RoomType == 100) Console.WriteLine("Individual room");
            else if (booking.RoomType == 200) Console.WriteLine("Double room");
            else if (booking.RoomType == 300) Console.WriteLine("Junior room");
            else if (booking.RoomType == 400) Console.WriteLine("Suite room");
            else Console.WriteLine($"Unknown room ({booking.RoomType})");


            // Printing if client is a smoker using SWITCH/CASE
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Smoker:".PadRight(20, ' '));
            switch (booking.Smoker)
            {
                case true:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Yes");
                    break;

                case false:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("No");
                    break;
            }

            // Printing if client is a smoker using IF/ELSE
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Smoker:".PadRight(20, ' '));

            if (booking.Smoker == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Yes");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("No");
            }

            PressAnyKeyToContinue();
        }

        /// <summary>
        /// Control statements. Iteration statements (DO, FOR, FOREACH, WHILE) demostration
        /// </summary>
        static void IterationStatements()
        {
            ///////////////////////////////////////////////////////////////
            //
            //  Control statements. Iteration statements
            //  (DO, FOR, FOREACH, WHILE)
            //
            ///////////////////////////////////////////////////////////////

            string[] fruits = { "orange", "lemon", "grapefruit", "lime" };
            Object[] objects = { "orange", 10, new Student(), new Booking() };


            // Iterating collection by position using FOR loop
            for (int i = 0; i < fruits.Length; i = i + 1)
            {
                Console.WriteLine($"Position {i} - Fruit {fruits[i]}");
            }
            Console.WriteLine(Environment.NewLine);

            // Iterating collection by position (without using brackets because only one sentence)
            for (int i = 0; i < fruits.Length; i++) Console.WriteLine($"Position {i} - Fruit {fruits[i]}");
            Console.WriteLine(Environment.NewLine);

            // Iterating collection using FOREACH, retrieving its values
            foreach (string fruit in fruits)
            {
                Console.WriteLine($"Fruit: {fruit}");
            }
            Console.WriteLine(Environment.NewLine);

            // Iterating collection using FOREACH, retrieving its values (without using brackets)
            foreach (string fruit in fruits) Console.WriteLine($"Fruit: {fruit}");
            Console.WriteLine(Environment.NewLine);

            foreach (var obj in objects)
            {
                Console.WriteLine($"Type: {obj.GetType().ToString()}");
            }
            Console.WriteLine(Environment.NewLine);

            // Iterating through collection using WHILE loop
            int count = 0;
            while (count < fruits.Length)
            {
                Console.WriteLine($"Positon {count} - Fruit {fruits[count]}");
                count++;
            }
            Console.WriteLine(Environment.NewLine);

            // Iterating through collection using DO/WHILE loop
            count = 0;
            do
            {
                Console.WriteLine($"Positon {count} - Fruit {fruits[count]}");
                count++;
            } while (count < fruits.Length);
            Console.WriteLine(Environment.NewLine);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            decimal[] numbers = { 10, 5, 345, 55, 13, 1000, 83 };

            // Iterating through collection by position
            decimal sum = 0;
            decimal max = 0;
            decimal min = numbers[0];

            for (int i = 0; i < numbers.Length; i = i + 1)
            {
                sum = sum + numbers[i];
                if (numbers[i] > max) max = numbers[i];
                if (numbers[i] < min) min = numbers[i];
            }

            Console.WriteLine($"Sum: {sum}");
            Console.WriteLine($"Avg: {(sum / numbers.Length).ToString("N2")}");
            Console.WriteLine($"Max: {max}");
            Console.WriteLine($"Min: {min}");
            Console.WriteLine(Environment.NewLine);

            // Iterating through collection using FOREACH
            sum = 0;
            foreach (var num in numbers)
            {
                sum += num;
                if (min > max) max = num;
                if (num < min) min = num;
            }

            Console.WriteLine($"Sum: {sum}");
            Console.WriteLine($"Avg: {(sum / numbers.Length).ToString("N2")}");
            Console.WriteLine($"Max: {max}");
            Console.WriteLine($"Min: {min}");
            Console.WriteLine(Environment.NewLine);

            // RE_DO using LINQ
            Console.WriteLine($"Sum: {numbers.Sum()}");
            Console.WriteLine($"Avg: {numbers.Average().ToString("N2")}");
            Console.WriteLine($"Max: {numbers.Max()}");
            Console.WriteLine($"Min: {numbers.Min()}");
            Console.WriteLine(Environment.NewLine);

            PressAnyKeyToContinue();
        }

        static void RestoreTerminal()
        {
            // Console.BackgroundColor = DefaultBackgroundColor;
            Console.Clear();
            Console.ForegroundColor = DefaultForegroundColor;
        }

        static void PressAnyKeyToContinue()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

        }

        static void PrintOptionEror(int? option)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Environment.NewLine + $"Option {option} is not a valid one.");
        }

        static void PrintMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("".PadRight(80, '*'));
            Console.WriteLine("*  DEMOS AND EXERCISES".PadRight(79) + "*");
            Console.WriteLine("".PadRight(80, '*'));
            Console.WriteLine("*".PadRight(79) + "*");
            Console.WriteLine("*  1. Variables declaration".PadRight(79) + "*");
            Console.WriteLine("*  2. Variables conversion".PadRight(79) + "*");
            Console.WriteLine("*  3. Null-able numeric variables".PadRight(79) + "*");
            Console.WriteLine("*  4. Control statements. Selection statements (IF, ELSE, SWITCH, CASE)".PadRight(79) + "*");
            Console.WriteLine("*  5. Control statements. Iteration statements (DO, FOR, FOREACH, WHILE)".PadRight(79) + "*");
            Console.WriteLine("*".PadRight(79) + "*");
            Console.WriteLine("*  0. Exit".PadRight(79) + "*");
            Console.WriteLine("*".PadRight(79) + "*");
            Console.WriteLine("".PadRight(80, '*'));
            Console.WriteLine(Environment.NewLine);
            Console.Write(">> Option: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
    }
}
