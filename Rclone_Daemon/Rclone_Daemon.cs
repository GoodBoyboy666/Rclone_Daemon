//#define Debug   //调试专用

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using IWshRuntimeLibrary;
using File = System.IO.File;

namespace Rclone_Daemon
{
    //Rclone进程结构体
    struct Rclone_Process
    {
        internal string Mount_Name;
        internal int Id;
    }
    public partial class Rclone_Daemon : Form
    {
        public Rclone_Daemon()
        {
            InitializeComponent();
            this.Opacity = 0;
        }
        string Rclone_Path = "";    //Rclone程序路径
        string APPDATA = "";    //APPDATA文件夹路径
        string startpath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\";     //startup目录路径
        string path_exe = Process.GetCurrentProcess().MainModule.FileName;      //当前程序路径
        List<Rclone_Process> rdlist = new List<Rclone_Process>();    //维护Rclone进程集合
        private void Rclone_Daemon_Load(object sender, EventArgs e)
        {
            //检测重复运行

            Process[] is_run = Process.GetProcessesByName("Rclone_Daemon");
            foreach (Process process in is_run)
            {
                if (process.Id != Process.GetCurrentProcess().Id)
                {
                    MessageBox.Show("检测到已运行Rclone_Daemon，请勿重复运行！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Process.GetCurrentProcess().Kill();
                }
            }

            //检测残留rclone进程

            Process[] rclones = Process.GetProcessesByName("rclone");
            if(rclones.Length > 0)
            {
                DialogResult re= MessageBox.Show("检测到已运行的rclone进程，可能为程序未正常退出所遗留的进程，是否清除？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (re == DialogResult.Yes)
                {
                    foreach (Process process in rclones)
                    {
                        process.Kill();
                    }
                }
            }

            //初始化程序
            APPDATA = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (File.Exists($"{APPDATA}/Rclone_Daemon/config.json"))
            {
                //隐藏窗口
#if !Debug
                this.BeginInvoke(new Action(() =>
                {
                    this.Hide();
                    this.Opacity = 1;
                }));
#endif
                StreamReader sr = new StreamReader($"{APPDATA}/Rclone_Daemon/config.json");
                string text_json = sr.ReadToEnd();
                sr.Dispose();
                JObject data_json = null;
                try
                {
                    data_json = JsonConvert.DeserializeObject<JObject>(text_json);
                    Rclone_Path = data_json["Rclone_Path"].Value<String>();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("读取数据文件出现错误！程序将自动删除数据文件。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    File.Delete($"{APPDATA}/Rclone_Daemon/config.json");
                    MessageBox.Show("请重启程序以完成初始化！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.GetCurrentProcess().Kill();
                }
                if (Rclone_Path == null)
                    MessageBox.Show("未设置Rclone文件路径，请在“设置”内设置Rclone程序的路径。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    add_btn.Enabled = true;
                    change_btn.Enabled = true;
                    del_btn.Enabled = true;
                    ed_btn.Enabled = true;
                }

                //初始化挂载点
                foreach (JObject mount in data_json["mount"])
                {
                    //加载挂载点配置
                    string name = mount["name"].Value<String>();
                    string path = mount["path"].Value<String>();
                    string drive = mount["drive"].Value<String>();
                    string status = mount["status"].Value<String>();
                    AddItem(name, path, drive, status);

                    //启动挂载点
                    if (status == "已启用")
                    {
                        Status_Start(name, path, drive);
                    }
                }

            }
            else
            {
                //显示窗体
                this.Opacity = 1;
                //初始化config
                Directory.CreateDirectory($"{APPDATA}/Rclone_Daemon");
                File.Create($"{APPDATA}/Rclone_Daemon/config.json").Close();
                MessageBox.Show("为了正常运行，请在“设置”内设置Rclone程序的路径。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (File.Exists(startpath + "Rclone_Daemon" + ".lnk"))
            {
                开机启动.Checked = true;
            }

        }

        private void 显示主界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //保存挂载点配置
            save_data_json();

            //释放所有挂载点
            foreach (Rclone_Process rp in rdlist)
            {
                try
                {
                    Process p = Process.GetProcessById(rp.Id);
                    p.Kill();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message,"警告",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            //关闭主程序
            Process.GetCurrentProcess().Kill();
        }

        private void Rclone_Daemon_FormClosing(object sender, FormClosingEventArgs e)
        {
#if !Debug
            e.Cancel = true;
            this.Hide();
#endif
        }

        public string[] args = { "", "", "", "" };
        private void add_btn_Click(object sender, EventArgs e)
        {

            ChangeForm cf = new ChangeForm();
            args[0] = "add";
            cf.ShowDialog(this);
            if (args[0] == "changed")
            {
                AddItem(args[1], args[2], args[3], "已禁用");

                //保存配置文件
                save_data_json();
            }
        }
        /// <summary>
        /// 新增ListViewItem函数
        /// </summary>
        /// <param name="name">挂载点名称</param>
        /// <param name="path">挂载路径</param>
        /// <param name="drive">盘符</param>
        /// <param name="status">状态</param>
        private void AddItem(string name, string path, string drive, string status)
        {
            ListViewItem.ListViewSubItem Mount_Path = new ListViewItem.ListViewSubItem();
            ListViewItem.ListViewSubItem Drive = new ListViewItem.ListViewSubItem();
            ListViewItem.ListViewSubItem Status = new ListViewItem.ListViewSubItem();
            Mount_Path.Name = "Mount_Path";
            Mount_Path.Text = path;
            Drive.Name = "Drive";
            Drive.Text = drive;
            Status.Name = "Status";
            Status.Text = status;
            ListViewItem listViewItem = new ListViewItem();
            listViewItem.Name = "Mount_Name";
            listViewItem.Text = name;
            listViewItem.SubItems.Add(Mount_Path);
            listViewItem.SubItems.Add(Drive);
            listViewItem.SubItems.Add(Status);
            mount_box.Items.Add(listViewItem);
        }
        /// <summary>
        /// 保存配置文件函数
        /// </summary>
        private void save_data_json()
        {
            JObject data_json = new JObject();
            data_json.Add("Rclone_Path", Rclone_Path);

            //遍历已保存的挂载点
            JArray mount = new JArray();
            foreach (ListViewItem listitem in mount_box.Items)
            {
                JObject item = new JObject();
                item.Add("name", listitem.Text);
                item.Add("path", listitem.SubItems["Mount_Path"].Text);
                item.Add("drive", listitem.SubItems["Drive"].Text);
                item.Add("status", listitem.SubItems["Status"].Text);
                mount.Add(item);
            }
            data_json.Add("mount", mount);

            //写入config
            StreamWriter sw = new StreamWriter($"{APPDATA}/Rclone_Daemon/config.json");
            sw.WriteLine(data_json.ToString());
            sw.Dispose();
        }

        private void change_btn_Click(object sender, EventArgs e)
        {
            ChangeForm cf = new ChangeForm();
            if (mount_box.SelectedItems.Count > 0)
            {
                args[0] = "change";
                args[1] = mount_box.SelectedItems[0].Text;
                args[2] = mount_box.SelectedItems[0].SubItems["Mount_Path"].Text;
                args[3] = mount_box.SelectedItems[0].SubItems["Drive"].Text;
                cf.ShowDialog(this);
                if (args[0] == "changed")
                {
                    mount_box.SelectedItems[0].Text = args[1];
                    mount_box.SelectedItems[0].SubItems["Mount_Path"].Text = args[2];
                    mount_box.SelectedItems[0].SubItems["Drive"].Text = args[3];

                    //自动更新状态
                    if (mount_box.SelectedItems[0].SubItems["Status"].Text == "已启用")
                    {
                        Change_Status("disable");
                        Change_Status("enable");
                    }

                    //保存配置文件
                    save_data_json();
                }
            }
            else
            {
                MessageBox.Show("请选择一项进行修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void mount_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mount_box.SelectedItems.Count > 0 && mount_box.SelectedItems[0].SubItems["Status"].Text == "已禁用")
            {
                ed_btn.Text = "启用";
            }
            else if (mount_box.SelectedItems.Count > 0 && mount_box.SelectedItems[0].SubItems["Status"].Text == "已启用")
            {
                ed_btn.Text = "禁用";
            }
        }

        private void ed_btn_Click(object sender, EventArgs e)
        {
            if (ed_btn.Text == "启用" && mount_box.SelectedItems.Count > 0)
            {
                if (Change_Status("enable"))
                {
                    mount_box.SelectedItems[0].SubItems["Status"].Text = "已启用";
                    ed_btn.Text = "禁用";

                    //保存配置文件
                    save_data_json();
                }
                else
                {
                    MessageBox.Show("启用失败！","错误",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (ed_btn.Text == "禁用" && mount_box.SelectedItems.Count > 0)
            {
                if (Change_Status("disable"))
                {
                    mount_box.SelectedItems[0].SubItems["Status"].Text = "已禁用";
                    ed_btn.Text = "启用";

                    //保存配置文件
                    save_data_json();
                }
                else
                {
                    MessageBox.Show("禁用失败！可能rclone程序未启动或已异常停止。","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else if (mount_box.SelectedItems.Count < 1)
            {
                MessageBox.Show("请选择一项进行设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 改变挂载状态
        /// </summary>
        /// <param name="operation">操作符</param>
        /// <returns></returns>
        private bool Change_Status(string operation)
        {
            if (operation == "enable")
            {
                return Status_Start(mount_box.SelectedItems[0].Text, mount_box.SelectedItems[0].SubItems["Mount_Path"].Text, mount_box.SelectedItems[0].SubItems["Drive"].Text);
            }
            else if (operation == "disable")
            {
                return Status_Stop(mount_box.SelectedItems[0].Text,false);
            }
            return false;
        }
        /// <summary>
        /// 关闭Rclone进程
        /// </summary>
        /// <returns></returns>
        private bool Status_Stop(string name ,bool force)
        {
            foreach (Rclone_Process rd in rdlist)
            {
                if (rd.Mount_Name == name)
                {
                    try
                    {
                        Process p = Process.GetProcessById(rd.Id);
                        p.Kill();
                        rdlist.Remove(rd);
                    }
                    catch (Exception ex)
                    {
                        if(force)
                        {
                            rdlist.Remove(rd);
                            return true;
                        }
                        else
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                        return false;
                    }

                    return true;
                }
            }
            MessageBox.Show("Rclone进程关闭失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        /// <summary>
        /// 开启Rclone进程
        /// </summary>
        /// <param name="name">挂载点名称</param>
        /// <param name="path">挂载路径</param>
        /// <param name="drive">盘符</param>
        /// <returns></returns>
        private bool Status_Start(string name, string path, string drive)
        {
            string start_args = "mount" + " " + $"{name}:{path}" + " " + $"{drive}" + " " + "--vfs-cache-mode writes";
            Process process = new Process();
            process.StartInfo.FileName = Rclone_Path;
            process.StartInfo.Arguments = start_args;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            Rclone_Process rd = new Rclone_Process();
            rd.Mount_Name = name;
            rd.Id = process.Id;
            rdlist.Add(rd);
            return true;
        }

        private void del_btn_Click(object sender, EventArgs e)
        {
            if (mount_box.SelectedItems[0].SubItems["Status"].Text == "已启用")
            {
                if (Change_Status("disable"))
                {
                    mount_box.SelectedItems[0].Remove();
                }
                else
                {
                    MessageBox.Show("删除失败！可能rclone程序未运行或已异常停止。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult re= MessageBox.Show("是否强制删除？","删除",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (re == DialogResult.Yes)
                    {
                        string select = mount_box.SelectedItems[0].Text;
                        mount_box.SelectedItems[0].Remove();
                        if(Status_Stop(select, true))
                            MessageBox.Show("强制删除成功！","提示",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                mount_box.SelectedItems[0].Remove();
            }

            //保存配置文件
            save_data_json();
        }

        private void 设置Rclone路径ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectFile.ShowDialog();
            if (SelectFile.FileName != "")
            {
                Rclone_Path = SelectFile.FileName;
                MessageBox.Show("设置成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                add_btn.Enabled = true;
                change_btn.Enabled = true;
                del_btn.Enabled = true;
                ed_btn.Enabled = true;
            }
        }

        /// <summary>
        /// 创建快捷方式
        /// </summary>
        private bool Create_ink()
        {
            try
            {

                if (File.Exists(startpath + "Rclone_Daemon" + ".lnk"))
                {
                    File.Delete(startpath + "Rclone_Daemon" + ".lnk");
                }
                WshShell wshell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)wshell.CreateShortcut(startpath + "Rclone_Daemon" + ".lnk");
                shortcut.TargetPath = path_exe;
                shortcut.WorkingDirectory = Directory.GetCurrentDirectory();
                shortcut.WindowStyle = 1;
                shortcut.Description = "Rclone_Daemon";
                shortcut.IconLocation = path_exe;
                shortcut.Arguments = "";
                shortcut.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void 开机启动_Click(object sender, EventArgs e)
        {
            if (开机启动.Checked)
            {
                try
                {
                    File.Delete(startpath + "Rclone_Daemon" + ".lnk");
                    开机启动.Checked = false;
                    MessageBox.Show("已取消开机自启。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                if (Create_ink())
                {
                    MessageBox.Show("开机启动设置成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    开机启动.Checked = true;
                }
                else
                {
                    MessageBox.Show("开机启动设置失败！请检查是否被杀毒软件拦截。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void notify_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about ab =new about();
            ab.ShowDialog();

        }
    }
}
