using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace проектстрашно4
{
    public partial class Form1 : Form
    {
        string dir;
        public bool goLeft, goRight;
        public Player player1 = new Player(), player2 = new Player();
        public Label healthLabel1, healthLabel2;

        private void timer_Tick(object sender, EventArgs e)
        {
            PlayerMove(player1);
            PlayerMove(player2);

            if (player1.IsDead || player2.IsDead)
            {
                timer.Stop();
                string winner = player1.Health > 0 ? player1.Name : player2.Name;
                MessageBox.Show($"Победитель: {winner}", "Игра окончена",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetGame();
            }

            LabelScaleGenerate(player1, healthLabel1);
            LabelScaleGenerate(player2, healthLabel2);
            if (player1.IsDodging && (DateTime.Now - player1.DodgeStartTime).TotalMilliseconds >= Player.DodgeDuration)
            {
                player1.EndDodge();
            }

            if (player2.IsDodging && (DateTime.Now - player2.DodgeStartTime).TotalMilliseconds >= Player.DodgeDuration)
            {
                player2.EndDodge();
            }

            ProcessAttack(player1);
            ProcessAttack(player2);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    player1.goLeft = false;
                    break;
                case Keys.J:
                    player2.goLeft = false;
                    break;
                case Keys.D:
                    player1.goRight = false;
                    break;
                case Keys.L:
                    player2.goRight = false;
                    break;
                case Keys.S:
                    player1.EndDodge();
                    break;
                case Keys.K:
                    player2.EndDodge();
                    break;
            }
        }

        public Form1(int char1Index, int char2Index, Player p1, Player p2)
        {
            InitializeComponent();

            dir = Directory.GetCurrentDirectory();

            //form
            this.Size = new Size(720, 420); healthLabel1 = new Label { Location = new Point(5, 5) };
            string[] BackgroundImages = { "\\plazh.jpg", "\\trava.jpg", "\\cenario.jpg", "\\nightcity.jpg" };
            BackgroundImageLayout = ImageLayout.Stretch;
            Random random = new Random();
            BackgroundImage = Image.FromFile(dir + BackgroundImages[random.Next(0, 3)]);
            healthLabel2 = new Label { Location = new Point(600, 5) };
            this.Controls.Add(healthLabel1);
            this.Controls.Add(healthLabel2);
            Button resetButton = new Button { Size = new Size(100, 20), Location = new Point(5, 30), Text = "Reset game" };
            resetButton.Click += ResetButton_Click;
            this.Controls.Add(resetButton);
            CreateHelpPanel(10, 50, "F", "Q", "A", "D", "S");
            CreateHelpPanel(600, 50, "H", "O", "J", "L", "K");

            //player
            player1 = p1;
            player2 = p2;
            player1.UpdateDirection(player1.LastDirection == 1);
            player2.UpdateDirection(player2.LastDirection == 1);
            CreateCharacterPictureBox(player1, 50, 250);
            CreateCharacterPictureBox(player2, 500, 250);

            player1.UltimateIndicator = new PictureBox { Size = new Size(20, 20), Location = new Point(10, ClientSize.Height - 30), BackColor = Color.Gray };
            player2.UltimateIndicator = new PictureBox { Size = new Size(20, 20), Location = new Point(ClientSize.Width - 30, ClientSize.Height - 30), BackColor = Color.Gray };
            this.Controls.Add(player1.UltimateIndicator); this.Controls.Add(player2.UltimateIndicator);
            player1.UltimateIndicator.BringToFront(); player2.UltimateIndicator.BringToFront();

            timer.Start();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        public void ResetGame()
        {
            MainMenuForm mainMenuForm = new MainMenuForm();
            mainMenuForm.Show();
            this.Close();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:

                    player1.goLeft = true;
                    player1.UpdateDirection(false);
                    break;
                case Keys.J:
                    player2.goLeft = true;
                    player2.UpdateDirection(false);
                    break;
                case Keys.D:
                    player1.goRight = true;
                    player1.UpdateDirection(true);
                    break;
                case Keys.L:
                    player2.goRight = true;
                    player2.UpdateDirection(true);
                    break;
                case Keys.S:
                    if (!player1.IsDodging)
                        player1.StartDodge();
                    break;
                case Keys.K:
                    if (!player2.IsDodging)
                        player2.StartDodge();
                    break;
                case Keys.F when !player1.IsDodging:
                    StartAttack(player1);
                    break;
                case Keys.H when !player2.IsDodging:
                    StartAttack(player2);
                    break;
                case Keys.Q when !player1.IsDodging:
                    StartUltimateAttack(player1);
                    break;
                case Keys.O when !player2.IsDodging:
                    StartUltimateAttack(player2);
                    break;
            }
        }

        public void LabelScaleGenerate(Player player, Label healthLabel)
        {
            healthLabel.BackColor = Color.Green;
            healthLabel.Size = new Size(player.Health, 20);
            healthLabel.Text = player.Health.ToString();
            healthLabel.ForeColor = Color.Black;

            if (player.Health < 70 && player.Health > 30)
            {
                healthLabel.BackColor = Color.Yellow;
            }
            else if (player.Health <= 30)
            {
                healthLabel.BackColor = Color.DarkRed;
            }
        }

        public void PlayerMove(Player player)
        {
            if (player.goLeft && !player.goRight)
            {
                player.LastDirection = -1;
                player.UpdateDirection(false);
            }
            else if (player.goRight && !player.goLeft)
            {
                player.LastDirection = 1;
                player.UpdateDirection(true);
            }

            int leftBound = 0;
            int rightBound = this.ClientSize.Width - player.picture.Width;

            if (player.goLeft && player.picture.Left > leftBound)
            {
                player.picture.Left -= player.Speed;
            }
            else if (player.goRight && player.picture.Left < rightBound)
            {
                player.picture.Left += player.Speed;
            }

            if (player.IsDodging)
            {
                if (player.picture.Height > 80)
                {
                    int newHeight = 80;
                    int heightDiff = player.picture.Height - newHeight;
                    player.picture.Height = newHeight;
                    player.picture.Top += heightDiff;
                }
            }
            else if (player.picture.Height < 120)
            {
                int newHeight = 120;
                int heightDiff = newHeight - player.picture.Height;
                player.picture.Height = newHeight;
                player.picture.Top -= heightDiff;
            }
        }
        public void CreateCharacterPictureBox(Player player, int x, int y)
        {
            player.picture = new PictureBox
            {
                Size = new Size(120, 120),
                Location = new Point(x, y),
                BackColor = Color.DarkGray,
                Tag = "player",
                Image = Image.FromFile(dir + player.CharactersRight[player.CurrentCharacter]),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            this.Controls.Add(player.picture);
        }

        private void CreateHelpPanel(int x, int y, string attackButton,
            string ultimateAttackButton, string left, string right, string down)
        {
            Label helpPanel = new Label
            {
                Size = new Size(100, 200),
                Location = new Point(x, y),
                BackColor = Color.Transparent,
                Text = $"{attackButton} - атака\n{ultimateAttackButton} - ульт\n{left} - влево\n{right} - вправо\n{down} - уворот",
                ForeColor = Color.White
            };
            this.Controls.Add(helpPanel);
        }
        private List<PictureBox> player1Attacks = new List<PictureBox>();
        private List<PictureBox> player2Attacks = new List<PictureBox>();
        public void StartAttack(Player player)
        {
            if (player.IsDodging ||
                (DateTime.Now - player.LastAttackTime).TotalMilliseconds < player.AttackCooldown)
                return;

            player.RegisterAttack();
            player.AttackDirection = player.LastDirection;
            player.LastAttackTime = DateTime.Now;

            var attack = new PictureBox
            {
                Size = new Size(40, 40),
                SizeMode = PictureBoxSizeMode.StretchImage,
                //Image = player.picture.Image,
                Tag = "attack_" + (player == player1 ? "p1" : "p2"),
                BackColor = Color.Red //!
            };

            if (player.AttackDirection == 1)
                attack.Left = player.picture.Right;
            else
                attack.Left = player.picture.Left - attack.Width;

            attack.Top = player.picture.Top + player.picture.Height / 2 - attack.Height / 2;
            this.Controls.Add(attack);
            attack.BringToFront();

            if (player == player1)
                player1Attacks.Add(attack);
            else
                player2Attacks.Add(attack);
        }

        private void ProcessAttack(Player player)
        {

            var attacks = player == player1 ? player1Attacks : player2Attacks;

            for (int i = attacks.Count - 1; i >= 0; i--)
            {
                var attack = attacks[i];
                attack.Left += player.BaseAttackSpeed * player.AttackDirection;

                // атака вышла за границы
                if (attack.Right < 0 || attack.Left > ClientSize.Width)
                {
                    SafeRemoveControl(attack);
                    attacks.RemoveAt(i);
                    continue;
                }
                //столкновение с противником
                Player opponent = player == player1 ? player2 : player1;
                if (attack.Bounds.IntersectsWith(opponent.picture.Bounds) && !opponent.IsDodging)
                {
                    opponent.Health -= player.AttackDamage;
                    SafeRemoveControl(attack);
                    attacks.RemoveAt(i);
                }
            }

        }
        private void SafeRemoveControl(Control control)
        {
            if (control != null && this.Controls.Contains(control))
            {
                this.Controls.Remove(control);
                control.Dispose();
            }
        }

        private void StartUltimateAttack(Player player)
        {
            if (!player.IsUltimateReady || player.IsDodging)
                return;
            player.UseUltimate();
            var ultimateAttack = new PictureBox
            {
                Size = new Size(50, 30),
                BackColor = Color.Gold,
                Tag = "ultimate_" + (player == player1 ? "p1" : "p2"),
                Location = new Point(
                player.AttackDirection == 1 ? player.picture.Right : player.picture.Left - 50,
                player.picture.Top + 40)
            };
            this.Controls.Add(ultimateAttack);
            ultimateAttack.BringToFront();

            Timer ultimateTimer = new Timer { Interval = 30 };
            ultimateTimer.Tick += (s, e) =>
            {
                ultimateAttack.Left += player.AttackDirection * 20;

                Player opponent = player == player1 ? player2 : player1;
                if (ultimateAttack.Bounds.IntersectsWith(opponent.picture.Bounds))
                {
                    if (!opponent.IsDodging)
                    {
                        opponent.Health -= player.UltimateAttackDamage;
                    }
                    this.Controls.Remove(ultimateAttack);
                    ultimateTimer.Stop();
                }
                else if (ultimateAttack.Right < 0 || ultimateAttack.Left > ClientSize.Width)
                {
                    this.Controls.Remove(ultimateAttack);
                    ultimateTimer.Stop();
                }
            };
            ultimateTimer.Start();
        }
    }
}
