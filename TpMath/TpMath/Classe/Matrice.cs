using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpMath.Classe
{
    class Matrice
    {

        static double[,] matrice;
        private int nbRow;
        private int nbCol;
        
        //Attributs
        public int NbRow
        {
            get
            {
                return nbRow;
            }

            set
            {
                nbRow = value;
            }
        }

        public int NbCol
        {
            get
            {
                return nbCol;
            }

            set
            {
                nbCol = value;
            }
        }

        public Matrice(int row, int column)
        {
            nbCol = column;
            nbRow = row;
            matrice = new double[nbRow, nbCol];

        }

        public void RemplirMatrice()
        {
            for (int i = 0; i < nbRow; i++)
            {
                for (int j = 0; j < nbCol; j++)
                {
                    matrice[i, j] = 2;
                }
            }
        }

        //Methode temporaire pour initialiser différentes sortes de matrices
        public void RemplirMatriceRapide1()
        {
            for (int i = 0; i < nbRow; i++)
            {
                for (int j = 0; j < nbCol; j++)
                {
                    matrice[i, j] = j+1;
                }
            }
        }

        //Methode temporaire pour initialiser différentes sortes de matrices
        public void RemplirMatriceRapide2()
        {
            for (int i = 0; i < nbRow; i++)
            {
                for (int j = 0; j < nbCol; j++)
                {
                    matrice[i, j] = i;
                }
            }
        }

        public void DisplayMatrice()
        {
            for (int i = 0; i < nbRow; i++)
            {
                for (int j = 0; j < nbCol; j++)
                {
                    Console.Write(matrice[i, j]);
                }
                Console.WriteLine();
            }
        }

        //Méthode pour l'addition entre 2 matrice
        public Matrice Additionner(Matrice pMatrice)
        {
            

            Matrice resultMatrice = new Matrice(nbRow, nbCol);
            for (int i = 0; i < nbRow; i++)
            {
                for (int j = 0; j < nbCol; j++)
                {
                    resultMatrice.matrice[i,j] = matrice[i, j] + pMatrice.matrice[i,j];
                }
            }

            return resultMatrice;
        }

        //Méthode pour la multiplication par un scalaire
        public Matrice FaireProduitScalaire(double scalaire)
        {
            Matrice resultMatrice = new Matrice(nbRow, nbCol);

            for (int i = 0; i < nbRow; i++)
            {
                for (int j = 0; j < nbCol; j++)
                {
                    resultMatrice.matrice[i, j] = matrice[i, j] * scalaire;
                }
            }

            return resultMatrice;
        }
    }


}
