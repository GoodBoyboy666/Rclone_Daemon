namespace Rclone_Daemon
{
    partial class ChangeForm
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
            button1 = new Button();
            label1 = new Label();
            mount_name = new TextBox();
            mount_path = new TextBox();
            label2 = new Label();
            label3 = new Label();
            drive = new ComboBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(680, 178);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 0;
            button1.Text = "保存";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(34, 61);
            label1.Name = "label1";
            label1.Size = new Size(99, 20);
            label1.TabIndex = 1;
            label1.Text = "挂载点名称：";
            // 
            // mount_name
            // 
            mount_name.Location = new Point(34, 113);
            mount_name.Name = "mount_name";
            mount_name.Size = new Size(125, 27);
            mount_name.TabIndex = 2;
            // 
            // mount_path
            // 
            mount_path.Location = new Point(215, 113);
            mount_path.Name = "mount_path";
            mount_path.Size = new Size(285, 27);
            mount_path.TabIndex = 3;
            mount_path.Text = "/";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(215, 61);
            label2.Name = "label2";
            label2.Size = new Size(84, 20);
            label2.TabIndex = 4;
            label2.Text = "挂载路径：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(550, 62);
            label3.Name = "label3";
            label3.Size = new Size(84, 20);
            label3.TabIndex = 5;
            label3.Text = "挂载盘符：";
            // 
            // drive
            // 
            drive.FormattingEnabled = true;
            drive.Items.AddRange(new object[] { "A:", "B:", "C:", "D:", "E:", "F:", "G:", "H:", "I:", "J:", "K:", "L:", "M:", "N:", "O:", "P:", "Q:", "R:", "S:", "T:", "U:", "V:", "W:", "X:", "Y:", "Z:" });
            drive.Location = new Point(550, 113);
            drive.Name = "drive";
            drive.Size = new Size(84, 28);
            drive.TabIndex = 6;
            // 
            // ChangeForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(806, 236);
            Controls.Add(drive);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(mount_path);
            Controls.Add(mount_name);
            Controls.Add(label1);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ChangeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ChangeForm";
            Load += ChangeForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private TextBox mount_name;
        private TextBox mount_path;
        private Label label2;
        private Label label3;
        private ComboBox drive;
    }
}