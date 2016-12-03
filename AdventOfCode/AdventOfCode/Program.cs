using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace AdventOfCode {
    class Program {
        static void Main(string[] args) {

            #region task1
            Console.WriteLine("Running task 1");
            Task1 task1 = new Task1();
            String[] commands = Regex.Split(("L2, L5, L5, R5, L2, L4, R1, R1, L4, R2, R1, L1, L4, R1, L4, L4, R5, R3, R1, L1, R1, L5, L1, R5, L4, R2, L5, L3, L3, R3, L3, R4, R4, L2, L5, R1, R2, L2, L1, R3, R4, L193, R3, L5, R45, L1, R4, R79, L5, L5, R5, R1, L4, R3, R3, L4, R185, L5, L3, L1, R5, L2, R1, R3, R2, L3, L4, L2, R2, L3, L2, L2, L3, L5, R3, R4, L5, R1, R2, L2, R4, R3, L4, L3, L1, R3, R2, R1, R1, L3, R4, L5, R2, R1, R3, L3, L2, L2, R2, R1, R2, R3, L3, L3, R4, L4, R4, R4, R4, L3, L1, L2, R5, R2, R2, R2, L4, L3, L4, R4, L5, L4, R2, L4, L4, R4, R1, R5, L2, L4, L5, L3, L2, L4, L4, R3, L3, L4, R1, L2, R3, L2, R1, R2, R5, L4, L2, L1, L3, R2, R3, L2, L1, L5, L2, L1, R4"), ", ");
            int task1Result = task1.CalculateDistanceToHQ(commands);
            Console.WriteLine("The answer to Task 1 is " + task1Result);
            #endregion

            #region task2
            Console.WriteLine("Running task 2");
            Task2 task2 = new Task2();



            //Open the file with the input
            var filePath = Path.Combine(Environment.CurrentDirectory, "\\Task2Input.txt");
            StreamReader file = new System.IO.StreamReader("Task2Input.txt");

            //Read all the lines
            List<string> instructions = new List<string>();
            string line;
            while((line = file.ReadLine()) != null) {
                instructions.Add(line);
            }

            //Call task2 and print the result
            string task2Result = task2.CalculateBathroomCode(instructions.ToArray());
            Console.WriteLine("The result of task 2 is: " + task2Result);
            #endregion

            // Keep the console window open
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
