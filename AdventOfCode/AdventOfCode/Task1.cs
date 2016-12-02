using System;
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


        public int CalculateDistanceToHQ(String[] commands) {
            direction = Direction.north;
            x_coord = 0;
            y_coord = 0;

            //Check every command
            foreach(String command in commands) {
                if(command[0] == 'R') {
                    Turn(true);
                }else {
                    Turn(false);
                }

                //Find out how many steps we want to take
                int steps = Int32.Parse(command.Substring(1, command.Length - 1));

                //Take the steps according to our direction
                switch (direction) {
                    case Direction.east:
                        x_coord += steps;
                        break;
                    case Direction.west:
                        x_coord -= steps;
                        break;
                    case Direction.north:
                        y_coord += steps;
                        break;
                    case Direction.south:
                        y_coord -= steps;
                        break;
                }   
            }
            //Calculate the final distance
            int distance = Math.Abs(x_coord) + Math.Abs(y_coord);
            return distance;
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
