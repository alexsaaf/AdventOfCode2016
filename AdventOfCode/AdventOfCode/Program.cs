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
            string task2AResult = task2.CalculateBathroomCode(ReadLinesFromFile("Task2Input.txt"), false);
            string task2BResult = task2.CalculateBathroomCode(ReadLinesFromFile("Task2Input.txt"), true);
            Console.WriteLine("The result of task 2 A is: " + task2AResult);
            Console.WriteLine("The result of task 2 B is: " + task2BResult);
            #endregion

            #region task3
            Console.WriteLine("Running task 3");
            Task3 task3 = new Task3();
            //Get the result for task 3A
            int task3AResult = task3.CheckTriangles(ReadLinesFromFile("Task3Input.txt"));

            //Get the result for task 3B
            //Read the input
            string[] task3Input = ReadLinesFromFile("Task3Input.txt");
            string[] task3BInput = new string[task3Input.Length];
            //Read 3 lines at a time and build the triangles from the "columns" in the lines
            int i = 0;
            while(i < task3Input.Length - 2){
                
                string firstTriangle = "";
                string secondTriangle = "";
                string thirdTriangle = "";

                for(int y = 0; y < 3; y++){
                    string[] line = Regex.Split(task3Input[i + y], "  ").Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    firstTriangle += "  " + line[0];
                    secondTriangle += "  " + line[1];
                    thirdTriangle += "  " + line[2];
                }

                task3BInput[i] = firstTriangle;
                task3BInput[i + 1] = secondTriangle;
                task3BInput[i + 2] = thirdTriangle;
                i += 3;
            }

            //Get the result for 3B
            int task3BResult = task3.CheckTriangles(task3BInput);
            Console.WriteLine("The result of task 3 A is: " + task3AResult);
            Console.WriteLine("The result of task 3 B is: " + task3BResult);
            #endregion

            #region task4
            Console.WriteLine("Running task 4: ");
            Task4 task4 = new Task4();
            int task4result =  task4.RealRoomSectorIDSum(ReadLinesFromFile("Task4Input.txt"));
            Console.WriteLine("The result of task 4 A is: " + task4result);
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
