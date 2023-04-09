using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TH6_Miguel
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        //Button
        Button[,] tempatnebak;
        Button posisikey;
        Button enter;
        Button delete;

        int x;
        int y;
        int panjangkata;
        int jumlahnebak;
        int keyboardx;
        int keyboardy;
        int posisinebak_a;
        int posisinebak_b;
        int counterguess;
        string[] keyboard;
        string kata = "";
        string jawaban;

        List<string> wordleword = new List<string>();
        

        private void Form2_Load(object sender, EventArgs e)
        {
            x = 15;
            y = 15;

            panjangkata = 5;
            jumlahnebak = Form1.inputan;

            tempatnebak = new Button[panjangkata, jumlahnebak];
            keyboard = new string[26] { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "A", "S", "D", "F", "G", "H", "J", "K", "L", "Z", "X", "C", "V", "B", "N", "M" };
            posisinebak_a = 0;
            posisinebak_b = 0;


            // buat button kolom tempat tebak world
            for (int i = 0; i < panjangkata; i++)
            {
                for (int j = 0; j < jumlahnebak; j++)
                {
                    tempatnebak[i, j] = new Button();
                    tempatnebak[i, j].Tag = i.ToString() + "," + j.ToString();
                    tempatnebak[i, j].Size = new Size(55, 55);
                    tempatnebak[i, j].Location = new Point(x, y);
                    this.Controls.Add(tempatnebak[i, j]);
                    y += 55;
                }
                y = 15;
                x += 55;
            }
            keyboardx = 305;
            keyboardy = 15;

            int abjad = 26;
            for (int i = 0; i < abjad; i++)
            {
                if (i == 10)
                {
                    keyboardx = 330;
                    keyboardy = 70;
                }
                if (i == 19)
                {
                    keyboardx = 390;
                    keyboardy = 130;
                }
                posisikey = new Button();
                posisikey.Text = keyboard[i];
                posisikey.Size = new Size(50, 50);
                posisikey.Location = new Point(keyboardx, keyboardy);

                this.Controls.Add(posisikey);
                posisikey.Click += key_click;
                keyboardx += 65;
            }
            // button enter
            enter = new Button();
            enter.Text = "Enter";
            enter.Size = new Size(75, 50);
            enter.Location = new Point(305, 130);
            this.Controls.Add(enter);
            enter.Click += enter_click;

            // button delete
            delete = new Button();
            delete.Text = "Delete";
            delete.Size = new Size(75, 50);
            delete.Location = new Point(835, 130);
            this.Controls.Add(delete);
            delete.Click += delete_click;

            string[] wordlines = File.ReadAllLines("Wordle Word List.txt");
            foreach (string word in wordlines)
            {
                wordleword.AddRange(word.Split(','));
            }
            jawaban = wordleword[new Random().Next(0, wordleword.Count - 1)].ToUpper();
            
        }

        private void key_click(object sender, EventArgs e)
        {
            var key = sender as Button;
            if (posisinebak_a < 5)
            {
                tempatnebak[posisinebak_a, posisinebak_b].Text = key.Text;
                posisinebak_a++;
            }
        }
        private void delete_click(object sender, EventArgs e)
        {
            if (posisinebak_a > 0)
            {
                posisinebak_a--;
                tempatnebak[posisinebak_a, posisinebak_b].Text = "";
            }
        }

        private void enter_click(object senders, EventArgs e)
        {
            int green = 0;
            int yellow = 0;

            if (posisinebak_a != 5)
            {
                MessageBox.Show("harus isi satu baris baru bisa enter", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                for (int l = 0; l < panjangkata; l++)
                {
                    kata += tempatnebak[l, posisinebak_b].Text;
                }
                if (wordleword.Contains(kata.ToLower()))
                {
                    foreach (char kata in jawaban)
                    {
                        for (int l = 0; l < panjangkata; l++)
                        {
                            if (tempatnebak[l, posisinebak_b].Text == kata.ToString())
                            {
                                green++;
                                tempatnebak[l, posisinebak_b].BackColor = Color.Green;
                            }
                            if (tempatnebak[l, posisinebak_b].Text.Contains(kata.ToString()) && tempatnebak[l, posisinebak_b].Text != jawaban[l].ToString())
                            {
                                tempatnebak[l, posisinebak_b].BackColor = Color.Yellow;
                            }
                        }

                    }
                    posisinebak_a= 0;
                    posisinebak_b++;
                    counterguess++;
                    kata = "";

                    if (green == 5)
                    {
                        MessageBox.Show("Yay kata ditemukan!!!", "", MessageBoxButtons.OK);
                    }
                    else if (posisinebak_b == jumlahnebak && green <5 )
                    {
                        MessageBox.Show("You Lose");
                    }
                    
   
                }
                else
                {
                    MessageBox.Show("Kata tidak ada dalam wordle word list", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    kata = "";
                }
            }
        }
    }
}

