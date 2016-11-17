// BuiltIn -- the data structure for built-in functions

// Class BuiltIn is used for representing the value of built-in functions
// such as +.  Populate the initial environment with
// (name, new BuiltIn(name)) pairs.

// The object-oriented style for implementing built-in functions would be
// to include the C# methods for implementing a Scheme built-in in the
// BuiltIn object.  This could be done by writing one subclass of class
// BuiltIn for each built-in function and implementing the method apply
// appropriately.  This requires a large number of classes, though.
// Another alternative is to program BuiltIn.apply() in a functional
// style by writing a large if-then-else chain that tests the name of
// the function symbol.

using System;
using Parse;

namespace Tree
{
    public class BuiltIn : Node
    {
        private Node symbol;            // the Ident for the built-in function

        public BuiltIn(Node s)		{ symbol = s; }

        public Node getSymbol()		{ return symbol; }

        //  Done TODO: The method isProcedure() should be defined in
        // class Node to return false.
        public  override  bool isProcedure()	{ return true; }

        public override void print(int n)
        {
            // there got to be a more efficient way to print n spaces
            for (int i = 0; i < n; i++)
                Console.Write(' ');
            Console.Write("#{Built-in Procedure ");
            if (symbol != null)
                symbol.print(-Math.Abs(n));
            Console.Write('}');
            if (n >= 0)
                Console.WriteLine();
        }

        //A if chain that encomposes all builtin functions
        public  override  Node apply (Node args)
        {
            if (symbol.getName().Equals("b+"))
            {
                if (args.isNull())
                    //return new IntLit(0) if no args
                    return new Cons(new IntLit(0), Nil.getInstance());  
                
                int sum = args.getCar().getValue(); //sum of first param
                //Add together sum of rest of params
                while (args.getCdr().isPair())
                {
                    args = args.getCdr();
                    sum += args.getCar().getValue();
                }
                return new IntLit(sum);
            }
            else if (symbol.getName().Equals("b-"))
            {

                int differnce = args.getCar().getValue();  //get value of first param
                //subtract remaining params
                while (args.getCdr().isPair()) 
                {
                    args = args.getCdr();
                    differnce -= args.getCar().getValue();
                }
                return new IntLit(differnce);

            }
            else if (symbol.getName().Equals("b*"))
            {
                int product = args.getCar().getValue(); //get value of first param
                //multiply remaining params
                while (args.getCdr().isPair())
                {
                    args = args.getCdr();
                    product *= args.getCar().getValue();
                }
                return new IntLit(product);

            }
            else if (symbol.getName().Equals("b/"))
            {
                int quotient = args.getCar().getValue(); //get value of first param
                //divide by remaining params
                while (args.getCdr().isPair())
                {
                    args = args.getCdr();
                    //ensure that we do not divide by 0
                    if (args.getCar().getValue() == 0)
                    {
                        Console.Error.WriteLine("Can't divide by 0");
                    }
                    quotient /= args.getCar().getValue();
                }
                return new IntLit(quotient);

            }

            //Get both arguements and checks to see if their values are equal
            else if(symbol.getName().Equals("b="))
            {
                int exp1 = args.getCar().getValue();
                int exp2 = args.getCdr().getCar().getValue();
                if (exp1 == exp2)
                    return BoolLit.getInstance(true);
                else
                    return BoolLit.getInstance(false);

            }
            //Get both arguments and check to see if the first is less than the second
            else if(symbol.getName().Equals("b<"))
            {
                int exp1 = args.getCar().getValue();
                int exp2 = args.getCdr().getCar().getValue();
                if (exp1 < exp2)
                    return BoolLit.getInstance(true);
                else
                    return BoolLit.getInstance(false);
            }
            /**
             * We implemented symbol? to only look at the first parameter and ignore any params coming after it.
             * Returns true if it is an identifier
            **/
            else if (symbol.getName().Equals("symbol?"))
            {
                if (args.getCar().isSymbol())
                    return BoolLit.getInstance(true);
                else
                    return BoolLit.getInstance(false);
            }
            /**
             * We implemented number? to only look at the first parameter and ignore any params coming after it
             * Returns true if it is an IntLit
            **/
            else if (symbol.getName().Equals("number?"))
            {
                if (args.getCar().isNumber())
                    return BoolLit.getInstance(true);
                else
                    return BoolLit.getInstance(false);
            }
            //Returns the car of the arguments
            else if (symbol.getName().Equals("car"))
            {
                args = args.getCar();
                return args.getCar();
            }
            //Returns the cdr of the arguments
            else if (symbol.getName().Equals("cdr"))
            {
                args = args.getCar();
                return args.getCdr();
            }
            //Returns the cons of param one and two
            else if (symbol.getName().Equals("cons"))
            {
                if (!(args.getCdr().getCdr().isNull()))
                    Console.Error.WriteLine("cons only accepts 2 parameters");
                Node ans = new Cons(args.getCar(), args.getCdr().getCar());
                return ans;
            }
            //Sets the car of the first arguement to the Node of the second arguement
            else if (symbol.getName().Equals("set-car!"))
            {
                args = args.getCdr();
                Node newCar = args.getCdr().getCar();
                args = args.getCar();
                args.setCar(newCar);
                return args;

            }
            //Sets the cdr of the first arguement to the Node of the second arguement
            else if (symbol.getName().Equals("set-cdr!"))
            {
                args = args.getCdr();
                Node newCdr = args.getCdr().getCar();
                args = args.getCar();
                args.setCdr(newCdr);
                return args;

            }
            //checks to see if it is a nil node
            else if (symbol.getName().Equals("null?"))
            {
                if (args.getCar().isNull())
                    return BoolLit.getInstance(true);
                else
                    return BoolLit.getInstance(false);

            }
            //checks to see if it is a cons node
            else if (symbol.getName().Equals("pair?"))
            {
                if (args.getCar().isPair())
                    return BoolLit.getInstance(true);
                else
                    return BoolLit.getInstance(false);
            }
            /**Checks for pointer equality
            * Exception to pointer equality is identifiers
            * If two identifiers have same name, it returns true
            * Two lists only return true, if they point to the exact same cons node
            * */
            else if (symbol.getName().Equals("eq?"))
            {
                Node exp1 = args.getCar();
                Node exp2 = args.getCdr().getCar();
                if (exp1.isSymbol() && exp2.isSymbol())
                {
                    if (exp1.getName().Equals(exp2.getName()))
                        return BoolLit.getInstance(true);
                }
                else if (exp1.isNull() && exp2.isNull())
                    return BoolLit.getInstance(true);
                else if (exp1.isBool() && exp2.isBool())
                {
                    if (exp1 == BoolLit.getInstance(true) && exp2 == BoolLit.getInstance(true))
                        return BoolLit.getInstance(true);
                    if (exp1 == BoolLit.getInstance(false) && exp2 == BoolLit.getInstance(false))
                        return BoolLit.getInstance(true);
                }
                //Only returns true is Cons nodes are the exact same
                else if (exp1.isPair() && exp2.isPair())
                {
                    if (exp1.isEqual(exp2))
                        return BoolLit.getInstance(true);
                }
                return BoolLit.getInstance(false);
            }
            //Checks to see if it is a Builtin or a closure
            else if (symbol.getName().Equals("procedure?"))
            {
                if (args.getCar().isProcedure())
                    return BoolLit.getInstance(true);
                else
                    return BoolLit.getInstance(false);
            }
            //reads the input of args and parses it
            else if (symbol.getName().Equals("read"))
            {
                Parser parser = new Parser(new Scanner(Console.In), new TreeBuilder());
                return (Node)parser.parseExp();
            }
            //Prints out the arguments
            else if (symbol.getName().Equals("write"))
            {
                args.getCar().print(0);
                return new StringLit("#{Unspecific}");               //prints out whatever is returned too
            }
            //Prints arguements without quotes
            else if (symbol.getName().Equals("display"))
            {
                StringLit.quotes = false;
                args.getCar().print(0);
                StringLit.quotes = true;
                return new StringLit("#{Unspecific}");
            }
            //Prints a newline
            else if (symbol.getName().Equals("newline"))
            {
                Console.WriteLine();
                return new StringLit("#{Unspecific}");

            }
            //Calls our implemented eval function
            else if (symbol.getName().Equals("eval"))
            {
                return args.eval(Scheme4101.globalEnv).getCar();
            }

            //Calls our apply function
            else if (symbol.getName().Equals("apply"))
            {
                return args.apply(args);      
            }
            //Returns the global environment
            else if (symbol.getName().Equals("interaction-environment"))
            {
                return Scheme4101.globalEnv;
            }
           //if it's not anything yet, it is not a builtin, so it returns a nil node
            else
                return Nil.getInstance();



    	}

        public override Node eval(Environment env)
        {
            return base.eval(env);
        }
    }    
}

