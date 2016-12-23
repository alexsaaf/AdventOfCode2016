using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task15 {

        public int GetEarliestTime(string[] diskSpecifications, bool taskb) {
            List<Disk> disks = new List<Disk>();
            
            //Build the disks from the input
            foreach(string disk in diskSpecifications){
                string[] components = disk.Split(' ');
                Disk newDisk = new Disk();
                newDisk.positions = Int32.Parse(components[3]);
                newDisk.startPosition = Int32.Parse(components[11].Substring(0, components[11].Length - 1));
                disks.Add(newDisk);
            }
            if (taskb) {
                Disk extraDisk = new Disk();
                extraDisk.positions = 11;
                extraDisk.startPosition = 0;
                disks.Add(extraDisk);
            }
            foreach (Disk disk in disks) {
                Console.WriteLine("Disk! Positions: " + disk.positions + " Startpos: " + disk.startPosition);
            }

            //Until a time is found, increment time and try again

            int time = 0;
            while(true){
                if(IsValidTime(time,disks)){
                    Console.WriteLine("Found valid time: " + time);
                    break;
                }
                time++;   
            }


            return time;
        }

        bool IsValidTime(int startTime, List<Disk> disks){
            int time = startTime;
            time++;
            foreach(Disk disk in disks){
                if(!disk.OpenAt(time)){
                    return false;
                }
                time++;
            }
            return true;
        }
    }


    class Disk {

        public int positions;
        public int startPosition;

        public bool OpenAt(int time) {
            if((startPosition + time) % positions == 0){
                return true;
            }
            return false;
        }

    }
}
