namespace AnyStore.UI
{
    partial class frmStatistics
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStatistics));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.lblTop = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvTransactions = new System.Windows.Forms.DataGridView();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtYestrdyTotal = new System.Windows.Forms.TextBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl5 = new System.Windows.Forms.Label();
            this.lbl6 = new System.Windows.Forms.Label();
            this.lbl4 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.TxtTotalTwo = new System.Windows.Forms.TextBox();
            this.TxtTotalThree = new System.Windows.Forms.TextBox();
            this.TxtTotalFour = new System.Windows.Forms.TextBox();
            this.TxtTotalFive = new System.Windows.Forms.TextBox();
            this.TxtTotalSix = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvAllSales = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllSales)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSeaGreen;
            this.panel1.Controls.Add(this.pictureBoxClose);
            this.panel1.Controls.Add(this.lblTop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 33);
            this.panel1.TabIndex = 23;
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxClose.Image")));
            this.pictureBoxClose.Location = new System.Drawing.Point(742, 0);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(31, 30);
            this.pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxClose.TabIndex = 1;
            this.pictureBoxClose.TabStop = false;
            this.pictureBoxClose.Click += new System.EventHandler(this.pictureBoxClose_Click);
            // 
            // lblTop
            // 
            this.lblTop.AutoSize = true;
            this.lblTop.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTop.Location = new System.Drawing.Point(341, 9);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(87, 21);
            this.lblTop.TabIndex = 0;
            this.lblTop.Text = "STATISTICS";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "label1";
            // 
            // dgvTransactions
            // 
            this.dgvTransactions.BackgroundColor = System.Drawing.Color.White;
            this.dgvTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransactions.Location = new System.Drawing.Point(9, 34);
            this.dgvTransactions.Name = "dgvTransactions";
            this.dgvTransactions.ReadOnly = true;
            this.dgvTransactions.Size = new System.Drawing.Size(721, 217);
            this.dgvTransactions.TabIndex = 25;
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(146, 35);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(177, 20);
            this.txtTotal.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Total For Today";
            // 
            // TxtYestrdyTotal
            // 
            this.TxtYestrdyTotal.Location = new System.Drawing.Point(146, 68);
            this.TxtYestrdyTotal.Name = "TxtYestrdyTotal";
            this.TxtYestrdyTotal.ReadOnly = true;
            this.TxtYestrdyTotal.Size = new System.Drawing.Size(177, 20);
            this.TxtYestrdyTotal.TabIndex = 28;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(33, 71);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(35, 13);
            this.lbl1.TabIndex = 29;
            this.lbl1.Text = "label3";
            // 
            // lbl5
            // 
            this.lbl5.AutoSize = true;
            this.lbl5.Location = new System.Drawing.Point(33, 194);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(35, 13);
            this.lbl5.TabIndex = 30;
            this.lbl5.Text = "label4";
            // 
            // lbl6
            // 
            this.lbl6.AutoSize = true;
            this.lbl6.Location = new System.Drawing.Point(33, 226);
            this.lbl6.Name = "lbl6";
            this.lbl6.Size = new System.Drawing.Size(35, 13);
            this.lbl6.TabIndex = 31;
            this.lbl6.Text = "label5";
            // 
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.Location = new System.Drawing.Point(33, 164);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(35, 13);
            this.lbl4.TabIndex = 32;
            this.lbl4.Text = "label6";
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Location = new System.Drawing.Point(33, 132);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(35, 13);
            this.lbl3.TabIndex = 33;
            this.lbl3.Text = "label7";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(33, 101);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(35, 13);
            this.lbl2.TabIndex = 34;
            this.lbl2.Text = "label8";
            // 
            // TxtTotalTwo
            // 
            this.TxtTotalTwo.Location = new System.Drawing.Point(146, 98);
            this.TxtTotalTwo.Name = "TxtTotalTwo";
            this.TxtTotalTwo.ReadOnly = true;
            this.TxtTotalTwo.Size = new System.Drawing.Size(177, 20);
            this.TxtTotalTwo.TabIndex = 35;
            // 
            // TxtTotalThree
            // 
            this.TxtTotalThree.Location = new System.Drawing.Point(146, 129);
            this.TxtTotalThree.Name = "TxtTotalThree";
            this.TxtTotalThree.ReadOnly = true;
            this.TxtTotalThree.Size = new System.Drawing.Size(177, 20);
            this.TxtTotalThree.TabIndex = 36;
            // 
            // TxtTotalFour
            // 
            this.TxtTotalFour.Location = new System.Drawing.Point(146, 161);
            this.TxtTotalFour.Name = "TxtTotalFour";
            this.TxtTotalFour.ReadOnly = true;
            this.TxtTotalFour.Size = new System.Drawing.Size(177, 20);
            this.TxtTotalFour.TabIndex = 37;
            // 
            // TxtTotalFive
            // 
            this.TxtTotalFive.Location = new System.Drawing.Point(146, 191);
            this.TxtTotalFive.Name = "TxtTotalFive";
            this.TxtTotalFive.ReadOnly = true;
            this.TxtTotalFive.Size = new System.Drawing.Size(177, 20);
            this.TxtTotalFive.TabIndex = 38;
            // 
            // TxtTotalSix
            // 
            this.TxtTotalSix.Location = new System.Drawing.Point(146, 223);
            this.TxtTotalSix.Name = "TxtTotalSix";
            this.TxtTotalSix.ReadOnly = true;
            this.TxtTotalSix.Size = new System.Drawing.Size(177, 20);
            this.TxtTotalSix.TabIndex = 39;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.dgvTransactions);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(30, 44);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(743, 265);
            this.panel2.TabIndex = 41;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(29, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "All Sales Today";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.txtTotal);
            this.panel3.Controls.Add(this.TxtYestrdyTotal);
            this.panel3.Controls.Add(this.TxtTotalSix);
            this.panel3.Controls.Add(this.lbl1);
            this.panel3.Controls.Add(this.TxtTotalFive);
            this.panel3.Controls.Add(this.lbl5);
            this.panel3.Controls.Add(this.TxtTotalFour);
            this.panel3.Controls.Add(this.lbl6);
            this.panel3.Controls.Add(this.TxtTotalThree);
            this.panel3.Controls.Add(this.lbl4);
            this.panel3.Controls.Add(this.TxtTotalTwo);
            this.panel3.Controls.Add(this.lbl3);
            this.panel3.Controls.Add(this.lbl2);
            this.panel3.Location = new System.Drawing.Point(30, 327);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(348, 254);
            this.panel3.TabIndex = 42;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 13);
            this.label5.TabIndex = 42;
            this.label5.Text = "Last Seven Days Income";
            // 
            // dgvAllSales
            // 
            this.dgvAllSales.BackgroundColor = System.Drawing.Color.White;
            this.dgvAllSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllSales.Location = new System.Drawing.Point(14, 36);
            this.dgvAllSales.Name = "dgvAllSales";
            this.dgvAllSales.ReadOnly = true;
            this.dgvAllSales.Size = new System.Drawing.Size(352, 206);
            this.dgvAllSales.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(30, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 43;
            this.label4.Text = "Yearly Income";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgvAllSales);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(393, 327);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(379, 254);
            this.panel4.TabIndex = 44;
            // 
            // frmStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(800, 593);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmStatistics";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmStatistics";
            this.Load += new System.EventHandler(this.frmStatistics_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllSales)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxClose;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvTransactions;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtYestrdyTotal;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl5;
        private System.Windows.Forms.Label lbl6;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.TextBox TxtTotalTwo;
        private System.Windows.Forms.TextBox TxtTotalThree;
        private System.Windows.Forms.TextBox TxtTotalFour;
        private System.Windows.Forms.TextBox TxtTotalFive;
        private System.Windows.Forms.TextBox TxtTotalSix;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvAllSales;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
    }
}