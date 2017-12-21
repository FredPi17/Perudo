﻿using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp1;

namespace Perudo.Backend
{
    class IA : Joueur
    {
        ///Propriétés
        private Niveau myNiveau;
        private int nbTotalDes;
        private List<Tuple<int, int>> listJoueurDes;


        /// <summary>
        /// Constructeur de l'IA
        /// </summary>
        /// <param name="choix">Le choix du niveau de l'IA</param>
        /// <param name="id">L'identifant ou l'index de l'IA</param>
        /// <param name="pseudo">Le pseudo de l'IA</param>
        /// <param name="randomizer">Permet de calculer les dès</param>
        /// <param name="nbJoueur">le nombre total de Joueur (humain & IA)</param>
        public IA(Niveau choix, int id, string pseudo, Randomizer randomizer, int nbJoueur)
            : base(id, pseudo, randomizer)
        {
            myNiveau = choix;
            typeJ = TypeJoueur.ordinateur;
            listJoueurDes = new List<Tuple<int, int>>();
            for (int i = 0; i < nbJoueur; i++)
            {
                listJoueurDes.Add(new Tuple<int, int>(i, 5));
            }
        }

        /// <summary>
        /// Le cerveau de l'IA
        /// </summary>
        /// <param name="listDes">La liste de tous les dès sur la table</param>
        /// <returns>La decision de l'IA</returns>
        public override Decision Jouer(List<Des> listDes)
        {
            Decision dec;
            Decision olddec = Manche.AncienneEnchere;
            double p1 = 0.5;
            double p2 = 0.6;
            int Z = 1;

            nbTotalDes = NbTotalDes();

            int Y = CalculY(olddec);
            int X = CalculX(olddec);

            switch (myNiveau)
            {
                case Niveau.Facile:
                    {
                        Random rng = new Random();
                        int choix = rng.Next(0, 6);
                        if (choix == 0)
                        {
                            dec = new Decision(Backend.Action.bluff);
                            return dec;
                        }
                        else if (choix == 1)
                        {
                            if (olddec.de + 1 > 6)
                            {
                                dec = new Decision(Backend.Action.encherir, olddec.de, olddec.nb + 1);
                                return dec;
                            }
                            else
                            {
                                dec = new Decision(Backend.Action.encherir, olddec.de + 1, olddec.nb);
                                return dec;
                            }
                        }
                        else if (choix == 2)
                        {
                            dec = new Decision(Backend.Action.encherir, olddec.de, olddec.nb + 1);
                            return dec;
                        }
                        else if (choix == 3)
                        {
                            double res = olddec.nb / 2;
                            double rnd = Math.Round(res);
                            int paco = Convert.ToInt32(rnd);
                            dec = new Decision(Backend.Action.encherir, 1, paco);
                            return dec;
                        }
                        else if (choix == 4)
                        {
                            if (olddec.de + 1 > 6)
                            {
                                dec = new Decision(Backend.Action.encherir, olddec.de, olddec.nb + 1);
                                return dec;
                            }
                            else
                            {
                                dec = new Decision(Backend.Action.encherir, olddec.de + 1, olddec.nb + 1);
                                return dec;
                            }
                        }
                        else if (choix == 5)
                        {
                            dec = new Decision(Backend.Action.calza);
                            return dec;
                        }
                    }
                    break;


                case Niveau.Moyen:
                    {
                        double T = X * p1 + Y * p2;
                        if (T < Z - 0.10)
                        {
                            dec = new Decision(Backend.Action.bluff);
                        }
                        else if (T > Z + 0.10)
                        {
                            if (Y >= 0 && Y < 5)
                            {
                                dec = new Decision(Backend.Action.encherir, olddec.de, olddec.nb + 1);

                            }
                            else if (olddec.de + 1 <= 6)
                            {
                                dec = new Decision(Backend.Action.encherir, olddec.de + 1, olddec.nb);
                            }
                            else
                            {
                                double res = olddec.nb / 2;
                                double rnd = Math.Round(res);
                                int paco = Convert.ToInt32(rnd);
                                dec = new Decision(Backend.Action.encherir, 1, paco);
                            }
                        }
                        else
                        {
                            dec = new Decision(Backend.Action.calza);
                        }
                    }
                    return dec;

                case Niveau.Difficile:
                    {
                        int paco = 0;
                        int deux = 0;
                        int trois = 0;
                        int quatre = 0;
                        int cinq = 0;
                        int six = 0;
                        Random rng = new Random();
                        int choix = rng.Next(1, 11);

                        for (int i = 0; i < listDes.Count - 1; i++)
                        {
                            if (listDes[i].valeur == "1")
                            {
                                paco++;
                            }
                            else if (listDes[i].valeur == "2")
                            {
                                deux++;
                            }
                            else if (listDes[i].valeur == "3")
                            {
                                trois++;
                            }
                            else if (listDes[i].valeur == "4")
                            {
                                quatre++;
                            }
                            else if (listDes[i].valeur == "5")
                            {
                                cinq++;
                            }
                            else if (listDes[i].valeur == "6")
                            {
                                six++;
                            }

                        }

                        if ((olddec.de == 1 && olddec.nb == paco) ||
                            (olddec.de == 2 && olddec.nb == deux) ||
                            (olddec.de == 3 && olddec.nb == trois) ||
                            (olddec.de == 4 && olddec.nb == quatre) ||
                            (olddec.de == 5 && olddec.nb == cinq) ||
                            (olddec.de == 6 && olddec.nb == six))
                        {
                            dec = new Decision(Backend.Action.calza);
                        }
                        else if ((olddec.de == 1 && olddec.nb > paco) ||
                            (olddec.de == 2 && olddec.nb > deux) ||
                            (olddec.de == 3 && olddec.nb > trois) ||
                            (olddec.de == 4 && olddec.nb > quatre) ||
                            (olddec.de == 5 && olddec.nb > cinq) ||
                            (olddec.de == 6 && olddec.nb > six))
                        {
                            dec = new Decision(Backend.Action.bluff);
                        }
                        else
                        {
                            dec = new Decision(Backend.Action.encherir, olddec.de, olddec.nb + 1);
                        }

                        if (choix > 6)
                        {
                            if (choix == 7)
                            {
                                dec = new Decision(Backend.Action.bluff);
                            }
                            else if (choix == 8)
                            {
                                if (olddec.de + 1 > 6)
                                {
                                    dec = new Decision(Backend.Action.encherir, olddec.de, olddec.nb + 1);
                                }
                                else
                                {
                                    dec = new Decision(Backend.Action.encherir, olddec.de + 1, olddec.nb);
                                }
                            }
                            else if (choix == 9)
                            {
                                dec = new Decision(Backend.Action.encherir, olddec.de, olddec.nb + 1);
                            }
                            else if (choix == 10)
                            {
                                dec = new Decision(Backend.Action.calza);
                            }
                        }

                    }
                    return dec;
            }
            return null;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="olddec">l'enchère en cours</param>
        /// <returns></returns>
        int CalculX(Decision olddec)
        {
            int x = olddec.nb / nbTotalDes;
            return x;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="olddec">l'enchère en cours</param>
        /// <returns></returns>
        int CalculY(Decision olddec)
        {
            int y = (olddec.nb - Combien(olddec.de)) / (nbTotalDes - nbDes);
            return y;
        }

        /// <summary>
        /// Permet de savoir quel joueur perd/gagne un dè
        /// </summary>
        /// <param name="idJoueur">l'identifiant du Joueur qui a une modification de dès</param>
        /// <param name="perdu">permet de savoir si le joueur a perdu ou gagné un dès</param>
        public override void Resultat(int idJoueur, bool perdu)
        {
            if (id == idJoueur)
            {
                if (perdu)
                {
                    nbDes--;
                    if (nbDes == 0)
                    {
                        alive = false;
                    }
                }
                else if (nbDes < 5)
                {
                    nbDes++;
                }
            }
            else
            {
                int i = 0;
                bool foundJoueur = false;
                while (i < listJoueurDes.Count && foundJoueur == false)
                {
                    if (listJoueurDes[i].Item1 == idJoueur)
                    {
                        foundJoueur = true;
                    }
                    else
                    {
                        i++;
                    }
                }
                int nbDes = listJoueurDes[i].Item2;
                if (perdu)
                {
                    nbDes--;
                }
                else if (nbDes < 5)
                {
                    nbDes++;
                }
                listJoueurDes[i] = Tuple.Create(i, nbDes);
                nbTotalDes = NbTotalDes();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>le nombre total de dès sur la table</returns>
        private int NbTotalDes()
        {
            int tot = 0;
            for (int j = 0; j < listJoueurDes.Count; j++)
            {
                tot += listJoueurDes[j].Item2;
            }
            return tot;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chiffre">le numéro du dé</param>
        /// <returns>Combien on a de dé "chiffre" dans notre main</returns>
        private int Combien(int chiffre)
        {
            int tot = 0;
            for (int i = 0; i < mesDes.Count; i++)
            {
                if (mesDes[i].valeur == chiffre.ToString())
                {
                    tot++;
                }
            }
            return tot;
        }
    }
}