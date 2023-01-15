using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace satsolver
{
    static class CVarAssignment
    {
        public static Dictionary<char, bool> _DictAssignment = new Dictionary<char, bool>();
    }
    class CLiteral
    {
        public char Var { get; set; }
        public bool Sign { get; set; }

        public bool Eval()
        {
            bool Val = CVarAssignment._DictAssignment[Var];
            if (Sign)
                return Val;
            else
                return !Val;
        }
    }

    class CClause
    {
        public List<CLiteral> Literals = new List<CLiteral>();
        public bool IsClauseSatisfiable()
        {
            return Literals.Any(x => x.Eval());
            //foreach (var l in Literals)
            //{
            //    bool b = l.Eval();
            //    if (b)
            //        return true;
            //}
            //return false;
        }
    }

    class CFormula
    {
        public List<CClause> Clauses = new List<CClause>();

        public bool IsFormulaSatisfiable()
        {
            return Clauses.All(x => x.IsClauseSatisfiable());

            //foreach(var  c in Clauses)
            //{
            //    bool b = c.IsClauseSatisfiable();
            //    if (!b)
            //        return false;
            //}

            //return true;
        }
    }

    class Program
    {
        static CFormula objFormula = new CFormula();
        const int VARCOUNT = 2;
        static bool[] bTempArray = new bool[VARCOUNT];

        static bool bSolutionFound = false;
        static void TryAssginment(int index)
        {
            if (index >= VARCOUNT)
                return;
            bTempArray[index] = false;
            TryAssginment(index + 1);
            if (index == VARCOUNT - 1)
            {
                CVarAssignment._DictAssignment.Clear();
                for (int k = 0; k < VARCOUNT; ++k)
                {
                    //Console.Write(bTempArray[k]);
                    CVarAssignment._DictAssignment.Add(Convert.ToChar(k + 'a'), bTempArray[k]);
                }
                //Console.WriteLine();

                if(objFormula.IsFormulaSatisfiable())
                {
                    bSolutionFound = true;
                    Console.WriteLine("Expression satisfiable.");
                    foreach (KeyValuePair<char,bool> kv in CVarAssignment._DictAssignment)
                    {
                        Console.WriteLine("{0} -> {1}",kv.Key, kv.Value);
                    }
                }
                //Console.WriteLine();
            }
            bTempArray[index] = true;
            TryAssginment(index + 1);
            if (index == VARCOUNT - 1)
            {
                CVarAssignment._DictAssignment.Clear();
                for (int k = 0; k < VARCOUNT; ++k)
                {
                    //Console.Write(bTempArray[k]);
                    CVarAssignment._DictAssignment.Add(Convert.ToChar(k + 'a'), bTempArray[k]);
                }
               
               // Console.WriteLine();
                if (objFormula.IsFormulaSatisfiable())
                {
                    bSolutionFound = true;
                    Console.WriteLine("Expression satisfiable.");
                    foreach (KeyValuePair<char, bool> kv in CVarAssignment._DictAssignment)
                    {
                        Console.WriteLine("{0} -> {1}", kv.Key, kv.Value);
                    }
                }
               // Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            try
            {
                string sFormula = "(a|b)&(-a|b)&(-a|-b)";
                //string sFormula = "(a|b)&(-a|b)&(a|-b)&(-a|-b)";

                string[] Clauses = sFormula.Split('&');

                foreach (string Clause in Clauses)
                {
                    string[] Literals = Clause.Split('|');

                    CClause objClause = new CClause();
                    foreach (string Literal in Literals)
                    {
                        string s = Literal.Trim(')', '(');
                        // Console.WriteLine(s);

                        CLiteral objLiteral = new CLiteral();
                        if (s.Length == 1)
                        {
                            objLiteral.Var = s[0]; objLiteral.Sign = true;
                        }
                        else if (s.Length == 2)
                        {
                            objLiteral.Var = s[1]; objLiteral.Sign = false;
                        }

                        objClause.Literals.Add(objLiteral);
                    }

                    objFormula.Clauses.Add(objClause);
                }

                TryAssginment(0);

                if (bSolutionFound == false)
                    Console.WriteLine("Expression not satisfiable.");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

