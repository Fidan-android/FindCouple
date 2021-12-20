using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.forma.Show();
        }

        List<PictureBox> pictureBoxes = new List<PictureBox>();
        private void Game_Load(object sender, EventArgs e)
        {
            GenerateOfColors();
            this.Size = new Size(N * 70 + 140, M * 70 + 210);
            createGameOptions();
            int y = 70, index = 0;
            for (int i = 0; i < M; i++)
            {
                int x = 70;
                for (int j = 0; j < N; j++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        Random random = new Random();
                        index = random.Next(0, colors.Count);
                    }
                    Console.WriteLine(index);

                    PictureBox picture = new PictureBox()
                    {
                        Size = new Size(70, 70),
                        Location = new Point(x, y),
                        BackColor = colors[index],

                        BorderStyle = BorderStyle.FixedSingle
                    };

                    colors.RemoveAt(index);
                    pictureBoxes.Add(picture);

                    PictureBox whitePicture = new PictureBox()
                    {
                        Size = new Size(70, 70),
                        Location = new Point(x, y),
                        BackColor = Color.White,
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    whitePicture.Click += whiteClick;
                    Controls.Add(whitePicture);
                    Controls.Add(picture);
                    x += 70;
                }
                y += 70;
            }

            timer2.Start();
        }

        private void GenerateOfColors()
        {
            Random random = new Random();
            for (int i = 0; i < N * M / 2; i++)
            {
                Color temp = Color.White;
                for (int k = 0; k < N * M; k++)
                {
                    temp = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
                }
                colors.Add(temp);
                colors.Add(temp);
            }
        }

        private void createGameOptions()
        {
            switch(N)
            {
                case 2: case 3: case 4: {
                        label1.Font = new Font("Palatino Linotype", 18);
                        label2.Font = new Font("Palatino Linotype", 18);
                        label1.Location = new Point(N * 70 + (70 - label1.Width), 0);
                        label2.Location = new Point(0, 0);
                        break;
                    }
                case 5: case 6: case 7: {
                        label1.Font = new Font("Palatino Linotype", 20);
                        label2.Font = new Font("Palatino Linotype", 20);
                        label1.Location = new Point(N * 70 + (70 - label1.Width), 0);
                        label2.Location = new Point(0, 0);
                        break;
                    }
                case 8: case 9: {
                        label1.Font = new Font("Palatino Linotype", 24);
                        label2.Font = new Font("Palatino Linotype", 24);
                        label1.Location = new Point(N * 70 + (70 - label1.Width), 0);
                        label2.Location = new Point(0, 0);
                        break;
                    }
            }

            label3.Font = new Font("Palatino Linotype", 18);
            label3.Location = new Point(0, M * 70 + (70 + label3.Height));
        }

        int numberOfBlocksShown = 0;
        PictureBox savedPictureBox = new PictureBox();
        List<PictureBox> lastWhiteBoxes = new List<PictureBox>();
        private int timeOut = 300, numberOfOpenColors = 0, numberOfSteps = 0;
        private bool isAllowedPress = true;

        public void whiteClick(object sender, EventArgs e)
        {
            if (numberOfBlocksShown < 2 && isAllowedPress)
            {
                numberOfSteps++;
                label3.Text = "Ходов: " + numberOfSteps.ToString();

                PictureBox tempBox = (PictureBox)sender;

                tempBox.Visible = false;
                numberOfBlocksShown++;


                if (numberOfBlocksShown == 2)
                {
                    foreach (PictureBox pb in pictureBoxes)
                    {
                        if (tempBox.Location == pb.Location)
                        {
                            if (savedPictureBox.BackColor == pb.BackColor)
                            {
                                numberOfBlocksShown -= 2;
                                lastWhiteBoxes.Clear();
                                numberOfOpenColors++;
                                label2.Text = "Открытых: " + numberOfOpenColors.ToString();
                            }
                            else
                            {
                                lastWhiteBoxes.Add(tempBox);
                                timer1.Start();
                            }
                        }
                    }
                    return;
                }

                foreach (PictureBox pb in pictureBoxes)
                {
                    if (tempBox.Location == pb.Location)
                    {
                        savedPictureBox = pb;
                    }
                }

                lastWhiteBoxes.Add(tempBox);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timeOut -= 1;
            TimeSpan ts = TimeSpan.FromSeconds(timeOut);
            label1.Text = string.Format("{0}:{1}", ts.Minutes, ts.Seconds); ;
            if (timeOut == 0)
            {
                isAllowedPress = !isAllowedPress;
                timer2.Stop();
                if (M*N /2 != numberOfOpenColors)
                {
                    MessageBox.Show("Время истекло, к сожалению вы не смогли открыть все цвета=(");
                    this.Close();
                }
            }

            if (M * N / 2 == numberOfOpenColors)
            {
                timer2.Stop();
                MessageBox.Show("Поздравляем, вы выиграли!!");
                this.Close();
            }
        }

        private void StartNewGame(object sender, EventArgs e)
        {
            new Game(N + 1, M + 1, forma).ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (PictureBox pb in lastWhiteBoxes)
            {
                pb.Visible = true;
            }

            numberOfBlocksShown -= 2;
            timer1.Stop();
        }
    }
}
