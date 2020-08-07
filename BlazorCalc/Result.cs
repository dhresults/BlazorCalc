using System;
namespace BlazorCalc
{
    public class Result
    {
        public Result()
        {
        }

        public Result(string equation, string answer)
        {
            Equation = equation;
            Answer = answer;
            HasEquation = true;
        }

        public Result(float left, string @operator, float right, string @answer)
        {
            Left = left;
            Operator = @operator;
            Right = right;
            Answer = @answer;
            HasEquation = false;
        }

        private Boolean HasEquation = false;
        public float Left { get; }
        public string Operator { get; }
        public float Right { get; }
        public string Equation { get; }
        public string Answer { get; }

        public override string ToString()
        {
            if (HasEquation)
            {
                return String.Format("{0} = {1}", Equation, Answer);
            }
            else
            {
                return String.Format("{0}{2}{1} = {3}", Left, Right, Operator, Answer);
            }
        }

    }
}
