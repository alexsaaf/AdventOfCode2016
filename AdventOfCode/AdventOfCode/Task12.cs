using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task12 {

        public Dictionary<string, int> variables;

        public void RunAssembunnyCode(string[] instructions, Dictionary<string, int> _variables) {
            //Set up new dictionary to store variables
            variables = _variables;
            for(int line = 0; line < instructions.Length;) {
                string instruction = instructions[line];
                string[] components = instruction.Split(' ');
                switch (components[0]) {
                    case "inc":
                        //Console.WriteLine("Incrementing " + components[1]);
                        Increment(components[1]);
                        line++;
                        break;
                    case "dec":
                        //Console.WriteLine("Decrementing " + components[1]);
                        Decrement(components[1]);
                        line++;
                        break;
                    case "cpy":
                        //Console.WriteLine("Copying " + components[1] + " to " + components[2]);
                        CopyValue(components[1], components[2]);
                        line++;
                        break;
                    case "jnz":
                        //Console.WriteLine("Jump if not " + components[1] + " is zero.");
                        line = line + JumpNotZero(components[1], int.Parse(components[2]));
                        break;
                }
            }
        }

        void Increment(string variable) {
            if (variables.ContainsKey(variable)) {
                variables[variable] += 1;
            }
        }

        void Decrement(string variable) {
            if (variables.ContainsKey(variable)) {
                variables[variable] -= 1; 
            }
        }

        void CopyValue(string value, string variable) {
            Int32 parsedValue;
            if(variables.ContainsKey(value)){
                if (variables.ContainsKey(variable)) {
                    variables[variable] = variables[value];
                } else {
                    variables.Add(variable, variables[value]);
                }
            }else if(Int32.TryParse(value, out parsedValue)){
                if(variables.ContainsKey(variable)){
                    variables[variable] = parsedValue;
                }else{
                    variables.Add(variable,parsedValue);
                }
            } 
        }

        int JumpNotZero(string variable, int jumpLength) {
            if (variables.ContainsKey(variable)) {
                if (variables[variable] == 0) {
                    return 1;
                }
                return jumpLength;
            }
            int parsedValue;
            if (int.TryParse(variable.ToString(), out parsedValue)) {
                if (parsedValue != 0) {
                    return jumpLength;
                }
            }
            return 1;
        }
    }
}
