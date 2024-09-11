using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms_Projet_Image
{
    class Huffman
    {
        Noeud root;
        SortedList<Pixel, int> dictFreq;
        List<Noeud> prio;
        SortedList<string, Pixel> codeTable;
        public SortedList<string, Pixel> CodeTable { get { return codeTable; } }

        int Min()
        {
            int ret = 0;
            for (int i = 0; i < prio.Count(); i++)
            {
                if (prio[i].Freq < prio[ret].Freq)
                {
                    ret = i;
                }
            }
            return ret;
        }
        public Huffman(Image123 im)
        {
            dictFreq = new SortedList<Pixel, int>();
            dictFreq.Add(im.IMG[0, 0], 1);



            prio = new List<Noeud>();
            for (int i = 0; i < im.IMG.GetLength(0); i++)
            {
                for (int j = 1; j < im.IMG.GetLength(1); j++)
                {

                    if (dictFreq.ContainsKey(im.IMG[i, j]))
                    {
                        dictFreq[im.IMG[i, j]] += 1;
                    }
                    else
                    {
                        dictFreq.Add(im.IMG[i, j], 1);
                    }

                }
            }

            for (int i = 0; i < dictFreq.Count; i++)
            {
                prio.Add(new Noeud(dictFreq.Keys[i], dictFreq[dictFreq.Keys[i]]));
            }

            Noeud gauche;
            Noeud droite;

            while (prio.Count > 1)
            {

                gauche = prio[Min()];
                prio.RemoveAt(Min());
                droite = prio[Min()];
                prio.RemoveAt(Min());

                root = new Noeud(null, gauche.Freq + droite.Freq, gauche, droite);
                prio.Add(root);
            }
            root = prio[0];

            codeTable = new SortedList<string, Pixel>();
            Codage(root);

        }

        SortedList<string, Pixel> Codage(Noeud noeud, string code = "")
        {

            if (noeud.Fils[0] == null && noeud.Fils[1] == null)
            {
                codeTable.Add(code, noeud.Pix);
            }
            else
            {
                Codage(noeud.Fils[0], code + "0");
                Codage(noeud.Fils[1], code + "1");
            }



            return null;

        }

        public string codeToString(SortedList<string, Pixel> li)
        {
            string ret = "";

            for (int i = 0; i < li.Keys.Count; i++)
            {
                ret += Convert.ToString(li.Keys[i]) + " : " + Convert.ToString(li[li.Keys[i]].toString()) + "\n";
            }
            return ret;
        }

        public string CodePix(Pixel pix)
        {
            return codeTable.Keys[codeTable.IndexOfValue(pix)];
        }

        public Pixel DecodePix(string s)
        {
            return codeTable[s];
        }
    }
}
