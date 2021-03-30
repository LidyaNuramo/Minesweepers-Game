using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mine_Sweepers_Game
{
    public partial class Medium : Form
    {
        NewGame game;
        private String mine = "💣";
        private Button[,] buttons=new Button[9,9];
        private String flag= "🚩";
        private int flagged = 7;
        private int foundmines = 0;
        public Medium()
        {
            InitializeComponent();
            this.game = new NewGame(7,9,this);
            int i = 8;
            int j = 8; 
            foreach (Button s in Controls.OfType<Button>())
            {
                buttons[i, j] = s;
                if (j == 0)
                {
                    i = i - 1;
                    j = 9 - 1;
                }
                else
                {
                    j = j - 1;
                }
            }
            MineLeft.Text = flagged.ToString();
        }

        private void nameGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NGame();
        }

        void NGame()
        {
            foreach (Button s in this.Controls.OfType<Button>())
            {
                s.Text = "";
                s.Enabled = true;
                s.BackColor = Color.LightBlue;
            }
            flagged = 7;
            foundmines = 0;
            MineLeft.Text = flagged.ToString();
            this.game = new NewGame(7, 9, this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void BoxClicked(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            if (e.Button == MouseButtons.Left)
            {
                if (b.Text == flag)
                {
                    flagged = flagged + 1;
                    MineLeft.Text = flagged.ToString();
                }
                String text = game.indexMines(b);
                b.Text = text;
                b.Enabled = false;
                b.BackColor = Color.DarkGray;
                if (text == "")
                {
                    checkBoxes(b);
                }
                else if (text == mine)
                {
                    MessageBox.Show("You have hit a mine!! Game over!");
                    NGame();
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                flaggedBox(b);
            }
            
        }

        void flaggedBox(Button b)
        {
            if (b.Text == flag)
            {
                b.Text = "";
                flagged = flagged + 1;
                MineLeft.Text = flagged.ToString();
            }
            else
            {
                if (flagged > 0)
                {
                    b.Text = flag;
                    flagged = flagged - 1;
                    if (flagged == 0)
                    {
                        foreach (Button s in this.Controls.OfType<Button>())
                        {
                            if (s.Text == flag)
                            {
                                if (game.checkFlagged(s) == true)
                                {
                                    foundmines = foundmines + 1;
                                }
                            }
                            
                        }
                        if (foundmines == 7)
                        {
                            MineLeft.Text = flagged.ToString();
                            MessageBox.Show("You have won the game!!");
                            NGame();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The maximum amount of flagged boxes have been reached.");
                }
            }
            MineLeft.Text = flagged.ToString();
        }

        void checkBoxes(Button b)
        {
            int h;
            int l;
            int i = 0;
            int j = 0;
            String name = b.Name.ToString();
            for (h = 0; h < 9; h++)
            {
                for (l = 0; l < 9; l++)
                {
                    if (buttons[h, l].Name.ToString() == name)
                    {
                        i = h;
                        j = l;
                        goto beginindex;
                    }
                }
            }
            beginindex:
            String text = game.indexMines(buttons[i,j]);
            if (text != mine)
            {
                if (text == "")
                {
                    buttons[i,j].Text = text;
                    buttons[i,j].Enabled = false;
                    buttons[i, j].BackColor = Color.DarkGray;
                    if (i - 1 >= 0 && buttons[i - 1, j].Enabled == true)
                    {
                        checkBoxes(buttons[i - 1, j]);
                    }
                    if (j - 1 >= 0 && buttons[i, j - 1].Enabled == true)
                    {
                        checkBoxes(buttons[i, j - 1]);
                    }
                    if (j + 1 < 9 && buttons[i, j + 1].Enabled == true)
                    {
                        checkBoxes(buttons[i, j + 1]);
                    }
                    if (i + 1 < 9 && buttons[i + 1, j].Enabled == true)
                    {
                        checkBoxes(buttons[i + 1, j]);
                    }
                }
                else
                {
                    buttons[i,j].Text = text;
                    buttons[i,j].Enabled = false;
                    buttons[i, j].BackColor = Color.DarkGray;
                    return;
                }
                
            }
            else
            {
                return;
            }
        }
        private void Medium_Load(object sender, EventArgs e)
        {
               
        }

        private void aboutGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This app is created by Lidya Nuramo.");
        }

        private void howToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("How to Play: \n\t 1. Left-click on Buttons to reveal how mines are close to a single box \n\t 2. If you suspect that a box is a mine, right-click on it to flag it. \n\t 3. If all flagged boxes are mines, you win the game.");
        }

        
    }
}
