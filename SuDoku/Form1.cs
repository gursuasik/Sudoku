using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SuDoku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int[,][] SuDoku=new int[9,9][];
        private void Form1_Load(object sender, EventArgs e)
        {
            //Start();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            GetBoxs();
            Possibiltys();
            Single();
            Search();
            SetBoxs();
        }
        private void Possibiltys()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (SuDoku[i, j][0] == 0)
                    {
                        int k = 2;//yeri tutar
                        for (int n = 1; n < 10; n++)//numara seçimi
                        {
                            bool find = false;
                            for (int l = 0; l < 9; l++)//var olanlara bakar
                            {
                                if (SuDoku[i, l][0] == n)//Horizontal
                                {
                                    find = true;
                                    break;
                                }
                                if (SuDoku[l, j][0] == n)//Vertical
                                {
                                    find = true;
                                    break;
                                }
                                if (SuDoku[(i - (i % 3)) + (l - (l % 3)) / 3, (j - (j % 3)) + (l % 3)][0] == n)//3x3 square
                                {
                                    find = true;
                                    break;
                                }
                            }
                            if (!find)
                            {
                                Array.Resize(ref SuDoku[i, j], k);//
                                SuDoku[i, j][k-1] = n;//temp//
                                k++;
                            }
                        }
                    }
                }
            }
        }
        private void Single()
        {
            bool find = true;
            while (find)
            {
                find = false;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (SuDoku[i,j].Length==2)
                        {
                            int temp = SuDoku[i, j][1];
                            SuDoku[i, j] = new int[1];
                            SuDoku[i, j][0] = temp;
                            find = true;
                        }
                    }
                }
                Possibiltys();
            }
        }
        private void Search()
        {
            int[,] tempSuDoku = new int[9,9];//temp
            int[][,][] differentSuDoku = new int[1][,][];//temp
            int increment = 1, different = 0;
            for (int i = 0; i>-1 && i < 9; i+=increment)
            {
                for (int j = 4-4*increment; j>-1 && j < 9; j+=increment)
                {
                    if (SuDoku[i, j].Length>1)
                    {
                        bool find = true;
                        for (int k = tempSuDoku[i, j] + 1; k < SuDoku[i, j].Length && find; k++)//Try possibilty
                        {
                            SuDoku[i, j][0] = SuDoku[i, j][k];
                            tempSuDoku[i, j] = k;
                            find = false;
                            //Control Function
                            for (int l = 0; l < 9; l++)//var olanlara bakar
                            {
                                if (!(j==l) && SuDoku[i, j][0] == SuDoku[i, l][0])//Horizontal
                                {
                                    find = true;
                                    break;
                                }
                                if (!(i == l) && SuDoku[i, j][0] == SuDoku[l, j][0])//Vertical
                                {
                                    find = true;
                                    break;
                                }
                                if ((!(i == (i - (i % 3)) + (l - (l % 3)) / 3 && j == (j - (j % 3)) + (l % 3))) && SuDoku[i, j][0] == SuDoku[(i - (i % 3)) + (l - (l % 3)) / 3, (j - (j % 3)) + (l % 3)][0])//3x3 square
                                {
                                    find = true;
                                    break;
                                }
                            }
                        }
                        if (find)
                        {
                            increment=-1;
                            SuDoku[i, j][0] = 0;
                            tempSuDoku[i, j] = 0;
                        }
                        else
                        {
                            increment = 1;
                            //if (i == 8 && j == 8)
                            //{
                            //    differentSuDoku[different++] = SuDoku;
                            //    if (tempSuDoku[i,j]+1 < SuDoku[i, j].Length)
                            //    {
                            //        increment = 0;
                            //    }
                            //    else
                            //    {
                            //        increment = -1;
                            //        tempSuDoku[i, j] = 0;
                            //    }
                            //    SuDoku[i, j][0] = 0;
                            //}
                        }
                    }
                }
            }
        }
        private void GetBoxs()
        {
            for (int i = 0; i < 81; i++)
                //if (this.Controls[i] is TextBox)
                {
                    SuDoku[i / 9, i % 9] = new int[1];
                    if (this.Controls[i].Text == "" || this.Controls[i].Text == "0")
                        SuDoku[i / 9, i % 9][0] = 0;
                    else
                    {
                        SuDoku[i / 9, i % 9][0] = Convert.ToInt16(this.Controls[i].Text);
                        this.Controls[i].BackColor = Color.Black;
                        this.Controls[i].ForeColor = Color.White;
                    }
                }
        }
        private void SetBoxs()
        {
            for (int i = 0; i < 81; i++)
                //if (this.Controls[i] is TextBox)
                {
                    this.Controls[i].Text = Convert.ToString(SuDoku[i / 9, i % 9][0]);
                }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 81; i++)
                //if (this.Controls[i] is TextBox)
                {
                    this.Controls[i].Text = "";
                    this.Controls[i].BackColor = Color.White;
                    this.Controls[i].ForeColor = Color.Black;
                }
        }
    }
}
