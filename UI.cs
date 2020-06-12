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

namespace MobyDick1_0
{
    public partial class UI : Form
    {
        public UI()
        {
            InitializeComponent();          
        }

        private void cmdAhab_Click(object sender, EventArgs e)
        {
            
            this.cmdAhab.Text = "Thar She Blows!";
            this.Refresh();

            List<string> distinct = new List<string>();
            List<int> cnt = new List<int>();
            GetBookWords(distinct, cnt);

            this.pbAhab.Visible = false;
            this.cmdAhab.Visible = false;
            this.button1.Left = 50;
            this.textBox1.Text = distinct[0];
            this.textBox1.Visible = true;
            this.cmdReadMe.Visible = true;
            this.cmdAdd.Visible = true;
            this.cmdRerun.Visible = true;

            string lines = "";
            for (int i = 0; i < 100; i++)
            {
                lines += FinalOutput(distinct, cnt);
            }
            this.textBox1.Text = lines;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void cmdReadMe_Click(object sender, EventArgs e)
        {
            string line;

            var readme = "";
            System.IO.StreamReader readFile =
                new System.IO.StreamReader("README.txt");

            while ((line = readFile.ReadLine()) != null)
            {
                readme += line + "\r\n";
            }

            readFile.Close();
            textBox1.Text = readme;
        }

        static string FinalOutput(List<string> word, List<int> count)
        {
            int max = count.Max();
            int index = count.IndexOf(max);
            string output = "The word " + word[index] + " appears " + count[index] + " times" + "\r\n";               
            word.Remove(word[index]);
            count.Remove(count[index]);
            return output;           
        }

        private void GetStopWords(List<string> stopWords)
        {
            string line;

            // Put the stop words in a list
            System.IO.StreamReader stopFile =
                new System.IO.StreamReader("stop-words.txt");

            while ((line = stopFile.ReadLine()) != null)
            {
                if ((line.IndexOf("#") == -1) && (line.Length > 0))
                {
                    stopWords.Add(TrimPunctuation(line));
                }
            }
            stopFile.Close();

            if (textBox2.Text != "")
            {
                var more = textBox2.Text;
                var words = more.Split(' ');

                foreach (var word in words)
                {
                    string newWord = word;
                    int there = stopWords.IndexOf(newWord);
                    if (there == -1)
                    {
                        stopWords.Add(newWord.ToLower());
                    }
                }
            }
        }


        private void GetBookWords(List<string> distinct, List<int> cnt)
        {
            string line;

            List<string> stopWords = new List<string>();
            GetStopWords(stopWords);

            string start = "";              // To find the book starting point
            System.IO.StreamReader file =
                new System.IO.StreamReader("mobydick.txt");

            // Use the first while loop to get to the start of the book
            while (((line = file.ReadLine()) != null) &&
                (start.IndexOf("—Whale Song") == -1))
            {
                start = line;
            }

            int distinctive = 0;
            bool keepReading = true;

            while (((line = file.ReadLine()) != null) && keepReading)
            {
                // Let's not process any empty lines
                // Annnnnd.... Stop after the epilog
                if (line.IndexOf("Gutenberg") != -1)
                {
                    keepReading = false;
                }
                else if (!IsEmpty(line))
                {
                    // Split the line into individual words
                    var words = line.Split(' ');

                    foreach (var word in words)
                    {
                        if (!IsEmpty(word))
                        {
                            List<string> processList = new List<string>();
                            CheckHyphen(processList, word);

                            for (int j = 0; j < processList.Count(); j++)
                            {
                                if (stopWords.IndexOf(processList[j]) == -1)
                                {
                                    int idx = distinct.IndexOf(processList[j]);
                                    if (idx == -1)
                                    {
                                        distinct.Add(processList[j]);
                                        cnt.Add(1);
                                        distinctive++;
                                    }
                                    else
                                    {
                                        cnt[idx]++;
                                    }
                                }   // End: if (stopWords...                              
                            }       // End: for (int j = 0;...
                        }           // End: if (!IsEmpty...
                    }               // End: foreach (var...
                }                   // End: else if (!IsEmpty...
            }                       // End: while (((line ...

            file.Close();
        }

        static String TrimPunctuation(string word)
        {
            string trimmed = word;

            string fWord = TrimFrontPunctuation(word);

            // A single punctuation mark check
            if (!IsEmpty(fWord))
                trimmed = TrimBackPunctuation(fWord);
            else
                trimmed = fWord;

            return trimmed;
        }

        static String TrimBackPunctuation(string word)
        {
            string trimmed = word;

            if (char.IsPunctuation(word[word.Count() - 1]))
                return trimmed.Substring(0, trimmed.Length - 1);

            return trimmed;
        }

        static String TrimFrontPunctuation(string word)
        {
            string trimmed = word;

            // Special check for a single char word that is a punctuation mark
            if ((char.IsPunctuation(trimmed[0])) && (trimmed.Length > 1))
            {
                trimmed = trimmed.Substring(1, trimmed.Length - 1);
            }
            else if (char.IsPunctuation(trimmed[0]))
            {
                trimmed = "";
            }

            return trimmed;
        }

        static bool IsEmpty(string input)
        {
            if (input == "")
                return true;
            else
                return false;
        }

        static void CheckHyphen(List<string> list, string word)
        {
            string trimmed = "";
            string preTrim = word;

            while ((trimmed != preTrim) && !IsEmpty(preTrim))
            {
                trimmed = preTrim;
                preTrim = TrimPunctuation(preTrim);
            }

            if (!IsEmpty(trimmed))
            {
                trimmed = trimmed.ToLower();
                int hyphen = trimmed.IndexOf("—");
                if (hyphen > -1)        // hyphen located
                {
                    string addString = trimmed.Substring(0, hyphen);
                    list.Add(addString);
                    string nextString = trimmed.Remove(0, addString.Length);

                    // Recursive call in the event that there are multiple hyphens
                    if (!IsEmpty(nextString))
                    {
                        CheckHyphen(list, nextString);
                    }
                }
                else
                {
                    list.Add(trimmed);      // Just add the word without a hyphen
                }
            }
        }

        private void pbAhab_Click(object sender, EventArgs e)
        {
            this.cmdAhab.Text = "Thar She Blows!";
            this.Refresh();

            List<string> distinct = new List<string>();
            List<int> cnt = new List<int>();
            GetBookWords(distinct, cnt);

            this.pbAhab.Visible = false;
            this.cmdAhab.Visible = false;
            this.button1.Left = 50;
            this.textBox1.Visible = true;
            this.cmdReadMe.Visible = true;
            this.cmdAdd.Visible = true;
            this.cmdRerun.Visible = true;

            string lines = "";
            for (int i = 0; i < 100; i++)
            {
                lines += FinalOutput(distinct, cnt);
            }
            this.textBox1.Text = lines;
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            textBox2.Visible = true;
            cmdToList.Visible = true;
            cmdRerun.Visible = false;
        }

        private void cmdToList_Click(object sender, EventArgs e)
        {
            cmdToList.Visible = false;
            textBox2.Visible = false;
            cmdRerun.Visible = true;

        }

        private void cmdRerun_Click(object sender, EventArgs e)
        {
            this.pbAhab.Visible = true;
            this.cmdAhab.Visible = true;
            this.button1.Visible = false;
            this.textBox1.Visible = false;
            this.cmdReadMe.Visible = false;
            this.cmdAdd.Visible = false;
            this.cmdRerun.Visible = false;
            this.Refresh();

            List<string> distinct = new List<string>();
            List<int> cnt = new List<int>();
            GetBookWords(distinct, cnt);

            string lines = "";
            for (int i = 0; i < 100; i++)
            {
                lines += FinalOutput(distinct, cnt);
            }
            this.textBox1.Text = lines;

            this.Refresh();

            this.pbAhab.Visible = false;
            this.cmdAhab.Visible = false;
            this.button1.Left = 50;
            this.textBox1.Visible = true;
            this.cmdReadMe.Visible = true;
            this.cmdAdd.Visible = true;
            this.cmdRerun.Visible = true;
            this.button1.Visible = true;
        }
    }
}
