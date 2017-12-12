﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Perudo;
using Perudo.Backend;
using System.Diagnostics;

namespace ConsoleApp1
{
    public class Partie
    {
        public List<Joueur> JoueurList;
        public static Partie MainPartie { get; set; }

        public Partie(int nbJoueurs, Randomizer randomizer)
        {
            this.Randomizer = randomizer;

            JoueurList = new List<Joueur>(nbJoueurs);
            this.AddJoueur(nbJoueurs);

            Manche.MainManche = new Manche(JoueurList);

        
            Partie.MainPartie = this;
        }

        public void AddJoueur(int nbJoueurs)
        {
            for (int i = 0; i < nbJoueurs; i++)
            {
                Joueur Joueur = new Humain("joueur" + i, 0, Randomizer);
                JoueurList.Add(Joueur);
            }
        }

        public void AddJoueur(Joueur joueur)
        {
            JoueurList.Add(joueur);
        }

        public Randomizer Randomizer { get; set; }

        public int GetNbDes(string valeur)
        {
            int compteurDes = 0;
            foreach (var joueur in JoueurList)
            {
                foreach (var de in joueur.GetDes())
                {
                    if (de.valeur == valeur)
                    {
                        compteurDes++;
                    }
                }
            }
            return compteurDes;
        }

        /// <summary>
        /// Fonction qui permet de calculer combien il reste de survivant dans ma liste de joueur
        /// <return>Elle renvoit un int de personnes en vie</return>
        /// </summary>
        /// <returns></returns>
        public int CountAlive()
        {
            //Vérifier si il reste un dans JoueurListDansManche
            int compteur = 0;
            foreach (var joueur in JoueurList)
            {
                if (joueur.IsAlive())
                {
                    compteur++;
                }
            }
            return compteur;
        }
        /// <summary>
        /// Fonction qui affiche le joueur ou IA gagnant. Elle renvois rien car elle fait appel à d'autre fonctions comme Sauvegarde etc...
        /// </summary>
        public void FinJeu()
        {
            if (CountAlive() == 1)
            {
                foreach (var joueur in JoueurList)
                {
                    if (joueur.IsAlive())
                    {
                        if (joueur.GetTypeJoueur() == TypeJoueur.humain)
                        {
                            Humain humain = joueur as Humain;
                            string pseudoGagnant = humain.Getpseudo();
                            Debug.WriteLine($"C'est la fin du jeu. C'est {pseudoGagnant} qui a gagné");
                            //Faire appel aux méthode de stockage
                        }
                        else
                        {
                            //TODO pour IA
                            /*  Humain humain = joueur as Humain;
                              string pseudoGagnant = humain.Getpseudo();
                              Console.WriteLine($"C'est la fin du jeu. C'est {pseudoGagnant} qui a gagné");*/
                        }
                    }
                }
            }
        }
    }
}
