// Begin -- Parse tree node strategy for printing the special form begin

using System;

namespace Tree
{
    public class Begin : Special
    {
	public Begin() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printBegin(t, n, p);
        }


        public override Node eval(Node exp, Environment env)
        {
            Node curNode = exp.getCdr();
            Node retNode = Nil.getInstance();

            //Evaluate the car of curNode into retNode
            //Increment curNode to next cdr until it's not a cons
            while (curNode.isPair())
            {
                retNode = curNode.getCar().eval(env);  
                curNode = curNode.getCdr();
            }
            return retNode;
        }
    }
}

