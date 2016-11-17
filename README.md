# SchemeInterpreter
Uploaded project
This project is using C# to implement a scheme48 interpretator.  It reads input from a user, and outputs what should be the correct outcome
of the function created or called.  It comes with builtin the basic arithmetic operations, and some other declarations like let and define.
When it reads the input of the user, it stores all the inputs by categorizing them into different types of data like int, string, etc. then 
places them in a tree that is easy to traverse through.  The define function also allows you to make your own functions and use them. 
We have not fully worked out all the bugs.
All the basic test cases work for each different eval method, builtin, and closure.
When we try to pass a function as a param to another function, we get a never ending loop and we get a stack overflow error.
But we have tested everything to work individually.
