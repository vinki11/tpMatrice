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

            //Saisit des paramètre de la matrice
            Console.WriteLine("");
            Console.WriteLine("Veuillez entrer le nombre de ligne de la matrice");
            nbRow = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Veuillez entrer le nombre de colonne de la matrice");
            nbCol = Int32.Parse(Console.ReadLine());

            Matrice newMatrice = new Matrice(nbRow, nbCol);

            //Remplissage des données de la matrice
            newMatrice.RemplirMatrice();

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

        //Opérations de matrice
        protected static void OperationMatrice()
        {
            int operation;

            Console.WriteLine("");
            Console.WriteLine("Quelle opération voulez-vous faire?");
            Console.WriteLine("1- Additionner 2 matrice");
            operation = Int32.Parse(Console.ReadLine());

            switch(operation)
            {
                case 1:
                    AdditionMatrice();
                    break;
                case 2:
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
            Console.WriteLine("Quel premier matrice vouler vous additionner?");
            matrice1 = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Quel seconde matrice vouler vous additionner?");
            matrice2 = Int32.Parse(Console.ReadLine());

            indMat1 = matrice1 - 1;
            indMat2 = matrice2 - 1;

            //Validation si elles sont du même format
            if (listeMatrice[indMat1].nbRow != listeMatrice[indMat2].nbRow || listeMatrice[indMat1].nbCol != listeMatrice[indMat2].nbCol)
            {
                Console.Clear();
                Console.WriteLine("Les 2 matrices ne sont pas de même format");
            }
            else
            {

                Matrice resultat = new Matrice(listeMatrice[indMat1].nbRow, listeMatrice[indMat1].nbCol);
                resultat = listeMatrice[indMat1].Additionner(listeMatrice[indMat2]);

                Console.Clear();
                Console.WriteLine("Voici le résultat de l'addition de la matrice #{0} et #{1} : ", matrice1, matrice2);
                resultat.DisplayMatrice();
            }

        }
        #endregion
    }
}
