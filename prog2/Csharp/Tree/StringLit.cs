// StringLit -- Parse tree node class for representing string literals

using System;

 
namespace Tree
{

    public class StringLit : Node
    {
        private string stringVal;
        public static Boolean quotes = true;

        public StringLit(string s)
        {
            stringVal = s;
        }

        public override void print(int n)
        {
            if(quotes)
                Printer.printStringLit(n, stringVal);
            else
            {
                for(int i = 0; i < n; i++)
                {
                    Console.Write(' ');
                }
                Console.Write(stringVal);
                if (n >= 0)
                    Console.WriteLine();
            }
        }

        public override bool isString()
        {
            return true;
        }

        public override Node eval(Environment env)
        {
            return this;
        }
    }
}

