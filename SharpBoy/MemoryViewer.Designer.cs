namespace SharpBoy
{
    partial class MemoryViewer
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
            this.cb_specAddress = new System.Windows.Forms.ComboBox();
            this.lv_memView = new System.Windows.Forms.ListView();
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_00 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // cb_specAddress
            // 
            this.cb_specAddress.FormattingEnabled = true;
            this.cb_specAddress.Items.AddRange(new object[] {
            "ROM Memory (0x0000 - 0x8000)",
            "VRAM (0x8000 - 0xA000)",
            "RAM Bank (0xA000 - 0xC000)",
            "RAM (0xC000-0xE000)",
            "ECHO RAM (0xE000-0xFE00)",
            "SPRITES (0xFE00-0xFEA0)",
            "IO (0xFF00-0xFF4C)",
            "INTERNAL RAM (0xFF80-0xFFFF)"});
            this.cb_specAddress.Location = new System.Drawing.Point(13, 13);
            this.cb_specAddress.Name = "cb_specAddress";
            this.cb_specAddress.Size = new System.Drawing.Size(255, 21);
            this.cb_specAddress.TabIndex = 0;
            this.cb_specAddress.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lv_memView
            // 
            this.lv_memView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader16,
            this.ch_00,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15});
            this.lv_memView.Location = new System.Drawing.Point(13, 41);
            this.lv_memView.Name = "lv_memView";
            this.lv_memView.Size = new System.Drawing.Size(524, 308);
            this.lv_memView.TabIndex = 1;
            this.lv_memView.UseCompatibleStateImageBehavior = false;
            this.lv_memView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Address";
            this.columnHeader16.Width = 55;
            // 
            // ch_00
            // 
            this.ch_00.Text = "00";
            this.ch_00.Width = 28;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "01";
            this.columnHeader1.Width = 28;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "02";
            this.columnHeader2.Width = 28;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "03";
            this.columnHeader3.Width = 28;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "04";
            this.columnHeader4.Width = 28;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "05";
            this.columnHeader5.Width = 28;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "06";
            this.columnHeader6.Width = 28;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "07";
            this.columnHeader7.Width = 28;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "08";
            this.columnHeader8.Width = 28;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "09";
            this.columnHeader9.Width = 28;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "0A";
            this.columnHeader10.Width = 28;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "0B";
            this.columnHeader11.Width = 28;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "0C";
            this.columnHeader12.Width = 28;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "0D";
            this.columnHeader13.Width = 28;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "0E";
            this.columnHeader14.Width = 28;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "0F";
            this.columnHeader15.Width = 28;
            // 
            // MemoryViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 382);
            this.Controls.Add(this.lv_memView);
            this.Controls.Add(this.cb_specAddress);
            this.Name = "MemoryViewer";
            this.Text = "MemoryViewer";
            this.Load += new System.EventHandler(this.MemoryViewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_specAddress;
        private System.Windows.Forms.ListView lv_memView;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader ch_00;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
    }
}