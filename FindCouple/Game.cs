using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindCouple
{
    public partial class Game : Form
    {
        private int N, M;
        private List<Color> colors = new List<Color>();
        private Form1 forma;

        public Game(int n, int m, Form1 form)
        {
            InitializeComponent();
            this.N = n;
            this.M = m;
            this.forma = form;
            GenerateOfColors();

            this.Size = new Size(N * 70 + 70, M * 70 + 70);

        }

        private void GenerateOfColors()
        {
            Random random = new Random();
            for (int i = 0; i < N * M / 2; i++)
            {
                Color temp = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
                colors.Add(temp);
                colors.Add(temp);
            }
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.forma.Show();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            int y = 10, index = 0;
            for (int i = 0; i < M; i++)
            {
                int x = 25;
                for (int j = 0; j < N; j++)
                {
                    PictureBox picture = new PictureBox()
                    {
                        Size = new Size(70, 70),
                        Location = new Point(x, y),
                        BackColor = colors[index],
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    PictureBox whitePicture = new PictureBox()
                    {
                        Size = new Size(70, 70),
                        Location = new Point(x, y),
                        BackColor = Color.White,
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    //Controls.Add(whitePicture);
                    Controls.Add(picture);
                    x += 70;
                    index++;
                }
                y += 70;
            }
        }
    }
}
