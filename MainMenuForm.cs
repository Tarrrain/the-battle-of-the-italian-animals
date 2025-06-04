using System;
using System.CodeDom.Compiler;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace проектстрашно4
{
    public partial class MainMenuForm : Form
    {
        string dir;
        public PictureBox picture1, picture2;
        int currentCharacter1 = 0, currentCharacter2 = 1;
        Player player1 = new Player();
        Player player2 = new Player();
        TextBox nameBox1, nameBox2;

        public MainMenuForm()
        {
            InitializeComponent();
            dir = Directory.GetCurrentDirectory();
            picture1 = new PictureBox { Size = new Size(150, 150), Location = new Point(20, 20), Image = Image.FromFile(dir + Characters[0]), SizeMode = PictureBoxSizeMode.StretchImage };
            picture2 = new PictureBox { Size = new Size(150, 150), Location = new Point(355, 20), Image = Image.FromFile(dir + Characters[1]), SizeMode = PictureBoxSizeMode.StretchImage };
            this.Controls.Add(picture1);
            this.Controls.Add(picture2);
            Button playButton = new Button { Text = "Play", Size = new Size(200, 50), Location = new Point(163, 200) };
            playButton.Click += PlayButton_Click;
            this.Controls.Add(playButton);

            Button swapButton = new Button { Text = "Swap", Size = new Size(200, 50), Location = new Point(163, 70) };
            swapButton.Click += SwapButton_Click;
            this.Controls.Add(swapButton);

            Button change1Button = new Button
            {
                Location = new Point(180, 130),
                Size = new Size(70, 30),
                Text = "change",

            };
            change1Button.Click += Change1Button_Click;
            this.Controls.Add(change1Button);
            Button change2Button = new Button
            {
                Location = new Point(275, 130),
                Size = new Size(70, 30),
                Text = "change",

            };
            change2Button.Click += Change2Button_Click;
            this.Controls.Add(change2Button);

            nameBox1 = new TextBox { Size = new Size(150, 20), Location = new Point(20, 170), Text = "игрок 1" };
            nameBox2 = new TextBox { Size = new Size(150, 20), Location = new Point(355, 170), Text = "игрок 2" };
            this.Controls.Add(nameBox1);
            player1.Name = nameBox1.Text;
            this.Controls.Add(nameBox2);
            player2.Name = nameBox2.Text;
        }

        private void Change2Button_Click(object sender, EventArgs e)
        {
            currentCharacter2 = (currentCharacter2 + 1) % Characters.Length; 
            if (currentCharacter2 == currentCharacter1)
            {
                currentCharacter2 = (currentCharacter2 + 1) % Characters.Length;
            }

            picture2.Image = Image.FromFile(dir + Characters[currentCharacter2]);
        }

        private void Change1Button_Click(object sender, EventArgs e)
        {
            currentCharacter1 = (currentCharacter1 + 1) % Characters.Length;
            if (currentCharacter1 == currentCharacter2)
            {
                currentCharacter1 = (currentCharacter1 + 1) % Characters.Length;
            }

            picture1.Image = Image.FromFile(dir + Characters[currentCharacter1]);
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            string name1 = nameBox1.Text.Trim();
            string name2 = nameBox2.Text.Trim();

            if (string.IsNullOrEmpty(name1) || string.IsNullOrEmpty(name2))
            {
                MessageBox.Show("Без шуточек! имена на базу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (name1 == name2)
            {
                MessageBox.Show("Без шуточек! имена игроков не должны совпадать", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            player1.Name = name1;
            player2.Name = name2;

            player1.CurrentCharacter = currentCharacter1;//сохранение выбранных персонажей
            player2.CurrentCharacter = currentCharacter2;

            Form1 gameForm = new Form1(currentCharacter1, currentCharacter2, player1, player2);
            gameForm.Show();
            this.Hide();
        }

        string[] Characters = new string[8] { "\\bananini_right.jpg", "\\kokokosini_right.jpg", "\\tralalerotralala_right.jpg", "\\lirililarela_right.jpg", "\\frigo_right.jpg",
            "\\tung_right.jpg", "\\bombordiro_right.jpg","\\bombombini_right.jpg"};
        private void SwapButton_Click(object sender, EventArgs e)
        {
            int temp = currentCharacter1;
            currentCharacter1 = currentCharacter2;
            currentCharacter2 = temp;

            picture1.Image = Image.FromFile(dir + Characters[currentCharacter1]);
            picture2.Image = Image.FromFile(dir + Characters[currentCharacter2]);
        }
    }
}
