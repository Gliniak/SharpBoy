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
            this.cb_log_error = new System.Windows.Forms.CheckBox();
            this.cb_log_warn = new System.Windows.Forms.CheckBox();
            this.cb_log_info = new System.Windows.Forms.CheckBox();
            this.cb_log_debug = new System.Windows.Forms.CheckBox();
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
            // cb_log_error
            // 
            this.cb_log_error.AutoSize = true;
            this.cb_log_error.Location = new System.Drawing.Point(12, 246);
            this.cb_log_error.Name = "cb_log_error";
            this.cb_log_error.Size = new System.Drawing.Size(53, 17);
            this.cb_log_error.TabIndex = 1;
            this.cb_log_error.Text = "Errors";
            this.cb_log_error.UseVisualStyleBackColor = true;
            this.cb_log_error.CheckedChanged += new System.EventHandler(this.cb_log_error_CheckedChanged);
            // 
            // cb_log_warn
            // 
            this.cb_log_warn.AutoSize = true;
            this.cb_log_warn.Location = new System.Drawing.Point(98, 246);
            this.cb_log_warn.Name = "cb_log_warn";
            this.cb_log_warn.Size = new System.Drawing.Size(71, 17);
            this.cb_log_warn.TabIndex = 2;
            this.cb_log_warn.Text = "Warnings";
            this.cb_log_warn.UseVisualStyleBackColor = true;
            this.cb_log_warn.CheckedChanged += new System.EventHandler(this.cb_log_warn_CheckedChanged);
            // 
            // cb_log_info
            // 
            this.cb_log_info.AutoSize = true;
            this.cb_log_info.Location = new System.Drawing.Point(189, 246);
            this.cb_log_info.Name = "cb_log_info";
            this.cb_log_info.Size = new System.Drawing.Size(44, 17);
            this.cb_log_info.TabIndex = 3;
            this.cb_log_info.Text = "Info";
            this.cb_log_info.UseVisualStyleBackColor = true;
            this.cb_log_info.CheckedChanged += new System.EventHandler(this.cb_log_info_CheckedChanged);
            // 
            // cb_log_debug
            // 
            this.cb_log_debug.AutoSize = true;
            this.cb_log_debug.Location = new System.Drawing.Point(252, 246);
            this.cb_log_debug.Name = "cb_log_debug";
            this.cb_log_debug.Size = new System.Drawing.Size(58, 17);
            this.cb_log_debug.TabIndex = 4;
            this.cb_log_debug.Text = "Debug";
            this.cb_log_debug.UseVisualStyleBackColor = true;
            this.cb_log_debug.CheckedChanged += new System.EventHandler(this.cb_log_debug_CheckedChanged);
            // 
            // LoggerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 274);
            this.ControlBox = false;
            this.Controls.Add(this.cb_log_debug);
            this.Controls.Add(this.cb_log_info);
            this.Controls.Add(this.cb_log_warn);
            this.Controls.Add(this.cb_log_error);
            this.Controls.Add(this.logger_textbox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoggerWindow";
            this.ShowIcon = false;
            this.Text = "LoggerWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox logger_textbox;
        private System.Windows.Forms.CheckBox cb_log_error;
        private System.Windows.Forms.CheckBox cb_log_warn;
        private System.Windows.Forms.CheckBox cb_log_info;
        private System.Windows.Forms.CheckBox cb_log_debug;
    }
}