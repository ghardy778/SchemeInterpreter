// Closure.java -- the data structure for function closures

// Class Closure is used to represent the value of lambda expressions.
// It consists of the lambda expression itself, together with the
// environment in which the lambda expression was evaluated.

// The method apply() takes the environment out of the closure,
// adds a new frame for the function call, defines bindings for the
// parameters with the argument values in the new frame, and evaluates
// the function body.

using System;

namespace Tree
{
    public class Closure : Node
    {
        private Node fun;		// a lambda expression
        private Environment env;	// the environment in which
                                        // the function was defined

        public Closure(Node f, Environment e)	{ fun = f;  env = e; }

        public Node getFun()		{ return fun; }
        public Environment getEnv()	{ return env; }

        // done TODO: The method isProcedure() should be defined in
        // class Node to return false.
        public  override  bool isProcedure()	{ return true; }

        public override void print(int n) {
            // there got to be a more efficient way to print n spaces
            for (int i = 0; i < n; i++)
                Console.Write(' ');
            Console.WriteLine("#{Procedure");
            if (fun != null)
                fun.print(Math.Abs(n) + 4);
            for (int i = 0; i < Math.Abs(n); i++)
                Console.Write(' ');
            Console.WriteLine('}');
        }

        
        public  override  Node apply (Node args)
        {
            Environment funcEnv = new Environment(env);              //Create new environment
            Node paramList = fun.getCdr().getCar();                  // Get the params from the frame
            while(!(paramList.isNull()))                             //For each param
            {
                funcEnv.define(paramList.getCar(), args.getCar());  //define a value to the id
                paramList = paramList.getCdr();                     //go to the next param
                args = args.getCdr();                               //go to the next id
            }
            Node bList = fun.getCdr().getCdr();                     //Evaluate the function body and return
            return bList.eval(funcEnv);
            

        }

        public override Node eval(Environment env)
        {
            return base.eval(env);
        }
    }    
}
