namespace SharpBoy
{
    partial class LoggerWindow
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
            this.logger_textbox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // logger_textbox
            // 
            this.logger_textbox.Location = new System.Drawing.Point(12, 12);
            this.logger_textbox.Name = "logger_textbox";
            this.logger_textbox.Size = new System.Drawing.Size(726, 228);
            this.logger_textbox.TabIndex = 0;
            this.logger_textbox.Text = "";
            // 
            // LoggerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 252);
            this.Controls.Add(this.logger_textbox);
            this.Name = "LoggerWindow";
            this.Text = "LoggerWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox logger_textbox;
    }
}