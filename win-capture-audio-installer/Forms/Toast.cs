using System;
using System.Drawing;
using System.Windows.Forms;
using win_capture_audio_installer.Classes;

namespace win_capture_audio_installer
{
    public partial class Toast : Form
    {
        public Toast()
        {
            InitializeComponent();
        }

        private int _x, _y;

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Interval = 1;
            this.Opacity += 0.1; //Fade in
            if (this.Opacity == 1.0)
            {
                timer1.Stop();
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            isClosing = true;
            timer2.Interval = 1;
            timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (isClosing)
            {
                this.timer2.Interval = 1;
                this.Opacity -= 0.1; //Fade in
                if (this.Opacity <= 0)
                {
                    timer1.Stop();
                    Notify.toasts.Remove(this);
                    this.Close();
                }
            }
            else
            {
                isClosing = true;
            }
        }
        public bool isClosing = false;
        public void ShowToast(string title, string msg, int autoCloseTime = 0)
        {
            MainWindow.INSTANCE.dLogger.Log("Toast Called : " + title + " | " + msg + " | " + autoCloseTime.ToString());

            this.StartPosition = FormStartPosition.Manual;

            this.Message.MaximumSize = new Size(this.Width - 30, 3000);

            this.Height = this.Message.Height + this.Title.Height + 95;

            this.Opacity = 0.0;

            this.Title.Text = title;
            this.Message.Text = msg;

            string fname;

            int maxAmount = (int)Math.Truncate((decimal)Screen.PrimaryScreen.WorkingArea.Height / this.Height);

            for (int i = 0; i < maxAmount; i++)
            {
                fname = "toast" + i.ToString();
                var toast = (Toast)Application.OpenForms[fname];

                if (toast == null)
                {
                    this.Name = fname;
                    this._x = Screen.PrimaryScreen.WorkingArea.Width - this.Width - 3;
                    this._y = 7 + ((this.Height + 3) * i);
                    this.Location = new Point(this._x, this._y);
                    break;
                }
            }

            this._x = Screen.PrimaryScreen.WorkingArea.Width - base.Width - 5;
            this.Show();
            this.timer1.Interval = 1;
            this.timer1.Start();
            if (autoCloseTime > 0)
            {
                timer2.Interval = autoCloseTime * 1000;
                timer2.Start();
            }
        }
    }
}
