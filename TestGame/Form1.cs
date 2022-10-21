using System;

namespace TestGame
{
    public partial class Form1 : Form
    {
        private enum colorOfButton
        {
            Black = 4,
            White = 0,
            Red = 1,
            Blue = 2,
            Green = 3
        };

        private static Color[] _colors = { Color.White, Color.Red, Color.Blue, Color.Green, Color.Black };
        private static int[,] _field = new int[6, 6];
        private static List<Button> _buttons = new List<Button>();
        private Button? firstSelectedButton = null;
        private Button? secondSelectedButton = null;
        private Label? label = null;
        public Form1()
        {
            _buttons = InitializeComponent();
            CreateField();
        }

        private void CreateField()
        {
            int[] color = { 5,5,5};
            _field[1, 2] = (int)colorOfButton.Black;
            _field[1, 4] = (int)colorOfButton.Black;
            _field[3, 2] = (int)colorOfButton.Black;
            _field[3, 4] = (int)colorOfButton.Black;
            _field[5, 2] = (int)colorOfButton.Black;
            _field[5, 4] = (int)colorOfButton.Black;
            _field[2, 2] = (int)colorOfButton.White;
            _field[2, 4] = (int)colorOfButton.White;
            _field[4, 2] = (int)colorOfButton.White;
            _field[4, 4] = (int)colorOfButton.White;
            Random random = new Random();
            for (int i = 1; i<=5; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    if (j % 2 == 0) continue;
                    int rand = random.Next() % 3;
                    while (color[rand] == 0)
                    {
                        rand = (rand + 1) % 3;
                    }
                    _field[i, j] = rand + 1;
                    color[rand] -= 1;
                }
            }
            foreach(Button button in _buttons)
            {
                button.BackColor = _colors[_field[int.Parse(button.Name[1].ToString()), int.Parse(button.Name[2].ToString())]];
            }
        }

        private void b_Click(object sender, EventArgs e)
        {
            bool pointOfWin = false;
            if(firstSelectedButton == null)
            {
                firstSelectedButton = (Button)sender;
                firstSelectedButton.Text = "#";
            }
            else
            {
                secondSelectedButton = (Button)sender;
                if (trySwap())
                {
                    pointOfWin = checkWin();
                }
                firstSelectedButton.Text = "";
                firstSelectedButton = null;
                secondSelectedButton = null;
            }
            if (pointOfWin) Win();
        }
        private void reload_Click(object sender, EventArgs e)
        {
            CreateField();
        }
        private bool trySwap()
        {
            int i1 = int.Parse(firstSelectedButton.Name[1].ToString());
            int j1 = int.Parse(firstSelectedButton.Name[2].ToString());
            int i2 = int.Parse(secondSelectedButton.Name[1].ToString());
            int j2 = int.Parse(secondSelectedButton.Name[2].ToString());
            if (!((Math.Abs(i2 - i1) == 1 && Math.Abs(j2 - j1) == 0) || (Math.Abs(i2 - i1) == 0 && Math.Abs(j2 - j1) == 1))) return false;
            if (j1 % 2 == 0 & i1 % 2 == 1) return false;
            if (j2 % 2 == 0 & i2 % 2 == 1) return false;
            if (firstSelectedButton.BackColor == Color.Black) return false;
            if (secondSelectedButton.BackColor == Color.Black) return false;
            if (firstSelectedButton.BackColor != Color.White && secondSelectedButton.BackColor != Color.White) return false;
            firstSelectedButton.BackColor = _colors[_field[i2, j2]];
            secondSelectedButton.BackColor = _colors[_field[i1, j1]];
            int a = _field[i1, j1];
            _field[i1, j1] = _field[i2, j2];
            _field[i2, j2] = a;
            return true;
        }
        private bool checkWin()
        {
            for(int i =1; i<=5; i++)
            {
                if (_field[i, 1] != 3) return false;
                if (_field[i, 3] != 1) return false;
                if (_field[i, 5] != 2) return false;
            }
            return true;
        }

        private void Win()
        {
            label = new Label();
            label.BackColor = Color.Red;
            label.Text = "WIN!!!";
            label.Size = new Size(this.Width, this.Height);
            label.Location = new Point(0, 0);
            this.Controls.Add(label);
            this.Update();
            Restart();
        }

        private void Restart()
        {
            Thread.Sleep(10000);
            this.Controls.Remove(label);
            CreateField();
        }
    }
}