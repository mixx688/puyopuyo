using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace puyopuyo
{
    public partial class Form1 : Form
    {
        int[,] a = new int[6, 12];
        PictureBox[,] p=new PictureBox[6,12];
        Random r = new Random();
        int cur1, cur2, next1, next2,x,y,dir,state,score,combo,time;



        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    p[i, j] = new PictureBox();
                    p[i, j].Size = new Size(48, 48);
                    p[i, j].Location = new Point(18 + 48 * i, 571 - 48 * j);
                    p[i, j].Image = puyopuyo.Properties.Resources._1;
                    p[i, j].BackColor = System.Drawing.Color.Transparent;
                    p[i, j].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    this.Controls.Add(p[i, j]);
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (state == 0)
            {
                if (e.KeyCode == Keys.Left && x > 0 && !(x == 1 && dir == 1)&&(dir==0&&a[x-1,y-1]==0||dir==1&&a[x-2,y]==0||dir==2&&a[x-1,y]==0||dir==3&&a[x-1,y]==0))
                {
                    a[x, y] = 0;
                    if (dir == 0) a[x, y - 1] = 0;
                    if (dir == 1) a[x - 1, y] = 0;
                    if (dir == 2) a[x, y + 1] = 0;
                    if (dir == 3) a[x + 1, y] = 0;
                    x--;
                    a[x, y] = cur2;
                    if (dir == 0) a[x, y - 1] = cur1;
                    if (dir == 1) a[x - 1, y] = cur1;
                    if (dir == 2) a[x, y + 1] = cur1;
                    if (dir == 3) a[x + 1, y] = cur1;
                    renew();
                }
                if (e.KeyCode == Keys.Right && x < 5 && !(x == 4 && dir == 3)&&(dir==0&&a[x+1,y-1]==0||dir==1&&a[x+1,y]==0||dir==2&&a[x+1,y]==0||dir==3&&a[x+2,y]==0))
                {
                    a[x, y] = 0;
                    if (dir == 0) a[x, y - 1] = 0;
                    if (dir == 1) a[x - 1, y] = 0;
                    if (dir == 2) a[x, y + 1] = 0;
                    if (dir == 3) a[x + 1, y] = 0;
                    x++;
                    a[x, y] = cur2;
                    if (dir == 0) a[x, y - 1] = cur1;
                    if (dir == 1) a[x - 1, y] = cur1;
                    if (dir == 2) a[x, y + 1] = cur1;
                    if (dir == 3) a[x + 1, y] = cur1;
                    renew();
                }
                if (e.KeyCode == Keys.Up)
                {
                    if (dir == 0 && x > 0 && a[x - 1, y] == 0 || dir == 1 && y < 11 || dir == 2 && x < 5 && a[x + 1, y] == 0 || dir == 3 && y > 0 && a[x, y - 1] == 0)
                    {
                        if (dir == 0) { a[x, y - 1] = 0; a[x - 1, y] = cur1; }
                        if (dir == 1) { a[x - 1, y] = 0; a[x, y + 1] = cur1; }
                        if (dir == 2) { a[x, y + 1] = 0; a[x + 1, y] = cur1; }
                        if (dir == 3) { a[x + 1, y] = 0; a[x, y - 1] = cur1; }
                        dir++;
                        if (dir == 4) dir = 0;
                        renew();
                    }
                }
                if (e.KeyCode == Keys.Down)
                {
                    if (dir == 0 && (y == 1 || a[x, y - 2] > 0) || dir == 1 && (y == 0 || a[x, y - 1] > 0 || a[x - 1, y - 1] > 0) || dir == 2 && (y == 0 || a[x, y - 1] > 0) || dir == 3 && (y == 0 || a[x, y - 1] > 0 || a[x + 1, y - 1] > 0)) { state = 1;timer1.Interval = 350; return; }
                    a[x, y] = 0;
                    if (dir == 0) a[x, y - 1] = 0;
                    else if (dir == 1) a[x - 1, y] = 0;
                    else if (dir == 2) a[x, y + 1] = 0;
                    else if (dir == 3) a[x + 1, y] = 0;
                    y--;
                    a[x, y] = cur2;
                    if (dir == 0) a[x, y - 1] = cur1;
                    else if (dir == 1) a[x - 1, y] = cur1;
                    else if (dir == 2) a[x, y + 1] = cur1;
                    else if (dir == 3) a[x + 1, y] = cur1;
                    renew();
                }
            }
        }



        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (state==4)//game over
            {
                timer1.Stop();
                button1.Enabled = true;
                label1.Text = "GAME OVER";
            }
            else if (state == 3)//next piece
            {
                if (a[2, 10] != 0) state = 4;
                else
                {
                    x = 2;
                    y = 11;
                    dir = 0;
                    cur1 = next1;
                    cur2 = next2;
                    next1 = r.Next(1, 6);
                    next2 = r.Next(1, 6);
                    a[2, 10] = cur1;
                    a[2, 11] = cur2;
                    renew();
                    timer1.Interval = time;
                    label4.Text = "";
                    label5.Text = "";
                    state = 0;
                }
            }
            else if(state==2)//eliminate state
            {
                int eli = 0;
                int[,] t = new int[6, 12];               
                for(int i = 0; i < 6; i++)
                {
                    for(int j=0; j < 12; j++)
                    {
                        int[,] q = new int[12, 3];
                        bool flag = true;
                        if (a[i, j] > 0 && t[i, j] == 0)
                        {
                            int p = 0,end=0;
                            t[i, j] = a[i,j];
                            q[0, 0] = i;
                            q[0, 1] = j;
                            q[0, 2] = 1;
                            while (flag)
                            {
                                if (q[end,0]<5 &&a[q[end,0]+1 ,q[end,1]]== a[i,j]&&t[q[end,0]+1 ,q[end,1]]==0) { t[q[end,0]+1 ,q[end,1]] = a[i, j];p++; q[p, 0] = q[end,0]+1;q[p, 1] = q[end,1];q[p, 2] = 1; }
                                if (q[end,1]<11&&a[q[end,0] ,q[end,1]+1]== a[i,j]&&t[q[end,0] ,q[end,1]+1]==0) { t[q[end,0] ,q[end,1]+1] = a[i, j];p++; q[p, 0] = q[end,0];q[p, 1] = q[end,1]+1;q[p, 2] = 1; }
                                if (q[end,0]>0 &&a[q[end,0]-1 ,q[end,1]]== a[i,j]&&t[q[end,0]-1 ,q[end,1]]==0) { t[q[end,0]-1 ,q[end,1]] = a[i, j];p++; q[p, 0] = q[end,0]-1;q[p, 1] = q[end,1];q[p, 2] = 1; }
                                if (q[end,1]>0 &&a[q[end,0] ,q[end,1]-1]== a[i,j]&&t[q[end,0] ,q[end,1]-1]==0) { t[q[end,0] ,q[end,1]-1] = a[i, j];p++; q[p, 0] = q[end,0];q[p, 1] = q[end,1]-1;q[p, 2] = 1; }
                                flag =false;
                                q[end, 2] = 2;
                                end++;
                                for(int z=0;z<12 ; z++)
                                {
                                    if(q[z, 2] == 1)
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                            }
                            p++;
                            if (p >= 4)
                            {
                                eli += p;
                                for(int z = 0; z < p; z++)a[q[z, 0], q[z, 1]] = 0;
                            }
                        }
                    }
                }
                if (eli == 0)
                {
                    combo = 0;
                    state = 3;
                }
                else
                {        
                    label4.Text = "Combo";
                    label5.Text = Convert.ToString(combo);
                    for (int i = 0; i < combo; i++) eli *= 2;
                    combo++;
                    score += eli;
                    state = 1;
                }
                if (score > 1000) time = 400;
                else if(score>500) time = 500;
                else if(score>300) time = 650;
                renew();
            }
            else if(state==1)//falling state
            {
                for(int i = 1; i < 12; i++)
                {
                    for(int j = 0; j < 6; j++)
                    {
                        if (a[j, i] > 0 && a[j, i - 1] == 0)
                        {
                            for(int k=0; ; k++)
                            {
                                if (a[j, k] == 0)
                                {
                                    a[j, k] = a[j, i];
                                    a[j, i] = 0;
                                    break;
                                }
                            }     
                        }
                    }
                }
                renew();
                state = 2;
            }
            else if (state==0)//control state
            {
                if (dir == 0 && (y == 1 || a[x, y - 2] > 0) || dir == 1 && (y == 0 || a[x, y - 1] > 0 || a[x - 1, y - 1] > 0) || dir == 2 && (y == 0 || a[x, y - 1] > 0) || dir == 3 && (y == 0 || a[x, y - 1] > 0 || a[x + 1, y - 1] > 0)) { state = 1; timer1.Interval = 350; return; }
                a[x, y] = 0;
                if (dir == 0) a[x, y - 1] = 0;
                else if (dir == 1) a[x - 1, y] = 0;
                else if (dir == 2) a[x, y + 1] = 0;
                else if (dir == 3) a[x + 1, y] = 0;
                y--;
                a[x, y] = cur2;
                if (dir == 0) a[x, y - 1] = cur1;
                else if (dir == 1) a[x - 1, y] = cur1;
                else if (dir == 2) a[x, y + 1] = cur1;
                else if (dir == 3) a[x + 1, y] = cur1;
                renew();
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 12; j++)
                    a[i, j] = 0;
            label1.Text = "";
            label4.Text = "";
            label5.Text = "";
            time = 800;
            timer1.Interval = time;
            next1 = r.Next(1,6);
            next2 = r.Next(1,6);
            cur1 = r.Next(1,6);
            cur2 = r.Next(1,6);
            score = 0;
            combo = 0;
            state = 0;
            dir = 0;
            x = 2;
            y = 11;
            a[2, 10] = cur1;
            a[2, 11] = cur2;
            renew();
            timer1.Start();
            button1.Enabled = false;
        }
        void renew()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (a[i, j] == 0) p[i, j].Image = null;
                    else if (a[i, j] == 1) p[i, j].Image = puyopuyo.Properties.Resources._1;
                    else if (a[i, j] == 2) p[i, j].Image = puyopuyo.Properties.Resources._2;
                    else if (a[i, j] == 3) p[i, j].Image = puyopuyo.Properties.Resources._3;
                    else if (a[i, j] == 4) p[i, j].Image = puyopuyo.Properties.Resources._4;
                    else if (a[i, j] == 5) p[i, j].Image = puyopuyo.Properties.Resources._5;
                }
            }
            if (next2 == 1) pictureBox1.Image = puyopuyo.Properties.Resources._1;
            if (next2 == 2) pictureBox1.Image = puyopuyo.Properties.Resources._2;
            if (next2 == 3) pictureBox1.Image = puyopuyo.Properties.Resources._3;
            if (next2 == 4) pictureBox1.Image = puyopuyo.Properties.Resources._4;
            if (next2 == 5) pictureBox1.Image = puyopuyo.Properties.Resources._5;
            if (next1 == 1) pictureBox2.Image = puyopuyo.Properties.Resources._1;
            if (next1 == 2) pictureBox2.Image = puyopuyo.Properties.Resources._2;
            if (next1 == 3) pictureBox2.Image = puyopuyo.Properties.Resources._3;
            if (next1 == 4) pictureBox2.Image = puyopuyo.Properties.Resources._4;
            if (next1 == 5) pictureBox2.Image = puyopuyo.Properties.Resources._5;
            label2.Text = Convert.ToString(score);
        }
    }
}
