using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BarcodesLib;

namespace Barcode
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtBarcodeStringRepresation_TextChanged(object sender, EventArgs e)
        {
            lblCountEnterNumber.Text = txtBarcodeStringRepresation.Text.Length.ToString();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            txtBarcodeStringRepresation.Text = EAN13.Generate();
        }

        private void btnValidation_Click(object sender, EventArgs e)
        {
            bool resultValidation = EAN13.Validation(txtBarcodeStringRepresation.Text);
            string message;

            if (resultValidation)
            {
                txtBarcodeStringRepresation.Text = txtBarcodeStringRepresation.Text;
                message = "Barcode is right";
            }
            else
            {
                message = "Barcode is wrong";
            }

            MessageBox.Show(message, "Result validation");
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            pnlSurfaceBarcodeDraw.Refresh();
            string barcode = txtBarcodeStringRepresation.Text;
            try
            {
                EAN13.DrawBarcode(pnlSurfaceBarcodeDraw.CreateGraphics(), pnlSurfaceBarcodeDraw.Width, pnlSurfaceBarcodeDraw.Height, barcode);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
    }
}
