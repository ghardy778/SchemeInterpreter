// If -- Parse tree node strategy for printing the special form if

using System;

namespace Tree
{
    public class If : Special
    {
	public If() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printIf(t, n, p);
        }

        public override Node eval(Node exp, Environment env)
        {
            Node curNode = exp.getCdr();
            curNode = curNode.eval(env);
            //if anything but a false BoolLit, take the true path
            if (!(curNode.getCar().Equals(BoolLit.getInstance(false))))
            {
                return curNode.getCdr().getCar().eval(env);
            }
            else
            {
                return curNode.getCdr().getCdr().getCar().eval(env);
            }
        }
    }
}

