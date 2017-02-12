using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cyberheart
{
    enum Speed : int{
        Fastest = 25,
        Fast = 50,
        Medium = 100,
        Slow = 200,
        Slowest = 500
    }
    public partial class Form1 : Form
    {

        
        Tree treeHeart = new Tree();
        Bitmap background = (Bitmap)Bitmap.FromFile("heart.png");
        Bitmap skeleton = (Bitmap)Bitmap.FromFile("Skeleton.png");

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = background;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            treeHeart.GenerateTree(skeleton, new Point(99, 105));

        }

        private void destroyHeart(Speed speed)
        {
            timer1.Interval = (int)speed;
            timer1.Start();
        }

        int curN = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (curN < treeHeart.tree.Count)
            {
                Point lastPoint = treeHeart.tree[curN].getPoint();

                using (Graphics grf = Graphics.FromImage(background))
                {
                    using (Brush brsh = new SolidBrush(SystemColors.Control))
                    {
                        grf.FillEllipse(brsh, lastPoint.X - 5, lastPoint.Y - 5,
                            10, 10);
                    }
                }

                for (int i=0; i< curN;i++)
                {
                    Point curPoint = treeHeart.tree[i].getPoint();
                    background.SetPixel(curPoint.X, curPoint.Y, Color.Black);
                }
                this.pictureBox1.Image = background;
                curN++;
            }
        }

        private void destroyButton_Click(object sender, EventArgs e)
        {
            destroyHeart(Speed.Fastest);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Save changes?", "Heart", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
                this.pictureBox1.Image.Save("result.png");
        }
    }
}
