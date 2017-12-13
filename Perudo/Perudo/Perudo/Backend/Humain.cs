﻿using Perudo.Backend;
using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp1;

namespace Perudo.Backend
{
    public class Humain : Joueur
    {        
        public Humain(string pseudo, int id, Randomizer randomizer)
            : base(id, pseudo, randomizer)
        {
            typeJ = TypeJoueur.humain;
        }
        
        public override void Resultat(int idJoueur, bool perdu)
        {
            if (id == idJoueur)
            {
                if (perdu == true)
                {
                    nbDes--;
                    if (nbDes == 0)
                    {
                        alive = false;
                    }
                } else if (nbDes < 5)
                {
                    nbDes++;
                }
            }
        }

        public override Decision Jouer(List<Des> TotalDes)
        {
            throw new NotImplementedException();
        }
    }
}
