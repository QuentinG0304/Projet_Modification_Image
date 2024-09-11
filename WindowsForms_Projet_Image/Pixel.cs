using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms_Projet_Image
{
    class Pixel
    {
        byte R;
        public byte r { get { return R; } set { R = value; } }

        byte G;
        public byte g { get { return G; } set { G = value; } }


        byte B;
        public byte b { get { return B; } set { B = value; } }


        public Pixel(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public void Grey()
        {
            R = Convert.ToByte((R + G + B) / 3);
            G = R;
            B = R;
        }

        public string toString()
        {
            return $"[{R} {B} {G}]";
        }
    }
}
