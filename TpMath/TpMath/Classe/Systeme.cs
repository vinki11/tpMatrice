using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpMath.Classe
{
    class Systeme
    {

        private Matrice matriceA; //se doit d'etre carré
        private Matrice matriceB; //se doit d'être de dimension [n,1] ou [1,n]
        private int n;

        #region Propriété
        public Matrice MatriceA
        {
            get
            {
                return matriceA;
            }
        }

        public Matrice MatriceB
        {
            get
            {
                return matriceB;
            }
        }

        public int N
        {
            get
            {
                return n;
            }
        }
        #endregion

        //Constructeur pour initialiser des matrices de tests au lancement initial du projet
        public Systeme(int test)
        {
            double[,] matrice1, matrice2;
            if (test == 1)
            {
                matrice1 = new double[3, 3] { { 2, 1, 3 }, { 1, -2, 1 }, { 1, 1, -2 } };
                matrice2 = new double[3, 1] { { 6 }, { 2 }, { 1 } };
                matriceA = new Matrice(3, 3);
                matriceB = new Matrice(3, 1);
                matriceA.matrice = matrice1;
                matriceB.matrice = matrice2;
                n = 3;
            }

            if (test == 2)
            {
                matrice1 = new double[3, 3] { { 4, -1, 0 }, { -1, 4, -1 }, { 0, -1, 4 } };
                matrice2 = new double[3, 1] { { 100 }, { 100 }, { 100 } };
                matriceA = new Matrice(3, 3);
                matriceB = new Matrice(3, 1);
                matriceA.matrice = matrice1;
                matriceB.matrice = matrice2;
                n = 3;
            }

            if (test == 3)
            {
                matrice1 = new double[3, 3] { { 0, 0, 0 }, { 4, 0, 0 }, { 0, 2, 0 } };
                matrice2 = new double[3, 1] { { 5 }, { 2 }, { 3 } };
                matriceA = new Matrice(3, 3);
                matriceB = new Matrice(3, 1);
                matriceA.matrice = matrice1;
                matriceB.matrice = matrice2;
                n = 3;
            }

           

        }

        public Systeme(Matrice a, Matrice b, int n)
        {
            this.n = n;
            matriceA = new Matrice(n, n);
            matriceB = new Matrice(n, 1);
            matriceA.matrice = a.matrice;
            matriceB.matrice = b.matrice;
        }

        public Matrice TrouverXParCramer()
        {
            double detA;
            double[] detN = new double[N];
            Matrice returnMatrice;

            //On va chercher le déterminant de la matrice
            detA = matriceA.Determinant;

            if (!matriceA.EstReguliere)
            {
                return null;
            }
            else
            {
                for (int col = 0; col < matriceA.NbCol; col++)
                {

                    Matrice newMat = new Matrice(N, N);

                    for (int i = 0; i < matriceA.NbRow; i++)
                    {
                        for (int j = 0; j < matriceA.NbCol; j++)
                        {
                            if (col != j)
                            {
                                newMat.matrice[i, j] = MatriceA.matrice[i, j];
                            }
                            else
                            {
                                newMat.matrice[i, j] = MatriceB.matrice[i, 0];
                            }

                        }
                    }

                    detN[col] = newMat.Determinant;
                    /*newMat.DisplayMatrice();
                    Console.WriteLine("Le determinant de la matrice est {0}", detN[col]);*/

                }

                returnMatrice = new Matrice(N, 1);
                double[,] returnTab = new double[N, 1];
                for (int ctr = 0; ctr < N; ctr++)
                {
                    returnTab[ctr, 0] = detN[ctr] / detA;
                }

                returnMatrice.matrice = returnTab;

                return returnMatrice;
            }
        }

        public Matrice TrouverXParInversionMatricielle()
        {
            Matrice returnMatrice = new Matrice(N, 1);
            Matrice newMat = new Matrice(N, N);
            int useless = 0;

            //On va chercher le déterminant de la matrice


            if (!matriceA.EstReguliere)
            {
                return null;
            }
            else
            {
                Matrice[] matriceToMultiply = new Matrice[1];
                matriceToMultiply[0] = matriceB;
                newMat = matriceA.MatriceInverse;
                returnMatrice = newMat.FaireProduitMatriciel(matriceToMultiply, 1, out useless);

                return returnMatrice;

            }

        }

        public Matrice TrouverXParJacobi(double epsilon)
        {
            if (!matriceA.EstStrictementDominante)
            {
                return null;
            }
            else
            {
                double somme, res;
                Boolean fin;

                Matrice newMat = new Matrice(N, 1);
                Matrice oldMat = new Matrice(N, 1);

                do
                {
                    fin = true;
                    for (int i = 0; i < matriceB.NbRow; i++)
                    {
                        somme = 0;
                        for (int j = 0; j < matriceB.NbRow; j++)
                        {
                            if (j != i)
                                somme += matriceA.matrice[i, j] * oldMat.matrice[j, 0];
                        }
                        res = 1 / matriceA.matrice[i, i] * (matriceB.matrice[i, 0] - somme);
                        newMat.matrice[i, 0] = res;
                        if (res - oldMat.matrice[i, 0] > epsilon)
                            fin = false;
                    }
                    //Console.WriteLine("Matrice temp = \n" + matriceX); Console.ReadLine();
                    oldMat.Copier(newMat);

                } while (fin == false);

                return newMat;
            }
        }

        public void DisplaySystème()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (j != 0)
                    {
                        if (matriceA.matrice[i, j] == 1)
                        {
                            Console.Write(" + ");
                            Console.Write("x" + (j + 1));
                        }
                        else if (matriceA.matrice[i, j] == -1)
                        {
                            Console.Write(" - ");
                            Console.Write("x" + (j + 1));
                        }
                        else if (matriceA.matrice[i, j] >= 0)
                        {
                            Console.Write(" + ");
                            Console.Write(matriceA.matrice[i, j].ToString() + "x" + (j + 1));
                        }
                        else
                        {
                            Console.Write(" - ");
                            double positivNumber = matriceA.matrice[i, j] * -1;
                            Console.Write(positivNumber.ToString() + "x" + (j + 1));
                        }

                    }
                    else
                    {
                        if (matriceA.matrice[i, j] == 1)
                        {
                            Console.Write("x" + (j + 1));
                        }
                        else if (matriceA.matrice[i, j] == -1)
                        {
                            Console.Write("-x" + (j + 1));
                        }
                        else
                        {
                            Console.Write(matriceA.matrice[i, j].ToString() + "x" + (j + 1));
                        }

                    }
                }
                Console.Write(" = " + matriceB.matrice[i, 0].ToString());
                Console.WriteLine();
            }
        }
    }
}