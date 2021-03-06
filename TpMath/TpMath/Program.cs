﻿using System;
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
        static Systeme[] listeSysteme = new Systeme[50];
        static int indexMatrice = 5; //a cause des matrices deja créé
        static int indexSysteme = 3; //a cause des matrices deja créé


        static void Main(string[] args)
        {
            //On initialise des matrices de départs de différent format de manière de ne pas avoir a les créer a toute les fois pour les tests
            Matrice mat1 = new Matrice(1);
            Matrice mat2 = new Matrice(2);
            Matrice mat3 = new Matrice(3);
            Matrice mat4 = new Matrice(4);
            Matrice mat5 = new Matrice(5);

            //On initialise des systeme dequation de départ
            Systeme sys1 = new Systeme(1);
            Systeme sys2 = new Systeme(2);
            Systeme sys3 = new Systeme(3);

            listeMatrice[0] = mat1;
            listeMatrice[1] = mat2;
            listeMatrice[2] = mat3;
            listeMatrice[3] = mat4;
            listeMatrice[4] = mat5;

            listeSysteme[0] = sys1;
            listeSysteme[1] = sys2;
            listeSysteme[2] = sys3;


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
            Console.WriteLine("4 -Regarder les propriétés d'une Matrice");
            Console.WriteLine("5 -Faire des opérations de système d'équation");
            Console.WriteLine("6- Quitter le programme");
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
                    ProprieteMatrice();
                    break;
                case 5:
                    MenuSysteme();
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
                case 7: //tempo
                    Console.Write("Matrice 1");
                    if (listeMatrice[0].EstStrictementDominante)
                    {
                        Console.WriteLine("Est strictement dominante");
                    }
                    else
                    {
                        Console.WriteLine("Est pas dominante");
                    }

                    Console.Write("Matrice 2");
                    if (listeMatrice[1].EstStrictementDominante)
                    {
                        Console.WriteLine("Est strictement dominante");
                    }
                    else
                    {
                        Console.WriteLine("Est pas dominante");
                    }

                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Vous avez fait un choix invalide.");
                    break;

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
            Console.WriteLine("1- Methode rapide #1");
            Console.WriteLine("2- Methode rapide #2");
            Console.WriteLine("3- Saisie manuelle");
            methodeRempl = Int32.Parse(Console.ReadLine());

            switch (methodeRempl)
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
            Console.WriteLine("La matrice #{0} a été créée", indexMatrice);
        }

        //Affichage d'une matrice
        protected static void AfficherUneMatrice()
        {
            int noMatrice;
            //Demande de quel matrice que l'on veut afficher
            Console.WriteLine("");
            Console.WriteLine("Quel matrice vouler vous afficher?");
            noMatrice = Int32.Parse(Console.ReadLine());

            if (noMatrice > indexMatrice || noMatrice <= 0)
            {
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
            Console.WriteLine("2- Faire le produit scalaire d'une matrice");
            Console.WriteLine("3- Faire le produit matriciel de plusieurs matrices");
            Console.WriteLine("4- Verifier si une matrice est triangulaire");
            operation = Int32.Parse(Console.ReadLine());

            switch (operation)
            {
                case 1:
                    AdditionMatrice();
                    break;
                case 2:
                    ProduitScalaire();
                    break;
                case 3:
                    ProduitMatriciel();
                    break;
                case 4:
                    EstTriangulaire();
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

            if (matrice1 > indexMatrice || matrice1 <= 0)
            {
                Console.Clear();
                Console.WriteLine("Erreur, cette matrice n'existe pas");
            }
            else
            {
                Console.WriteLine("Quel seconde matrice vouler vous additionner?");
                matrice2 = Int32.Parse(Console.ReadLine());

                if (matrice2 > indexMatrice || matrice2 <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("Erreur, cette matrice n'existe pas");
                }
                else
                {
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

            if (matrice1 > indexMatrice || matrice1 <= 0)
            {
                Console.Clear();
                Console.WriteLine("Erreur, cette matrice n'existe pas");
            }
            else
            {
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



        }

        //Produit matriciel de plusieurs matrices
        protected static void ProduitMatriciel()
        {
            int matrice1, matrice2, indMat1, indMat2, nbMatriceVoulu;
            int nbMatrice = 0;
            Matrice[] matriceToMultiply = new Matrice[50];
            int indMatriceMult = 0;
            bool bContinu = true;
            int nbOperation = 0;

            //Saisit des matrice a additionner
            Console.WriteLine("");
            Console.WriteLine("Quel premiere matrice vouler vous multiplier?");
            matrice1 = Int32.Parse(Console.ReadLine());

            if (matrice1 > indexMatrice || matrice1 <= 0)
            {
                Console.Clear();
                Console.WriteLine("Erreur, cette matrice n'existe pas");
            }
            else
            {
                indMat1 = matrice1 - 1;
                //On demande le nombre de matrice qui seront multiplier
                Console.WriteLine("");
                Console.WriteLine("Combien de matrice voulez vous multiplier à la suite de la première matrice?");
                nbMatriceVoulu = Int32.Parse(Console.ReadLine());

                if (nbMatriceVoulu <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("Erreur, vous devez multiplier au moins une matrice");
                }
                else
                {
                    while (nbMatrice < nbMatriceVoulu)
                    {
                        Console.WriteLine("Quelle est la prochaine matrice à multiplier?");
                        matrice2 = Int32.Parse(Console.ReadLine());

                        if (matrice2 > indexMatrice || matrice2 <= 0)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Erreur, cette matrice n'existe pas");
                            Console.WriteLine("");
                        }
                        else
                        {
                            indMat2 = matrice2 - 1;


                            //Validation si la multiplication est valide
                            // Le nb de colonne de la premiere matrice doit etre egal au nb de ligne de la seconde matrice
                            if (indMatriceMult == 0)
                            {
                                if (listeMatrice[indMat2].NbRow != listeMatrice[indMat1].NbCol)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Erreur, format invalide");
                                    Console.WriteLine("Pour que le produit matriciel soit possible, le nombre de colonnes de la première matrice doit être égal au nombre de lignes de la seconde matrice");
                                    bContinu = false;
                                }
                                else
                                {
                                    bContinu = true;
                                }
                            }
                            else
                            {
                                int previousInd = indMatriceMult - 1;

                                //Validation si la multiplication est valide
                                // Le nb de colonne de la premiere matrice doit etre egal au nb de ligne de la seconde matrice
                                if (matriceToMultiply[previousInd].NbCol != listeMatrice[indMat2].NbRow)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Erreur, format invalide");
                                    Console.WriteLine("Pour que le produit matriciel soit possible, le nombre de la colonne de résultat de la précedente multiplication doit être égal au nombre de lignes de la nouvelle matrice");
                                    bContinu = false;
                                }
                                else
                                {
                                    bContinu = true;
                                }
                            }

                            //Si c'est le bon format de matrice pour la multiplication, on passe à l'autre étape
                            if (bContinu)
                            {
                                matriceToMultiply[indMatriceMult] = listeMatrice[indMat2];
                                nbMatrice++;
                                indMatriceMult++;
                            }


                        }
                    }
                    //La matrice produit a le même nombre de ligne que la 1ere Matrice et le même nombre de colonne que la 2e matrice
                    Matrice resultat;
                    if (nbMatriceVoulu == 1)
                    {
                        int lastInd = indMatriceMult - 1;
                        resultat = new Matrice(matriceToMultiply[lastInd].NbRow, listeMatrice[indMat1].NbCol);
                        resultat = listeMatrice[indMat1].FaireProduitMatriciel(matriceToMultiply, nbMatrice, out nbOperation);
                    }
                    else
                    {

                        int lastInd = indMatriceMult - 1;
                        int secondLastInd = lastInd - 1;
                        resultat = new Matrice(matriceToMultiply[secondLastInd].NbRow, matriceToMultiply[lastInd].NbCol);
                        resultat = listeMatrice[indMat1].FaireProduitMatriciel(matriceToMultiply, nbMatrice, out nbOperation);
                    }


                    Console.Clear();
                    //Console.WriteLine("Voici le résultat de la multiplication matricielle de la matrice #{0} et #{1} : ", matrice1, matrice2);
                    Console.WriteLine("Voici le résultat de la multiplication matricielle demandé.");
                    resultat.DisplayMatrice();
                    Console.WriteLine("");
                    Console.WriteLine("Le nombre d'opération de produit est le suivant : {0}", nbOperation);

                }
            }
        }

        protected static void EstTriangulaire()
        {
            int matrice1, indMat1, typeTriang, isStrict;
            bool bTypeTriang = false;
            bool bIsStrict = false;
            bool bResult;

            //Saisit de la matrice à valider
            Console.WriteLine("");
            Console.WriteLine("Quelle matrice vouler vous valider?");
            matrice1 = Int32.Parse(Console.ReadLine());

            if (matrice1 > indexMatrice || matrice1 <= 0)
            {
                Console.Clear();
                Console.WriteLine("Erreur, cette matrice n'existe pas");
            }
            else
            {
                indMat1 = matrice1 - 1;
                typeTriang = isStrict = 0;

                //Valider si la matrice est carré
                if (!listeMatrice[indMat1].EstCarre)
                {
                    Console.Clear();
                    Console.WriteLine("Vous avez entrer une matrice qui n'est pas carré");
                }
                else
                {
                    //Saisit des paramètres
                    while (!bTypeTriang)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Quel type de matrice triangulaire voulez-vous tester?");
                        Console.WriteLine("1- Matrice triangulaire supérieure");
                        Console.WriteLine("2- Matrice triangulaire inférieure");
                        Console.WriteLine("3- Matrice triangulaire supérieure ou inférieure");
                        typeTriang = Int32.Parse(Console.ReadLine());

                        if (typeTriang >= 1 && typeTriang <= 3)
                        {
                            bTypeTriang = true;
                        }
                        else
                        {
                            Console.WriteLine("Choix invalide");
                        }
                    }

                    while (!bIsStrict)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Souhaiter-vous valider si la matrice est triangulaire strict ou pas?");
                        Console.WriteLine("1- Matrice triangulaire strict");
                        Console.WriteLine("2- Matrice triangulaire non strict");
                        isStrict = Int32.Parse(Console.ReadLine());

                        if (isStrict >= 1 && isStrict <= 3)
                        {
                            bIsStrict = true;
                        }
                        else
                        {
                            Console.WriteLine("Choix invalide");
                        }
                    }

                    bResult = listeMatrice[indMat1].EstTriangulaire(typeTriang, isStrict);
                    Console.Clear();
                    if (bResult)
                    {
                        Console.WriteLine("La matrice est triangulaire selon les paramètres demandés.");
                    }
                    else
                    {
                        Console.WriteLine("La matrice n'est pas triangulaire selon les paramètres demandés.");
                    }
                }
            }



        }

        #endregion

        protected static void ProprieteMatrice()
        {
            int operation;

            Console.WriteLine("");
            Console.WriteLine("Quelle propriété voulez-vous regarder?");
            Console.WriteLine("1- Valider si la matrice est carré");
            Console.WriteLine("2- Valider si la matrice est réguliere");
            Console.WriteLine("3- Retourner la trace d'une matrice");
            Console.WriteLine("4- Retourner le déterminant d'une matrice");
            Console.WriteLine("5- Retourner la matrice inverse d'une matrice");
            Console.WriteLine("6- Retourner la matrice transposé d'une matrice");
            Console.WriteLine("7- Retourner la comatrice d'une matrice");
            operation = Int32.Parse(Console.ReadLine());

            switch (operation)
            {
                case 1:
                    EstCarre();
                    break;
                case 2:
                    EstReguliere();
                    break;
                case 3:
                    Trace();
                    break;
                case 4:
                    Determinant();
                    break;
                case 5:
                    MatriceInverse();
                    break;
                case 6:
                    Transpose();
                    break;
                case 7:
                    Comatrice();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Vous avez fait un choix invalide.");
                    break;
            }
        }

        #region proprietematrice

        //Valide si une matrice est carré ou non
        protected static void EstCarre()
        {
            int matrice1, indMat1;

            Console.WriteLine("");
            Console.WriteLine("Quelle matrice vouler vous valider?");
            matrice1 = Int32.Parse(Console.ReadLine());

            if (matrice1 > indexMatrice || matrice1 <= 0)
            {
                Console.Clear();
                Console.WriteLine("Erreur, cette matrice n'existe pas");
            }
            else
            {
                indMat1 = matrice1 - 1;

                if (listeMatrice[indMat1].EstCarre)
                {
                    Console.Clear();
                    Console.WriteLine("La matrice #{0} est carré", matrice1);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("La matrice #{0} n'est pas carré", matrice1);
                }
            }
        }

        protected static void EstReguliere()
        {
            int matrice1, indMat1;

            Console.WriteLine("");
            Console.WriteLine("Quelle matrice vouler vous valider?");
            matrice1 = Int32.Parse(Console.ReadLine());

            if (matrice1 > indexMatrice || matrice1 <= 0)
            {
                Console.Clear();
                Console.WriteLine("Erreur, cette matrice n'existe pas");
            }
            else
            {
                indMat1 = matrice1 - 1;

                if (listeMatrice[indMat1].EstReguliere)
                {
                    Console.Clear();
                    Console.WriteLine("La matrice #{0} est réguliere", matrice1);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("La matrice #{0} n'est pas réguliere (elle est singulière)", matrice1);
                }
            }
        }

        //Retourne la trace (somme de la diagonale, d'une matrice si elle est carré
        protected static void Trace()
        {
            int matrice1, indMat1;

            Console.WriteLine("");
            Console.WriteLine("Quelle matrice souhaiter vous voir la trace?");
            matrice1 = Int32.Parse(Console.ReadLine());

            if (matrice1 > indexMatrice || matrice1 <= 0)
            {
                Console.Clear();
                Console.WriteLine("Erreur, cette matrice n'existe pas");
            }
            else
            {
                indMat1 = matrice1 - 1;

                if (listeMatrice[indMat1].EstCarre)
                {
                    Console.Clear();
                    Console.WriteLine("La trace de la matrice #{0} est {1}", matrice1, listeMatrice[indMat1].Trace.ToString());
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Erreur, la matrice #{0} doit être carré pour pouvoir calculer sa trace", matrice1);
                }
            }
        }

        //Retourne le déterminant d'une matrice, si elle est carré
        protected static void Determinant()
        {
            int matrice1, indMat1;

            Console.WriteLine("");
            Console.WriteLine("Quelle matrice souhaiter vous retourner le déterminant?");
            matrice1 = Int32.Parse(Console.ReadLine());

            if (matrice1 > indexMatrice || matrice1 <= 0)
            {
                Console.Clear();
                Console.WriteLine("Erreur, cette matrice n'existe pas");
            }
            else
            {
                indMat1 = matrice1 - 1;

                if (listeMatrice[indMat1].EstCarre)
                {
                    Console.Clear();
                    Console.WriteLine("Le determinant de la matrice #{0} est {1}", matrice1, listeMatrice[indMat1].Determinant);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Erreur, la matrice #{0} doit être carré pour pouvoir calculer son déterminant", matrice1);
                }
            }
        }

        //Retourne la matrice inverse d'une matrice, si elle est carré et régulière
        protected static void MatriceInverse()
        {
            int matrice1, indMat1;
            Matrice matriceInverse;

            Console.WriteLine("");
            Console.WriteLine("La matrice inverse de quelle matrice voulez-vous retourner?");
            matrice1 = Int32.Parse(Console.ReadLine());

            if (matrice1 > indexMatrice || matrice1 <= 0)
            {
                Console.Clear();
                Console.WriteLine("Erreur, cette matrice n'existe pas");
            }
            else
            {
                indMat1 = matrice1 - 1;
                matriceInverse = new Matrice(listeMatrice[indMat1].NbRow, listeMatrice[indMat1].NbCol);

                if (!listeMatrice[indMat1].EstCarre)
                {
                    Console.Clear();
                    Console.WriteLine("Erreur, la matrice #{0} doit être carré pour pouvoir calculer sa matrice inverse", matrice1);
                }
                else
                {
                    if (!listeMatrice[indMat1].EstReguliere)
                    {
                        Console.Clear();
                        Console.WriteLine("Erreur, la matrice #{0} doit être réguliere pour pouvoir calculer sa matrice inverse", matrice1);
                    }
                    else
                    {
                        matriceInverse = listeMatrice[indMat1].MatriceInverse;
                        Console.Clear();
                        Console.WriteLine("La matrice inverse de la matrice #{0} est la suivante: ", matrice1);
                        matriceInverse.DisplayMatrice();
                    }
                }
            }
        }
        //Retourne la comatrice d'une matrice
        protected static void Comatrice()
        {
            int matrice1, indMat1;
            Matrice comatrice;

            Console.WriteLine("");
            Console.WriteLine("Pour quelle matrice souhaiter vous retourner la comatrice?");
            matrice1 = Int32.Parse(Console.ReadLine());

            if (matrice1 > indexMatrice || matrice1 <= 0)
            {
                Console.Clear();
                Console.WriteLine("Erreur, cette matrice n'existe pas");
            }
            else
            {
                indMat1 = matrice1 - 1;

                if (!listeMatrice[indMat1].EstCarre)
                {
                    Console.Clear();
                    Console.WriteLine("Erreur, la matrice #{0} doit être carré pour pouvoir calculer sa comatrice", matrice1);
                }
                else
                {
                    comatrice = new Matrice(listeMatrice[indMat1].NbRow, listeMatrice[indMat1].NbCol);
                    comatrice = listeMatrice[indMat1].CoMatrice;
                    Console.Clear();
                    Console.WriteLine("La comatrice de la matrice #{0} est la suivante: ", matrice1);
                    comatrice.DisplayMatrice();
                }
            }
        }

        //Retourne la transposé d'une matrice
        protected static void Transpose()
        {
            int matrice1, indMat1;
            Matrice matriceTransp;


            Console.WriteLine("");
            Console.WriteLine("Pour quelle matrice souhaiter vous retourner la transposée?");
            matrice1 = Int32.Parse(Console.ReadLine());

            if (matrice1 > indexMatrice || matrice1 <= 0)
            {
                Console.Clear();
                Console.WriteLine("Erreur, cette matrice n'existe pas");
            }
            else
            {
                indMat1 = matrice1 - 1;
                matriceTransp = new Matrice(listeMatrice[indMat1].NbCol, listeMatrice[indMat1].NbRow);
                matriceTransp = listeMatrice[indMat1].Transposee;
                Console.Clear();
                Console.WriteLine("La transposée de la matrice #{0} est la suivante: ", matrice1);
                matriceTransp.DisplayMatrice();
            }
        }

        #endregion

        //Toute les opérations de système d'équation se font ici
        protected static void MenuSysteme()
        {
            int operation;

            Console.WriteLine("");
            Console.WriteLine("Que voulez-vous faire?");
            Console.WriteLine("1- Saisir un nouveau système");
            Console.WriteLine("2- Afficher un système");
            Console.WriteLine("3- Trouver X par Cramer");
            Console.WriteLine("4- Trouver X par Inversion");
            Console.WriteLine("5- Trouver X par Jacobi");
            operation = Int32.Parse(Console.ReadLine());

            switch (operation)
            {
                case 1:
                    AjouterSysteme();
                    break;
                case 2:
                    AfficherSysteme();
                    break;
                case 3:
                    Cramer();
                    break;
                case 4:
                    InversionMatricielle();
                    break;
                case 5:
                    Jacobi();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Vous avez fait un choix invalide.");
                    break;
            }
        }

        #region Systeme D'équation
        //Ajout d'un système
        protected static void AjouterSysteme()
        {
            Systeme newSysteme;
            int n;

            //Saisit des paramètre de la matrice
            Console.WriteLine("");
            Console.WriteLine("Veuillez entrer le N du système");
            n = Int32.Parse(Console.ReadLine());

            Matrice newMatriceA = new Matrice(n, n);
            Matrice newMatriceB = new Matrice(n, 1);

            //On remplit les matrices du nouveau système
            newMatriceA.RemplirMatrice(0);
            newMatriceB.RemplirMatrice(1);

            newSysteme = new Systeme(newMatriceA, newMatriceB, n);

            //Ajout du systeme à la liste des Systemes
            listeSysteme[indexSysteme] = newSysteme;
            indexSysteme++;

            Console.Clear();
            Console.WriteLine("Le système #{0} a été créée", indexSysteme);
        }

        //Affichage d'un systeme
        protected static void AfficherSysteme()
        {
            int noSysteme;
            //Demande de quel systeme que l'on veut afficher
            Console.WriteLine("");
            Console.WriteLine("Quel système vouler vous afficher?");
            noSysteme = Int32.Parse(Console.ReadLine());

            if (noSysteme > indexSysteme || noSysteme <= 0)
            {
                Console.Clear();
                Console.WriteLine("Erreur, ce système n'existe pas");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Affichage du système #{0}", noSysteme);
                noSysteme--;
                listeSysteme[noSysteme].DisplaySystème();
            }
        }

        protected static void Cramer()
        {
            int sys1, ind1, newNbCol, newNbRow;
            Matrice matResult;
            Systeme systeme1;
            Console.WriteLine("");
            Console.WriteLine("Saississez le système voulu.");
            sys1 = Int32.Parse(Console.ReadLine());


            if (sys1 > indexSysteme || sys1 <= 0)
            {
                Console.Clear();
                Console.WriteLine("Erreur, ce système n'existe pas");
            }
            else
            {
                ind1 = sys1 - 1;
                systeme1 = listeSysteme[ind1];

                newNbCol = systeme1.MatriceB.NbCol;
                newNbRow = systeme1.MatriceB.NbRow;
                matResult = new Matrice(newNbRow, newNbCol);

                matResult = systeme1.TrouverXParCramer();

                if (matResult == null)
                {
                    Console.Clear();
                    Console.WriteLine("Erreur, le déterminant de la matrice est nul.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Voici le résultat par Cramer.");
                    matResult.DisplayMatrice();
                }
            }
        }

        protected static void InversionMatricielle()
        {
            int sys1, ind1, newNbCol, newNbRow;
            Matrice matResult;
            Systeme systeme1;
            Console.WriteLine("");
            Console.WriteLine("Saississez le système voulu.");
            sys1 = Int32.Parse(Console.ReadLine());

            if (sys1 > indexSysteme || sys1 <= 0)
            {
                Console.Clear();
                Console.WriteLine("Erreur, ce système n'existe pas");
            }
            else
            {
                ind1 = sys1 - 1;
                systeme1 = listeSysteme[ind1];

                newNbCol = systeme1.MatriceB.NbCol;
                newNbRow = systeme1.MatriceB.NbRow;
                matResult = new Matrice(newNbRow, newNbCol);

                matResult = systeme1.TrouverXParInversionMatricielle();

                if (matResult == null)
                {
                    Console.Clear();
                    Console.WriteLine("Erreur, le déterminant de la matrice est nul.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Voici le résultat par Inversion Matricielle.");
                    matResult.DisplayMatrice();
                }
            }
        }


        protected static void Jacobi()
        {
            int sys1, ind1, newNbCol, newNbRow;
            double epsilon;
            Matrice matResult;
            Systeme systeme1;
            Console.WriteLine("");
            Console.WriteLine("Saississez le système voulu.");
            sys1 = Int32.Parse(Console.ReadLine());

            if (sys1 > indexSysteme || sys1 <= 0)
            {
                Console.Clear();
                Console.WriteLine("Erreur, ce système n'existe pas");
            }
            else
            {

                ind1 = sys1 - 1;

                systeme1 = listeSysteme[ind1];

                Console.WriteLine("");
                Console.WriteLine("Saississez l'epsilon voulu.");
                epsilon = Double.Parse(Console.ReadLine());

                newNbCol = systeme1.MatriceB.NbCol;
                newNbRow = systeme1.MatriceB.NbRow;
                matResult = new Matrice(newNbRow, newNbCol);

                matResult = systeme1.TrouverXParJacobi(epsilon);

                if (matResult == null)
                {
                    Console.Clear();
                    Console.WriteLine("Erreur, la matrice n'est pas strictement dominante diagonalement.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Voici le résultat par la méthode de Jacobi.");
                    matResult.DisplayMatrice();
                }
            }
        }
        #endregion

    }
}