using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using HideStartMenuTask;
using System.Timers;

namespace TvTiCiQi
{
    public partial class Form1 : Form
    {
        int CharSize = 120;
        int initTimerInterval = 10;
        int initMoveSize = 2;
        Int32 sizeFHeight;
        Int32 sizeFWidth;
        Int32 playLines;

        private delegate void InvokeHandler();
        System.Timers.Timer t = new System.Timers.Timer();



        public Form1()
        {
            InitializeComponent();
        }

        private void btnFullScreen_Click(object sender, EventArgs e)
        {
   
            
            if (this.textBox1.Text == "")
            {
                this.textPlay.Text = "没有发布任何文字！";
            }
            else
            {
                this.textPlay.Text = this.textBox1.Text.TrimEnd().Replace("\r\n\r\n", "\r\n");
            }
            this.textPlay.Focus();
            playLines = 0;

            Graphics graphics = CreateGraphics();
            Font strFont = new Font("楷体", CharSize);
            SizeF sizeF1 = graphics.MeasureString("江苏", new Font("楷体", CharSize));
            sizeFHeight = Convert.ToInt16(strFont.Height);
            for (int ii = 0; ii <= this.textPlay.Lines.GetUpperBound(0); ii++)
            {
                SizeF sizeF = graphics.MeasureString(this.textPlay.Lines[ii], new Font("楷体", CharSize));
                sizeFWidth = Convert.ToInt16(sizeF.Width);
                    playLines += ( sizeFWidth / this.Width + 1);
            }
            graphics.Dispose();
            this.textPlay.Font = new System.Drawing.Font("楷体", CharSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textPlay.BackColor = System.Drawing.Color.Black;
            this.textPlay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textPlay.ForeColor = System.Drawing.Color.White;
            this.textPlay.Top = this.Height / 2;
            this.textPlay.Left = 0;
            this.textPlay.Width = this.Width;
            this.textPlay.Height = playLines * sizeFHeight;
            this.textPlay.Visible = true;

            this.textPlay.SelectionStart = 0;
            this.textPlay.SelectionLength = 0;

            this.timer1.Interval = initTimerInterval;

            AlterControlStatus(false);
            ClsWin32Hide.HideTask(true);
            this.SetVisibleCore(false);
            this.FormBorderStyle = FormBorderStyle.None; //窗口无边框
            this.WindowState = FormWindowState.Maximized;
            this.SetVisibleCore(true);

            this.textPlay.Focus();

            t.SynchronizingObject = this;
            t.Elapsed += new System.Timers.ElapsedEventHandler(timersTimer_Elapsed);
            t.Interval = initTimerInterval;

        }

        public void timersTimer_Elapsed(object source, ElapsedEventArgs e)
        {
            this.Invoke(new InvokeHandler(delegate()
            {
                //for (int ii = 0; ii < initMoveSize;ii++)
                {
                    this.textPlay.Top -= initMoveSize;
                    //Application.DoEvents();
                }
                //if ((this.textPlay.Bottom) < this.Height / 2)
                //{
                //    //this.textPlay.Top = this.Height / 2;
                //    t.Enabled = false;
                //}
                if ((this.textPlay.Bottom) < this.Height /2)
                {
                    //this.textPlay.Top = this.Height / 2;
                    t.Enabled = false;
                }
            }));


        }
    

        private void textPlay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)//esc键盘退出全屏
            {
                StopPlay2();
            }
            else if (e.KeyCode == Keys.Space)
            {
                //this.timer1.Enabled = !this.timer1.Enabled ;
                t.Enabled = !t.Enabled;
            }
            else if (e.KeyCode == Keys.Up)
            {
                initMoveSize += 1;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (initMoveSize >= 1)
                {
                    initMoveSize -= 1;
                }
            }
            else if(e.KeyCode == Keys.Left)
            {
                if (initTimerInterval > 1)
                {
                    initTimerInterval -= 1;
                    this.timer1.Interval = initTimerInterval;
                    t.Interval = initTimerInterval;
                }
            }
            else if(e.KeyCode == Keys.Right)
            {
                if (initTimerInterval <= 500)
                {
                    initTimerInterval += 1;
                    this.timer1.Interval = initTimerInterval;
                    t.Interval = initTimerInterval;
                }
            }
        }


        private void StopPlay2()
        {
            this.timer1.Enabled = false;
            AlterControlStatus(true);
            this.SetVisibleCore(false);
            ClsWin32Hide.HideTask(false);
            this.FormBorderStyle = FormBorderStyle.Sizable; //还原窗口边框
            this.WindowState = FormWindowState.Maximized; 
            this.SetVisibleCore(true);
        }
    

        private void AlterControlStatus(Boolean booleanValue)
        {
            textBox1.Visible = booleanValue;
            panel1.Visible = booleanValue;
            textPlay.Visible = (!booleanValue);
        }


      

        private void timer1_Tick(object sender, EventArgs e)
        {

            //this.textPlay.Top -= initMoveSize;
            if ((this.textPlay.Bottom) < this.Height / 2)
            {
               // this.textPlay.Top = this.Height/2; 
                //timer1.Enabled = false;
            }
            
            ////this.pictureBox1.Top -= initMoveSize;
            //if ((this.pictureBox1.Bottom) < this.Height / 2)
            //{
            //    this.pictureBox1.Top = this.Height / 2; ;
            //    //timer1.Enabled = false;
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClsWin32Hide.HideTask(false);
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClsWin32Hide.HideTask(false); 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ClsWin32Hide.HideTask(false); 
            this.Close();
        }

        private  void textBox1_MouseDown(object sender, MouseEventArgs e)
        {

        
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Text = "总字数：" + this.textBox1.TextLength.ToString();
        } 
  

  }
}
