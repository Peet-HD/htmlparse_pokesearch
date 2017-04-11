using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using HtmlAgilityPack;

namespace Htmlparse
{
    public partial class Form1 : Form
    {
        Label[] _Labels = new Label[4];
        PictureBox[] _PictureBox = new PictureBox[4];

        public Form1()
        {
            InitializeComponent();
            _Labels[0] = this.label1;
            _Labels[1] = this.label2;
            _Labels[2] = this.label6;
            _Labels[3] = this.label7;
            _PictureBox[0] = this.pictureBox1;
            _PictureBox[1] = this.pictureBox2;
            _PictureBox[2] = this.pictureBox3;
            _PictureBox[3] = this.pictureBox4;
            textBox1.KeyDown += (sender, args) =>
            {
                if (args.KeyCode == Keys.Return)
                {
                    button1.PerformClick();
                }
            };
        }


        private void button1_Click(object sender, EventArgs e)
        {
            String s = textBox1.Text;
            HtmlWeb client = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = client.Load("http://www.pokewiki.de/" + s);
            HtmlNodeCollection node = doc.DocumentNode.SelectNodes("/html[1]/body[1]/div[2]/div[1]/div[4]/div[1]/div[2]/div[4]/table[@class=\"right round innerround\"]/tr/td/a[@href]");
            HtmlNodeCollection nodealola = doc.DocumentNode.SelectNodes("/html[1]/body[1]/div[2]/div[1]/div[4]/div[1]/div[2]/div[4]/table[@class=\"right round innerround\"]/tr/td/span/a[@href]");
            HtmlNodeCollection nodeextra = doc.DocumentNode.SelectNodes("/html[1]/body[1]/div[2]/div[1]/div[4]/div[1]/div[2]/div[4]/table[@class=\"right round innerround\"]/tr/td/span[3]");
            _PictureBox[0].Image = null;
            _PictureBox[1].Image = null;
            _PictureBox[2].Image = null;
            _PictureBox[3].Image = null;
            pictureBox5.Image = null;
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";
            int x = 0;
            try
            {
                foreach (var link in node)
                {
                    if (link.Attributes["title"].Value == "Normal" || link.Attributes["title"].Value == "Kampf" || link.Attributes["title"].Value == "Flug" ||
                        link.Attributes["title"].Value == "Gift" || link.Attributes["title"].Value == "Boden" || link.Attributes["title"].Value == "Gestein" ||
                            link.Attributes["title"].Value == "Käfer" || link.Attributes["title"].Value == "Geist" || link.Attributes["title"].Value == "Stahl" ||
                            link.Attributes["title"].Value == "Feuer" || link.Attributes["title"].Value == "Wasser" || link.Attributes["title"].Value == "Pflanze" ||
                            link.Attributes["title"].Value == "Elektro" || link.Attributes["title"].Value == "Psycho" || link.Attributes["title"].Value == "Eis" ||
                            link.Attributes["title"].Value == "Drache" || link.Attributes["title"].Value == "Unlicht" || link.Attributes["title"].Value == "Fee")
                    {
                        String picture = link.InnerHtml;
                        int start, end;
                        start = picture.IndexOf("src=\"") + 5;
                        end = picture.IndexOf("\" width");

                        _PictureBox[x].ImageLocation = "http://www.pokewiki.de" + picture.Substring(start, end - start);
                        _Labels[x].Text = link.Attributes["title"].Value;

                        x++;
                    }
                }
                if (x == 1)
                {
                    label2.Text = "N/A";
                    pictureBox2.Image = null;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Pokemon wurde nicht gefunden");
            }
            int y = 0;
            try
            {
                foreach (var link in nodealola)
                {
                    if (link.Attributes["title"].Value == "Normal" || link.Attributes["title"].Value == "Kampf" || link.Attributes["title"].Value == "Flug" ||
                        link.Attributes["title"].Value == "Gift" || link.Attributes["title"].Value == "Boden" || link.Attributes["title"].Value == "Gestein" ||
                            link.Attributes["title"].Value == "Käfer" || link.Attributes["title"].Value == "Geist" || link.Attributes["title"].Value == "Stahl" ||
                            link.Attributes["title"].Value == "Feuer" || link.Attributes["title"].Value == "Wasser" || link.Attributes["title"].Value == "Pflanze" ||
                            link.Attributes["title"].Value == "Elektro" || link.Attributes["title"].Value == "Psycho" || link.Attributes["title"].Value == "Eis" ||
                            link.Attributes["title"].Value == "Drache" || link.Attributes["title"].Value == "Unlicht" || link.Attributes["title"].Value == "Fee")
                    {
                        String picture = link.InnerHtml;
                        int start, end;
                        start = picture.IndexOf("src=\"") + 5;
                        end = picture.IndexOf("\" width");
                        label5.Text = "Alola oder Mega-Entwicklung:";
                        label8.Text = "Typ2:";
                        label9.Text = "Typ1:";
                        _PictureBox[y + 2].ImageLocation = "http://www.pokewiki.de" + picture.Substring(start, end - start);
                        _Labels[y + 2].Text = link.Attributes["title"].Value;
                        y++;
                    }
                    if (y == 1)
                    {
                        _PictureBox[y + 2].ImageLocation = null;
                        _Labels[y + 2].Text = "N/A";
                    }
                }
            }
            catch (Exception) { }
            try
            {
                HtmlNodeCollection nodeThumb = doc.DocumentNode.SelectNodes("/html[1]/body[1]/div[2]/div[1]/div[4]/div[1]/div[2]/div[4]/table[@class=\"right round innerround\"]/tr[1]/td[2]/img[@src]");
                foreach (var z in nodeThumb)
                {
                    pictureBox5.ImageLocation = "http://www.pokewiki.de" + z.Attributes["src"].Value;
                }
            }
            catch (Exception) { }

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
