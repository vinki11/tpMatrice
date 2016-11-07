using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpMath.Classe
{
    class Matrice
    {

        double[,] matrice;
        private int nbRow;
        private int nbCol;

        #region Attributs
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
#endregion

        public Matrice(int row, int column)
        {
            nbCol = column;
            nbRow = row;
            matrice = new double[nbRow, nbCol];

        }

        //Constructeur de test pour initialiser des matrices triangulaire
        public Matrice(int test)
        {
            nbCol = 3;
            nbRow = 3;
            if (test == 1)
            {
                matrice = new double[3, 3] { { 1, 2, 0 }, { 0,0, 1 }, { 0, 0, 1 } };
            }
            else
            {
                matrice = new double[3, 3] { { 0, 0, 0 }, { 4, 0, 0 }, { 0, 2, 0 } };
            }

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
                    Console.Write(matrice[i, j].ToString().PadRight(3));
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

        //Méthode pour la multiplication matricielle
        //Oui je sais que j'aurais du simplifier cette méthode avec une méthode privé et l'appeler avec 2 matrices comme parametre au lieu de me faie un if comme j'ai fait...j'étais fatigué apparament
        public Matrice FaireProduitMatriciel(Matrice[] pMatrice, int nbMatrice, out int nbOperation)
        {
            int indMat = 0;
            int currentMatrice = 1;
            Matrice resultMatrice = this; //Ne sera pas utiliser avec cette valeur
            Matrice previousResultMatrice = this; //Ne sera pas utiliser avec cette valeur
            nbOperation = 0;

            int resultNbRow, resultNbCol, indResultRow, indResultCol, previousInd;
            previousInd = resultNbCol = resultNbRow = 0;

            while (currentMatrice <= nbMatrice)
            {

                indResultRow = indResultCol = 0;
                if (currentMatrice == 1)
                {
                    resultNbRow = nbRow;
                    resultNbCol = pMatrice[indMat].nbCol;
                }
                else
                {
                    previousInd = indMat - 1;
                    resultNbRow = pMatrice[previousInd].NbRow;
                    resultNbCol = pMatrice[indMat].nbCol;
                }

                resultMatrice = new Matrice(resultNbRow, resultNbCol);

                double resultat;

                // On boucle parmis les lignes de la première matrice
                if (currentMatrice == 1)
                {
                    for (int i = 0; i < nbRow; i++)
                    {
                        indResultCol = 0;
                        //Pour chaque ligne de la première matrice, on calcul une somme de produit correspondant a la position dans la matrice résultante à la colonne de la seconde matrice
                        while (indResultCol < resultNbCol)
                        {
                            resultat = 0;
                            //On calcul les produits de chaque position dans ligne
                            for (int j = 0; j < nbCol; j++)
                            {

                                resultat += matrice[i, j] * pMatrice[indMat].matrice[j, indResultCol];
                            }
                            resultMatrice.matrice[indResultRow, indResultCol] = resultat;
                            indResultCol++;
                        }

                        indResultRow++;
                    }
                    //On ajoute au nombre d'opération le nombre d'opération de cette étape
                    nbOperation += nbRow * pMatrice[indMat].NbRow * pMatrice[indMat].NbCol;
                }
                else
                {
                    for (int i = 0; i < previousResultMatrice.NbRow; i++)
                    {
                        indResultCol = 0;
                        //Pour chaque ligne de la matrice résultat, on calcul une somme de produit correspondant a la position dans la matrice résultante à la colonne de la seconde matrice
                        while (indResultCol < resultNbCol)
                        {
                            resultat = 0;
                            //On calcul les produits de chaque position dans ligne
                            for (int j = 0; j < previousResultMatrice.NbCol; j++)
                            {

                                resultat += previousResultMatrice.matrice[i, j] * pMatrice[indMat].matrice[j, indResultCol];
                            }
                            resultMatrice.matrice[indResultRow, indResultCol] = resultat;
                            indResultCol++;
                        }

                        indResultRow++;
                    }
                    //On ajoute au nombre d'opération le nombre d'opération de cette étape
                    nbOperation += previousResultMatrice.NbRow * pMatrice[indMat].NbRow * pMatrice[indMat].NbCol;
                }
                currentMatrice++;
                indMat++;
                previousResultMatrice = (Matrice)resultMatrice.MemberwiseClone();
                
            }

            //On retourne la matrice finale qui correspond au dernier resultat obtenu
            Matrice finalMatrice = resultMatrice;


            return finalMatrice;
        }

        public bool EstTriangulaire(int typeTriang, int isStrict)
        {
            switch (typeTriang)
            {
                
                case 1: //Triangulaire supérieur
                    return VerifTriangulaireSup(isStrict);
                case 2: //Triangulaire inférieur
                    return VerifTriangulaireInf(isStrict);
                case 3: // Triangulaire inférieur ou supérieur
                    if (VerifTriangulaireInf(isStrict) == false)
                    {
                       return VerifTriangulaireSup(isStrict);
                    }
                    else
                    {
                        return true;
                    }
                default: //Ne devrait jamais passer dans le default car il y a validation du choix passé en paramètre
                    return false;
            }
        }

        private bool VerifTriangulaireSup(int isStrict)
        {
            //On verifie si elle est triangulaire strict
            if (isStrict == 1)
            {
                for (int i = 0; i < nbRow; i++)
                {
                    for (int j = 0; j < nbCol; j++)
                    {
                        if (i >= j)
                        {
                            if (matrice[i, j] != 0)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            else //Pas obligé d'être triangulaire strict
            {
                for (int i = 0; i < nbRow; i++)
                {
                    for (int j = 0; j < nbCol; j++)
                    {
                        if (i > j)
                        {
                            if (matrice[i,j] != 0)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        private bool VerifTriangulaireInf(int isStrict)
        {
            //On verifie si elle est triangulaire strict
            if (isStrict == 1)
            {
                for (int i = 0; i < nbRow; i++)
                {
                    for (int j = 0; j < nbCol; j++)
                    {
                        if (i <= j)
                        {
                            if (matrice[i, j] != 0)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            else //Pas obligé d'être triangulaire strict
            {
                for (int i = 0; i < nbRow; i++)
                {
                    for (int j = 0; j < nbCol; j++)
                    {
                        if (i < j)
                        {
                            if (matrice[i, j] != 0)
                            {
                                return false;
                            }
                        }
                    }
                }

            }

            return true;
        }

    }


}
