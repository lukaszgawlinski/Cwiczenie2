using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KNN_Proba
{
    public partial class Form1 : Form
    {

        //int x = 5;
        public string wczytaj;
        private int[][] systemTestowy;
        private int[][] systemTreningowy;

        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var wynik = openFileDialog1.ShowDialog();
            if (wynik != DialogResult.OK)
                return;
            if (wynik == DialogResult.OK)
            {

                systemTestowy = Wczytaj(openFileDialog1.FileName, richTextBox1);
                
            }
        }



        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var wynik = openFileDialog2.ShowDialog();
            if (wynik != DialogResult.OK)
                return;
            if (wynik == DialogResult.OK)
            {

                systemTreningowy = Wczytaj(openFileDialog2.FileName, richTextBox2);

            }

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }


        int [][] Wczytaj(string path, RichTextBox rtb = null)
        {
            string trescPliku = System.IO.File.ReadAllText(path);
            if (rtb != null)
                rtb.AppendText(trescPliku);

            string[] poziomy = trescPliku.Split('\n');
            int[][] daneZPliku = new int[poziomy.Length][];
            for (int i = 0; i < poziomy.Length; i++)
            {
                string poziom = poziomy[i].Trim();
                string[] miejscaParkingowe = poziom.Split(' ');
                daneZPliku[i] = new int[miejscaParkingowe.Length];
                for (int j = 0; j < miejscaParkingowe.Length; j++)
                {
                    daneZPliku[i][j] = int.Parse(miejscaParkingowe[j]);
                }
            }

            return daneZPliku;
        }

        private void cbMetryki_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pomoc");

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //richTextBox3.Clear();
            foreach (var item in systemTestowy)
            {
                KlasyfikujObiekt(item, systemTreningowy, (int)numericUpDown1.Value); //wywolanie funkcji do testow
                
                //richTextBox3.Text += string.Join(" ", KlasyfikujObiekt(item, systemTreningowy, (int)numericUpDown1.Value));
                //richTextBox3.Text += "\n";
            }

        }
        
        private double metrykaManhattan(int[] x, int[] y)
        {
            double wynik = 0;
            for (int i = 0; i < x.Length - 1; i++)
            {
                wynik += Math.Abs(x[i] - y[i]);
            }
            return wynik;
        }

        private double metrykaEuklidesowa(int[] x, int [] y)
        {
            double wynik = 0;
            for (int i = 0; i < x.Length - 1; i++)
            {
                wynik += Math.Pow(x[i] - y[i], 2);

            }
            return Math.Sqrt(wynik);

        }
        private double metrykaCanberra(int[] x, int [] y)
        {
            double wynik = 0;
            for (int i = 0; i < x.Length - 1; i++)
            {
                wynik += Math.Abs((double)(x[i] - y[i]) / (double)(x[i] + y[i]));
            }
            return wynik;

        }
        private double metrykaCzebyszewa(int[] x, int[] y)
        {
            double[] wyniki = new double[x.Length - 1];
            for (int i = 0; i < x.Length - 1; i++)
            {
                wyniki[i] = Math.Abs(x[i] - y[i]);
            }
            return wyniki.OrderByDescending(z => z).First(); // sortuje po wynikach i wybieram pierwszy
        }




        int? KlasyfikujObiekt(int[] obiekt, int[][] TRN, int k)
        {

            List<double> wyniki = new List<double>(); // tworzenie listy doubli 
            foreach (var item in TRN)
            {
                switch (cbMetryki.SelectedIndex)
                {
                    case 0:
                        {
                            wyniki.Add(metrykaEuklidesowa(obiekt, item));
                            break;
                        }
                    case 1:
                        {
                            wyniki.Add(metrykaCanberra(obiekt, item));
                            break;
                        }
                    case 2:
                        {
                            wyniki.Add(metrykaManhattan(obiekt, item));
                            break;
                        }
                    case 3:
                        {
                            wyniki.Add(metrykaCzebyszewa(obiekt, item));
                            break;
                        }            
                    default:
                        break;
                }

            }

            return 1;
        }
    }
}
