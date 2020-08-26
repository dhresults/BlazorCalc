using System;
using System.Data;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace BlazorCalc
{

    public class Calculator
    {
        public Calculator()
        {
        }

        public Calculator(string left, string right, string @operator)
        {
            float value;
            if (!float.TryParse(left, out value))
            {
                Left = 0;
            } else
            {
                Left = value;
            }
            if (!float.TryParse(right, out value))
            {
                Right = 0;
            }
            else
            {
                Right = value;
            }

            Right = float.Parse(right);
            Operator = @operator;
        }

        public int HistoryIndex = -1;
        List<Result> Results = new List<Result>();
        public string Answer { get; set; } = "0";
        public float Left { get; set; } = 0;
        public float Right { get; set; } = 0;
        public string Operator { get; set; } = "+";
        public string Equation { get; set; } = "";

        public Boolean canStepBack { get; set; } = false;
        public Boolean canStepForward { get; set; } = false;
        public Boolean canClearHistory { get; set; } = false;

        // - assigns the left side of the equation
        // - value is a string representing a number
        public void setLeft(string value)
        {
            Left = float.Parse(value);
        }

        // - assigns the right side of the equation
        // - value is a string representing a number
        public void setRight(string value)
        {
            Right = float.Parse(value);
        }

        // - assigns the operator
        // - value is one of: "+", "-", "*", "/"
        public void setOperator(string value)
        {
            Operator = value;
        }

        public void ClearHistory()
        {
            HistoryIndex = -1;
            Results = new List<Result>();
            Answer = "0";
            canClearHistory = false;
            canStepBack = false;
            canStepForward = false;
        }

        public void StepBack()
        {
            if(!canStepBack)
            {
                return;
            }
            HistoryIndex -= 1;
            if(HistoryIndex == 0)
            {
                canStepBack = false;
            } else
            {
                canStepBack = true;
            }
            if (HistoryIndex < Results.Count - 1)
            {
                canStepForward = true;
            }
            Left = Results[HistoryIndex].Left;
            Right = Results[HistoryIndex].Right;
            Operator = Results[HistoryIndex].Operator;
            Equation = Results[HistoryIndex].Equation;
            Answer = Results[HistoryIndex].ToString();
        }

        public void StepForward()
        {
            if (!canStepForward)
                return;
            HistoryIndex += 1;
            if(HistoryIndex < Results.Count - 1)
            {
                canStepForward = true;
            } else
            {
                canStepForward = false;
            }
            if(HistoryIndex > 0)
            {
                canStepBack = true;
            }
            Left = Results[HistoryIndex].Left;
            Right = Results[HistoryIndex].Right;
            Operator = Results[HistoryIndex].Operator;
            Equation = Results[HistoryIndex].Equation;
            Answer = Results[HistoryIndex].ToString();
        }

        private void UpdateStepperControls()
        {
            if(HistoryIndex >= 0)
            {
                canClearHistory = true;
            }
            if(HistoryIndex > 0)
            {
                canStepBack = true;
            } else
            {
                canStepBack = false;
            }
            if(Results.Count > 0 && HistoryIndex < Results.Count-1)
            {
                canStepForward = true;
            } else if(HistoryIndex == Results.Count-1)
            {
                canStepForward = false;
            }
        }

        public string calculateEquation()
        {
            try
            {
                var answer = evaluate(Equation);
                Result result = new Result(Equation, answer);
                Results.Add(result);
                HistoryIndex += 1;
                UpdateStepperControls();
                Answer = String.Format("{0} = {1}", Equation, answer);
                return Answer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Result result = new Result(Equation, "Invalid equation");
                Results.Add(result);
                HistoryIndex += 1;
                UpdateStepperControls();
                Answer = String.Format("{0} = {1}", Equation, "Invalid equation");
                return e.ToString();
            }
        }

        public string calculate()
        {
            string equation = String.Format("{0}{1}{2}", Left, Operator, Right);
            var v = evaluate(equation);
            string answer = String.Format("{0} = {1}", equation, v);
            Result result = new Result(Left, Operator, Right, v);
            Results.Add(result);
            HistoryIndex += 1;
            UpdateStepperControls();
            Answer = answer;
            return v.ToString();
        }

        public string evaluate(string expr)
        {
            CalcLexer lexer = new CalcLexer(new AntlrInputStream(expr));
            CalcParser parser = new CalcParser(new CommonTokenStream(lexer));
            IParseTree tree = parser.start();
            double answer = new CalcVisitor().Visit(tree);
            return answer.ToString();
        }
    }

}
