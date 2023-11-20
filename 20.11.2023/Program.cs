using System;
using System.Collections.Generic;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите выражение:");
            string input = Console.ReadLine();

            int result = CalculateExpression(input);
            Console.WriteLine("Результат: " + result);
        }

        static int CalculateExpression(string input)
        {
            string[] tokens = input.Split(' ');

            Stack<int> numbersStack = new Stack<int>();
            Stack<char> operatorsStack = new Stack<char>();

            foreach (string token in tokens)
            {
                if (int.TryParse(token, out int number))
                {
                    numbersStack.Push(number);
                }
                else
                {
                    char op = token[0];

                    while (operatorsStack.Count > 0 && GetPrecedence(op) <= GetPrecedence(operatorsStack.Peek()))
                    {
                        int result = ApplyOperator(numbersStack.Pop(), numbersStack.Pop(), operatorsStack.Pop());
                        numbersStack.Push(result);
                    }

                    operatorsStack.Push(op);
                }
            }

            while (operatorsStack.Count > 0)
            {
                int result = ApplyOperator(numbersStack.Pop(), numbersStack.Pop(), operatorsStack.Pop());
                numbersStack.Push(result);
            }

            return numbersStack.Pop();
        }

        static int GetPrecedence(char op)
        {
            switch (op)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                default:
                    return 0;
            }
        }

        static int ApplyOperator(int b, int a, char op)
        {
            switch (op)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                case '/':
                    return a / b;
                default:
                    throw new ArgumentException("Неверная операция");
            }
        }
    }
}
