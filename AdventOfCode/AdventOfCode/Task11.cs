using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task11 {

        Dictionary<int, List<string>> floors;

        public int shortestPath;
        int objectCount;

        public void FindShortestPlan(Dictionary<int, List<string>> floors, int nrOfObjects) {
            shortestPath = int.MaxValue;
            objectCount = nrOfObjects;
            //Call the recursive function. When it returns all of it's children will have found their paths
            FindShortestPlanRec(floors, 0, 0);
        }


        void FindShortestPlanRec(Dictionary<int, List<string>> floors, int stepsTaken, int atFloor) {

            //If we have taken more steps than the shortest path, we are not on a desired path
            if(stepsTaken >= shortestPath) {
                return;
            }

            //If this state is not valid, abort
            if (!IsValidState(floors)) {
                Console.WriteLine("Invalidstate!");
                return;
            }
            Console.WriteLine("Not invalid state");
            //If this is a goalstate, we are done!
            if (IsGoalState(floors)) {
                if(shortestPath > stepsTaken) {
                    shortestPath = stepsTaken;
                }
                return;
            }

            //If there is no generator or chip, the elevator wont move and we are in a dead end
            if(floors.Count == 0) {
                return;
            }

            Console.WriteLine("At floor " + atFloor + " With " + floors[atFloor].Count + " objects");
            for(int i = 0; i < floors[atFloor].Count; i++) {
                Dictionary<int, List<string>> newDict = floors;
                List<string> floorList = floors[atFloor].ConvertAll(component => new string(component.ToCharArray()));
                string element = floors[atFloor][i];
                Console.WriteLine("Handling element: " + element + " (index = " + i + ", total = " + floors[atFloor].Count+ ")");
                floorList.RemoveAt(i);
                newDict[atFloor] = floorList;
                //First try moving the element up
                if(atFloor < floors.Count()) {
                    Console.WriteLine("Moving " + element + "to floor " + (atFloor + 1));
                    Dictionary<int, List<string>> upOneFloor = newDict;
                    upOneFloor[atFloor + 1].Add(element);
                    FindShortestPlanRec(upOneFloor, stepsTaken++, atFloor + 1);
                }

                //Then try moving it down
                if(atFloor > 0) {
                    Console.WriteLine("Moving " + element + " to floor " + (atFloor - 1));
                    Dictionary<int, List<string>> downOneFloor = newDict;
                    downOneFloor[atFloor - 1].Add(element);
                    FindShortestPlanRec(downOneFloor, stepsTaken++, atFloor - 1);
                }
            }

        }

        //Returns true if all objects are on the top floor
        bool IsGoalState(Dictionary<int, List<string>> floors) {
            int topFloor = floors.Keys.ToArray().Max();
            Console.WriteLine("Checking goalstate! Count of top floor is: " + floors[topFloor].Count());
            return (floors[topFloor].Count() == objectCount);
        }

        bool IsValidState(Dictionary<int, List<string>> floors) {
            bool valid = true;
            int[] keys = floors.Keys.ToArray();
            foreach(int floor in keys) {
                if (!IsValidFloor(floors[floor])) {
                    valid = false;
                }
            }
            return valid;
        }

        bool IsValidFloor(List<string> floor) {
            bool floorOk = true;

            List<string> microchips = new List<string>();
            List<string> generators = new List<string>();

            GetComponentsAtFloor(floor, ref microchips, ref generators);

            //For every microchip, check if it is not fried
            foreach(string microchipElement in microchips) {
                //If we find a generator of the same kind, the chip is not fried
                if (!generators.Contains(microchipElement)) {
                    //If we don't, we need to check if there are any other generators
                    if (generators.Count > 0) {
                        //If so, the chip is fried
                        floorOk = false;
                    }
                }
            }
            return floorOk;
        }

        void GetComponentsAtFloor(List<string> floor, ref List<string> microchips, ref List<string> generators) {
            foreach (string component in floor) {
                string[] componentParts = component.Split(' ');
                //Add to the corresponding list
                if (componentParts[1] == "microchip") {
                    microchips.Add(componentParts[0].Split('-')[0]);
                } else if (componentParts[1] == "generator") {
                    generators.Add(componentParts[0]);
                }
            }
        }
    }
}
