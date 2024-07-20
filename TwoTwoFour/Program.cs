using System.Collections.Generic;
using System.Diagnostics;

namespace TwoTwoFour
{
    internal class Program
    {
        static void ConnectLevels(List<int> usedValues, Graph<int> currentPath, Vertex<int> parentLevel, Vertex<int> nextLevel)
        {
            usedValues.Add(nextLevel.Data);
            currentPath.AddVertex(nextLevel);
            currentPath.AddEdge(parentLevel, nextLevel, 1);
        }

        static List<Graph<int>> GenerateVariableRoutes(List<Variable> variables)
        {
            List<Graph<int>> possibleValuePaths = new();
            int currentVariableIndex = 0;

            //begin Root Level gen
            for (int rootValue = variables[currentVariableIndex].DomainBeginning; rootValue <= variables[0].DomainEnding; rootValue++)
            {
                if (variables[currentVariableIndex].Restrictions == null || !variables[currentVariableIndex].Restrictions.Contains(rootValue))
                {
                    Graph<int> currentPath = new();
                    List<int> usedValues = new();

                    Vertex<int> rootLevel = new Vertex<int>(rootValue);

                    usedValues.Add(rootValue);
                    currentPath.AddVertex(rootLevel);

                    GenerateVariableRouteHelper(variables, currentVariableIndex + 1, usedValues, possibleValuePaths, currentPath, rootLevel, rootValue, 0, rootValue);
                }
            }

            return possibleValuePaths;
        }

        static void GenerateVariableRouteHelper(List<Variable> variables, int currentVariableIndex, List<int> usedValues, List<Graph<int>> possibleValuePaths, Graph<int> currentPath, Vertex<int> parentLevel, int parentValue, int parentCarry, int rootValue)
        {
            if (currentVariableIndex < variables.Count)
            {
                int carry = (parentValue * 2 + parentCarry) / 10;
                int result = (parentValue * 2 + parentCarry) % 10;

                for (int currentValue = variables[currentVariableIndex].DomainBeginning; currentValue <= variables[currentVariableIndex].DomainEnding; currentValue++)
                {
                    if (variables[currentVariableIndex].Restrictions == null || !variables[currentVariableIndex].Restrictions.Contains(currentValue))
                    {
                        Vertex<int> currentLevel = new Vertex<int>(currentValue);
                        if (!usedValues.Contains(currentValue))
                        {
                            //constraints go here
                            if (
                                   (variables[currentVariableIndex].ParentResultIsVar && result == currentValue)
                                || (variables[currentVariableIndex].ParentCarryIsVar && parentCarry == currentValue)
                                || (variables[currentVariableIndex].RootIsResult && (currentValue * 2 + parentCarry) % 10 == rootValue)
                                || variables[currentVariableIndex].NoConstraint
                               )
                            {
                                ConnectLevels(usedValues, currentPath, parentLevel, currentLevel);

                                GenerateVariableRouteHelper(variables, currentVariableIndex + 1, usedValues, possibleValuePaths, currentPath, currentLevel, currentValue, (parentValue * 2 + carry) / 10, rootValue);
                            }
                        }
                    }
                }
                usedValues.Remove(parentValue);
            }
            else
            {
                possibleValuePaths.Add(currentPath);
            }
        }

        static void Main(string[] args)
        {
            // Constraints:

            // O {2, 3, 4, 5, 6, 7, 8}
            // R {0, 2, 4, 6, 8}
            // W {0, 2, 3, 4, 5, 6, 7, 8, 9}
            // U {0, 2, 3, 4, 5, 6, 7, 8, 9}

            // T {6, 7, 8, 9}
            // F {1}

            // C1 {0, 1}
            // C2 {0, 1}
            // C3 {1}

            // 0-9
             
            // C3 C2 C1
            //     T  W  O
            // +   T  W  O
            //--------------
            //  F  O  U  R

       
            List<Variable> Variables = new();


            Variable OVar = new Variable('o', 2, 8);
            Variables.Add(OVar);

            List<int> RRestrictions = new();
            RRestrictions.Add(1);
            RRestrictions.Add(3);
            RRestrictions.Add(5);
            RRestrictions.Add(7);
            Variable RVar = new Variable('r', RRestrictions, 0, 8);
            RVar.ParentResultIsVar = true;
            Variables.Add(RVar);

            List<int> WRestrictions = new();
            WRestrictions.Add(1);
            Variable WVar = new Variable('w', WRestrictions, 0, 9);
            WVar.NoConstraint = true;
            Variables.Add(WVar);

            List<int> URestrictions = new();
            URestrictions.Add(1);
            Variable UVar = new Variable('u', URestrictions, 0, 9);
            UVar.ParentResultIsVar = true;
            Variables.Add(UVar);

            Variable TVar = new Variable('t', 6, 9);
            TVar.RootIsResult = true;
            Variables.Add(TVar);

            Variable FVar = new Variable('f', 1, 1);
            FVar.ParentCarryIsVar = true;
            Variables.Add(FVar);



            List<Graph<int>> possibleValuePaths = new();

            //begin O Level gen
            /*for (int o = 2; o <= 8; o++)
            {
                Graph<int> currentPath = new();
                List<int> usedValues = new();

                Vertex<int> oLevel = new Vertex<int>(o);

                usedValues.Add(o);
                currentPath.AddVertex(oLevel);

                int c1Carry = (o * 2) / 10;
                int c1Result = (o * 2) % 10;

                //begin R Level gen
                for (int r = 0; r <= 8; r += 2)
                {
                    Vertex<int> rLevel = new Vertex<int>(r);
                    if (!usedValues.Contains(r) && c1Result == r)
                    {
                        ConnectLevels(usedValues, currentPath, oLevel, rLevel);

                        //begin W Level gen
                        for (int w = 0; w <= 9; w++)
                        {
                            Vertex<int> wLevel = new Vertex<int>(w);
                            if (!usedValues.Contains(w) && w != 1)
                            {
                                ConnectLevels(usedValues, currentPath, rLevel, wLevel);

                                int c2Carry = ((w * 2) + c1Carry) / 10;
                                int c2Result = ((w * 2) + c1Carry) % 10;

                                //begin W Level gen
                                for (int u = 0; u <= 9; u++)
                                {
                                    Vertex<int> uLevel = new Vertex<int>(u);
                                    if (!usedValues.Contains(u) && u != 1 && c2Result == u)
                                    {
                                        ConnectLevels(usedValues, currentPath, wLevel, uLevel);

                                        //begin T Level gen
                                        for (int t = 6; t <= 9; t++)
                                        {
                                            Vertex<int> tLevel = new Vertex<int>(t);
                                            int c3Result = ((t * 2) + c2Carry) % 10;
                                            if (!usedValues.Contains(t) && c3Result == o)
                                            {
                                                ConnectLevels(usedValues, currentPath, uLevel, tLevel);

                                                int c3Carry = ((t * 2) + c2Carry) / 10;                                               

                                                //begin F Level gen
                                                for (int f = 1; f <= 1; f++)
                                                {
                                                    Vertex<int> fLevel = new Vertex<int>(f);
                                                    if (!usedValues.Contains(f) && c3Carry == f)
                                                    {
                                                        ConnectLevels(usedValues, currentPath, tLevel, fLevel);
                                                        possibleValuePaths.Add(currentPath);
                                                        //usedValues.Remove(1);
                                                    }
                                                }
                                                usedValues.Remove(t);
                                            }
                                        }
                                        usedValues.Remove(u);
                                    }
                                }
                                usedValues.Remove(w);
                            }
                        }
                        usedValues.Remove(r);
                    }
                }
                usedValues.Remove(o);
            }*/

            possibleValuePaths = GenerateVariableRoutes(Variables);

            for (int i = 0; i < possibleValuePaths.Count; i++)
            {
                Vertex<int>? start = possibleValuePaths[i].Verticies[0];
                Vertex<int>? end = possibleValuePaths[i].Search(1);

                var result = possibleValuePaths[i].TraversalSearch(start, end, possibleValuePaths[i].DFSSelector);

                int O = result[0].Data;
                int R = result[1].Data;
                int W = result[2].Data;
                int U = result[3].Data;
                int T = result[4].Data;
                int F = result[5].Data;

                Console.WriteLine(" " + T.ToString() + W.ToString() + O.ToString());
                Console.WriteLine("+" + T.ToString() + W.ToString() + O.ToString());
                Console.WriteLine("----");
                Console.WriteLine(F.ToString() + O.ToString() + U.ToString() + R.ToString());

                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
