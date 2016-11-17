// Set -- Parse tree node strategy for printing the special form set!

using System;

namespace Tree
{
    public class Set : Special
    {
	public Set() { }
	
        public override void print(Node t, int n, bool p)
        {
            Printer.printSet(t, n, p);
        }

        public override Node eval(Node exp, Environment env)
        {
            //Assign val to id in all scopes
            Node val = exp.getCdr().getCdr().getCar().eval(env);
            Node id = exp.getCdr().getCar();
            env.assign(id, val);
            return new Ident("set done;");
        }
    }
}

