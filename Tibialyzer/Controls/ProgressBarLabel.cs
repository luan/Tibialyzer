using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tibialyzer {
    class ProgressBarLabel : Label {
        public double percentage;
        public Color backColor = StyleManager.BlendTransparencyKey;
        public bool reverse;
        public bool centerText;

        public ProgressBarLabel() {
            this.percentage = 1;
        }
        
        protected override void OnPaint(PaintEventArgs e) {
            int x = 5;
            if (!reverse) {
                x = (int)(this.Width - 10 - this.Text.Length * this.Font.Size);
            }
            if (centerText) {
                x = (int)(this.Width / 2);
            }
            SummaryForm.RenderText(e.Graphics, this.Text, x, Color.Empty, StyleManager.NotificationTextColor, Color.FromArgb(0, 0, 0), this.Height, 4, this.Font, true, System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor, backColor == StyleManager.TransparencyKey ? System.Drawing.Drawing2D.SmoothingMode.None : System.Drawing.Drawing2D.SmoothingMode.HighQuality);
        }

        protected override void OnPaintBackground(PaintEventArgs e) {
            e.Graphics.Clear(backColor);
            /*
            using (Brush brush = new SolidBrush(this.BackColor)) {
                int x = 0;
                if (reverse) {
                    int offset = (int)(this.Width - this.Width * percentage);
                    x = offset;
                }
                e.Graphics.FillRectangle(brush, new Rectangle(x, 0, (int)(this.Width * percentage), this.Height));
            }
            using (Pen pen = new Pen(Color.Black, 2)) {
                e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, this.Width, this.Height));
            }
            */
            using (Brush brush = new SolidBrush(Color.Black)) {
                e.Graphics.FillRectangle(brush, new Rectangle(0, 0, this.Width, this.Height));
            }
            using (LinearGradientBrush brush = new LinearGradientBrush(new Point(0, 0), new Point(0, this.Height), Color.Black, Color.Black)) {
                ColorBlend cb = new ColorBlend();
                cb.Positions = new[] { 0, 1 / 6f, 2 / 6f, 2.5f / 6f, 4 / 6f, 5 / 6f, 1 };
                Color darken1 = Color.FromArgb((int)(BackColor.R*0.7), (int)(BackColor.G*0.7), (int)(BackColor.B*0.7));
                Color darken2 = Color.FromArgb((int)(BackColor.R*0.6), (int)(BackColor.G*0.6), (int)(BackColor.B*0.6));
                Color darken3 = Color.FromArgb((int)(BackColor.R*0.5), (int)(BackColor.G*0.5), (int)(BackColor.B*0.5));
                cb.Colors = new[] { darken2, BackColor, Color.White, BackColor, darken1, darken2, darken3 };

                brush.InterpolationColors = cb;

                int x = 0;
                if (reverse) {
                    int offset = (int)(this.Width - this.Width * percentage);
                    x = offset;
                }
                e.Graphics.FillRectangle(brush, new Rectangle(x, 0, (int)(this.Width * percentage), this.Height));
            }
            using(Pen pen = new Pen(Color.Black, 1)) {
                e.Graphics.DrawRectangle(pen, new Rectangle(1, 1, this.Width-3, this.Height-3));
            }
            using(Pen pen = new Pen(Color.Gold, 1)) {
                e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, this.Width-1, this.Height-1));
            }
        }
    }
}
