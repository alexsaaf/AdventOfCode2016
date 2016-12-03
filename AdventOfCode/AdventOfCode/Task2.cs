using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task2 {

        int xPos;
        int yPos;

        public string CalculateBathroomCode(String[] instructions) {

            string code = "";

            int[,] keypad = new int[3,3] { { 1, 4, 7 }, { 2, 5, 8 }, { 3, 6, 9 } };

           

            //We start at 5
            xPos = 1;
            yPos = 1;

            Console.WriteLine(keypad[2, yPos]);

            //Decode every instruction
            foreach (string instruction in instructions) {
                //For every character in the instruction, apply change
                foreach (char character in instruction) {
                    switch (character) {
                        case 'U':
                            ChangeY(-1);
                            break;
                        case 'D':
                            ChangeY(1);
                            break;
                        case 'L':
                            ChangeX(-1);
                            break;
                        case 'R':
                            ChangeX(1);
                            break;
                    }
                }

                code += keypad[xPos, yPos];
            }

            return code;
        }

        //Change xPos by dir if it is within the bounds
        void ChangeX(int dir) {
            if(dir == 1 && xPos != 2) {
                xPos += 1;
            }
            if(dir == -1 && xPos != 0) {
                xPos -= 1;
            }
        }

        //Change xPos by dir if it is within the bounds
        void ChangeY(int dir) {
            if (dir == 1 && yPos != 2) {
                yPos += 1;
            }
            if (dir == -1 && yPos != 0) {
                yPos -= 1;
            }
        }

    }
}
