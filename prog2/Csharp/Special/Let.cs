// Let -- Parse tree node strategy for printing the special form let

using System;

namespace Tree
{
    public class Let : Special
    {
	public Let() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printLet(t, n, p);
        }

        public override Node eval(Node exp, Environment env)
        {
            //Intialize id and val for bindings
            //Make new env with old env over it
            Node curNode = exp.getCdr().getCar();
            Node id = Nil.getInstance();
            Node val = Nil.getInstance();
            Environment envl = new Environment(env);

            //Binding loop
            while(curNode!= Nil.getInstance())
            {
                id = curNode.getCar().getCar();
                val = curNode.getCar().getCdr().getCar().eval(envl);
                envl.define(id, val);
                curNode = curNode.getCdr();
            }

            //Evaluate the function body and return
            curNode = exp.getCdr().getCdr();
            return curNode.getCar().eval(envl);
     
        }
    }
}


