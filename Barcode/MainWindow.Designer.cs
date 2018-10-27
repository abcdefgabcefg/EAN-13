namespace Barcode
{
    partial class MainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlSurfaceBarcodeDraw = new System.Windows.Forms.Panel();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnDraw = new System.Windows.Forms.Button();
            this.btnValidation = new System.Windows.Forms.Button();
            this.lblCountEnterNumber = new System.Windows.Forms.Label();
            this.txtBarcodeStringRepresation = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // pnlSurfaceBarcodeDraw
            // 
            this.pnlSurfaceBarcodeDraw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSurfaceBarcodeDraw.Location = new System.Drawing.Point(170, 122);
            this.pnlSurfaceBarcodeDraw.Name = "pnlSurfaceBarcodeDraw";
            this.pnlSurfaceBarcodeDraw.Size = new System.Drawing.Size(674, 274);
            this.pnlSurfaceBarcodeDraw.TabIndex = 0;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(-3, 122);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(173, 69);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(-3, 258);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(173, 69);
            this.btnDraw.TabIndex = 1;
            this.btnDraw.Text = "Draw";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // btnValidation
            // 
            this.btnValidation.Location = new System.Drawing.Point(-3, 190);
            this.btnValidation.Name = "btnValidation";
            this.btnValidation.Size = new System.Drawing.Size(173, 69);
            this.btnValidation.TabIndex = 2;
            this.btnValidation.Text = "Validate";
            this.btnValidation.UseVisualStyleBackColor = true;
            this.btnValidation.Click += new System.EventHandler(this.btnValidation_Click);
            // 
            // lblCountEnterNumber
            // 
            this.lblCountEnterNumber.AutoSize = true;
            this.lblCountEnterNumber.Location = new System.Drawing.Point(469, 60);
            this.lblCountEnterNumber.Name = "lblCountEnterNumber";
            this.lblCountEnterNumber.Size = new System.Drawing.Size(25, 28);
            this.lblCountEnterNumber.TabIndex = 4;
            this.lblCountEnterNumber.Text = "0";
            // 
            // txtBarcodeStringRepresation
            // 
            this.txtBarcodeStringRepresation.Location = new System.Drawing.Point(34, 24);
            this.txtBarcodeStringRepresation.Name = "txtBarcodeStringRepresation";
            this.txtBarcodeStringRepresation.Size = new System.Drawing.Size(460, 34);
            this.txtBarcodeStringRepresation.TabIndex = 6;
            this.txtBarcodeStringRepresation.TextChanged += new System.EventHandler(this.txtBarcodeStringRepresation_TextChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 445);
            this.Controls.Add(this.txtBarcodeStringRepresation);
            this.Controls.Add(this.lblCountEnterNumber);
            this.Controls.Add(this.btnValidation);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.pnlSurfaceBarcodeDraw);
            this.Font = new System.Drawing.Font("Cambria", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EAN-13 Generator";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlSurfaceBarcodeDraw;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.Button btnValidation;
        private System.Windows.Forms.Label lblCountEnterNumber;
        private System.Windows.Forms.TextBox txtBarcodeStringRepresation;
    }
}

