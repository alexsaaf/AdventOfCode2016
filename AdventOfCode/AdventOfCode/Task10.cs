using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task10 {

        public Dictionary<int, Bot> bots;
        public Dictionary<int, int> bins;

        public int lookingForLower;
        public int lookingForUpper;


        public int FollowInstructions(string[] instructions, int lower, int upper) {
            bots = new Dictionary<int, Bot>();
            bins = new Dictionary<int, int>();
            lookingForLower = lower;
            lookingForUpper = upper;
            foreach(string instruction in instructions) {
                FollowInstruction(instruction);
            }

            return FindTheOne();
        }

        int FindTheOne() {
            //Find the bot with iAmTheOne = true
            foreach (int robotNr in bots.Keys.ToArray()) {
                if (bots[robotNr].iAmTheOne) {
                    return robotNr;
                }
            }
            return -1;
        }

        void FollowInstruction(string instruction) {
            string[] parts = instruction.Split(' ');
            //Check which of the two types it is
            if(parts[0] == "value") {
                int bot_number = int.Parse(parts[5]);
                int value = int.Parse(parts[1]);
                if (bots.ContainsKey(bot_number)) {
                    bots[bot_number].ReceiveValue(value);
                } else {
                    Bot bot = new Bot(this);
                    bot.ReceiveValue(value);
                    bots.Add(bot_number, bot);
                }
            } else {
                int bot_number = int.Parse(parts[1]);
                if (bots.ContainsKey(bot_number)) {
                    bots[bot_number].AddGiveInstruction(parts[5], int.Parse(parts[6]), parts[10], int.Parse(parts[11]));
                }else {
                    Bot bot = new Bot(this);
                    bot.AddGiveInstruction(parts[5], int.Parse(parts[6]), parts[10], int.Parse(parts[11]));
                    bots.Add(bot_number, bot);
                }

            }
        }

    }


    class Bot {

        public bool iAmTheOne;

        public int value1;
        public int value2;

        public int upperTo;
        public bool upperToBin;

        public int lowerTo;
        public bool lowerToBin;

        public bool hasInstruction;

        Task10 task10;

        public Bot(Task10 _task10) {
            task10 = _task10;
            value1 = -1;
            value2 = -1;
            iAmTheOne = false;
            hasInstruction = false;
        }

        public void AddGiveInstruction(string lowerCategory, int lowerIndex, string higherCategory, int higherIndex) {
            lowerToBin = (lowerCategory == "output") ? true : false;
            lowerTo = lowerIndex;

            upperToBin = (higherCategory == "output") ? true : false;
            upperTo = higherIndex;
            hasInstruction = true;
            if (IsFull()) {
                GiveValues();
            }
        }

        public bool IsFull() {
            return (value1 != -1 && value2 != -1);
        }

        public void ReceiveValue(int value) {
            if(value1 == -1) {
                value1 = value;
            }else {
                value2 = value;
            }
            if (IsFull()) {
                GiveValues();
            }
        }

        public void GiveValues() {
            if (!hasInstruction) {
                return;
            }

            if ((value1 == task10.lookingForLower && value2 == task10.lookingForUpper)
                    || value2 == task10.lookingForLower && value1 == task10.lookingForUpper) {
                iAmTheOne = true;
            }

            //Get our values
            int lower = GetLower();
            int higher = GetHigher();


            Console.WriteLine("I have " + lower + " and " + higher);
            Console.WriteLine("I give " + lower + " to " + lowerTo);
            Console.WriteLine("I give " + higher + " to " + upperTo);
            //Set my values to -1
            value1 = -1;
            value2 = -1;

            //Give lower
            if (lowerToBin) {
                //task10.bins[lowerTo] = lower;
            } else {
                if (task10.bots.ContainsKey(lowerTo)) {
                    task10.bots[lowerTo].ReceiveValue(lower);
                }else {
                    Bot bot = new Bot(task10);
                    bot.ReceiveValue(lower);
                    task10.bots.Add(lowerTo, bot);
                }
            }

            //Give higher
            if (upperToBin) {
                //task10.bins[upperTo] = higher;
            } else {
                if (task10.bots.ContainsKey(upperTo)) {
                    task10.bots[upperTo].ReceiveValue(higher);
                }else {
                    Bot bot = new Bot(task10);
                    bot.ReceiveValue(higher);
                    task10.bots.Add(upperTo, bot);
                }
            }
        }

        public int GetLower() {
            return (value1 < value2) ? value1 : value2;
        }

        public int GetHigher() {
            return (value1 > value2) ? value1 : value2;
        }
    }
}
