using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace DesktopApp1
{
    public partial class Form1 : Form
    {
        

        public int medFunction(String str1, String str2, int m, int n)
        {
            // Create a table to store results of subproblems 
            int[,] dp = new int[m + 1, n + 1];

            // Fill d[][] in bottom up manner 
            for (int i = 0; i <= m; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    // If first string is empty, only option is to  insert all characters of second string 
                    if (i == 0)

                        dp[i, j] = j;

                    // If second string is empty, only option is to remove all characters of second string 
                    else if (j == 0)
                        dp[i, j] = i;

                    // If last characters are same, ignore last char and recur for remaining string 
                    else if (str1[i - 1] == str2[j - 1])
                        dp[i, j] = dp[i - 1, j - 1];

                    // If the last character is different, consider all possibilities and find the minimum 
                    else
                        dp[i, j] = 1 + minValue(dp[i, j - 1], // Insert 
                                       dp[i - 1, j], // Remove 
                                       dp[i - 1, j - 1]); // Replace 
                }
            }

            return dp[m, n];
        }

        public int minValue(int x, int y, int z)
        {
            if (x <= y && x <= z) return x;
            if (y <= x && y <= z) return y;
            else return z;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            List<string> wordList = new List<string>();
            foreach (string line in File.ReadLines("sozluk.txt", Encoding.GetEncoding("Windows-1254")))
            {
                wordList.Add(line);
            }

            string myString = textBox1.Text;
            string[] myWords = myString.ToLower().Split(' ');
            int[] indexArray = new int[myWords.Length];


            Queue<string> alternatives = new Queue<string>();
            List<string> resultList = new List<string>();
            List<string> resultsList = new List<string>();

            for (int i = 0; i < myWords.Length; i++)
            {
                if (wordList.Contains(myWords[i]))
                {
                    continue;
                }
                int minimumValue = 100;
                for (int j = 0; j < wordList.Count; j++)
                {
                    int temp = (medFunction(myWords[i], wordList[j], myWords[i].Length, wordList[j].ToString().Length));

                    if (minimumValue >= temp)
                    {
                        minimumValue = temp;
                        indexArray[i] = j;

                        if (alternatives.Count >= 5)
                        {
                            alternatives.Dequeue();
                            alternatives.Enqueue(wordList[j]);
                        }
                        else
                        {
                            alternatives.Enqueue(wordList[j]);
                        }
                    }
                }

                string result = myWords[i] + " -------->> ";


                alternatives = new Queue<string>(alternatives.Reverse());


                while (alternatives.Count > 0)
                {
                    listBox1.Items.Add(alternatives.Dequeue());


                }

            }

            stopWatch.Stop();

            label4.Text = stopWatch.ElapsedMilliseconds.ToString() + " ms";

        }

        private void helloWorldLabel_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
