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
        HashSet<string> visitedStates;

        public void FindShortestPlan(Dictionary<int, List<string>> floors, int nrOfObjects) {
            shortestPath = int.MaxValue;
            objectCount = nrOfObjects;
            visitedStates = new HashSet<string>();
            //Call the recursive function. When it returns all of it's children will have found their paths
            FindShortestPlanRec(floors, 0, 0, "start -> ");
        }


        void FindShortestPlanRec(Dictionary<int, List<string>> floors, int stepsTaken, int atFloor, string sequence) {
            //Console.WriteLine(floors[3].Count());
            string state = StringifyState(floors, atFloor);
            Console.WriteLine(state);
            if(visitedStates.Contains(state)){
                return;
            }else{
                visitedStates.Add(state);
            }

            //If we have taken more steps than the shortest path, we are not on a desired path
            if(stepsTaken >= shortestPath) {
                return;
            }

            //If this state is not valid, abort
            if (!IsValidState(floors)) {
                //Console.WriteLine("Invalidstate!");
                return;
            }
            //Console.WriteLine("Not invalid state");

            //If this is a goalstate, we are done!
            if (IsGoalState(floors)) {
                if(shortestPath > stepsTaken) {
                    int topFloor = floors.Keys.ToArray().Max();
                    Console.WriteLine("Found goalstate and setting shortestpath! ShortestBefore: " + shortestPath + " after: " + stepsTaken);
                    Console.WriteLine("Objects in top floor: " + floors[topFloor].Count);
                    Console.WriteLine("Found throught sequence: " + sequence);
                    shortestPath = stepsTaken;
                }
                return;
            }

            //If there is no generator or chip, the elevator wont move and we are in a dead end
            if(floors[atFloor].Count == 0) {
                return;
            }

            if (stepsTaken > 10) {
                return;
            }

            //Console.WriteLine("At floor " + atFloor + " With " + floors[atFloor].Count + " objects");
            for(int i = 0; i < floors[atFloor].Count; i++) {
                string element = floors[atFloor][i];
                //Console.WriteLine("Handling element: " + element + " (index = " + i + ", total = " + floors[atFloor].Count+ ")");
                floors[atFloor].RemoveAt(i);
                //First try moving the element up
                if(atFloor < floors.Count - 1) {
                    //Console.WriteLine("Moving " + element + "to floor " + (atFloor + 1));
                    floors[atFloor + 1].Add(element);
                    FindShortestPlanRec(floors, stepsTaken + 1, atFloor + 1, sequence + "moving " + element + " to " + (atFloor + 1) + " -> ");
                    floors[atFloor + 1].Remove(element);
                }
                floors[atFloor].Insert(i, element);
            }



            //Then try moving them down
            for(int i = 0; i < floors[atFloor].Count; i++){
                string element = floors[atFloor][i];
                //Console.WriteLine("Handling element: " + element + " (index = " + i + ", total = " + floors[atFloor].Count+ ")");
                floors[atFloor].RemoveAt(i);
                if (atFloor > 0) {
                    //  Console.WriteLine("Moving " + element + " to floor " + (atFloor - 1));
                    floors[atFloor - 1].Add(element);
                    FindShortestPlanRec(floors, stepsTaken + 1, atFloor - 1, sequence + "moving " + element + " to " + (atFloor + -1) + " -> ");
                    floors[atFloor - 1].Remove(element);
                }
                floors[atFloor].Insert(i, element);
            }

            //Try moving down
            if (atFloor > 0) {
                FindShortestPlanRec(floors, stepsTaken + 1, atFloor - 1, sequence + " moving myself down -> ");
            }

            //Try moving up
            if (atFloor < floors.Count - 1) {
                FindShortestPlanRec(floors, stepsTaken + 1, atFloor + 1, sequence + " moving myself up ->");
            }


        }

        //Returns true if all objects are on the top floor
        bool IsGoalState(Dictionary<int, List<string>> floors) {
            //int topFloor = floors.Keys.ToArray().Max();
            //Console.WriteLine("Checking goalstate! Count of top floor is: " + floors[3].Count());
            return (floors[3].Count() == objectCount);
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

        string StringifyState(Dictionary<int, List<string>> floors, int atFloor){
            string result = "";
            result += atFloor;

            foreach (int key in floors.Keys) {
                result += "floor" + key;
                List<string> components = floors[key];
                components.Sort();
                foreach (string component in components) {
                    result += component;
                    
                }
            }

            return result;
        }
    }
}
