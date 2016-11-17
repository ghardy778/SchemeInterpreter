// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree
{
    public class Define : Special
    {
	public Define() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printDefine(t, n, p);
        }

        public override Node eval(Node exp, Environment env)
        {
            //Function Define
            //Create a lambda expression that contains the parameters and the function body
            if (exp.getCdr().getCar().isPair())
            {
                Lambda lam = new Lambda();
                Node param = exp.getCdr().getCar().getCdr();
                Node body = exp.getCdr().getCdr().getCar();
                Node lamExp = new Cons(new Ident("lambda"), new Cons(param, body));

                env.define(exp.getCdr().getCar().getCar(), new Closure(lamExp, env));
   

            }
            //Variable define
            else
            {
                Node cadr = exp.getCdr().getCar();
                Node caddr = exp.getCdr().getCdr().getCar();
                env.define(cadr, caddr);
                
            }
            return new StringLit("; no values returned;");
        }
    }
}


