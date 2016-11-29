using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpMath.Classe
{
    class Matrice
    {

        public double[,] matrice;
        private int nbRow;
        private int nbCol;

        #region Propriétés
        public int NbRow
        {
            get
            {
                return nbRow;
            }
        }

        public int NbCol
        {
            get
            {
                return nbCol;
            }
        }

        public bool EstCarre
        {
            get
            {
                if (nbRow == nbCol)
                    return true;
                else
                    return false;
            }
        }

        public bool EstReguliere
        {
            get
            {
                if (Determinant != 0)
                    return true;
                else
                    return false;
            }
        }

        public double Trace
        {
            get
            {
                double trace = 0;
                for (int i = 0; i < nbRow; i++)
                {
                    for (int j = 0; j < nbCol; j++)
                    {
                        if (i == j)
                        {
                            trace += matrice[i, j];
                        }
                    }
                }
                return trace;
            }

        }

        public double Determinant
        {
            get
            {
                double determinant = 0;

                //Si la taille de la matrice est de 1
                if (nbCol == 1)
                {
                    determinant = matrice[0, 0];
                }
                //Si la taille de la matrice est de 2
                else if (nbCol == 2)
                {
                    determinant = matrice[0, 0] * matrice[1, 1] - matrice[0, 1] * matrice[1, 0];
                }
                //Si la taille est plus grande que 3
                else
                {
                    bool signeIsPos = true;
                    //On boucle parmis les colonnes
                    for (int j = 0; j < nbCol; j++)
                    {
                        if (signeIsPos)
                        {
                            determinant += ComplementAlgebrique(j, this);
                            signeIsPos = false;
                        }
                        else
                        {
                            determinant -= (ComplementAlgebrique(j, this));
                            signeIsPos = true;
                        }
                    }
                }



                return determinant;
            }

        }

        public Matrice Transposee
        {
            get
            {
                Matrice matTran = new Matrice(nbCol, nbRow);
                for (int i = 0; i < nbRow; i++)
                {
                    for (int j = 0; j < nbCol; j++)
                    {
                        matTran.matrice[j, i] = matrice[i, j];
                    }
                }
                return matTran;
            }
        }

        public Matrice CoMatrice
        {
            get
            {
                Matrice comatrice = new Matrice(nbRow, nbCol);
                for (int i = 0; i < nbRow; i++)
                {
                    for (int j = 0; j < nbCol; j++)
                    {
                        if ((i + j) % 2 == 0)
                        {
                            comatrice.matrice[i, j] = ComplementAlgebriqueComatrice(i, j, this);
                        }
                        else
                        {
                            comatrice.matrice[i, j] = -ComplementAlgebriqueComatrice(i, j, this);
                        }
                    }
                }
                return comatrice;
            }
        }

        public Matrice MatriceInverse
        {
            get
            {
                Matrice resultMatrice = new Matrice(nbRow, nbCol);
                Matrice coMatrice = new Matrice(nbRow, nbCol);
                Matrice transMatrice = new Matrice(nbRow, nbCol);

                //Verification si la matrice est triangulaire
                if (EstTriangulaire(1, 2) && EstTriangulaire(2, 2))
                {
                    for (int i = 0; i < nbRow; i++)
                    {
                        for (int j = 0; j < nbCol; j++)
                        {
                            if (i == j)
                            {
                                resultMatrice.matrice[i, j] = (1 / matrice[i, j]);
                            }
                            else
                            {
                                resultMatrice.matrice[i, j] = 0;
                            }
                        }
                    }
                }
                else
                {
                    //On retrouve la comatrice de la matrice
                    coMatrice = CoMatrice;
                    //On transpose cette comatrice
                    transMatrice = coMatrice.Transposee;
                    //On calcul la matrice inverse
                    resultMatrice = DivideByDeterminant(transMatrice, Determinant);
                }

                return resultMatrice;

            }
        }

        //Retourne vrai si la matrice est stricement dominante diagonalement (pour la méthode de jacobi)
        public Boolean EstStrictementDominante
        {
            get
            {
                Boolean cond = true;
                double somme;
                for (int i = 0; i < nbRow; i++)
                {
                    somme = 0;
                    for (int j = 0; j < nbCol; j++)
                    {
                        if (i != j)
                        {
                            somme += matrice[i, j];
                        }

                    }
                    if (matrice[i, i] <= somme)
                    {
                        cond = false;
                    }
                }

                return cond;
            }
        }

        //Copie une matrice (pour jacobi)
        public void Copier(Matrice mat)
        {

            if (mat.NbRow == nbRow && mat.NbCol == nbCol)
            {
                for (int i = 0; i < mat.NbRow; i++)
                {
                    for (int j = 0; j < mat.NbCol; j++)
                    {
                        matrice[i, j] = mat.matrice[i, j];
                    }
                }
            }
        }
        #endregion


        public Matrice(int row, int column)
        {
            nbCol = column;
            nbRow = row;
            matrice = new double[nbRow, nbCol];

        }

        //Constructeur pour initialiser des matrices de tests au lancement initial du projet
        public Matrice(int test)
        {
            nbCol = 3;
            nbRow = 3;
            if (test == 1)
            {
                matrice = new double[3, 3] { { 2, 0, 0 }, { 0, 3, 0 }, { 0, 0, 4 } };
                //matrice =  new double[3, 3] { { 4, 0, 0 }, { 2, 5, 0 }, { 4, 5, 6 } }; 
            }
            else if (test == 2)
            {
                matrice = new double[3, 3] { { 0, 0, 0 }, { 4, 0, 0 }, { 0, 2, 0 } };
                //matrice = new double[3, 3] { { 4, -1, 0 }, { -1, 4, -1 }, { 0, -1, 4 } };
            }
            else if (test == 3)
            {
                matrice = new double[3, 3] { { 1, 2, -1 }, { -2, 1, 1 }, { 0, 3, -3 } };
            }
            else if (test == 4)
            {
                nbCol = 4;
                nbRow = 4;
                matrice = new double[4, 4] { { 4, 2, 8, 3 }, { 5, 1, 7, 5 }, { 8, 0, 8, 5 }, { 3, 2, 3, 8 } };
            }
            else if (test == 5)
            {
                nbCol = 7;
                nbRow = 7;
                matrice = new double[7, 7] { { 4, 2, 8, 3, 3, 2, 1 }, { 5, 1, 7, 5, 3, 2, 1 }, { 8, 0, 8, 5, 4, 4, 2 }, { 3, 2, 3, 8 , 4, 2, 1 } , { 6, 7, 3, 0 , 1, 1, 3 }
                , { 3, 2, 3, 4 , 4, 4, 4 } , { 3, 5, 3, 2 , 7, 3, 2 }};
            }

        }

        public void RemplirMatrice()
        {
            for (int i = 0; i < nbRow; i++)
            {
                for (int j = 0; j < nbCol; j++)
                {
                    Console.Clear();
                    Console.WriteLine("Saisissez la valeur à la position {0},{1}", i + 1, j + 1);
                    matrice[i, j] = Int32.Parse(Console.ReadLine());
                }
            }
        }

        //Methode remplir matrice des système
        public void RemplirMatrice(int nbMat)
        {
            for (int i = 0; i < nbRow; i++)
            {
                for (int j = 0; j < nbCol; j++)
                {
                    Console.Clear();
                    if (nbMat == 0)
                    {
                        Console.WriteLine("Veuillez entrer les données de la matrice A");
                    }
                    else
                    {
                        Console.WriteLine("Veuillez entrer les données de la matrice B");
                    }
                    Console.WriteLine("Saisissez la valeur à la position {0},{1}", i + 1, j + 1);
                    matrice[i, j] = Int32.Parse(Console.ReadLine());
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
                    matrice[i, j] = j + 1;
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
        //Je n'affiche pas les fractions par soucis de simplicité car c'était pas demandé. J'affiche 2 chiffre après la virgule. Le padRight est pour la visibilité
        public void DisplayMatrice()
        {
            for (int i = 0; i < nbRow; i++)
            {
                for (int j = 0; j < nbCol; j++)
                {
                    //On arrondi les nombres à virgules : perte de précision mais cela reste représentatif tout en étant lisible
                    Console.Write(Math.Round(matrice[i, j], 2).ToString().PadRight(6));
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
                    resultMatrice.matrice[i, j] = matrice[i, j] + pMatrice.matrice[i, j];
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


                resultNbRow = nbRow;
                resultNbCol = pMatrice[indMat].nbCol;
                if (currentMatrice != 1)
                {
                    previousInd = indMat - 1;
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

        //Méthode qui popule la comatrice
        private double ComplementAlgebriqueComatrice(int row, int col, Matrice pMatrice)
        {
            double comp = 0;

            if (pMatrice.NbRow > 3)
            {
                int size = pMatrice.NbRow - 1;
                double[,] newMatrice = new double[size, size];
                Matrice newMat = new Matrice(size, size);
                newMat.matrice = newMatrice;

                int rowInd, colInd, nbDataNewMatrice;
                rowInd = colInd = nbDataNewMatrice = 0;

                for (int i = 0; i < pMatrice.NbRow; i++)
                {
                    for (int j = 0; j < pMatrice.NbCol; j++)
                    {
                        //On élimine la colonne du chiffre dont on calcul le complément
                        if (i != row && j != col)
                        {
                            newMatrice[rowInd, colInd] = pMatrice.matrice[i, j];
                            //On passe a une autre ligne de la nouvelle matrice
                            if (colInd == size - 1)
                            {
                                colInd = 0;
                                rowInd++;
                            }
                            //On change de colonne de la nouvelle matrice
                            else
                            {
                                colInd++;
                            }
                        }

                    }
                }

                newMat.matrice = newMatrice;
                bool isSignePos = true;
                for (int j = 0; j < newMat.NbCol; j++)
                {
                    if (isSignePos)
                    {
                        comp += ComplementAlgebrique(j, newMat);
                        isSignePos = false;
                    }
                    else
                    {
                        comp -= ComplementAlgebrique(j, newMat);
                        isSignePos = true;
                    }
                }

            }
            else
            {
                double[,] newMatrice = new double[2, 2];
                int rowInd, colInd, nbDataNewMatrice;
                rowInd = colInd = nbDataNewMatrice = 0;
                int size = pMatrice.nbRow - 1;
                for (int i = 0; i < pMatrice.nbRow; i++)
                {
                    for (int j = 0; j < pMatrice.nbCol; j++)
                    {
                        //On élimine la colonne du chiffre dont on calcul le complément
                        if (i != row && j != col)
                        {
                            newMatrice[rowInd, colInd] = pMatrice.matrice[i, j];

                            if (colInd == size - 1)
                            {
                                colInd = 0;
                                rowInd++;
                            }
                            //On change de colonne de la nouvelle matrice
                            else
                            {
                                colInd++;
                            }
                        }
                    }
                }
                comp = (newMatrice[0, 0] * newMatrice[1, 1] - newMatrice[0, 1] * newMatrice[1, 0]);
            }

            return comp;
        }


        private double ComplementAlgebrique(int col, Matrice pMatrice)
        {
            double comp = 0;

            if (pMatrice.NbRow > 3)
            {
                int size = pMatrice.NbRow - 1;
                double[,] newMatrice = new double[size, size];
                Matrice newMat = new Matrice(size, size);
                newMat.matrice = newMatrice;

                int rowInd, colInd, nbDataNewMatrice;
                rowInd = colInd = nbDataNewMatrice = 0;

                for (int i = 0; i < pMatrice.NbRow; i++)
                {
                    for (int j = 0; j < pMatrice.NbCol; j++)
                    {
                        //On élimine la colonne du chiffre dont on calcul le complément
                        if (i != 0 && j != col)
                        {
                            newMatrice[rowInd, colInd] = pMatrice.matrice[i, j];
                            //On passe a une autre ligne de la nouvelle matrice
                            if (colInd == size - 1)
                            {
                                colInd = 0;
                                rowInd++;
                            }
                            //On change de colonne de la nouvelle matrice
                            else
                            {
                                colInd++;
                            }
                        }

                    }
                }

                newMat.matrice = newMatrice;
                bool isSignePos = true;
                for (int j = 0; j < newMat.NbCol; j++)
                {
                    if (isSignePos)
                    {
                        comp += pMatrice.matrice[0, col] * ComplementAlgebrique(j, newMat);
                        isSignePos = false;
                    }
                    else
                    {
                        comp -= pMatrice.matrice[0, col] * ComplementAlgebrique(j, newMat);
                        isSignePos = true;
                    }
                }

            }
            else
            {
                double[,] newMatrice = new double[2, 2];
                int rowInd, colInd, nbDataNewMatrice;
                rowInd = colInd = nbDataNewMatrice = 0;
                int size = pMatrice.nbRow - 1;
                for (int i = 0; i < pMatrice.nbRow; i++)
                {
                    for (int j = 0; j < pMatrice.nbCol; j++)
                    {
                        //On élimine la colonne du chiffre dont on calcul le complément
                        if (i != 0 && j != col)
                        {
                            newMatrice[rowInd, colInd] = pMatrice.matrice[i, j];

                            if (colInd == size - 1)
                            {
                                colInd = 0;
                                rowInd++;
                            }
                            //On change de colonne de la nouvelle matrice
                            else
                            {
                                colInd++;
                            }
                        }
                    }
                }
                comp = pMatrice.matrice[0, col] * (newMatrice[0, 0] * newMatrice[1, 1] - newMatrice[0, 1] * newMatrice[1, 0]);
            }

            return comp;
        }

        private Matrice DivideByDeterminant(Matrice pMatrice, double determinant)
        {
            Matrice resultMatrice = new Matrice(pMatrice.NbRow, pMatrice.NbCol);

            for (int i = 0; i < pMatrice.NbRow; i++)
            {
                for (int j = 0; j < pMatrice.NbCol; j++)
                {
                    resultMatrice.matrice[i, j] = pMatrice.matrice[i, j] / determinant;
                }
            }

            return resultMatrice;
        }

    }


}