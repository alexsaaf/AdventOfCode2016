using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task1 {

        private enum Direction {
            north, east, south, west
        }

        private Direction direction;

        private int x_coord;
        private int y_coord;

        private List<string> visited;
        private bool firstRepeatFound;

        //Info about found repeats
        private bool newRepeatFound;
        private int repeatDistance;

        public int[] CalculateDistanceToHQ(String[] commands) {
            direction = Direction.north;
            x_coord = 0;
            y_coord = 0;
            newRepeatFound = false;
            firstRepeatFound = false;

            int[] result = new int[2];

            visited = new List<string>();

            //Check every command
            foreach (String command in commands) {
                if (command[0] == 'R') {
                    Turn(true);
                } else {
                    Turn(false);
                }

                //Find out how many steps we want to take
                int steps = Int32.Parse(command.Substring(1, command.Length - 1));

                //Take the steps according to our direction
                TakeSteps(steps);
                if (newRepeatFound && !firstRepeatFound) {
                    //We have found the first repeat
                    firstRepeatFound = true;
                    result[1] = repeatDistance;
                }
            }
            //Calculate the final distance
            result[0] = Math.Abs(x_coord) + Math.Abs(y_coord);
            return result;
        }

        private void TakeSteps(int steps) {
            //For every step
            for(int i = 0; i < steps; i++) {
                //First take the step, then check if we have visited it before
                switch (direction) {
                    case Direction.east:
                        x_coord++;
                        break;
                    case Direction.west:
                        x_coord--;
                        break;
                    case Direction.north:
                        y_coord++;
                        break;
                    case Direction.south:
                        y_coord--;
                        break;
                }

                //Then check if this position has already been visited
                if(visited.Contains(x_coord.ToString() + y_coord.ToString())) {
                    //If it does, we have found a new repeat
                    newRepeatFound = true;
                    repeatDistance = Math.Abs(x_coord) + Math.Abs(y_coord);
                }
                visited.Add(x_coord.ToString() + y_coord.ToString());
            }
        }

        //This is ugly...
        private void Turn(bool turnRight) {
            if (direction == Direction.west) {
                if (turnRight) {
                    direction = Direction.north;
                } else {
                    direction = Direction.south;
                }
            }else if(direction == Direction.north) {
                if (turnRight) {
                    direction = Direction.east;
                } else {
                    direction = Direction.west;
                }
            } else if(direction == Direction.east) {
                if (turnRight) {
                    direction = Direction.south;
                } else {
                    direction = Direction.north;
                }
            } else if(direction == Direction.south) {
                if (turnRight) {
                    direction = Direction.west;
                } else {
                    direction = Direction.east;
                }
            }
        }

    }
}
