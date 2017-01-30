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

            bool runTask5 = false;
            bool runTask12B = false;
            bool runTask14B = false;

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
            //B
            //Search all the real rooms for a room with the three keywords
            string targetRoom = null;
            string[] realRooms = task4.realRooms.Keys.ToArray();
            foreach (string room in realRooms) {
                //If it contains the keywords
                if (room.Contains("north") && room.Contains("pole") && room.Contains("object")) {
                    targetRoom = room;
                    break;
                }
            }

            //If we found a room, get the sector ID for that room
            if (targetRoom != null) {
                Console.WriteLine("The result of task 4 B is: " + task4.realRooms[targetRoom] + " (Room: " + targetRoom +")");
            } else {
                Console.WriteLine("The result of task 4 B was not found :(");
            }
            #endregion

            #region task5
            if (runTask5) {
                Console.WriteLine("Running task 5 (This could take some time)");
                Task5 task5 = new Task5();
                string task5AResult = task5.FindEasyPassword("cxdnnyjw");
                Console.WriteLine("The result of task 5 A is: " + task5AResult);
                string task5BResult = task5.FindAdvancedPassword("cxdnnyjw");
                Console.WriteLine("The result of task 5 B is:" + task5BResult);
            } else {
                Console.WriteLine("Skipping task 5 (Enabled by default)");
            }
            #endregion

            #region task6
            Console.WriteLine("Running task 6: ");
            Task6 task6 = new Task6();
            string task6AResult = task6.InterpretMessage(ReadLinesFromFile("Task6Input.txt"), false);
            Console.WriteLine("The result of task 6 A is: " + task6AResult);
            string task6BResult = task6.InterpretMessage(ReadLinesFromFile("Task6Input.txt"), true);
            Console.WriteLine("The result of task 6 B is: " + task6BResult);
            #endregion

            #region Task7
            Console.WriteLine("Running task 7: ");
            Task7 task7 = new Task7();
            int task7AResult = task7.CountTLSSupporting(ReadLinesFromFile("Task7Input.txt"));
            Console.WriteLine("The result of task 7 A is: " + task7AResult);
            int task7BResult = task7.CountSSLSupporting(ReadLinesFromFile("Task7Input.txt"));
            Console.WriteLine("The result of task 7 B is: " + task7BResult);
            #endregion

            #region Task8
            Console.WriteLine("Running task 7: ");
            Task8 task8 = new Task8();
            int task8AResult = task8.RunScreenCommands(ReadLinesFromFile("Task8Input.txt"));
            Console.WriteLine("The result of task 8 A is: " + task8AResult);
            Console.WriteLine("The result of task 8 B is: ");
            task8.PrintScreen();

            #endregion

            #region task 9
            Task9 task9 = new Task9();
            string input = ReadLinesFromFile("Task9Input.txt")[0];
            long task9AResult = task9.CalculateSize(input, 0, input.Length, false);
            Console.WriteLine("The answer to task 9 A is: " + task9AResult);
            long task9BResult = task9.CalculateSize(input, 0, input.Length, true);
            Console.WriteLine("The answer to task 9 B is: " + task9BResult);
            #endregion


            #region task 10
            Task10 task10 = new Task10();
            int task10AResult = task10.FollowInstructions(ReadLinesFromFile("Task10Input.txt"), 17, 61);
            Console.WriteLine("The answer to task 10 A is: " + task10AResult);
            Console.WriteLine("The answer to task 10 B is: " + task10.bins[0] * task10.bins[1] * task10.bins[2]);
            #endregion

            #region task 11
            Task11 task11 = new Task11();
            Dictionary<int, List<string>> problem = new Dictionary<int, List<string>>();

            //Setting up the problem
            List<string> firstFloor = new List<string>();
            firstFloor.Add("polonium generator");
            firstFloor.Add("thulium generator");
            firstFloor.Add("thulium-compatible microchip");
            firstFloor.Add("promethium generator");
            firstFloor.Add("ruthenium generator");
            firstFloor.Add("ruthenium-compatible microchip");
            firstFloor.Add("cobalt generator");
            firstFloor.Add("cobalt-compatible microchip");
            List<string> secondFloor = new List<string>();
            secondFloor.Add("polonium-compatible microchip");
            secondFloor.Add("promethium-compatible microchip");
            List<string> thirdFloor = new List<string>();
            List<string> fourthFloor = new List<string>();
            problem.Add(0, firstFloor);
            problem.Add(1, secondFloor);
            problem.Add(2, thirdFloor);
            problem.Add(3, fourthFloor);

            //task11.FindShortestPlan(problem, 10);
            //Console.WriteLine("The answer to task 11 A is: " + task11.shortestPath);
            #endregion

            #region task12
            Task12 task12 = new Task12();
            Dictionary<string, int> variables = new Dictionary<string, int>();
            task12.RunAssembunnyCode(ReadLinesFromFile("task12Input.txt"), variables);
            Console.WriteLine("The answer to task 12 A is: " + task12.variables["a"]);
            if (runTask12B) {
                Dictionary<string, int> variables2 = new Dictionary<string, int>();
                variables2.Add("c", 1);
                task12.RunAssembunnyCode(ReadLinesFromFile("task12Input.txt"), variables2);
                Console.WriteLine("The answer to task 12 B is: " + task12.variables["a"]);
            } else {
                Console.WriteLine("Skipping task 12 B (Enabled by default)");
            }
            #endregion

            #region task13
            Task13 task13 = new Task13();
            int task13AResult = task13.FindPosition(31, 39, 1362, false);
            Console.WriteLine("The answer to task 13 A is: " + task13AResult);
            int task13BResult = task13.FindPosition(31, 39, 1362, true);
            Console.WriteLine("The answer to task 13 B is: " + task13BResult);
            #endregion

            #region task14
            Task14 task14 = new Task14();
            int task14AResult = task14.GenerateKey(64, "ngcjuoqr", 0);
            Console.WriteLine("The answer to task 14 A is: " + task14AResult);
            if (runTask14B) {
                int task14BResult = task14.GenerateKey(64, "ngcjuoqr", 2016);
                Console.WriteLine("The answer to task 14 B is: " + task14BResult);
            } else {
                Console.WriteLine("Skipping task 14B (Enabled by default)");
            }
            #endregion

            #region task15
            Task15 task15 = new Task15();
            int task15AResult = task15.GetEarliestTime(ReadLinesFromFile("Task15Input.txt"), false);
            Console.WriteLine("The answer to task 15 A is: " + task15AResult);
            int task15BResult = task15.GetEarliestTime(ReadLinesFromFile("Task15Input.txt"), true);
            Console.WriteLine("The answer to task 15 B is: " + task15BResult);
            #endregion

            #region task16
            Task16 task16 = new Task16();
            string task16AResult = task16.FillDisk("10001110011110000", 272);
            Console.WriteLine("The answer to task 16 A is: " + task16AResult);
            string task16BResult = task16.FillDisk("10001110011110000", 35651584);
            Console.WriteLine("The answer to task 16 B is: " + task16BResult);
            #endregion

            #region task18
            Task18 task18 = new Task18();
            int task18AResult = task18.CountSafeTiles("^..^^.^^^..^^.^...^^^^^....^.^..^^^.^.^.^^...^.^.^.^.^^.....^.^^.^.^.^.^.^.^^..^^^^^...^.....^....^.", 40);
            Console.WriteLine("The answer to task 18 A is: " + task18AResult);
            int task18BResult = task18.CountSafeTiles("^..^^.^^^..^^.^...^^^^^....^.^..^^^.^.^.^^...^.^.^.^.^^.....^.^^.^.^.^.^.^.^^..^^^^^...^.....^....^.", 400000);
            Console.WriteLine("The answer to task 18 B is: " + task18BResult);
            #endregion

            #region task19
            Task19 task19 = new Task19();
            int task19AResult = task19.Winner(3018458);
            Console.WriteLine(task19AResult);

            #endregion

            #region task20
            Task20 task20 = new Task20();
            long task20AResult = task20.FindLowestOkIP(ReadLinesFromFile("Task20Input.txt"));
            Console.WriteLine("The answer to task 20 A is: " + task20AResult);
            long task20BResult = task20.CountOpenIPs(ReadLinesFromFile("Task20Input.txt"));
            Console.WriteLine("The answer to task 20 B is: " + task20BResult);

            #endregion

            #region task21
            Task21 task21 = new Task21();
            string task21AResult = task21.ScramblePassword("abcdefgh", ReadLinesFromFile("Task21Input.txt"));
            Console.WriteLine("The answer to task 21 A is: " + task21AResult);
            string task21BResult = task21.UnscramblePassword("fbgdceah", ReadLinesFromFile("Task21Input.txt"));
            Console.WriteLine("The answer to task 21 B is: " + task21BResult);
            #endregion

            #region task22
            Task22 task22 = new Task22();
            int task22AResult = task22.CalculateViablePairs(ReadLinesFromFile("Task22Input.txt"));
            Console.WriteLine("The answer to task 22 A is: " + task22AResult);

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
