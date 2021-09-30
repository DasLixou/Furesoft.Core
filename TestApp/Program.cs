﻿using Furesoft.Core.CLI;
using System;
using TestApp.MathEvaluator;

namespace TestApp
{
    internal static class Program
    {
        public static int Main(string[] args)
        {
            ExpressionParser.Init();

            ExpressionParser.AddVariable("x", 42);
            ExpressionParser.RootScope.Import(typeof(Math));

            var floorPi = ExpressionParser.Evaluate("floor(PI);");
            ExpressionParser.Evaluate("g(x) = x*x");
            //[1, 5]
            var gres = ExpressionParser.Evaluate("g: x in N [1, 5];g(4);");
            //ToDo: fix ]1, 5] condition null

            ExpressionParser.RootScope.ImportedFunctions.Add("display", new Func<double[], double>((x) => { Console.WriteLine(x[0]); return 0; }));

            var result = ExpressionParser.Evaluate("f: x in N 2 <= x < 20 && x % 2 == 0; f(x) = 2*x; f(2); f(3); f(4);  display(-f(6));|-4**2|");
            //ToDo: is Infinity Binding implemented?

            //ToDo: implement constraint for interval
            //ToDo: implement tests
            //ToDo: add simplification mode instead of evaluation?
            //ToDo: compiler?

            //ToDo: add module for functionparameterconstrain f: x in N x % 2 == 0
            //ToDo: add boolean operators == !=
            //ToDo: add constrain for return value?
            //ToDo: add measurements for parameters and variables and resolve or specify is return value is in correct measurement: f: x is [m]
            //y is [m/s]
            //measure for f(x) is [m/s*s]
            //ToDo: move to new assembly
            //ToDo: add position to error messages if possible
            //ToDo: add module system
            //ToDo: add module definion parsing: module geometry; identifier or string as argument
            //ToDo: add importing module to global scope: use geometry; identifier or string as argument
            //ToDo: add loading module from file: use "./geometry.math"; automatic import all things to scope

            return App.Current.Run();
        }
    }
}