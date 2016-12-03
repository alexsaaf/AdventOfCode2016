using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
 
/*          \ /
          -->*<--
            /_\
           /_\_\
          /_/_/_\
          /_\_\_\
         /_/_/_/_\
         /_\_\_\_\
        /_/_/_/_/_\
        /_\_\_\_\_\
       /_/_/_/_/_/_\
       /_\_\_\_\_\_\
      /_/_/_/_/_/_/_\
           [___]
*/
//Possible TODO: Let the tasks read their input themselves
namespace AdventOfCode {
    class Program {
        static void Main(string[] args) {

            #region task1
            Console.WriteLine("Running task 1: ");
            Task1 task1 = new Task1();
            String[] commands = Regex.Split(("L2, L5, L5, R5, L2, L4, R1, R1, L4, R2, R1, L1, L4, R1, L4, L4, R5, R3, R1, L1, R1, L5, L1, R5, L4, R2, L5, L3, L3, R3, L3, R4, R4, L2, L5, R1, R2, L2, L1, R3, R4, L193, R3, L5, R45, L1, R4, R79, L5, L5, R5, R1, L4, R3, R3, L4, R185, L5, L3, L1, R5, L2, R1, R3, R2, L3, L4, L2, R2, L3, L2, L2, L3, L5, R3, R4, L5, R1, R2, L2, R4, R3, L4, L3, L1, R3, R2, R1, R1, L3, R4, L5, R2, R1, R3, L3, L2, L2, R2, R1, R2, R3, L3, L3, R4, L4, R4, R4, R4, L3, L1, L2, R5, R2, R2, R2, L4, L3, L4, R4, L5, L4, R2, L4, L4, R4, R1, R5, L2, L4, L5, L3, L2, L4, L4, R3, L3, L4, R1, L2, R3, L2, R1, R2, R5, L4, L2, L1, L3, R2, R3, L2, L1, L5, L2, L1, R4"), ", ");
            int[] task1Result = task1.CalculateDistanceToHQ(commands);
            Console.WriteLine("The answer to Task 1 A is " + task1Result[0]);
            Console.WriteLine("The answer to Task 1 B is " + task1Result[1]);
            #endregion

            #region task2
            Console.WriteLine("Running task 2");
            Task2 task2 = new Task2();
            string task2Result = task2.CalculateBathroomCode(ReadLinesFromFile("Task2Input.txt"));
            Console.WriteLine("The result of task 2 is: " + task2Result);
            #endregion

            #region task3
            Console.WriteLine("Running task 3");
            Task3 task3 = new Task3();
            int task3Result = task3.CheckTriangles(ReadLinesFromFile("Task3Input.txt"));
            Console.WriteLine("The result of task 3 is: " + task3Result);
            #endregion

            // Keep the console window open
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }


        static string[] ReadLinesFromFile(string fileName) {
            //Open the file
            StreamReader file = new System.IO.StreamReader(fileName);

            //Read all the lines
            List<string> instructions = new List<string>();
            string line;
            while ((line = file.ReadLine()) != null) {
                instructions.Add(line);
            }

            //Return them as array
            return instructions.ToArray();
        }
    }
}
