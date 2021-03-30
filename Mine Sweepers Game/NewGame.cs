using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mine_Sweepers_Game
{
    class NewGame
    {
        private int minecount;
        private String mine = "💣";
        private Button[,] buttons;
        private Form form1;
        private int index;

        public NewGame(int mines, int size, Form form)
        {
            int i, j;
            this.form1 = form;
            this.minecount=mines;
            this.buttons = new Button[size, size];
            i = size-1;
            j = size-1;
            this.index = size;
            foreach (Button s in form1.Controls.OfType<Button>())
            {
                buttons[i, j] = s;
                if (j == 0)
                {
                    i = i - 1;
                    j = index-1;
                }
                else
                {
                    j = j - 1;
                }
            }
            assignIndex();
            assignMines();

        }

        void assignMines()
        {
            int l, m;
            int k = minecount;
            while (k>0)
            {
                startpoint:
                Random mines = new Random();
                l = mines.Next(0, index);
                m = mines.Next(0, index);
                try
                {
                    if (buttons[l, m].Tag.ToString() != mine)
                    {
                        buttons[l, m].Tag = (object) mine;
                        k = k - 1;
                    }
                    else
                    {
                        goto startpoint;
                    }
                }
                catch
                {
                    goto startpoint;
                }
            }
        }

        void assignIndex()
        {
            for (int l=0; l< index; l++)
            {
                for (int m=0; m<index; m++)
                {
                    buttons[l, m].Tag = (object)"0";
                }
            }
        }

        public Boolean checkFlagged(Button b)
        {
            int h;
            int l;
            int i = 0;
            int j = 0;
            String name = b.Name.ToString();
            for (h = 0; h < index; h++)
            {
                for (l = 0; l < index; l++)
                {
                    if (buttons[h, l].Name.ToString() == name)
                    {
                        i = h;
                        j = l;
                        goto check;
                    }
                }
            }

            check:
            if (buttons[i, j].Tag.ToString() == mine)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public String indexMines(Button button)
        {
            int h;
            int l;
            int i = 0;
            int j = 0;
            String name = button.Name.ToString();
            for (h = 0; h < index; h++)
            {
                for (l = 0; l < index; l++)
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
            if (buttons[i, j].Tag.ToString() != mine)
            {
                
                if (i - 1 >= 0)
                {
                    if (buttons[i - 1, j].Tag.ToString() == mine)
                    {
                        buttons[i, j].Tag = (object) (Convert.ToInt16(buttons[i, j].Tag) + 1).ToString();
                    }
                    if (j - 1 >= 0)
                    {
                        if (buttons[i - 1, j - 1].Tag.ToString() == mine)
                        {
                            buttons[i, j].Tag = (object)(Convert.ToInt16(buttons[i, j].Tag) + 1).ToString();
                        }
                    }
                    if (j + 1 < index)
                    {
                        if (buttons[i - 1, j + 1].Tag.ToString() == mine)
                        {
                            buttons[i, j].Tag = (object)(Convert.ToInt16(buttons[i, j].Tag) + 1).ToString();
                        }
                    }
                }
                if (j - 1 >= 0)
                {
                    if (buttons[i, j - 1].Tag.ToString() == mine)
                    {
                        buttons[i, j].Tag = (object)(Convert.ToInt16(buttons[i, j].Tag) + 1).ToString();
                    }
                }
                if (j + 1 < index)
                {
                    if (buttons[i, j + 1].Tag.ToString() == mine)
                    {
                        buttons[i, j].Tag = (object)(Convert.ToInt16(buttons[i, j].Tag) + 1).ToString();
                    }
                }
                if (i + 1 < index)
                {
                    if (buttons[i + 1, j].Tag.ToString() == mine)
                    {
                        buttons[i, j].Tag = (object)(Convert.ToInt16(buttons[i, j].Tag) + 1).ToString();
                    }
                    if (j - 1 >= 0)
                    {
                        if (buttons[i + 1, j - 1].Tag.ToString() == mine)
                        {
                            buttons[i, j].Tag = (object)(Convert.ToInt16(buttons[i, j].Tag) + 1).ToString();
                        }
                    }
                    if (j + 1 < index)
                    {
                        if (buttons[i + 1, j + 1].Tag.ToString() == mine)
                        {
                            buttons[i, j].Tag = (object)(Convert.ToInt16(buttons[i, j].Tag) + 1).ToString();
                        }
                    }
                }
                if (buttons[i, j].Tag.ToString() == "0")
                {
                    return "";
                }
                else
                {
                    return buttons[i, j].Tag.ToString();
                }
            }
            else
            {
                return mine;
            }
        }
    }
}