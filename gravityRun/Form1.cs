using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gravityRun
{
    public partial class Form1 : Form
    {
        //Global Variables
        bool landed = false;
        int gravity = 5;
        int score = 0;
        int platformSpeed = 10;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        //Check to see what key has been pushed.
        private void keyisdown(object sender, KeyEventArgs e)
        {
            //if the Spacebar is pressed and we have landed, disable gravity and allow the player to jump!
            if (e.KeyCode == Keys.Space && landed == true)
            {
                gravity = -gravity;
                landed = false;
            }

            //If R is pressed, execute the Reset function and bring the game back to its start.
            if (e.KeyCode == Keys.R)
            {
                reset();
            }
        }

        //Logic for the timer, gravity and how points are incremented
        private void timer1_Tick(object sender, EventArgs e)
        {   
            //As the timer progresses, gravity brings the player downward. Player will always be 80 pixels away from the border.
            player.Top += gravity;
            player.Left = 80;
            label1.Text = "Score: " + score;

            //Logic for Controls and moving the platform to the left. 
            foreach (Control x in this.Controls)
            {
                if ( x is PictureBox && x.Tag == "Platform")
                {
                    x.Left -= platformSpeed;
                
                    if (x.Left < -500)
                    {
                        //Difficulty is controlled here, the rnd.Next shows how we change the width of x randomly to make it challenging.
                        x.Width = rnd.Next(100, 300);
                        x.Left = 500;
                        score++;
                    }
                }
            }

            // Logic for the which image is shown. If the player has landed on P1 or P2, show batman_1
            if (player.Bounds.IntersectsWith(p2.Bounds) || player.Bounds.IntersectsWith(p1.Bounds))
            {
                landed = true;
                player.Top = p2.Top - player.Height;
                player.Image = Properties.Resources.batman_1;
            }

            // Logic for the which image is shown. If the player has landed on P3 or P4, show batman_2
            if (player.Bounds.IntersectsWith(p3.Bounds) || player.Bounds.IntersectsWith(p4.Bounds))
            {
                landed = true;
                player.Top = p3.Top + p3.Height;
                player.Image = Properties.Resources.batman_2;
            }

            //Logic for how the game ends. If the player exceeds the size of the game, stop the timer and change the text to Post Game Reset
            if(player.Top < -40 || player.Top > ClientSize.Height)
            {
                timer1.Stop();
                label1.Text += " -- Press R to reset";
            }
        }

        //The Settings the game will be set to when it restarts. 
        //......1) Bring the score to 0
        //......2) Bring the Width of the 4 platforms back to the default size
        //......3) Bring the Player back to the starting X and Y position
        //......4) Bring the Platforms back to the starting X and Y positions
        //......5) Restart the timer
        private void reset()
        {
            score = 0;
            p1.Width = 273;
            p2.Width = 273;
            p3.Width = 273;
            p4.Width = 273;
            player.Left = 119;
            player.Top = 128;

            p1.Left = 104;
            p1.Top = 235;

            p2.Left = 491;
            p2.Top = 235;

            p3.Left = 304;
            p3.Top = 26;

            p4.Left = 705;
            p4.Top = 26;

            timer1.Start();
        }
    }
}
