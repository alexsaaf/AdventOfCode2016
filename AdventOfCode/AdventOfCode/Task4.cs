
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task4 {

        List<string> realRoomNames;

        public int RealRoomSectorIDSum(string[] rooms) {
            realRoomNames = new List<string>)();
            int total = 0;
            foreach (string room in rooms) {
                total += CheckRoom(room);
            }
            return total;
        }

        int CheckRoom(string room) {

            //Replace [ with - to for the split
            string roomEdit = room.Replace('[', '-').Replace(']',' ');
           
            //Split into components and get checksum, sectorID
            string[] components = roomEdit.Split('-');
            string checkSum = components[components.Length - 1];
            int sectorID = Int32.Parse(components[components.Length - 2]);

            //Using a dictionary to count the letters
            Dictionary<char, int> letterCounts = new Dictionary<char, int>();

            //For every component, add the letters to the dictionary
            for(int i = 0; i < components.Length - 2; i++) {
                string component = components[i];
                foreach(char letter in component) {
                    if (letterCounts.ContainsKey(letter)) {
                        letterCounts[letter]++;
                    } else {
                        letterCounts.Add(letter, 1);
                    }
                }
            }

            for (int i = 0; i < checkSum.Length - 1; i++) {
                int mostCommon = letterCounts.OrderByDescending(kvp => kvp.Value).FirstOrDefault().Value;
                int myValue;
                if (letterCounts.ContainsKey(checkSum[i])) {
                    myValue = letterCounts[checkSum[i]];
                }else {
                    return 0;
                }

                //If the letter is not as large as the most common, it is a false room
                if (myValue < mostCommon) {
                    return 0;
                }

                char lastLetter = checkSum[i];
                for(int j = i + 1; j < checkSum.Length - 1; j++) {
                    int value = 0;
                    letterCounts.TryGetValue(checkSum[j], out value); 
                    if (value == myValue) {
                        if(lastLetter > checkSum[j]) {
                            return 0;
                        }
                    }
                }

                letterCounts.Remove(checkSum[i]);
            }
            string[] words = components.Take(components.Length - 2).ToArray();
            DecodeName(sectorID, words);
            return sectorID;
        }
        
        void DecodeName(int sectorID, string[] components){
            //Shift the letters by sectorID and add the string and sector ID to the list
            foreach (string word in components)
            {

            }
        }
    }


}
