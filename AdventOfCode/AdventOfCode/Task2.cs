using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task2 {

        int xPos;
        int yPos;

        string[,] keypad;

        public string CalculateBathroomCode(String[] instructions, bool advancedKeypad) {

            string code = "";

            if (advancedKeypad) {
                //Structure the advanced keypad
                keypad = new string[5, 5] {
                { "", "", "5", "", "" },
                { "", "2", "6", "A", "" },
                { "1", "3", "7", "B", "D"},
                { "", "4", "8", "C", ""},
                { "", "", "9", "", "" } };
                //We start at 5
                xPos = 0;
                yPos = 2;
            } else {
                //Structure the basic keypad
                keypad = new string[3,3] { { "1", "4", "7" }, { "2", "5", "8" }, { "3", "6", "9" } };
                //We start at 5
                xPos = 1;
                yPos = 1;
            }

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
            if(dir == 1 && xPos != keypad.GetLength(0) - 1) {
                if(keypad[xPos + 1, yPos] != "") {
                    xPos += 1;
                }
            }
            if(dir == -1 && xPos != 0) {
                if (keypad[xPos - 1, yPos] != "") {
                    xPos -= 1;
                }
            }
        }

        //Change xPos by dir if it is within the bounds
        void ChangeY(int dir) {
            if (dir == 1 && yPos != keypad.GetLength(0) - 1) {
                if (keypad[xPos, yPos + 1] != "") {
                    yPos += 1;
                }
            }
            if (dir == -1 && yPos != 0) {
                if (keypad[xPos, yPos - 1] != "") {
                    yPos -= 1;
                }
            }
        }
    }


}
