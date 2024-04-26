namespace Rclone_Daemon
{
    partial class Rclone_Daemon
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rclone_Daemon));
            label1 = new Label();
            add_btn = new Button();
            change_btn = new Button();
            del_btn = new Button();
            ed_btn = new Button();
            mount_box = new ListView();
            Mount_Name = new ColumnHeader();
            Mount_Path = new ColumnHeader();
            Drive = new ColumnHeader();
            Status = new ColumnHeader();
            notify = new NotifyIcon(components);
            notify_menu = new ContextMenuStrip(components);
            显示主界面ToolStripMenuItem = new ToolStripMenuItem();
            退出ToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            设置ToolStripMenuItem = new ToolStripMenuItem();
            开机启动 = new ToolStripMenuItem();
            设置Rclone路径ToolStripMenuItem = new ToolStripMenuItem();
            关于ToolStripMenuItem = new ToolStripMenuItem();
            SelectFile = new OpenFileDialog();
            notify_menu.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 47);
            label1.Name = "label1";
            label1.Size = new Size(114, 20);
            label1.TabIndex = 0;
            label1.Text = "已保存的挂载：";
            // 
            // add_btn
            // 
            add_btn.Enabled = false;
            add_btn.Location = new Point(707, 93);
            add_btn.Name = "add_btn";
            add_btn.Size = new Size(94, 29);
            add_btn.TabIndex = 2;
            add_btn.Text = "新增";
            add_btn.UseVisualStyleBackColor = true;
            add_btn.Click += add_btn_Click;
            // 
            // change_btn
            // 
            change_btn.Enabled = false;
            change_btn.Location = new Point(707, 171);
            change_btn.Name = "change_btn";
            change_btn.Size = new Size(94, 29);
            change_btn.TabIndex = 3;
            change_btn.Text = "修改";
            change_btn.UseVisualStyleBackColor = true;
            change_btn.Click += change_btn_Click;
            // 
            // del_btn
            // 
            del_btn.Enabled = false;
            del_btn.Location = new Point(707, 249);
            del_btn.Name = "del_btn";
            del_btn.Size = new Size(94, 29);
            del_btn.TabIndex = 4;
            del_btn.Text = "删除";
            del_btn.UseVisualStyleBackColor = true;
            del_btn.Click += del_btn_Click;
            // 
            // ed_btn
            // 
            ed_btn.Enabled = false;
            ed_btn.Location = new Point(707, 327);
            ed_btn.Name = "ed_btn";
            ed_btn.Size = new Size(94, 29);
            ed_btn.TabIndex = 5;
            ed_btn.Text = "启用";
            ed_btn.UseVisualStyleBackColor = true;
            ed_btn.Click += ed_btn_Click;
            // 
            // mount_box
            // 
            mount_box.Columns.AddRange(new ColumnHeader[] { Mount_Name, Mount_Path, Drive, Status });
            mount_box.Location = new Point(26, 93);
            mount_box.Name = "mount_box";
            mount_box.Size = new Size(637, 263);
            mount_box.TabIndex = 6;
            mount_box.UseCompatibleStateImageBehavior = false;
            mount_box.View = View.Details;
            mount_box.SelectedIndexChanged += mount_box_SelectedIndexChanged;
            // 
            // Mount_Name
            // 
            Mount_Name.Text = "名称";
            Mount_Name.Width = 100;
            // 
            // Mount_Path
            // 
            Mount_Path.Text = "挂载路径";
            Mount_Path.Width = 380;
            // 
            // Drive
            // 
            Drive.Text = "盘符";
            // 
            // Status
            // 
            Status.Text = "状态";
            Status.Width = 100;
            // 
            // notify
            // 
            notify.ContextMenuStrip = notify_menu;
            notify.Icon = (Icon)resources.GetObject("notify.Icon");
            notify.Text = "Rclone_Daemon";
            notify.Visible = true;
            notify.DoubleClick += notify_DoubleClick;
            // 
            // notify_menu
            // 
            notify_menu.ImageScalingSize = new Size(20, 20);
            notify_menu.Items.AddRange(new ToolStripItem[] { 显示主界面ToolStripMenuItem, 退出ToolStripMenuItem });
            notify_menu.Name = "contextMenuStrip1";
            notify_menu.Size = new Size(154, 52);
            // 
            // 显示主界面ToolStripMenuItem
            // 
            显示主界面ToolStripMenuItem.Name = "显示主界面ToolStripMenuItem";
            显示主界面ToolStripMenuItem.Size = new Size(153, 24);
            显示主界面ToolStripMenuItem.Text = "显示主界面";
            显示主界面ToolStripMenuItem.Click += 显示主界面ToolStripMenuItem_Click;
            // 
            // 退出ToolStripMenuItem
            // 
            退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            退出ToolStripMenuItem.Size = new Size(153, 24);
            退出ToolStripMenuItem.Text = "退出";
            退出ToolStripMenuItem.Click += 退出ToolStripMenuItem_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { 设置ToolStripMenuItem, 关于ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(843, 28);
            menuStrip1.TabIndex = 7;
            menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            设置ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 开机启动, 设置Rclone路径ToolStripMenuItem });
            设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            设置ToolStripMenuItem.Size = new Size(53, 24);
            设置ToolStripMenuItem.Text = "设置";
            // 
            // 开机启动
            // 
            开机启动.Name = "开机启动";
            开机启动.Size = new Size(202, 26);
            开机启动.Text = "开机启动";
            开机启动.Click += 开机启动_Click;
            // 
            // 设置Rclone路径ToolStripMenuItem
            // 
            设置Rclone路径ToolStripMenuItem.Name = "设置Rclone路径ToolStripMenuItem";
            设置Rclone路径ToolStripMenuItem.Size = new Size(202, 26);
            设置Rclone路径ToolStripMenuItem.Text = "设置Rclone路径";
            设置Rclone路径ToolStripMenuItem.Click += 设置Rclone路径ToolStripMenuItem_Click;
            // 
            // 关于ToolStripMenuItem
            // 
            关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            关于ToolStripMenuItem.Size = new Size(53, 24);
            关于ToolStripMenuItem.Text = "关于";
            关于ToolStripMenuItem.Click += 关于ToolStripMenuItem_Click;
            // 
            // SelectFile
            // 
            SelectFile.Filter = "Rclone文件|rclone.exe";
            SelectFile.Title = "选择Rclone程序";
            // 
            // Rclone_Daemon
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(843, 428);
            Controls.Add(menuStrip1);
            Controls.Add(mount_box);
            Controls.Add(ed_btn);
            Controls.Add(del_btn);
            Controls.Add(change_btn);
            Controls.Add(add_btn);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Rclone_Daemon";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Rclone_Daemon by GoodBoyboy v1.0";
            FormClosing += Rclone_Daemon_FormClosing;
            Load += Rclone_Daemon_Load;
            notify_menu.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button add_btn;
        private Button change_btn;
        private Button del_btn;
        private Button ed_btn;
        private ListView mount_box;
        private ColumnHeader Mount_Name;
        private ColumnHeader Mount_Path;
        private ColumnHeader Drive;
        private ColumnHeader Status;
        private NotifyIcon notify;
        private ContextMenuStrip notify_menu;
        private ToolStripMenuItem 显示主界面ToolStripMenuItem;
        private ToolStripMenuItem 退出ToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem 设置ToolStripMenuItem;
        private ToolStripMenuItem 开机启动;
        private ToolStripMenuItem 设置Rclone路径ToolStripMenuItem;
        private OpenFileDialog SelectFile;
        private ToolStripMenuItem 关于ToolStripMenuItem;
    }
}
