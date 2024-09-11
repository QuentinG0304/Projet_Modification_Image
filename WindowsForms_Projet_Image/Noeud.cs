using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms_Projet_Image
{
    class Noeud
    {
        Pixel pix;
        public Pixel Pix { get { return pix; } }
        int freq;
        public int Freq { get { return freq; } }
        Noeud[] fils; //null quand pas de successeur
        public Noeud[] Fils { get { return fils; } }

        public Noeud(Pixel p = null, int f = 0, Noeud f1 = null, Noeud f2 = null)
        {
            pix = p;
            freq = f;
            fils = new Noeud[] { f1, f2 };
        }
    }
}
