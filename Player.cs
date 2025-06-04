using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace проектстрашно4
{
    public class Player
    {
        // осн свойства
        public string Name { get; set; }
        public int Health { get; set; }
        public bool IsDead => Health <= 0;

        // графика и анимация
        public PictureBox picture { get; set; }
        public string[] CharactersRight { get; set; }
        public string[] CharactersLeft { get; set; }
        public int CurrentCharacter { get; set; }

        // движение и направление
        public int Speed { get; set; }
        public bool goLeft { get; set; }
        public bool goRight { get; set; }
        public int LastDirection { get; set; } = 1;

        // Уворот (Dodge)
        public bool IsDodging { get; set; }
        public DateTime DodgeStartTime { get; set; }
        public const int DodgeDuration = 1000;

        // атака
        public int AttackDamage { get; set; }
        public int AttackDirection { get; set; }
        public int BaseAttackSpeed { get; set; } = 20;
        public DateTime LastAttackTime { get; set; }
        public int AttackCooldown { get; set; } = 100;
        public PictureBox AttackPicture { get; set; }

        // ультаааа
        public int UltimateAttackDamage { get; set; }
        public int UltimateCharge { get; set; } = 0;
        public const int UltimateMaxCharge = 30;
        public bool IsUltimateReady => UltimateCharge >= UltimateMaxCharge;
        public PictureBox UltimateIndicator { get; set; }

        // всп
        public string dir;

        public Player()
        {
            dir = Directory.GetCurrentDirectory();
            CharactersRight = new string[8] {
            "\\bananini_right.jpg",
            "\\kokokosini_right.jpg",
            "\\tralalerotralala_right.jpg",
            "\\lirililarela_right.jpg",
            "\\frigo_right.jpg",
            "\\tung_right.jpg",
            "\\bombordiro_right.jpg",
            "\\bombombini_right.jpg"
            };
            CharactersLeft = new string[8] {
            "\\bananini_left.jpg",
            "\\kokokosini_left.jpg",
            "\\tralalerotralala_left.jpg",
            "\\lirililarela_left.jpg",
            "\\frigo_left.jpg",
            "\\tung_left.jpg",
            "\\bombordiro_left.jpg",
            "\\bombombini_left.jpg"
            };

            Health = 100;
            Speed = 14;
            AttackDamage = 2;
            UltimateAttackDamage = AttackDamage * 3;
        }

        public void UpdateDirection(bool isFacingRight)
        {
            if (picture == null) return;

            string imagePath = dir + (isFacingRight
                ? CharactersRight[CurrentCharacter]
                : CharactersLeft[CurrentCharacter]);

            try
            {
                if (picture.Image != null)
                    picture.Image.Dispose();

                picture.Image = Image.FromFile(imagePath);
            }
            catch
            {
                picture.BackColor = Color.DarkGray;
            }
        }
        public void StartDodge()
        {
            IsDodging = true;
            DodgeStartTime = DateTime.Now;
        }

        public void EndDodge()
        {
            IsDodging = false;
        }
        public void RegisterAttack()
        {
            if (UltimateCharge < UltimateMaxCharge)
            {
                UltimateCharge++;
                UpdateUltimateIndicator();
            }
        }

        private void UpdateUltimateIndicator()
        {
            if (UltimateIndicator != null) UltimateIndicator.BackColor = IsUltimateReady ?
                    Color.Gold : Color.Gray;
        }
        public void UseUltimate()
        {
            if (!IsUltimateReady) return;

            UltimateCharge = 0;
            UpdateUltimateIndicator();
        }
    }
}
