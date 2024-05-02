namespace Rclone_Daemon
{
    partial class about
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(about));
            about_text = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            linkLabel1 = new LinkLabel();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            SuspendLayout();
            // 
            // about_text
            // 
            about_text.AutoSize = true;
            about_text.Font = new Font("Segoe UI", 9F);
            about_text.Location = new Point(42, 32);
            about_text.Name = "about_text";
            about_text.Size = new Size(63, 20);
            about_text.TabIndex = 0;
            about_text.Text = "Product:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F);
            label1.Location = new Point(147, 32);
            label1.Name = "label1";
            label1.Size = new Size(117, 20);
            label1.TabIndex = 1;
            label1.Text = "Rclone_Daemon";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F);
            label2.Location = new Point(42, 88);
            label2.Name = "label2";
            label2.Size = new Size(57, 20);
            label2.TabIndex = 2;
            label2.Text = "Author:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F);
            label3.Location = new Point(147, 88);
            label3.Name = "label3";
            label3.Size = new Size(96, 20);
            label3.TabIndex = 3;
            label3.Text = "GoodBoyboy";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F);
            label4.Location = new Point(378, 32);
            label4.Name = "label4";
            label4.Size = new Size(67, 20);
            label4.TabIndex = 4;
            label4.Text = "WebSite:";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Segoe UI", 9F);
            linkLabel1.Location = new Point(469, 32);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(157, 20);
            linkLabel1.TabIndex = 5;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "www.goodboyboy.top";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F);
            label5.Location = new Point(378, 88);
            label5.Name = "label5";
            label5.Size = new Size(60, 20);
            label5.TabIndex = 6;
            label5.Text = "Version:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F);
            label6.Location = new Point(469, 88);
            label6.Name = "label6";
            label6.Size = new Size(46, 20);
            label6.TabIndex = 7;
            label6.Text = "v1.0.1";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 6.60000038F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(176, 288);
            label7.Name = "label7";
            label7.Size = new Size(304, 15);
            label7.TabIndex = 8;
            label7.Text = "Copyright (c) 2015-2024  GoodBoyboy, GoodBoyboy.top";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("等线", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label8.Location = new Point(32, 154);
            label8.Name = "label8";
            label8.Size = new Size(604, 30);
            label8.TabIndex = 9;
            label8.Text = "本软件是由GoodBoyboy（https://www.goodboyboy.top）开发的免费软件，无任何担保。\r\n此项目已在GitHub开源。（https://github.com/GoodBoyboy666/Rclone_Daemon）";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(32, 200);
            label9.Name = "label9";
            label9.Size = new Size(555, 51);
            label9.TabIndex = 10;
            label9.Text = resources.GetString("label9.Text");
            // 
            // about
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(664, 312);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(linkLabel1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(about_text);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "about";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "关于";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label about_text;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private LinkLabel linkLabel1;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
    }
}