using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tautology
{
    class Program
    {
        static void Main(string[] args)
        {
            string operators = "CIDEN";
            string prefixExp = "IIpqDpNp";
            const int MAX_VARS = 16;
            int n = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                prefixExp = Console.ReadLine();
                Dictionary<char, int> positions = new Dictionary<char, int>();
                int num_vars = PreProcess(prefixExp, positions, operators);
                if (num_vars > MAX_VARS || prefixExp.Length > 111)
                    Console.WriteLine("NO");
                else
                    Console.WriteLine(IsTautology(prefixExp, num_vars, operators, positions));
            }
            Console.ReadLine();
        }

        public static int PreProcess(string prefixExp,Dictionary<char,int> positions,string operators)
        {
            int pos = 0;
            foreach (char c in prefixExp)
            {
                if (!operators.Contains(c) && !positions.ContainsKey(c))
                {
                    positions.Add(c,pos++);
                }
            }
            return positions.Count;
        }

        public static string IsTautology(string exp, int num_vars, string operators, Dictionary<char, int> positions)
        {
            string ans = "NO";
            try
            {
                int num_states = Convert.ToInt32(Math.Pow(2, num_vars));
                bool[] binary_state;
                bool truth = true;
                for (int state = 0; state < num_states; state++)
                {
                    binary_state = ToBooleanState(state, num_vars);
                    truth = Evaluate(exp, operators, positions, num_vars, binary_state);
                    if (!truth)
                        break;
                }

                if (!truth)
                    ans = "NO";
                else
                    ans = "YES";
            }
            catch (Exception e) { }
            return ans;
        }

        public static bool[] ToBooleanState(int integerState,int num_vars)
        {
            bool[] state = new bool[num_vars];
            string str = Convert.ToString(integerState, 2);
            for(int i=0;i<str.Length;i++)
                state[i] = (str[i] == '1') ? true : false;
            return state;
        }

        public static bool Evaluate(String expression, string operators,Dictionary<char,int> positions,int num_vars,bool[] state)
        {
            bool res = true;
            bool op1,op2;
            char elm;
            Stack<bool> eval = new Stack<bool>();
            int i = expression.Length-1;
            while (i >=0)
            {
                elm = expression[i];
                switch (elm)
                {
                    case 'C':
                        op1 = eval.Pop();
                        op2 = eval.Pop();
                        eval.Push(And(op1, op2));
                        break;
                    case 'I': 
                        op1 = eval.Pop();
                        op2 = eval.Pop();
                        eval.Push(Implies(op1, op2));
                        break;
                    case 'D': 
                        op1 = eval.Pop();
                        op2 = eval.Pop();
                        eval.Push(Or(op1, op2)); 
                        break;
                    case 'E':
                        op1 = eval.Pop();
                        op2 = eval.Pop();
                        eval.Push(Equivalence(op1, op2));
                        break;
                    case 'N': 
                        op1 = eval.Pop();
                        eval.Push(Not(op1));
                        break;
                    default : eval.Push(state[positions[elm]]); 
                        break;
                }
                i--;
            }
            return eval.Pop();
        }

        public static bool Not(bool operand) { return !operand; }
        public static bool And(bool op1,bool op2) {return (op1 && op2);}
        public static bool Or(bool op1, bool op2) { return (op1 || op2); }
        public static bool Implies(bool op1, bool op2) { return (op1 && !op2) ? false : true; }
        public static bool Equivalence(bool op1, bool op2) { return (op1 == op2); }

    }
}