// Cond -- Parse tree node strategy for printing the special form cond

using System;

namespace Tree
{
    public class Cond : Special
    {
	public Cond() { }

        public override void print(Node t, int n, bool p)
        { 
            Printer.printCond(t, n, p);
        }

        public override Node eval(Node exp, Environment env)
        {
            Node curNode = exp.getCdr();
            while(curNode.isPair())
            {
                //If evaluated exp is anything but a false node, evaluate it's expression
                if(!(curNode.getCar().getCar().eval(env) == BoolLit.getInstance(false)))
                {
                    curNode = curNode.getCar();               //update curNode to the true path
                    if(curNode.getCdr().isPair())
                    {
                        curNode = curNode.getCdr();
                        Node retNode = Nil.getInstance();
                        
                        while (curNode.isPair())
                        {
                            retNode = curNode.getCar().eval(env);
                            curNode = curNode.getCdr();
                        }
                        return retNode;
                    }
                    else
                    {
                        return curNode.getCar();
                    }
                }
                curNode = curNode.getCdr();
            }
            return Nil.getInstance();
        }
    }
}


