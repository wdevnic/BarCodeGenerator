using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace Generate_Barcode
{
    public partial class BarcodeGenerator : Form
    {
        public BarcodeGenerator()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //create string to hold barcode name
            string barcode = textBox1.Text;
            int barcodeLength= textBox1.Text.Length;

            if (barcodeLength == 8)
            {

                //create bitmapped area
                Bitmap bitmap = new Bitmap(300, 300);

                //using bitmapped image to created graphics object
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {

                    // create required font and color objects
                    Font fontname = new System.Drawing.Font("IDAutomationHC39M", 19);
                    PointF point = new PointF(0, 0);
                    SolidBrush black = new SolidBrush(Color.Black);
                    SolidBrush white = new SolidBrush(Color.White);

                    // draw barcode
                    graphics.FillRectangle(white, 0, 0, 300, 300);
                    graphics.DrawString("*" + barcode + "*", fontname, black, point);
                }

                //call crop barcode method
                Bitmap cropped = CropBitmap(bitmap, 0, 42, 258, 54);

                //place barcode in Imagebox (preview barcode)
                using (MemoryStream memstr = new MemoryStream())
                {
                    cropped.Save(memstr, ImageFormat.Png);
                    pictureBox1.Image = cropped;
                    pictureBox1.Height = 54;
                    pictureBox1.Width = 258;
                }

            }

            else
            {
                MessageBox.Show("Barcode Length must be eight characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // method crops barcode
        public Bitmap CropBitmap(Bitmap bitmap, int cropX, int cropY, int cropWidth, int cropHeight)
        {
            Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
            Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
            return cropped;
        }

        //button that copies barcode to clipboard
        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pictureBox1.Image);
        }
    }
}
