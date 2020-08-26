using System;
using System.Collections.Generic;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace BlazorCalc
{
    internal class CalcVisitor : CalcBaseVisitor<double>
    {
        private readonly Dictionary<string, Func<double, double, double>> _funcMap =
            new Dictionary<string, Func<double, double, double>>
            {
                {"+", (a, b) => a + b},
                {"-", (a, b) => a - b},
                {"*", (a, b) => a * b},
                {"/", (a, b) => a / b}
            };

        public override double VisitStart([NotNull] CalcParser.StartContext context)
        {
            return this.Visit(context.expr());
        }

        public override double VisitAtomExpr([NotNull] CalcParser.AtomExprContext context)
        {
            return Double.Parse(context.GetText());
        }

        public override double VisitOpExpr([NotNull] CalcParser.OpExprContext context)
        {
            double left = Visit(context.left);
            double right = Visit(context.right);
            string op = context.op.Text;
            return _funcMap[op](left, right);
        }

    }

}