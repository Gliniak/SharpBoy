namespace SharpBoy
{
    partial class DisassemblerWindow
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
            this.pause_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_reg_de = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tb_reg_bc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_reg_hl = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tb_reg_af = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tb_reg_l = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tb_reg_h = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_reg_f = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_reg_e = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_reg_d = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_reg_c = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_reg_b = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_reg_sp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_reg_a = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_reg_pc_dis = new System.Windows.Forms.TextBox();
            this.lv_dissassembly = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cb_carry_flag = new System.Windows.Forms.CheckBox();
            this.cb_half_carry_flag = new System.Windows.Forms.CheckBox();
            this.cb_substract_flag = new System.Windows.Forms.CheckBox();
            this.cb_zero_flag = new System.Windows.Forms.CheckBox();
            this.tb_goto_address = new System.Windows.Forms.TextBox();
            this.b_goto_address = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bt_addBreakpoint = new System.Windows.Forms.Button();
            this.lb_breakpoints = new System.Windows.Forms.ListBox();
            this.tb_breakpointAdr = new System.Windows.Forms.TextBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pause_button
            // 
            this.pause_button.Location = new System.Drawing.Point(12, 62);
            this.pause_button.Name = "pause_button";
            this.pause_button.Size = new System.Drawing.Size(184, 40);
            this.pause_button.TabIndex = 0;
            this.pause_button.Text = "Next Instruction";
            this.pause_button.UseVisualStyleBackColor = true;
            this.pause_button.Click += new System.EventHandler(this.pause_button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_reg_de);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.tb_reg_bc);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tb_reg_hl);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.tb_reg_af);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.tb_reg_l);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.tb_reg_h);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.tb_reg_f);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tb_reg_e);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tb_reg_d);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tb_reg_c);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tb_reg_b);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tb_reg_sp);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tb_reg_a);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tb_reg_pc_dis);
            this.groupBox1.Location = new System.Drawing.Point(794, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 426);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CPU Registers";
            // 
            // tb_reg_de
            // 
            this.tb_reg_de.Location = new System.Drawing.Point(52, 141);
            this.tb_reg_de.Name = "tb_reg_de";
            this.tb_reg_de.Size = new System.Drawing.Size(126, 20);
            this.tb_reg_de.TabIndex = 27;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 144);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 13);
            this.label14.TabIndex = 26;
            this.label14.Text = "reg_de";
            // 
            // tb_reg_bc
            // 
            this.tb_reg_bc.Location = new System.Drawing.Point(52, 115);
            this.tb_reg_bc.Name = "tb_reg_bc";
            this.tb_reg_bc.Size = new System.Drawing.Size(126, 20);
            this.tb_reg_bc.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "reg_hl";
            // 
            // tb_reg_hl
            // 
            this.tb_reg_hl.Location = new System.Drawing.Point(52, 168);
            this.tb_reg_hl.Name = "tb_reg_hl";
            this.tb_reg_hl.Size = new System.Drawing.Size(126, 20);
            this.tb_reg_hl.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 118);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "reg_bc";
            // 
            // tb_reg_af
            // 
            this.tb_reg_af.Location = new System.Drawing.Point(52, 89);
            this.tb_reg_af.Name = "tb_reg_af";
            this.tb_reg_af.Size = new System.Drawing.Size(126, 20);
            this.tb_reg_af.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 91);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "reg_af";
            // 
            // tb_reg_l
            // 
            this.tb_reg_l.Location = new System.Drawing.Point(52, 400);
            this.tb_reg_l.Name = "tb_reg_l";
            this.tb_reg_l.Size = new System.Drawing.Size(126, 20);
            this.tb_reg_l.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 403);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "reg_l";
            // 
            // tb_reg_h
            // 
            this.tb_reg_h.Location = new System.Drawing.Point(52, 374);
            this.tb_reg_h.Name = "tb_reg_h";
            this.tb_reg_h.Size = new System.Drawing.Size(126, 20);
            this.tb_reg_h.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 377);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "reg_h";
            // 
            // tb_reg_f
            // 
            this.tb_reg_f.Location = new System.Drawing.Point(52, 348);
            this.tb_reg_f.Name = "tb_reg_f";
            this.tb_reg_f.Size = new System.Drawing.Size(126, 20);
            this.tb_reg_f.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 351);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "reg_f";
            // 
            // tb_reg_e
            // 
            this.tb_reg_e.Location = new System.Drawing.Point(52, 322);
            this.tb_reg_e.Name = "tb_reg_e";
            this.tb_reg_e.Size = new System.Drawing.Size(126, 20);
            this.tb_reg_e.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 325);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "reg_e";
            // 
            // tb_reg_d
            // 
            this.tb_reg_d.Location = new System.Drawing.Point(52, 296);
            this.tb_reg_d.Name = "tb_reg_d";
            this.tb_reg_d.Size = new System.Drawing.Size(126, 20);
            this.tb_reg_d.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 299);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "reg_d";
            // 
            // tb_reg_c
            // 
            this.tb_reg_c.Location = new System.Drawing.Point(52, 270);
            this.tb_reg_c.Name = "tb_reg_c";
            this.tb_reg_c.Size = new System.Drawing.Size(126, 20);
            this.tb_reg_c.TabIndex = 11;
            this.tb_reg_c.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tb_reg_c_MouseDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 273);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "reg_c";
            // 
            // tb_reg_b
            // 
            this.tb_reg_b.Location = new System.Drawing.Point(52, 244);
            this.tb_reg_b.Name = "tb_reg_b";
            this.tb_reg_b.Size = new System.Drawing.Size(126, 20);
            this.tb_reg_b.TabIndex = 9;
            this.tb_reg_b.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tb_reg_b_MouseDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 247);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "reg_b";
            // 
            // tb_reg_sp
            // 
            this.tb_reg_sp.Location = new System.Drawing.Point(52, 46);
            this.tb_reg_sp.Name = "tb_reg_sp";
            this.tb_reg_sp.Size = new System.Drawing.Size(126, 20);
            this.tb_reg_sp.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "reg_sp";
            // 
            // tb_reg_a
            // 
            this.tb_reg_a.Location = new System.Drawing.Point(52, 218);
            this.tb_reg_a.Name = "tb_reg_a";
            this.tb_reg_a.Size = new System.Drawing.Size(126, 20);
            this.tb_reg_a.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 221);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "reg_a";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "reg_pc";
            // 
            // tb_reg_pc_dis
            // 
            this.tb_reg_pc_dis.Location = new System.Drawing.Point(52, 19);
            this.tb_reg_pc_dis.Name = "tb_reg_pc_dis";
            this.tb_reg_pc_dis.Size = new System.Drawing.Size(126, 20);
            this.tb_reg_pc_dis.TabIndex = 0;
            // 
            // lv_dissassembly
            // 
            this.lv_dissassembly.AutoArrange = false;
            this.lv_dissassembly.Location = new System.Drawing.Point(215, 13);
            this.lv_dissassembly.Name = "lv_dissassembly";
            this.lv_dissassembly.Size = new System.Drawing.Size(573, 595);
            this.lv_dissassembly.TabIndex = 3;
            this.lv_dissassembly.UseCompatibleStateImageBehavior = false;
            this.lv_dissassembly.View = System.Windows.Forms.View.Details;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cb_carry_flag);
            this.groupBox2.Controls.Add(this.cb_half_carry_flag);
            this.groupBox2.Controls.Add(this.cb_substract_flag);
            this.groupBox2.Controls.Add(this.cb_zero_flag);
            this.groupBox2.Location = new System.Drawing.Point(794, 445);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(184, 72);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CPU Flags";
            // 
            // cb_carry_flag
            // 
            this.cb_carry_flag.AutoSize = true;
            this.cb_carry_flag.Location = new System.Drawing.Point(76, 42);
            this.cb_carry_flag.Name = "cb_carry_flag";
            this.cb_carry_flag.Size = new System.Drawing.Size(33, 17);
            this.cb_carry_flag.TabIndex = 7;
            this.cb_carry_flag.Text = "C";
            this.cb_carry_flag.UseVisualStyleBackColor = true;
            // 
            // cb_half_carry_flag
            // 
            this.cb_half_carry_flag.AutoSize = true;
            this.cb_half_carry_flag.Location = new System.Drawing.Point(76, 19);
            this.cb_half_carry_flag.Name = "cb_half_carry_flag";
            this.cb_half_carry_flag.Size = new System.Drawing.Size(41, 17);
            this.cb_half_carry_flag.TabIndex = 6;
            this.cb_half_carry_flag.Text = "HC";
            this.cb_half_carry_flag.UseVisualStyleBackColor = true;
            // 
            // cb_substract_flag
            // 
            this.cb_substract_flag.AutoSize = true;
            this.cb_substract_flag.Location = new System.Drawing.Point(13, 42);
            this.cb_substract_flag.Name = "cb_substract_flag";
            this.cb_substract_flag.Size = new System.Drawing.Size(33, 17);
            this.cb_substract_flag.TabIndex = 5;
            this.cb_substract_flag.Text = "S";
            this.cb_substract_flag.UseVisualStyleBackColor = true;
            // 
            // cb_zero_flag
            // 
            this.cb_zero_flag.AutoSize = true;
            this.cb_zero_flag.Location = new System.Drawing.Point(13, 19);
            this.cb_zero_flag.Name = "cb_zero_flag";
            this.cb_zero_flag.Size = new System.Drawing.Size(33, 17);
            this.cb_zero_flag.TabIndex = 4;
            this.cb_zero_flag.Text = "Z";
            this.cb_zero_flag.UseVisualStyleBackColor = true;
            // 
            // tb_goto_address
            // 
            this.tb_goto_address.Location = new System.Drawing.Point(12, 118);
            this.tb_goto_address.Name = "tb_goto_address";
            this.tb_goto_address.Size = new System.Drawing.Size(100, 20);
            this.tb_goto_address.TabIndex = 5;
            // 
            // b_goto_address
            // 
            this.b_goto_address.Location = new System.Drawing.Point(119, 118);
            this.b_goto_address.Name = "b_goto_address";
            this.b_goto_address.Size = new System.Drawing.Size(77, 21);
            this.b_goto_address.TabIndex = 6;
            this.b_goto_address.Text = "Go To Address";
            this.b_goto_address.UseVisualStyleBackColor = true;
            this.b_goto_address.Click += new System.EventHandler(this.b_goto_address_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.bt_addBreakpoint);
            this.groupBox3.Controls.Add(this.lb_breakpoints);
            this.groupBox3.Controls.Add(this.tb_breakpointAdr);
            this.groupBox3.Location = new System.Drawing.Point(13, 166);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(183, 233);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Breakpoints";
            // 
            // bt_addBreakpoint
            // 
            this.bt_addBreakpoint.Location = new System.Drawing.Point(106, 24);
            this.bt_addBreakpoint.Name = "bt_addBreakpoint";
            this.bt_addBreakpoint.Size = new System.Drawing.Size(71, 21);
            this.bt_addBreakpoint.TabIndex = 2;
            this.bt_addBreakpoint.Text = "Add";
            this.bt_addBreakpoint.UseVisualStyleBackColor = true;
            this.bt_addBreakpoint.Click += new System.EventHandler(this.bt_addBreakpoint_Click);
            // 
            // lb_breakpoints
            // 
            this.lb_breakpoints.FormattingEnabled = true;
            this.lb_breakpoints.Location = new System.Drawing.Point(7, 51);
            this.lb_breakpoints.Name = "lb_breakpoints";
            this.lb_breakpoints.Size = new System.Drawing.Size(170, 173);
            this.lb_breakpoints.TabIndex = 1;
            // 
            // tb_breakpointAdr
            // 
            this.tb_breakpointAdr.Location = new System.Drawing.Point(7, 24);
            this.tb_breakpointAdr.Name = "tb_breakpointAdr";
            this.tb_breakpointAdr.Size = new System.Drawing.Size(92, 20);
            this.tb_breakpointAdr.TabIndex = 0;
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(12, 13);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(184, 40);
            this.btn_start.TabIndex = 8;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_refresh
            // 
            this.btn_refresh.Location = new System.Drawing.Point(13, 415);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(183, 44);
            this.btn_refresh.TabIndex = 9;
            this.btn_refresh.Text = "Refresh";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // DisassemblerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 620);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.b_goto_address);
            this.Controls.Add(this.tb_goto_address);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lv_dissassembly);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pause_button);
            this.Name = "DisassemblerWindow";
            this.Text = "DisassemblerWindow";
            this.Load += new System.EventHandler(this.DisassemblerWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button pause_button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_reg_pc_dis;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_reg_a;
        private System.Windows.Forms.TextBox tb_reg_hl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_reg_de;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tb_reg_bc;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tb_reg_af;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tb_reg_l;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb_reg_h;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_reg_f;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tb_reg_e;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_reg_d;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_reg_c;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_reg_b;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_reg_sp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lv_dissassembly;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cb_carry_flag;
        private System.Windows.Forms.CheckBox cb_half_carry_flag;
        private System.Windows.Forms.CheckBox cb_substract_flag;
        private System.Windows.Forms.CheckBox cb_zero_flag;
        private System.Windows.Forms.TextBox tb_goto_address;
        private System.Windows.Forms.Button b_goto_address;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button bt_addBreakpoint;
        private System.Windows.Forms.ListBox lb_breakpoints;
        private System.Windows.Forms.TextBox tb_breakpointAdr;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_refresh;
    }
}