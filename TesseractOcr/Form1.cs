using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDosyaSec_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFile = new OpenFileDialog();
            oFile.Filter ="Resim Dosyaları |*.jpeg;*.jpg;*.png";
            oFile.InitialDirectory = Application.StartupPath; //var sayılan dizin uygulamamın bulunduğu dizin olsun
            if (DialogResult.OK == oFile.ShowDialog())
            {
                pbFoto.ImageLocation = oFile.FileName; //Seçilen dosyayı görmek içi PictureBox nesnesine atıyoruz.

                //tessdata dizininde dil paketleri bulunmaktadır. Türkçe için tur yazıyoruz.
                using (var dosya = new TesseractEngine(@"./tessdata", "tur", EngineMode.Default)) 
                {
                    using (var img = Pix.LoadFromFile(oFile.FileName))
                    {
                        using (var txt = dosya.Process(img))
                        {
                            txtMetin.Text = txt.GetText(); 
                        }
                    }
                }
                
            }
        }
    }
}
