// Regular -- Parse tree node strategy for printing regular lists

using System;

namespace Tree
{
    public class Regular : Special
    {
        public Regular() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printRegular(t, n, p);
        }

        /**
         * This is the eval for regular cons node(ie when the car is another cons or not a different special
         * 
         * */
        public override Node eval(Node exp, Environment env)
        {
            Node car = exp.getCar().eval(env);
            Node cdr = Nil.getInstance();
            if (!(exp.getCdr().isNull()))
            {
                cdr = exp.getCdr().eval(env);
            }

            //If the car is asymbol, it will be a builtin or a fucntion call so we can use apply
            if (exp.getCar().getCar().getName().Equals("lambda"))
            {
                return car.apply(cdr).getCar();
            }
            else if (exp.getCar().isSymbol()) 
            {
                if (car.isNumber())
                    return new Cons(car, cdr);
                return car.apply(cdr);
            }

            //If none of the conditions are met, just return a cons with the evaluated car and cdr
            return new Cons(car, cdr);


        }
    }
}


