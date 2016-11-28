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

            if (detA == 0)
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
                    returnTab[ctr,0] = detN[ctr] / detA;
                }

                returnMatrice.matrice = returnTab;

                return returnMatrice;
            }
        }

        public Matrice TrouverXParInversionMatricielle()
        {
            return null;
        }

        public Matrice TrouverXParJacobi(double epsilon)
        {
            return null;
        }

        public void DisplaySystème()
        {

        }
    }
}
