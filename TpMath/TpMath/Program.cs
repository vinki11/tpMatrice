using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpMath.Classe;

namespace TpMath
{
    class Program
    {
        static Matrice[] listeMatrice = new Matrice[50];
        static int indexMatrice = 0;

        static void Main(string[] args)
        {
            NavigationMenu();
        }

        static void NavigationMenu()
        {
            int choix;

            Console.WriteLine("");
            Console.WriteLine("Que voulez-vous faire?");
            Console.WriteLine("1- Créer une nouvelle Matrice");
            Console.WriteLine("2 -Afficher une Matrice");
            Console.WriteLine("3 -Faire des opération de Matrice");
            Console.WriteLine("4- Quitter le programme");
            choix = Int32.Parse(Console.ReadLine());

            switch (choix)
            {
                case 1:
                    AjouterUneMatrice();
                    break;
                case 2:
                    AfficherUneMatrice();
                    break;
                case 3:
                    OperationMatrice();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Vous avez fait un choix invalide.");
                    break;

                    NavigationMenu();
            }

            NavigationMenu();

        }
        //Ajout d'une matrice
        protected static void AjouterUneMatrice()
        {
            int nbRow, nbCol;
            int methodeRempl;

            //Saisit des paramètre de la matrice
            Console.WriteLine("");
            Console.WriteLine("Veuillez entrer le nombre de ligne de la matrice");
            nbRow = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Veuillez entrer le nombre de colonne de la matrice");
            nbCol = Int32.Parse(Console.ReadLine());

            Matrice newMatrice = new Matrice(nbRow, nbCol);

            //Remplissage des données de la matrice
            Console.WriteLine("");
            Console.WriteLine("Choississez la methode de remplissage de la matrice");
            Console.WriteLine("1- methode rapide #1");
            Console.WriteLine("2- methode rapide #2");
            Console.WriteLine("3- methode rapide #3");
            methodeRempl = Int32.Parse(Console.ReadLine());

            switch(methodeRempl)
            {
                case 1:
                    newMatrice.RemplirMatriceRapide1();
                    break;
                case 2:
                    newMatrice.RemplirMatriceRapide2();
                    break;
                case 3:
                    newMatrice.RemplirMatrice();
                    break;
                default:
                    newMatrice.RemplirMatrice();
                    break;
            }


            //Ajout de la matrice à la liste des Matrices
            listeMatrice[indexMatrice] = newMatrice;
            indexMatrice++;

            Console.Clear();
            Console.WriteLine("La matrice #{0} a été créer", indexMatrice);
        }

        //Affichage d'une matrice
        protected static void AfficherUneMatrice()
        {
            int noMatrice;
            //Demande de quel matrice que l'on veut saisir
            Console.WriteLine("");
            Console.WriteLine("Quel matrice vouler vous afficher?");
            noMatrice = Int32.Parse(Console.ReadLine());

            if (noMatrice > indexMatrice || noMatrice <= 0) {
                Console.Clear();
                Console.WriteLine("Erreur, cette matrice n'existe pas");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Affichage de la matrice #{0}", noMatrice);
                noMatrice--;
                listeMatrice[noMatrice].DisplayMatrice();
            }
        }

        //Liste des Opérations possible sur les matrices
        protected static void OperationMatrice()
        {
            int operation;

            Console.WriteLine("");
            Console.WriteLine("Quelle opération voulez-vous faire?");
            Console.WriteLine("1- Additionner deux matrices");
            Console.WriteLine("1- Faire le produit scalaire d'une matrice");
            operation = Int32.Parse(Console.ReadLine());

            switch(operation)
            {
                case 1:
                    AdditionMatrice();
                    break;
                case 2:
                    ProduitScalaire();
                    break;
                case 3:
                    break;
                case 4:
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Vous avez fait un choix invalide.");
                    break;
            }
        }

        #region Operations de Matrices
        //Addition de 2 matrice
        protected static void AdditionMatrice()
        {
            int matrice1, matrice2, indMat1, indMat2;

            //Saisit des matrice a additionner
            Console.WriteLine("");
            Console.WriteLine("Quel premiere matrice vouler vous additionner?");
            matrice1 = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Quel seconde matrice vouler vous additionner?");
            matrice2 = Int32.Parse(Console.ReadLine());

            indMat1 = matrice1 - 1;
            indMat2 = matrice2 - 1;

            //Validation si elles sont du même format
            if (listeMatrice[indMat1].NbRow != listeMatrice[indMat2].NbRow || listeMatrice[indMat1].NbCol != listeMatrice[indMat2].NbCol)
            {
                Console.Clear();
                Console.WriteLine("Les 2 matrices ne sont pas de même format");
            }
            else
            {

                Matrice resultat = new Matrice(listeMatrice[indMat1].NbRow, listeMatrice[indMat1].NbCol);
                resultat = listeMatrice[indMat1].Additionner(listeMatrice[indMat2]);

                Console.Clear();
                Console.WriteLine("Voici le résultat de l'addition de la matrice #{0} et #{1} : ", matrice1, matrice2);
                resultat.DisplayMatrice();
            }

        }

        //Produit scalaire d'une matrice
        protected static void ProduitScalaire()
        {
            int matrice1, indMat1;
            double scalaire;

            //Saisit de la matrice a multiplier
            Console.WriteLine("");
            Console.WriteLine("Quel matrice vouler vous multiplier par un scalaire?");
            matrice1 = Int32.Parse(Console.ReadLine());
            indMat1 = matrice1 - 1;

            //Saisit du scalaire
            Console.WriteLine("");
            Console.WriteLine("Veuiller saisir le scalaire par lequel la matrice sera multiplié?");
            scalaire = Double.Parse(Console.ReadLine());

            Matrice resultat = new Matrice(listeMatrice[indMat1].NbRow, listeMatrice[indMat1].NbCol);
            resultat = listeMatrice[indMat1].FaireProduitScalaire(scalaire);

            Console.Clear();
            Console.WriteLine("Voici le résultat de la multiplication de la matrice #{0} et du scalaire {1} : ", matrice1, scalaire);
            resultat.DisplayMatrice();


        }
        #endregion
    }
}
