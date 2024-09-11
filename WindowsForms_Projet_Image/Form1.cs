using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;



namespace WindowsForms_Projet_Image
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        TextBox textBox1;
        TextBox textBox2;
        TrackBar trackBar1;
        Label label1;
        TrackBar trackBar2;
        Label label2;
        TrackBar trackBar3;
        Label label3;
        TrackBar trackBar4;
        Label label4;
        TrackBar trackBar5;
        Label label5;
        string valeurSaisie;
        bool bouton1actif = false;
        bool bouton2actif = false;
        bool bouton3actif = false;
        bool boutonsortieactif = false;
        
        //Le compteur sert à mettre chaque sortie dans un fichier différent
        int cpt = 0;

        //Image Coco
        private void button1_Click(object sender, EventArgs e)
        {
            Image image = Image.FromFile("./Image/coco.bmp");
            bouton1actif = true;
            bouton2actif = false;
            bouton3actif = false;
            boutonsortieactif = false;
            // Afficher l'image dans la PictureBox
            pictureBox2.Image = image;
            pictureBox2.Location = new Point(300, 10);
            pictureBox2.ClientSize = new Size(image.Width, image.Height);
        }

        //Image Lac
        private void button2_Click(object sender, EventArgs e)
        {
            Image image = Image.FromFile("./Image/lac.bmp");
            bouton2actif = true;
            bouton1actif = false;
            bouton3actif = false;
            boutonsortieactif = false;

            // Afficher l'image dans la PictureBox
            pictureBox2.Image = image;
            pictureBox2.Location = new Point(300, 10);
            pictureBox2.ClientSize = new Size(image.Width, image.Height);
        }

        //Image Test
        private void button3_Click(object sender, EventArgs e)
        {
            Image image = Image.FromFile("./Image/test.bmp");
            bouton3actif = true;
            bouton1actif = false;
            bouton2actif = false;
            boutonsortieactif = false;
            // Afficher l'image dans la PictureBox
            pictureBox2.Image = image;
            pictureBox2.Location = new Point(300, 10);
            pictureBox2.ClientSize = new Size(image.Width, image.Height);
        }

        //Grey
        private void button4_Click(object sender, EventArgs e)
        {
            byte[] myfile = null;
            bool nulle=false;
            if (boutonsortieactif)
            {
                myfile = File.ReadAllBytes("./Image/Sortie" + Convert.ToString(cpt - 1) + ".bmp");
            }
            else if (bouton2actif)
            {

               myfile = File.ReadAllBytes("./Image/lac.bmp");
            }
            else if (bouton3actif)
            {
                myfile = File.ReadAllBytes("./Image/test.bmp");         
            }
            else if (bouton1actif)
            {
                myfile = File.ReadAllBytes("./Image/coco.bmp");
            }
            else
            {
                nulle = true;
            }
            if(!nulle)
            {
                boutonsortieactif = true;
                Image123 a = new Image123(myfile);
                a.Grey();
                a.SaveImg(cpt);
                Image image = Image.FromFile("./Image/Sortie" + Convert.ToString(cpt) + ".bmp");
                cpt++;
                pictureBox2.Image = image;
                pictureBox2.Location = new Point(300, 10);
                pictureBox2.ClientSize = new Size(image.Width, image.Height);
            }
            

        }

        //Rotation
        private void button5_Click(object sender, EventArgs e)
        {
            byte[] myfile = null;
            bool nulle = false;
            if (boutonsortieactif)
            {
                myfile = File.ReadAllBytes("./Image/Sortie" + Convert.ToString(cpt - 1) + ".bmp");
            }
            else if (bouton2actif)
            {

                myfile = File.ReadAllBytes("./Image/lac.bmp");
            }
            else if (bouton3actif)
            {
                myfile = File.ReadAllBytes("./Image/test.bmp");
            }
            else if (bouton1actif)
            {
                myfile = File.ReadAllBytes("./Image/coco.bmp");
            }
            else
            {
                nulle = true;
            }
            if (!nulle)
            {
                boutonsortieactif = true;
                Image123 a = new Image123(myfile);
                a.Rota(Convert.ToDouble(trackBar1.Value));
                a.SaveImg(cpt);
                Image image = Image.FromFile("./Image/Sortie" + Convert.ToString(cpt) + ".bmp");
                cpt++;
                pictureBox2.Image = image;
                pictureBox2.Location = new Point(300, 10);
                pictureBox2.ClientSize = new Size(image.Width, image.Height);
            }
        }
        
        //Zoom
        private void button6_Click(object sender, EventArgs e)
        {
            byte[] myfile = null;
            bool nulle = false;
            if (boutonsortieactif)
            {
                myfile = File.ReadAllBytes("./Image/Sortie" + Convert.ToString(cpt - 1) + ".bmp");
            }
            else if (bouton2actif)
            {

                myfile = File.ReadAllBytes("./Image/lac.bmp");
            }
            else if (bouton3actif)
            {
                myfile = File.ReadAllBytes("./Image/test.bmp");
            }
            else if (bouton1actif)
            {
                myfile = File.ReadAllBytes("./Image/coco.bmp");
            }
            else
            {
                nulle = true;
            }
            
            if (!nulle)
            {
                boutonsortieactif = true;
                Image123 a = new Image123(myfile);
                a.Zoom(2);
                a.SaveImg(cpt);
                try
                {
                    Image image = Image.FromFile("./Image/Sortie" + Convert.ToString(cpt) + ".bmp");
                    cpt++;
                    pictureBox2.Image = image;
                    pictureBox2.Location = new Point(300, 10);
                    pictureBox2.ClientSize = new Size(image.Width, image.Height);
                }
                catch (Exception)
                {
                    Label label2 = new Label();
                    label2.Location = new Point(0, 10);
                    label2.AutoSize = true;
                    label2.Text = "La taille de l'image est trop grande pour pouvoir être sauvegardée";
                    Controls.Add(label2);
                }
                
            }
        }

        //Detection des contours
        private void button7_Click(object sender, EventArgs e)
        {
            byte[] myfile = null;
            bool nulle = false;
            if (boutonsortieactif)
            {
                myfile = File.ReadAllBytes("./Image/Sortie" + Convert.ToString(cpt - 1) + ".bmp");
            }
            else if (bouton2actif)
            {

                myfile = File.ReadAllBytes("./Image/lac.bmp");
            }
            else if (bouton3actif)
            {
                myfile = File.ReadAllBytes("./Image/test.bmp");
            }
            else if (bouton1actif)
            {
                myfile = File.ReadAllBytes("./Image/coco.bmp");
            }
            else
            {
                nulle = true;
            }

            if (!nulle)
            {
                boutonsortieactif = true;
                Image123 a = new Image123(myfile);
                a.DetectionDesContours();
                a.SaveImg(cpt);
                try
                {
                    Image image = Image.FromFile("./Image/Sortie" + Convert.ToString(cpt) + ".bmp");
                    cpt++;
                    pictureBox2.Image = image;
                    pictureBox2.Location = new Point(300, 10);
                    pictureBox2.ClientSize = new Size(image.Width, image.Height);
                }
                catch (Exception)
                {
                    Label label2 = new Label();
                    label2.Location = new Point(0, 10);
                    label2.AutoSize = true;
                    label2.Text = "La taille de l'image est trop grande pour pouvoir être sauvegardée";
                    Controls.Add(label2);
                }

            }
        }

        //Flou
        private void button8_Click(object sender, EventArgs e)
        {
            byte[] myfile = null;
            bool nulle = false;
            if (boutonsortieactif)
            {
                myfile = File.ReadAllBytes("./Image/Sortie" + Convert.ToString(cpt - 1) + ".bmp");
            }
            else if (bouton2actif)
            {

                myfile = File.ReadAllBytes("./Image/lac.bmp");
            }
            else if (bouton3actif)
            {
                myfile = File.ReadAllBytes("./Image/test.bmp");
            }
            else if (bouton1actif)
            {
                myfile = File.ReadAllBytes("./Image/coco.bmp");
            }
            else
            {
                nulle = true;
            }

            if (!nulle)
            {
                boutonsortieactif = true;
                Image123 a = new Image123(myfile);
                a.Flou();
                a.SaveImg(cpt);
                try
                {
                    Image image = Image.FromFile("./Image/Sortie" + Convert.ToString(cpt) + ".bmp");
                    cpt++;
                    pictureBox2.Image = image;
                    pictureBox2.Location = new Point(300, 10);
                    pictureBox2.ClientSize = new Size(image.Width, image.Height);
                }
                catch (Exception)
                {
                    Label label2 = new Label();
                    label2.Location = new Point(0, 10);
                    label2.AutoSize = true;
                    label2.Text = "La taille de l'image est trop grande pour pouvoir être sauvegardée";
                    Controls.Add(label2);
                }

            }
        }

        //Repoussage
        private void button9_Click(object sender, EventArgs e)
        {
            byte[] myfile = null;
            bool nulle = false;
            if (boutonsortieactif)
            {
                myfile = File.ReadAllBytes("./Image/Sortie" + Convert.ToString(cpt - 1) + ".bmp");
            }
            else if (bouton2actif)
            {

                myfile = File.ReadAllBytes("./Image/lac.bmp");
            }
            else if (bouton3actif)
            {
                myfile = File.ReadAllBytes("./Image/test.bmp");
            }
            else if (bouton1actif)
            {
                myfile = File.ReadAllBytes("./Image/coco.bmp");
            }
            else
            {
                nulle = true;
            }

            if (!nulle)
            {
                boutonsortieactif = true;
                Image123 a = new Image123(myfile);
                a.Repoussage();
                a.SaveImg(cpt);
                try
                {
                    Image image = Image.FromFile("./Image/Sortie" + Convert.ToString(cpt) + ".bmp");
                    cpt++;
                    pictureBox2.Image = image;
                    pictureBox2.Location = new Point(300, 10);
                    pictureBox2.ClientSize = new Size(image.Width, image.Height);
                }
                catch (Exception)
                {
                    Label label2 = new Label();
                    label2.Location = new Point(0, 10);
                    label2.AutoSize = true;
                    label2.Text = "La taille de l'image est trop grande pour pouvoir être sauvegardée";
                    Controls.Add(label2);
                }

            }
        }

        //Mandelbrot
        private void button10_Click(object sender, EventArgs e)
        {
            boutonsortieactif=true;
            Image123 img = new Image123(270, 240);
            img.MandelBrot(100);
            img.SaveImg(cpt);
            
            Image image = Image.FromFile("./Image/Sortie" + Convert.ToString(cpt) + ".bmp");
           
            cpt++;
            pictureBox2.Image = image;
            pictureBox2.Location = new Point(300, 10);
            pictureBox2.ClientSize = new Size(image.Width, image.Height);
        }

        //Afficher l'image que l'on souhaite
        private void Button11_Click(object sender, EventArgs e)
        {
            valeurSaisie = textBox1.Text;
            try
            {
                Image image = Image.FromFile(valeurSaisie);
                bouton2actif = false;
                bouton1actif = false;
                bouton3actif = false;
                boutonsortieactif = true;

                // Afficher l'image dans la PictureBox
                pictureBox2.Image = image;
                pictureBox2.Location = new Point(300, 10);
                pictureBox2.ClientSize = new Size(image.Width, image.Height);
                // Faire quelque chose avec la valeur saisie...
            }
            catch (Exception)
            {

            }
            Image image1 = Image.FromFile(valeurSaisie);
        }

        //Encodage
        private void Button12_Click(object sender, EventArgs e)
        {
            valeurSaisie = textBox2.Text;
            try
            {
                byte[] myfile1 = null;
                bool nulle = false;
                if (boutonsortieactif)
                {
                    myfile1 = File.ReadAllBytes("./Image/Sortie" + Convert.ToString(cpt - 1) + ".bmp");
                }
                else if (bouton2actif)
                {

                    myfile1 = File.ReadAllBytes("./Image/lac.bmp");
                }
                else if (bouton3actif)
                {
                    myfile1 = File.ReadAllBytes("./Image/test.bmp");
                }
                else if (bouton1actif)
                {
                    myfile1 = File.ReadAllBytes("./Image/coco.bmp");
                }
                else
                {
                    nulle = true;
                }
                if (!nulle)
                {
                    boutonsortieactif = true;
                    Image123 b = new Image123(myfile1);
                    byte[] file = File.ReadAllBytes(valeurSaisie);

                    Image123 a = new Image123(file);
                    a.Encode(b);
                    a.SaveImg(cpt);
                    try
                    {
                        Image image = Image.FromFile("./Image/Sortie" + Convert.ToString(cpt) + ".bmp");
                        cpt++;
                        pictureBox2.Image = image;
                        pictureBox2.Location = new Point(300, 10);
                        pictureBox2.ClientSize = new Size(image.Width, image.Height);
                    }
                    catch (Exception)
                    {
                        Label label2 = new Label();
                        label2.Location = new Point(0, 10);
                        label2.AutoSize = true;
                        label2.Text = "La taille de l'image est trop grande pour pouvoir être sauvegardée";
                        Controls.Add(label2);
                    }
                }
            }
            catch (Exception)
            {

            }
        }
                
        

        private void Buttondecodage_Click(object sender, EventArgs e)
        {
            byte[] myfile = null;
            bool nulle = false;
            if (boutonsortieactif)
            {
                myfile = File.ReadAllBytes("./Image/Sortie" + Convert.ToString(cpt - 1) + ".bmp");
            }
            else if (bouton2actif)
            {

                myfile = File.ReadAllBytes("./Image/lac.bmp");
            }
            else if (bouton3actif)
            {
                myfile = File.ReadAllBytes("./Image/test.bmp");
            }
            else if (bouton1actif)
            {
                myfile = File.ReadAllBytes("./Image/coco.bmp");
            }
            else
            {
                nulle = true;
            }

            if (!nulle)
            {
                boutonsortieactif = true;
                Image123 a = new Image123(myfile);
                a.Decode();
                a.SaveImg(cpt);
                try
                {
                    Image image = Image.FromFile("./Image/Sortie" + Convert.ToString(cpt) + ".bmp");
                    cpt++;
                    pictureBox2.Image = image;
                    pictureBox2.Location = new Point(300, 10);
                    pictureBox2.ClientSize = new Size(image.Width, image.Height);
                }
                catch (Exception)
                {
                    Label label2 = new Label();
                    label2.Location = new Point(0, 10);
                    label2.AutoSize = true;
                    label2.Text = "La taille de l'image est trop grande pour pouvoir être sauvegardée";
                    Controls.Add(label2);
                }

            }
        }

        //Modif Rouge
        private void Button13_Click(object sender, EventArgs e)
        {
            byte[] myfile = null;
            bool nulle = false;
            if (boutonsortieactif)
            {
                myfile = File.ReadAllBytes("./Image/Sortie" + Convert.ToString(cpt - 1) + ".bmp");
            }
            else if (bouton2actif)
            {

                myfile = File.ReadAllBytes("./Image/lac.bmp");
            }
            else if (bouton3actif)
            {
                myfile = File.ReadAllBytes("./Image/test.bmp");
            }
            else if (bouton1actif)
            {
                myfile = File.ReadAllBytes("./Image/coco.bmp");
            }
            else
            {
                nulle = true;
            }
            if (!nulle)
            {
                boutonsortieactif = true;
                Image123 a = new Image123(myfile);
                a.ModifRouge(Convert.ToInt32(trackBar2.Value));
                a.SaveImg(cpt);
                Image image = Image.FromFile("./Image/Sortie" + Convert.ToString(cpt) + ".bmp");
                cpt++;
                pictureBox2.Image = image;
                pictureBox2.Location = new Point(300, 10);
                pictureBox2.ClientSize = new Size(image.Width, image.Height);
            }
        }

        //Modif Bleu
        private void Button14_Click(object sender, EventArgs e)
        {
            byte[] myfile = null;
            bool nulle = false;
            if (boutonsortieactif)
            {
                myfile = File.ReadAllBytes("./Image/Sortie" + Convert.ToString(cpt - 1) + ".bmp");
            }
            else if (bouton2actif)
            {

                myfile = File.ReadAllBytes("./Image/lac.bmp");
            }
            else if (bouton3actif)
            {
                myfile = File.ReadAllBytes("./Image/test.bmp");
            }
            else if (bouton1actif)
            {
                myfile = File.ReadAllBytes("./Image/coco.bmp");
            }
            else
            {
                nulle = true;
            }
            if (!nulle)
            {
                boutonsortieactif = true;
                Image123 a = new Image123(myfile);
                a.ModifBleu(Convert.ToInt32(trackBar3.Value));
                a.SaveImg(cpt);
                Image image = Image.FromFile("./Image/Sortie" + Convert.ToString(cpt) + ".bmp");
                cpt++;
                pictureBox2.Image = image;
                pictureBox2.Location = new Point(300, 10);
                pictureBox2.ClientSize = new Size(image.Width, image.Height);
            }
        }

        //Modif Vert
        private void Button15_Click(object sender, EventArgs e)
        {
            byte[] myfile = null;
            bool nulle = false;
            if (boutonsortieactif)
            {
                myfile = File.ReadAllBytes("./Image/Sortie" + Convert.ToString(cpt - 1) + ".bmp");
            }
            else if (bouton2actif)
            {

                myfile = File.ReadAllBytes("./Image/lac.bmp");
            }
            else if (bouton3actif)
            {
                myfile = File.ReadAllBytes("./Image/test.bmp");
            }
            else if (bouton1actif)
            {
                myfile = File.ReadAllBytes("./Image/coco.bmp");
            }
            else
            {
                nulle = true;
            }
            if (!nulle)
            {
                boutonsortieactif = true;
                Image123 a = new Image123(myfile);
                a.ModifVert(Convert.ToInt32(trackBar4.Value));
                a.SaveImg(cpt);
                Image image = Image.FromFile("./Image/Sortie" + Convert.ToString(cpt) + ".bmp");
                cpt++;
                pictureBox2.Image = image;
                pictureBox2.Location = new Point(300, 10);
                pictureBox2.ClientSize = new Size(image.Width, image.Height);
            }
        }

        //Miroir
        private void Button16_Click(object sender, EventArgs e)
        {
            byte[] myfile = null;
            bool nulle = false;
            if (boutonsortieactif)
            {
                myfile = File.ReadAllBytes("./Image/Sortie" + Convert.ToString(cpt - 1) + ".bmp");
            }
            else if (bouton2actif)
            {

                myfile = File.ReadAllBytes("./Image/lac.bmp");
            }
            else if (bouton3actif)
            {
                myfile = File.ReadAllBytes("./Image/test.bmp");
            }
            else if (bouton1actif)
            {
                myfile = File.ReadAllBytes("./Image/coco.bmp");
            }
            else
            {
                nulle = true;
            }
            if (!nulle)
            {
                boutonsortieactif = true;
                Image123 a = new Image123(myfile);
                a.Miroir();
                a.SaveImg(cpt);
                Image image = Image.FromFile("./Image/Sortie" + Convert.ToString(cpt) + ".bmp");
                cpt++;
                pictureBox2.Image = image;
                pictureBox2.Location = new Point(300, 10);
                pictureBox2.ClientSize = new Size(image.Width, image.Height);
            }
        }

        //Constraste
        private void Button17_Click(object sender, EventArgs e)
        {
            byte[] myfile = null;
            bool nulle = false;
            if (boutonsortieactif)
            {
                myfile = File.ReadAllBytes("./Image/Sortie" + Convert.ToString(cpt - 1) + ".bmp");
            }
            else if (bouton2actif)
            {

                myfile = File.ReadAllBytes("./Image/lac.bmp");
            }
            else if (bouton3actif)
            {
                myfile = File.ReadAllBytes("./Image/test.bmp");
            }
            else if (bouton1actif)
            {
                myfile = File.ReadAllBytes("./Image/coco.bmp");
            }
            else
            {
                nulle = true;
            }
            if (!nulle)
            {
                boutonsortieactif = true;
                Image123 a = new Image123(myfile);
                a.Saturation(20);
                a.SaveImg(cpt);
                Image image = Image.FromFile("./Image/Sortie" + Convert.ToString(cpt) + ".bmp");
                cpt++;
                pictureBox2.Image = image;
                pictureBox2.Location = new Point(300, 10);
                pictureBox2.ClientSize = new Size(image.Width, image.Height);
            }
        }


        private void Button19_Click(object sender, EventArgs e)
        {
            byte[] myfile = null;
            bool nulle = false;
            if (boutonsortieactif)
            {
                myfile = File.ReadAllBytes("./Image/Sortie" + Convert.ToString(cpt - 1) + ".bmp");
            }
            else if (bouton2actif)
            {

                myfile = File.ReadAllBytes("./Image/lac.bmp");
            }
            else if (bouton3actif)
            {
                myfile = File.ReadAllBytes("./Image/test.bmp");
            }
            else if (bouton1actif)
            {
                myfile = File.ReadAllBytes("./Image/coco.bmp");
            }
            else
            {
                nulle = true;
            }
            if (!nulle)
            {
                boutonsortieactif = true;
                Image123 a = new Image123(myfile);
                a.Saturation(-20);
                a.SaveImg(cpt);
                Image image = Image.FromFile("./Image/Sortie" + Convert.ToString(cpt) + ".bmp");
                cpt++;
                pictureBox2.Image = image;
                pictureBox2.Location = new Point(300, 10);
                pictureBox2.ClientSize = new Size(image.Width, image.Height);
            }
        }
        //Négatif
        private void Button18_Click(object sender, EventArgs e)
        {
            byte[] myfile = null;
            bool nulle = false;
            if (boutonsortieactif)
            {
                myfile = File.ReadAllBytes("./Image/Sortie" + Convert.ToString(cpt - 1) + ".bmp");
            }
            else if (bouton2actif)
            {

                myfile = File.ReadAllBytes("./Image/lac.bmp");
            }
            else if (bouton3actif)
            {
                myfile = File.ReadAllBytes("./Image/test.bmp");
            }
            else if (bouton1actif)
            {
                myfile = File.ReadAllBytes("./Image/coco.bmp");
            }
            else
            {
                nulle = true;
            }
            if (!nulle)
            {
                boutonsortieactif = true;
                Image123 a = new Image123(myfile);
                a.Negatif();
                a.SaveImg(cpt);
                Image image = Image.FromFile("./Image/Sortie" + Convert.ToString(cpt) + ".bmp");
                cpt++;
                pictureBox2.Image = image;
                pictureBox2.Location = new Point(300, 10);
                pictureBox2.ClientSize = new Size(image.Width, image.Height);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = "Valeur (en degré): " + trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label2.Text = "Multiplicateur: " + trackBar2.Value.ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label3.Text = "Multiplicateur: " + trackBar3.Value.ToString();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            label4.Text = "Multiplicateur: " + trackBar4.Value.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            // Create a new button control
            int test = 0;
            string filePath = null;
            int cpt1 = 0;

            /// Pour que chaque fichier ayant pu être créer avant soient supprimé
            do
            {
                filePath = "./Image/Sortie" + Convert.ToString(cpt1) + ".bmp";
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    cpt1++;
                }
                else 
                {
                    test = 1; 
                }

            } while (test == 0);

            #region bouton1
            Button button1 = new Button();
            button1.Text = "Coco";
            button1.Location = new Point(50, 70);

            // Register an event handler for the button click event
            button1.Click += new EventHandler(button1_Click);
            this.Controls.Add(button1);
            #endregion

            #region bouton2
            Button button2 = new Button();
            button2.Text = "Lac";
            button2.Location = new Point(50, 90);

            // Register an event handler for the button click event
            button2.Click += new EventHandler(button2_Click);

            this.Controls.Add(button2);
            #endregion

            #region bouton3
            Button button3 = new Button();
            button3.Text = "Test";
            button3.Location = new Point(50, 110);
            // Register an event handler for the button click event
            button3.Click += new EventHandler(button3_Click);

            // Add the button control to the form
            this.Controls.Add(button3);

            #endregion

            ///Grey
            #region bouton4
            Button button4 = new Button();
            button4.Text = "Grey";
            button4.Location = new Point(50, 200);
            // Register an event handler for the button click event
            button4.Click += new EventHandler(button4_Click);

            // Add the button control to the form
            this.Controls.Add(button4);

            #endregion   

            ///Rotation
            #region bouton5
            Button button5 = new Button();
            button5.Text = "Rota";
            button5.Location = new Point(50, 220);
            // Register an event handler for the button click event
            button5.Click += new EventHandler(button5_Click);

            // Add the button control to the form
            this.Controls.Add(button5);

            #endregion ///Grey

            ///Zoom
            #region bouton6
            Button button6 = new Button();
            button6.Text = "Zoom";
            button6.Location = new Point(50, 310);
            // Register an event handler for the button click event
            button6.Click += new EventHandler(button6_Click);

            // Add the button control to the form
            this.Controls.Add(button6);

            #endregion

            ///Détection des contours
            #region bouton7
            Button button7 = new Button();
            button7.Text = "Détection des contours";
            button7.Location = new Point(50, 330);
            // Register an event handler for the button click event
            button7.Click += new EventHandler(button7_Click);

            // Add the button control to the form
            this.Controls.Add(button7);

            #endregion

            ///Flou
            #region bouton8
            Button button8 = new Button();
            button8.Text = "Flou";
            button8.Location = new Point(50, 350);
            // Register an event handler for the button click event
            button8.Click += new EventHandler(button8_Click);

            // Add the button control to the form
            this.Controls.Add(button8);

            #endregion

            ///Repoussage
            #region bouton9
            Button button9 = new Button();
            button9.Text = "Repoussage";
            button9.Location = new Point(50, 370);
            // Register an event handler for the button click event
            button9.Click += new EventHandler(button9_Click);

            // Add the button control to the form
            this.Controls.Add(button9);

            #endregion

            ///Mandelbrot
            #region bouton10
            Button button10 = new Button();
            button10.Text = "MandelBrot";
            button10.Location = new Point(50, 390);
            // Register an event handler for the button click event
            button10.Click += new EventHandler(button10_Click);

            // Add the button control to the form
            this.Controls.Add(button10);

            #endregion
            

            #region trackbar1
            trackBar1 = new TrackBar();
            trackBar1.Minimum = 0;
            trackBar1.Maximum = 360;
            trackBar1.Location = new Point(10, 250);
            trackBar1.Width = 300;

            // Créer le Label
            label1 = new Label();
            label1.Location = new Point(50, 295);
            label1.AutoSize = true;

            // Ajouter la TrackBar et le Label à la fenêtre
            Controls.Add(trackBar1);
            Controls.Add(label1);

            // Ajouter un gestionnaire d'événements pour la TrackBar
            trackBar1.Scroll += new EventHandler(trackBar1_Scroll);
            #endregion

            #region textbox
            textBox1 = new TextBox();
            textBox1.Location = new Point(50, 10); // Définir la position du contrôle
            textBox1.Size = new Size(200, 20); // Définir la taille du contrôle
            this.Controls.Add(textBox1); // Ajouter le contrôle au formulaire
            #endregion

            #region Bouton11
            Button button11 = new Button();
            button11.Location = new Point(50, 40);
            button11.Text = "Soumettre";
            button11.Click += Button11_Click; // Ajouter un gestionnaire d'événements pour le clic du bouton
            this.Controls.Add(button11);
            #endregion

            ///Codage Décodage 
            #region Bouton12
            Button button12 = new Button();
            button12.Text = "Encodage";
            button12.Location = new Point(0, 420);
            // Register an event handler for the button click event
            button12.Click += new EventHandler(Button12_Click);

            // Add the button control to the form
            this.Controls.Add(button12);

            textBox2 = new TextBox();
            textBox2.Location = new Point(80, 420); // Définir la position du contrôle
            textBox2.Size = new Size(200, 20); // Définir la taille du contrôle
            this.Controls.Add(textBox2); // Ajouter le contrôle au formulaire

            Button buttondecodage= new Button();
            buttondecodage.Text = "Decodage";
            buttondecodage.Location = new Point(150, 390);
            // Register an event handler for the button click event
            buttondecodage.Click += new EventHandler(Buttondecodage_Click);

            // Add the button control to the form
            this.Controls.Add(buttondecodage);

            #endregion

            #region trackbar2
            trackBar2 = new TrackBar();
            trackBar2.Minimum = 0;
            trackBar2.Maximum = 100;
            trackBar2.Location = new Point(10, 440);
            trackBar2.Width = 300;

            // Créer le Label
            label2 = new Label();
            label2.Location = new Point(30, 490);
            label2.Width = 100;

            // Ajouter la TrackBar et le Label à la fenêtre
            Controls.Add(trackBar2);
            Controls.Add(label2);

            // Ajouter un gestionnaire d'événements pour la TrackBar
            trackBar2.Scroll += new EventHandler(trackBar2_Scroll);
            #endregion

            #region Bouton13
            Button button13 = new Button();
            button13.Text = "Rouge";
            button13.Location = new Point(150, 485);
            // Register an event handler for the button click event
            button13.Click += new EventHandler(Button13_Click);
            
            // Add the button control to the form
            this.Controls.Add(button13);
            #endregion

            #region trackbar3
            trackBar3 = new TrackBar();
            trackBar3.Minimum = 0;
            trackBar3.Maximum = 100;
            trackBar3.Location = new Point(10, 510);
            trackBar3.Width = 300;

            // Créer le Label
            label3 = new Label();
            label3.Location = new Point(30, 560);
            label3.Width = 100;

            // Ajouter la TrackBar et le Label à la fenêtre
            Controls.Add(trackBar3);
            Controls.Add(label3);

            // Ajouter un gestionnaire d'événements pour la TrackBar
            trackBar3.Scroll += new EventHandler(trackBar3_Scroll);
            #endregion

            #region Bouton14
            Button button14 = new Button();
            button14.Text = "Bleu";
            button14.Location = new Point(150, 555);
            // Register an event handler for the button click event
            button14.Click += new EventHandler(Button14_Click);

            // Add the button control to the form
            this.Controls.Add(button14);
            #endregion

            #region trackbar4
            trackBar4 = new TrackBar();
            trackBar4.Minimum = 0;
            trackBar4.Maximum = 100;
            trackBar4.Location = new Point(10, 580);
            trackBar4.Width = 300;

            // Créer le Label
            label4 = new Label();
            label4.Location = new Point(30, 630);
            label4.Width = 100;

            // Ajouter la TrackBar et le Label à la fenêtre
            Controls.Add(trackBar4);
            Controls.Add(label4);

            // Ajouter un gestionnaire d'événements pour la TrackBar
            trackBar4.Scroll += new EventHandler(trackBar4_Scroll);
            #endregion

            #region trackbar5
            trackBar5 = new TrackBar();
            trackBar5.Minimum = 0;
            trackBar5.Maximum = 100;
            trackBar5.Location = new Point(10, 580);
            trackBar5.Width = 300;

            // Créer le Label
            label5 = new Label();
            label5.Location = new Point(30, 630);
            label5.Width = 100;

            // Ajouter la TrackBar et le Label à la fenêtre
            Controls.Add(trackBar5);
            Controls.Add(label4);

            // Ajouter un gestionnaire d'événements pour la TrackBar
            trackBar4.Scroll += new EventHandler(trackBar4_Scroll);
            #endregion

            #region Bouton15
            Button button15 = new Button();
            button15.Text = "Vert";
            button15.Location = new Point(150, 625);
            // Register an event handler for the button click event
            button15.Click += new EventHandler(Button15_Click);

            // Add the button control to the form
            this.Controls.Add(button15);
            #endregion

            #region Bouton16
            Button button16 = new Button();
            button16.Text = "Miroir";
            button16.Location = new Point(150, 370);
            // Register an event handler for the button click event
            button16.Click += new EventHandler(Button16_Click);

            // Add the button control to the form
            this.Controls.Add(button16);
            #endregion

            #region Bouton17
            Button button17 = new Button();
            button17.Text = "Constraste +";
            button17.Location = new Point(150, 350);
            // Register an event handler for the button click event
            button17.Click += new EventHandler(Button17_Click);

            // Add the button control to the form
            this.Controls.Add(button17);
            #endregion

            #region Bouton18
            Button button18 = new Button();
            button18.Text = "Négatif";
            button18.Location = new Point(150, 310);
            // Register an event handler for the button click event
            button18.Click += new EventHandler(Button18_Click);

            // Add the button control to the form
            this.Controls.Add(button18);
            #endregion

            #region bouton 19
            Button button19 = new Button();
            button19.Text = "Constraste -";
            button19.Location = new Point(150, 330);
            // Register an event handler for the button click event
            button19.Click += new EventHandler(Button19_Click);

            // Add the button control to the form
            this.Controls.Add(button19);
            #endregion


        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
