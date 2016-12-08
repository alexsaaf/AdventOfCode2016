using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task8 {

        //The screen. 0 means the pixel is off. 1 means it is on.
        int[][] screen;

        public int ScreenPixelsLit(string[] commands) {
            //Initialize the screen
            screen = new int[50][];
            for(int i = 0; i < screen.Length; i++) {
                screen[i] = new int[6];
            }

            //Parse the commands and execute them
            foreach(string command in commands) {
                string[] parts = command.Split(' ');
                if(parts[0] == "rect") {
                    CreateRect(parts[1]);
                }else if(parts[0] == "rotate") {
                    Rotate(parts[1], parts[2], parts[4]);
                }
            }

            //Count all lit pixels
            int result = CountPixels();

            return result;
        }

        void CreateRect(string size) {
            string[] dimensions = size.Split('x');
            int xSize = Int32.Parse(dimensions[0]);
            int ySize = Int32.Parse(dimensions[1]);

            //Set all the pixels covered to 1
            for(int i = 0; i < xSize; i++) {
                for(int j = 0; j < ySize; j++) {
                    screen[i][j] = 1;
                }
            }
        }

        void Rotate(string dimension, string index, string amount) {
            bool rotateColumn = (dimension == "column");
            int rotateIndex = Int32.Parse(index.Split('=')[1]);
            int rotateAmount = Int32.Parse(amount);

            //Shift column or row
            if (rotateColumn) {
                for(int z = 0; z < rotateAmount; z++) {
                    ShiftColumn(rotateIndex);
                }
            } else {
                for (int z = 0; z < rotateAmount; z++) {
                    ShiftRow(rotateIndex);
                }
            }
        }

        //Shifts the column with the given index one step down
        void ShiftColumn(int rotateIndex) {
            int screenHeight = screen[rotateIndex].Length;
            int lastValue = screen[rotateIndex][screenHeight - 1];
            for (int i = 0; i < screenHeight; i++) {
                int tmp = screen[rotateIndex][i];
                screen[rotateIndex][i] = lastValue;
                lastValue = tmp;
            }
        }

        //Shifts the row with the given index one step right
        void ShiftRow(int rotateIndex) {
            int screenWidth = screen.Length;
            int lastValue = screen[screenWidth - 1][rotateIndex];
            for (int i = 0; i < screenWidth; i++) {
                int tmp = screen[i][rotateIndex];
                screen[i][rotateIndex] = lastValue;
                lastValue = tmp;
            }
        }

        int CountPixels() {
            int count = 0;
            foreach(int[] column in screen) {
                foreach(int value in column){
                    count += value;
                }
            }
            return count;
        }

    }
}
