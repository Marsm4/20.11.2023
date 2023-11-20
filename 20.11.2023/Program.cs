using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите выражение:");
        string input = Console.ReadLine();

        double result = CalculateExpression(input);
        Console.WriteLine("Результат: " + result);
    }

    static double CalculateExpression(string input)
    {
        string[] tokens = input.Split(' ');

        Stack<double> numbersStack = new Stack<double>();
        Stack<char> operatorsStack = new Stack<char>();

        Dictionary<char, Func<double, double, double>> operators = new Dictionary<char, Func<double, double, double>>();
        operators['+'] = (a, b) => a + b;
        operators['-'] = (a, b) => a - b;
        operators['*'] = (a, b) => a * b;
        operators['/'] = (a, b) => a / b;

        foreach (string token in tokens)
        {
            if (double.TryParse(token, out double number))
            {
                numbersStack.Push(number);
            }
            else
            {
                char op = token[0];

                while (operatorsStack.Count > 0 && GetPrecedence(op) <= GetPrecedence(operatorsStack.Peek()))
                {
                    double operand2 = numbersStack.Pop();
                    double operand1 = numbersStack.Pop();
                    double result = operators[operatorsStack.Pop()](operand1, operand2);
                    numbersStack.Push(result);
                }

                operatorsStack.Push(op);
            }
        }

        while (operatorsStack.Count > 0)
        {
            double operand2 = numbersStack.Pop();
            double operand1 = numbersStack.Pop();
            double result = operators[operatorsStack.Pop()](operand1, operand2);
            numbersStack.Push(result);
        }

        return numbersStack.Pop();
    }

    static int GetPrecedence(char op)
    {
        string operators = "+ - * /";
        return operators.IndexOf(op) / 2;
    }
}