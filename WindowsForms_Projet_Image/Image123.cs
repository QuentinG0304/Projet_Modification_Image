using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsForms_Projet_Image
{
    class Image123
    {
        Pixel[,] img;
        public Pixel[,] IMG { get { return img; } }
        int hauteur;
        int largeur;
        long taille;
        public byte[] imBase;
        int headerlength;

        public Image123(byte[] i)
        {
            headerlength = 54;
            largeur = LargeurBtoInt(i);
            hauteur = HauteurBtoInt(i);
            taille = i.Length;
            imBase = i;

            Pixel[] temp = new Pixel[(i.Length - headerlength) / 3];
            for (int j = 0; j < (i.Length - headerlength) / 3; j++)
            {
                temp[j] = new Pixel(i[headerlength + j * 3], i[headerlength + 2 + j * 3], i[headerlength + 1 + j * 3]);
            }

            img = new Pixel[hauteur, largeur];

            for (int j = 0; j < hauteur; j++)
            {

                for (int k = 0; k < largeur; k++)
                {
                    img[j, k] = temp[largeur * j + k];
                }

            }
        }

        public Image123(int h, int l)
        {
            img = new Pixel[h, l];
            hauteur = h;
            largeur = l;
            taille = h * l * 3 + 54;
            headerlength = 54;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < l; j++)
                {
                    img[i, j] = new Pixel(0, 0, 0);
                }

            }
            imBase = new byte[54] { 66, 77, 54, 238, 2, 0, 0, 0, 0, 0, 54, 0, 0, 0, 40, 0, 0, 0, 64, 1, 0, 0, 200, 0, 0, 0, 1, 0, 24, 0, 0, 0, 0, 0, 0, 238, 2, 0, 35, 46, 0, 0, 35, 46, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        public static Image123 AleaImage123(int h, int l)
        {
          Image123 al = new Image123(h, l);
            Random r = new Random();

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < l; j++)
                {
                    al.img[i, j].r = Convert.ToByte(r.Next(0, 255));
                    al.img[i, j].g = Convert.ToByte(r.Next(0, 255));
                    al.img[i, j].b = Convert.ToByte(r.Next(0, 255));
                }
            }
            return al;
        }

        static int LargeurBtoInt(byte[] i)
        {
            return Convert.ToInt32(i[18]) + Convert.ToInt32(i[19]) * Convert.ToInt32(Math.Pow(2, 8)) + Convert.ToInt32(i[20]) * Convert.ToInt32(Math.Pow(2, 16));
        }

        static int HauteurBtoInt(byte[] i)
        {
            return Convert.ToInt32(i[22]) + Convert.ToInt32(i[23]) * Convert.ToInt32(Math.Pow(2, 8)) + Convert.ToInt32(i[24]) * Convert.ToInt32(Math.Pow(2, 16));
        }

        public string toString()
        {
            string Image123 = "";
            for (int i = 0; i < hauteur; i++)
            {
              Image123 += "[";
                for (int j = 0; j < largeur; j++)
                {
                  Image123 += $"{img[i, j].toString()} ";
                }
              Image123 += "]\n";
            }
            return Image123;
        }

        public void Zoom(int a)
        {
            hauteur = hauteur * a;
            largeur = largeur * a;
            taille = Convert.ToInt64(headerlength + hauteur * largeur * Math.Pow(a, 2));
            Pixel[,] zoom = new Pixel[hauteur, largeur];
            for (int i = 0; i < img.GetLength(0); i++)
            {
                for (int j = 0; j < img.GetLength(1); j++)
                {
                    for (int k = 0; k < a; k++)
                    {
                        for (int l = 0; l < a; l++)
                        {
                            zoom[i * a + k, j * a + l] = new Pixel(img[i, j].r, img[i, j].g, img[i, j].b);
                        }
                    }
                }
            }
            this.img = zoom;
        }

        public void Grey()
        {
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    img[i, j].Grey();
                }
            }
        }

        public void Rota(double angle)
        {
            int dis = Convert.ToInt32(Math.Sqrt(img.GetLength(0) * img.GetLength(0) + img.GetLength(1) * img.GetLength(1)));

            while (dis % 4 != 0)
            {
                dis += 1;
            }

          Image123 im = new Image123(dis, dis);
            //im.SaveImg();

            int[] centre = new int[] { img.GetLength(0) / 2, img.GetLength(1) / 2 };
            double angle_dep = 0;
            double distance;


            int[] xy = new int[2];

            //Partie haute de l'Image123----------------------------------------------------------------------
            for (int i = img.GetLength(0) / 2 + 1; i < img.GetLength(0); i++)
            {
                for (int j = 0; j < img.GetLength(1); j++)
                {
                    distance = Math.Sqrt((centre[1] - j) * (centre[1] - j) + (i - centre[0]) * (i - centre[0]));

                    angle_dep = Math.Atan(Convert.ToDouble((centre[1] - j)) / Convert.ToDouble((i - centre[0])));
                    //Console.WriteLine(angle_dep*180/Math.PI);



                    xy[0] = Convert.ToInt32(distance * Math.Cos(angle_dep + angle * Math.PI / 180) + im.img.GetLength(0) / 2);
                    xy[1] = Convert.ToInt32(-(distance * Math.Sin(angle_dep + angle * Math.PI / 180)) + im.img.GetLength(1) / 2);

                    im.img[xy[0], xy[1]].r = img[i, j].r;
                    im.img[xy[0], xy[1]].g = img[i, j].g;
                    im.img[xy[0], xy[1]].b = img[i, j].b;

                }


            }
            //Fin partie haute--------------------------------------------------------------------------------
            //Partie basse de l'Image123 -----------------------------------------------------
            for (int i = 0; i < img.GetLength(0) / 2; i++)
            {
                for (int j = 0; j < img.GetLength(1); j++)
                {
                    distance = -Math.Sqrt((centre[1] - j) * (centre[1] - j) + (i - centre[0]) * (i - centre[0]));

                    if (centre[0] != i)
                    {
                        angle_dep = Math.Atan(Convert.ToDouble((centre[1] - j)) / Convert.ToDouble((i - centre[0])));
                    }
                    else
                    {
                        angle_dep = 0;
                    }



                    xy[0] = Convert.ToInt32(distance * Math.Cos(angle_dep + angle * Math.PI / 180) + im.img.GetLength(0) / 2 + Math.Cos(angle * Math.PI / 180));
                    xy[1] = Convert.ToInt32(-(distance * Math.Sin(angle_dep + angle * Math.PI / 180)) + im.img.GetLength(1) / 2 - Math.Sin(angle * Math.PI / 180));

                    im.img[xy[0], xy[1]].r = img[i, j].r;
                    im.img[xy[0], xy[1]].g = img[i, j].g;
                    im.img[xy[0], xy[1]].b = img[i, j].b;


                }

            }
            //Fin partie basse---------------------------------------------------------------------------------
            //LissageImage123---------------------------------------------------------------------------------------

            for (int i = 0; i < im.img.GetLength(0); i++)
            {
                for (int j = 0; j < im.img.GetLength(1); j++)
                {
                    if (im.img[i, j].r == 0 && im.img[i, j].g == 0 && im.img[i, j].b == 0 && i - 1 > 0 && i + 1 < im.img.GetLength(0) && j - 1 > 0 && j + 1 < im.img.GetLength(1))
                    {
                        im.img[i, j].r = Convert.ToByte((im.img[i - 1, j - 1].r + im.img[i, j - 1].r + im.img[i + 1, j - 1].r + im.img[i - 1, j].r + im.img[i, j].r + im.img[i + 1, j].r + im.img[i - 1, j + 1].r + im.img[i, j + 1].r + im.img[i + 1, j + 1].r) / 9);


                        im.img[i, j].g = Convert.ToByte((im.img[i - 1, j - 1].g + im.img[i, j - 1].g + im.img[i + 1, j - 1].g + im.img[i - 1, j].g + im.img[i, j].g + im.img[i + 1, j].g + im.img[i - 1, j + 1].g + im.img[i, j + 1].g + im.img[i + 1, j + 1].g) / 9);

                        im.img[i, j].b = Convert.ToByte((im.img[i - 1, j - 1].b + im.img[i, j - 1].b + im.img[i + 1, j - 1].b + im.img[i - 1, j].b + im.img[i, j].b + im.img[i + 1, j].b + im.img[i - 1, j + 1].b + im.img[i, j + 1].b + im.img[i + 1, j + 1].b) / 9);
                    }
                }
            }

            //Fin Lissage------------------------------------------------

            img = im.img;
            headerlength = im.headerlength;
            hauteur = im.hauteur;
            largeur = im.largeur;
            imBase = im.imBase;
            taille = im.taille;

        }

        public byte Multiplicationmatrice(int i, int j, double[,] mat2, string a)
        {
            int n = mat2.GetLength(0);
            byte[,] mat = new byte[n, n];
            int b = 0;
            int c = 0;
            for (int m = (i - (n - 1) / 2); m < (i + (n - 1) / 2) + 1; m++)
            {
                c = 0;
                for (int p = (j - (n - 1) / 2); p < (j + (n - 1) / 2) + 1; p++)
                {
                    if (a == "r")
                    {
                        mat[b, c] = img[m, p].r;
                    }
                    else if (a == "g")
                    {
                        mat[b, c] = img[m, p].g;
                    }
                    else
                    {
                        mat[b, c] = img[m, p].b;
                    }
                    c++;
                }
                b++;
            }

            double rep = 0;
            for (int k = 0; k < n; k++)
            {
                for (int l = 0; l < n; l++)
                {
                    //Console.WriteLine(mat[k, l]);
                    //Console.WriteLine(mat2[k, l]);
                    rep += mat[k, l] * mat2[k, l];
                    //Console.WriteLine(rep);
                }
            }
            //Console.WriteLine("\n\n");
            if (rep >= 0 && rep <= 255)
            {
                return Convert.ToByte(rep);
            }
            else if (rep < 0)
            {
                return 0;
            }
            else
            {
                return 255;
            }
        }

        public void DetectionDesContours()
        {
            Pixel[,] imgsortie = new Pixel[hauteur, largeur];
            double[,] mat = new double[3, 3] { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };
            for (int i = 1; i < hauteur - 1; i++)
            {
                for (int j = 1; j < largeur - 1; j++)
                {
                    imgsortie[i, j] = new Pixel(0, 0, 0);
                    imgsortie[i, j].r = Multiplicationmatrice(i, j, mat, "r");
                    imgsortie[i, j].g = Multiplicationmatrice(i, j, mat, "g");
                    imgsortie[i, j].b = Multiplicationmatrice(i, j, mat, "b");
                }
            }
            for (int j = 0; j < largeur; j++)
            {
                imgsortie[0, j] = new Pixel(0, 0, 0);
                imgsortie[hauteur - 1, j] = new Pixel(0, 0, 0);
            }
            for (int j = 0; j < hauteur; j++)
            {
                imgsortie[j, 0] = new Pixel(0, 0, 0);
                imgsortie[j, largeur - 1] = new Pixel(0, 0, 0);
            }
            img = imgsortie;
        }

        public void Flou()
        {
            Pixel[,] imgsortie = new Pixel[hauteur, largeur];
            double[,] mat = new double[3, 3] { { 0.111, 0.111, 0.111 }, { 0.111, 0.111, 0.111 }, { 0.111, 0.111, 0.111 } };
            for (int i = 1; i < hauteur - 1; i++)
            {
                for (int j = 1; j < largeur - 1; j++)
                {
                    imgsortie[i, j] = new Pixel(0, 0, 0);
                    imgsortie[i, j].r = Multiplicationmatrice(i, j, mat, "r");
                    imgsortie[i, j].g = Multiplicationmatrice(i, j, mat, "g");
                    imgsortie[i, j].b = Multiplicationmatrice(i, j, mat, "b");
                }
            }
            for (int j = 0; j < largeur; j++)
            {
                imgsortie[0, j] = new Pixel(0, 0, 0);
                imgsortie[hauteur - 1, j] = new Pixel(0, 0, 0);
            }
            for (int j = 0; j < hauteur; j++)
            {
                imgsortie[j, 0] = new Pixel(0, 0, 0);
                imgsortie[j, largeur - 1] = new Pixel(0, 0, 0);
            }
            img = imgsortie;
        }

        public void Repoussage()
        {
            Pixel[,] imgsortie = new Pixel[hauteur, largeur];
            double[,] mat = new double[3, 3] { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
            for (int i = 1; i < hauteur - 1; i++)
            {
                for (int j = 1; j < largeur - 1; j++)
                {
                    imgsortie[i, j] = new Pixel(0, 0, 0);
                    imgsortie[i, j].r = Multiplicationmatrice(i, j, mat, "r");
                    imgsortie[i, j].g = Multiplicationmatrice(i, j, mat, "g");
                    imgsortie[i, j].b = Multiplicationmatrice(i, j, mat, "b");
                }
            }
            for (int j = 0; j < largeur; j++)
            {
                imgsortie[0, j] = new Pixel(0, 0, 0);
                imgsortie[hauteur - 1, j] = new Pixel(0, 0, 0);
            }
            for (int j = 0; j < hauteur; j++)
            {
                imgsortie[j, 0] = new Pixel(0, 0, 0);
                imgsortie[j, largeur - 1] = new Pixel(0, 0, 0);
            }
            img = imgsortie;
        }

        public void MandelBrot(int max)
        {
            double x1 = -2.13;
            double x2 = 0.64;
            double y1 = -1.25;
            double y2 = 1.26;
            int zoom = 100;


            int Image123x = Convert.ToInt32((x2 - x1) * zoom);
            int Image123y = Convert.ToInt32((y2 - y1) * zoom);

            for (int x = 0; x <Image123x; x++)
            {
                for (int y = 0; y <Image123y; y++)
                {
                    double c_r = x / Convert.ToDouble(zoom) + x1;
                    double c_i = y / Convert.ToDouble(zoom) + y1;
                    double z_r = 0;
                    double z_i = 0;
                    int i = 0;

                    do
                    {
                        double temp = z_r;
                        z_r = z_r * z_r - z_i * z_i + c_r;
                        z_i = 2 * z_i * temp + c_i;
                        i++;
                    } while (z_r * z_r + z_i * z_i < 4 && i < max);

                    if (i == max)
                    {
                        img[x, y] = new Pixel(255, 255, 255);
                    }
                }
            }
        }

        public void Encode(Image123 showed)
        {
            
            string pix_s = "";
            string pix_h = "";
            if (taille >= showed.taille)
            {

               Image123 finale = new Image123(hauteur, largeur);
                for (int i = 0; i < showed.img.GetLength(0); i++)
                {
                    for (int j = 0; j < showed.img.GetLength(1); j++)
                    {
                        pix_s = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_s += ToBinary(showed.img[i, j].r)[k];
                        }
                        pix_h = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_h += ToBinary(img[i, j].r)[k];
                        }
                        finale.img[i, j].r = Convert.ToByte(toDec(pix_s + pix_h));

                        pix_s = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_s += ToBinary(showed.img[i, j].g)[k];
                        }
                        pix_h = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_h += ToBinary(img[i, j].g)[k];
                        }
                        finale.img[i, j].g = Convert.ToByte(toDec(pix_s + pix_h));

                        pix_s = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_s += ToBinary(showed.img[i, j].b)[k];
                        }
                        pix_h = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_h += ToBinary(img[i, j].b)[k];
                        }
                        finale.img[i, j].b = Convert.ToByte(toDec(pix_s + pix_h));
                    }
                    for (int j = showed.img.GetLength(1); j < img.GetLength(1); j++)
                    {
                        pix_s = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_s += ToBinary(finale.img[i, j].r)[k];
                        }
                        pix_h = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_h += ToBinary(img[i, j].r)[k];
                        }
                        finale.img[i, j].r = Convert.ToByte(toDec(pix_s + pix_h));

                        pix_s = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_s += ToBinary(finale.img[i, j].g)[k];
                        }
                        pix_h = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_h += ToBinary(img[i, j].g)[k];
                        }
                        finale.img[i, j].g = Convert.ToByte(toDec(pix_s + pix_h));

                        pix_s = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_s += ToBinary(finale.img[i, j].b)[k];
                        }
                        pix_h = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_h += ToBinary(img[i, j].b)[k];
                        }
                        finale.img[i, j].b = Convert.ToByte(toDec(pix_s + pix_h));
                    }

                }
                for (int i = showed.img.GetLength(0); i < img.GetLength(0); i++)
                {
                    for (int j = 0; j < img.GetLength(1); j++)
                    {
                        pix_s = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_s += ToBinary(finale.img[i, j].r)[k];
                        }
                        pix_h = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_h += ToBinary(img[i, j].r)[k];
                        }
                        finale.img[i, j].r = Convert.ToByte(toDec(pix_s + pix_h));

                        pix_s = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_s += ToBinary(finale.img[i, j].g)[k];
                        }
                        pix_h = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_h += ToBinary(img[i, j].g)[k];
                        }
                        finale.img[i, j].g = Convert.ToByte(toDec(pix_s + pix_h));

                        pix_s = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_s += ToBinary(finale.img[i, j].b)[k];
                        }
                        pix_h = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_h += ToBinary(img[i, j].b)[k];
                        }
                        finale.img[i, j].b = Convert.ToByte(toDec(pix_s + pix_h));
                    }
                }
                img= finale.img;
                hauteur = finale.hauteur;
                largeur = finale.largeur;
                taille = finale.taille;

            }
            else
            {
               Image123 finale = new Image123(showed.hauteur, showed.largeur);
                for (int i = 0; i < img.GetLength(0); i++)
                {
                    for (int j = 0; j < img.GetLength(1); j++)
                    {
                        pix_s = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_s += ToBinary(showed.img[i, j].r)[k];
                        }
                        pix_h = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_h += ToBinary(img[i, j].r)[k];
                        }
                        finale.img[i, j].r = Convert.ToByte(toDec(pix_s + pix_h));

                        pix_s = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_s += ToBinary(showed.img[i, j].g)[k];
                        }
                        pix_h = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_h += ToBinary(img[i, j].g)[k];
                        }
                        finale.img[i, j].g = Convert.ToByte(toDec(pix_s + pix_h));

                        pix_s = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_s += ToBinary(showed.img[i, j].b)[k];
                        }
                        pix_h = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_h += ToBinary(img[i, j].b)[k];
                        }
                        finale.img[i, j].b = Convert.ToByte(toDec(pix_s + pix_h));
                    }
                    for (int j = img.GetLength(1); j < showed.img.GetLength(1); j++)
                    {
                        pix_s = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_s += ToBinary(showed.img[i, j].r)[k];
                        }
                        pix_h = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_h += ToBinary(finale.img[i, j].r)[k];
                        }
                        finale.img[i, j].r = Convert.ToByte(toDec(pix_s + pix_h));

                        pix_s = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_s += ToBinary(showed.img[i, j].g)[k];
                        }
                        pix_h = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_h += ToBinary(finale.img[i, j].g)[k];
                        }
                        finale.img[i, j].g = Convert.ToByte(toDec(pix_s + pix_h));

                        pix_s = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_s += ToBinary(showed.img[i, j].b)[k];
                        }
                        pix_h = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_h += ToBinary(finale.img[i, j].b)[k];
                        }
                        finale.img[i, j].b = Convert.ToByte(toDec(pix_s + pix_h));
                    }
                }
                for (int i = img.GetLength(0); i < showed.img.GetLength(0); i++)
                {
                    for (int j = 0; j < showed.img.GetLength(1); j++)
                    {
                        pix_s = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_s += ToBinary(showed.img[i, j].r)[k];
                        }
                        pix_h = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_h += ToBinary(finale.img[i, j].r)[k];
                        }
                        finale.img[i, j].r = Convert.ToByte(toDec(pix_s + pix_h));

                        pix_s = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_s += ToBinary(showed.img[i, j].g)[k];
                        }
                        pix_h = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_h += ToBinary(finale.img[i, j].g)[k];
                        }
                        finale.img[i, j].g = Convert.ToByte(toDec(pix_s + pix_h));

                        pix_s = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_s += ToBinary(showed.img[i, j].b)[k];
                        }
                        pix_h = "";
                        for (int k = 0; k < 4; k++)
                        {
                            pix_h += ToBinary(finale.img[i, j].b)[k];
                        }
                        finale.img[i, j].b = Convert.ToByte(toDec(pix_s + pix_h));
                    }
                }
                img=finale.img;
                hauteur = finale.hauteur;
                largeur = finale.largeur;
                taille = finale.taille;

            }

        }

        public void Decode()
        {
            for (int i = 0; i < img.GetLength(0); i++)
            {
                for (int j = 0; j < img.GetLength(1); j++)
                {
                    img[i, j].r = Convert.ToByte((toDec(ToBinary(img[i, j].r)) % 16) * 16);
                    img[i, j].g = Convert.ToByte((toDec(ToBinary(img[i, j].g)) % 16) * 16);
                    img[i, j].b = Convert.ToByte((toDec(ToBinary(img[i, j].b)) % 16) * 16);
                }
            }
        }

        static string ToBinary(int n)
        {
            string ret = "";
            string ret_rev = "";

            while (n != 0)
            {
                ret += Convert.ToString(n % 2);
                n /= 2;
            }
            while (ret.Length != 8)
            {
                ret += '0';
            }
            int len = ret.Length;
            for (int i = 0; i < len; i++)
            {
                ret_rev += ret[len - 1 - i];
            }
            return ret_rev;
        }

        static int toDec(string n)
        {
            int ret = 0;
            for (int i = 0; i < n.Length; i++)
            {
                ret += Convert.ToInt32(Convert.ToInt32(n[n.Length - 1 - i] - 48) * Math.Pow(2, i));
            }
            return ret;
        }

        public void Miroir()
        {
            Pixel temp;
            for (int i = 0; i < img.GetLength(0); i++)
            {
                for (int j = 0; j < img.GetLength(1) / 2; j++)
                {
                    temp = new Pixel(img[i, j].r, img[i, j].g, img[i, j].b);
                    img[i, j] = img[i, img.GetLength(1) - j - 1];
                    img[i, img.GetLength(1) - j - 1] = temp;
                }
            }
        }

        public void ModifRouge(int n) //nbr positif --> multiplicateur des pixels rouges --> de 0 à infini pour les pourcentages
        {
            double c = n / 100.0;
            for (int i = 0; i < img.GetLength(0); i++)
            {
                for (int j = 0; j < img.GetLength(1); j++)
                {
                    if (img[i, j].r * c >= 0 && img[i, j].r * c <= 255)
                    {
                        img[i, j].r = Convert.ToByte(img[i, j].r * c);
                    }
                    else if (img[i, j].r * c > 255)
                    {
                        img[i, j].r = 255;
                    }
                    else
                    {
                        img[i, j].r = 0;
                    }
                }
            }
        }
        public void ModifVert(int n) //nbr positif --> multiplicateur des pixels verts --> de 0 à infini pour les pourcentages
        {
            double c = n / 100.0;
            for (int i = 0; i < img.GetLength(0); i++)
            {
                for (int j = 0; j < img.GetLength(1); j++)
                {
                    if (img[i, j].g * c >= 0 && img[i, j].g * c <= 255)
                    {
                        img[i, j].g = Convert.ToByte(img[i, j].g * c);
                    }
                    else if (img[i, j].g * c > 255)
                    {
                        img[i, j].g = 255;
                    }
                    else
                    {
                        img[i, j].g = 0;
                    }
                }
            }
        }
        public void ModifBleu(int n) //nbr positif --> multiplicateur des pixels bleus --> de 0 à infini pour les pourcentages
        {
            double c = n / 100.0;
            for (int i = 0; i < img.GetLength(0); i++)
            {
                for (int j = 0; j < img.GetLength(1); j++)
                {
                    if (img[i, j].b * c >= 0 && img[i, j].b * c <= 255)
                    {
                        img[i, j].b = Convert.ToByte(img[i, j].b * c);
                    }
                    else if (img[i, j].b * c > 255)
                    {
                        img[i, j].b = 255;
                    }
                    else
                    {
                        img[i, j].b = 0;
                    }
                }
            }
        }

        public void Saturation(int n) //nbr positif --> multiplicateur des contrastes --> curseur de -100 à infini
        {
            double c = (100.0 + n) / 100.0;
            double temp;
            for (int i = 0; i < img.GetLength(0); i++)
            {
                for (int j = 0; j < img.GetLength(1); j++)
                {
                    temp = 127 + (Convert.ToInt32(img[i, j].r) - 127) * c;
                    if (temp >= 0 && temp <= 255)
                    {
                        img[i, j].r = Convert.ToByte(temp);
                    }
                    else if (temp < 0)
                    {
                        img[i, j].r = 0;
                    }
                    else
                    {
                        img[i, j].r = 255;
                    }

                    temp = 127 + (Convert.ToInt32(img[i, j].g) - 127) * c;
                    if (temp >= 0 && temp <= 255)
                    {
                        img[i, j].g = Convert.ToByte(temp);
                    }
                    else if (temp < 0)
                    {
                        img[i, j].g = 0;
                    }
                    else
                    {
                        img[i, j].g = 255;
                    }

                    temp = 127 + (Convert.ToInt32(img[i, j].b) - 127) * c;
                    if (temp >= 0 && temp <= 255)
                    {
                        img[i, j].b = Convert.ToByte(temp);
                    }
                    else if (temp < 0)
                    {
                        img[i, j].b = 0;
                    }
                    else
                    {
                        img[i, j].b = 255;
                    }
                }
            }
        }

        public void Negatif()
        {
            for (int i = 0; i < img.GetLength(0); i++)
            {
                for (int j = 0; j < img.GetLength(1); j++)
                {
                    img[i, j].r = Convert.ToByte(255 - img[i, j].r);
                    img[i, j].g = Convert.ToByte(255 - img[i, j].g);
                    img[i, j].b = Convert.ToByte(255 - img[i, j].b);

                }
            }
        }

        #region JPEG

        void RGBtoYCbCr()
        {
            for (int i = 0; i < img.GetLength(0); i++)
            {
                for (int j = 0; j < img.GetLength(1); j++)
                {
                    if ((i * img.GetLength(0) + j) % 2 == 0)
                    {
                        img[i, j].r = Convert.ToByte(0.299 * img[i, j].r + 0.087 * img[i, j].g + 0.114 * img[i, j].b);
                        img[i, j].g = Convert.ToByte(-0.1687 * img[i, j].r - 0.3313 * img[i, j].g + 0.5 * img[i, j].b + 128);
                        img[i, j].b = Convert.ToByte(0.5 * img[i, j].r - 0.4187 * img[i, j].g - 0.0813 * img[i, j].b + 128);
                    }
                    else
                    {
                        img[i, j].r = Convert.ToByte(0.299 * img[i, j].r + 0.087 * img[i, j].g + 0.114 * img[i, j].b);
                        img[i, j].g = Convert.ToByte(0.299 * img[i, j].r + 0.087 * img[i, j].g + 0.114 * img[i, j].b);
                        img[i, j].b = Convert.ToByte(0.299 * img[i, j].r + 0.087 * img[i, j].g + 0.114 * img[i, j].b);
                    }
                }
            }
        }

        void YCbCrtoRGB()
        {
            byte b;
            byte r;
            byte g;
            for (int i = 0; i < img.GetLength(0); i++)
            {
                for (int j = 0; j < img.GetLength(1); j++)
                {
                    b = Convert.ToByte(2 * (img[i, j].r - 0.598 * (img[i, j].b - 128) + 0.7139242 * (img[i, j].g - 128 + 0.3374 * (img[i, j].b - 128))));
                    g = Convert.ToByte((-1 / 0.47256938) * (img[i, j].g - 128 + 0.3374 * (img[i, j].b - 128) - 0.47256938 * b));
                    r = Convert.ToByte(2 * (img[i, j].b - 128 + 0.4187 * g + 0.0813 * b));

                    img[i, j].r = r;
                    img[i, j].g = g;
                    img[i, j].b = b;

                }
            }
        }

        Pixel[,] Bloc(int x, int y)
        {
            Pixel[,] bloc = new Pixel[8, 8];
            for (int i = x; i < x + 8; i++)
            {
                for (int j = y; j < y + 8; j++)
                {
                    bloc[i, j] = img[8 * x + i, 8 * y + j];
                }
            }
            return bloc;
        }

        int[,][] DCT(Pixel[,] bloc)
        {
            int[,][] output = new int[8, 8][];
            double[] C = { 1, 1 / Math.Sqrt(2) };
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    double sumR = 0;
                    double sumG = 0;
                    double sumB = 0;
                    for (int x = 0; x < 8; x++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            sumR += bloc[x, y].r * Math.Cos(((2 * x + 1) * i * Math.PI) / 16) * Math.Cos(((2 * y + 1) * j * Math.PI) / 16);
                            sumG += bloc[x, y].g * Math.Cos(((2 * x + 1) * i * Math.PI) / 16) * Math.Cos(((2 * y + 1) * j * Math.PI) / 16);
                            sumB += bloc[x, y].b * Math.Cos(((2 * x + 1) * i * Math.PI) / 16) * Math.Cos(((2 * y + 1) * j * Math.PI) / 16);

                        }
                    }
                    sumR *= 0.25 * C[Convert.ToInt32(i > 0)] * C[Convert.ToInt32(j > 0)];
                    sumG *= 0.25 * C[Convert.ToInt32(i > 0)] * C[Convert.ToInt32(j > 0)];
                    sumB *= 0.25 * C[Convert.ToInt32(i > 0)] * C[Convert.ToInt32(j > 0)];

                    output[i, j] = new int[] { Convert.ToInt32(sumR), Convert.ToInt32(sumG), Convert.ToInt32(sumB) };

                }
            }
            return output;
        }

        int[,][] Quantification(int[,][] bloc)
        {
            int[,][] output = new int[8, 8][];

            int[,] Q = new int[,]
                {
                    { 16, 11, 10, 16, 24,  40,  51,  61 },
                    { 12, 12, 14, 19, 26,  58,  60,  55 },
                    { 14, 13, 16, 24, 40,  57,  69,  56 },
                    { 14, 17, 22, 29, 51,  87,  80,  62 },
                    { 18, 22, 37, 56, 68, 109, 103,  77 },
                    { 24, 35, 55, 64, 81, 104, 113,  92 },
                    { 49, 64, 78, 87, 103, 121, 120, 101 },
                    { 72, 92, 95, 98, 112, 100, 103,  99 }
                };

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    output[i, j] = new int[3];
                    output[i, j][0] = (bloc[i, j][0] + (Q[i, j] / 2)) / Q[i, j];
                    output[i, j][1] = (bloc[i, j][1] + (Q[i, j] / 2)) / Q[i, j];
                    output[i, j][2] = (bloc[i, j][2] + (Q[i, j] / 2)) / Q[i, j];
                }
            }

            return output;
        }

        public List<int[]> Codage(int[,][] bloc)
        {
            List<int[]> output = new List<int[]>();
            int i = 0;
            int j = 0;

            output.Add(new int[] { bloc[i, j][0], bloc[i, j][1], bloc[i, j][2] });
            do
            {
                j += 1;
                output.Add(new int[] { bloc[i, j][0], bloc[i, j][1], bloc[i, j][2] });
                do
                {
                    i += 1;
                    j -= 1;
                    output.Add(new int[] { bloc[i, j][0], bloc[i, j][1], bloc[i, j][2] });
                } while (j != 0);

                if (!(i == 7 && j == 0))
                {
                    i += 1;
                    output.Add(new int[] { bloc[i, j][0], bloc[i, j][1], bloc[i, j][2] });

                    do
                    {
                        i -= 1;
                        j += 1;
                        output.Add(new int[] { bloc[i, j][0], bloc[i, j][1], bloc[i, j][2] });

                    } while (i != 0);


                }


            } while (!(i == 7 && j == 0));

            do
            {
                j += 1;
                output.Add(new int[] { bloc[i, j][0], bloc[i, j][1], bloc[i, j][2] });
                if (!(i == 7 && j == 7))
                {
                    do
                    {
                        i -= 1;
                        j += 1;
                        output.Add(new int[] { bloc[i, j][0], bloc[i, j][1], bloc[i, j][2] });
                    } while (j != 7);

                    i += 1;
                    output.Add(new int[] { bloc[i, j][0], bloc[i, j][1], bloc[i, j][2] });

                    if (i < 8)
                    {
                        do
                        {
                            i += 1;
                            j -= 1;
                            output.Add(new int[] { bloc[i, j][0], bloc[i, j][1], bloc[i, j][2] });

                        } while (i != 7);
                    }
                }



            } while (!(i == 7 && j == 7));

            i = output.Count - 1;
            while (output[i][0] == 0 && output[i][1] == 0 && output[i][2] == 0)
            {
                output.RemoveAt(i);
                i -= 1;
            }

            output.Add(new int[] { int.MaxValue, int.MaxValue, int.MaxValue });
            return output;
        }

        /*byte[] CompressJpeg()
        {
            RGBtoYCbCr();

            List<Pixel[,]> PixelBlocks = new List<Pixel[,]>();
            for (int i = 0; i < img.GetLength(0)/8; i++)
            {
                for (int j = 0; j < img.GetLength(1)/8; j++)
                {
                    PixelBlocks.Add(Bloc(i, j));
                }
            }

            List<int[,][]> comp = new List<int[,][]>();

            for (int i = 0; i < PixelBlocks.Count; i++)
            {
                comp.Add(Quantification(DCT(PixelBlocks[i])));
            }

            for (int i = 0; i < comp.Count; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        img[i, j] = new Pixel(Convert.ToByte(comp[i][i * 8 + j, i * 8 + k][0]),Convert.ToByte(comp[i][i * 8 + j, i * 8 + k][1]),Convert.ToByte(comp[i][i * 8 + j, i * 8 + k][2]));
                    }
                }
            }

            YCbCrtoRGB();

            byte[] output = 
            
        }
        */
        #endregion
        public void SaveImg(int cpt)
        {
            try
            {
                double temph = hauteur;
                double templ = largeur;
                byte[] im = new byte[taille];
                for (int i = 53; i >= 0; i--)
                {

                    if (i == 25)
                    {
                        for (int j = 1; temph - Math.Pow(2, 32) >= 0; j++)
                        {
                            templ = temph - Math.Pow(2, 32);
                            im[i] = Convert.ToByte(j);
                        }
                    }
                    else if (i == 24)
                    {
                        for (int j = 1; temph - Math.Pow(2, 16) >= 0; j++)
                        {
                            temph = temph - Math.Pow(2, 16);
                            im[i] = Convert.ToByte(j);
                        }
                    }
                    else if (i == 23)
                    {
                        for (int j = 1; temph - 256 >= 0; j++)
                        {
                            temph = temph - 256;
                            im[i] = Convert.ToByte(j);
                        }
                    }
                    else if (i == 22)
                    {
                        im[i] = Convert.ToByte(temph);
                    }
                    else if (i == 21)
                    {
                        for (int j = 1; templ - Math.Pow(2, 32) >= 0; j++)
                        {
                            templ = templ - Math.Pow(2, 32);
                            im[i] = Convert.ToByte(j);
                        }
                    }
                    else if (i == 20)
                    {
                        for (int j = 1; templ - Math.Pow(2, 16) >= 0; j++)
                        {
                            templ = templ - Math.Pow(2, 16);
                            im[i] = Convert.ToByte(j);
                        }
                    }
                    else if (i == 19)
                    {
                        for (int j = 1; templ - 256 >= 0; j++)
                        {
                            templ = templ - 256;
                            im[i] = Convert.ToByte(j);
                        }
                    }
                    else if (i == 18)
                    {
                        im[i] = Convert.ToByte(templ);
                    }
                    else
                    {
                        im[i] = imBase[i];
                    }
                }
                int c = headerlength;
                for (int i = 0; i < hauteur; i++)
                {

                    for (int j = 0; j < largeur; j++)
                    {
                        im[c] = img[i, j].r;
                        c++;
                        im[c] = img[i, j].b;
                        c++;
                        im[c] = img[i, j].g;
                        c++;
                    }
                }
                File.WriteAllBytes("./Image/Sortie" + Convert.ToString(cpt) + ".bmp", im);
            }
            catch (Exception)
            {
                
            }
            
        }
    }
}
