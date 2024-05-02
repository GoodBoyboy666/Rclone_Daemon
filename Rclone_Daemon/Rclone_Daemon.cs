//#define Debug   //����ר��

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using IWshRuntimeLibrary;
using File = System.IO.File;

namespace Rclone_Daemon
{
    //Rclone���̽ṹ��
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
        string Rclone_Path = "";    //Rclone����·��
        string APPDATA = "";    //APPDATA�ļ���·��
        string startpath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\";     //startupĿ¼·��
        string path_exe = Process.GetCurrentProcess().MainModule.FileName;      //��ǰ����·��
        List<Rclone_Process> rdlist = new List<Rclone_Process>();    //ά��Rclone���̼���
        private void Rclone_Daemon_Load(object sender, EventArgs e)
        {
            //����ظ�����

            Process[] is_run = Process.GetProcessesByName("Rclone_Daemon");
            foreach (Process process in is_run)
            {
                if (process.Id != Process.GetCurrentProcess().Id)
                {
                    MessageBox.Show("��⵽������Rclone_Daemon�������ظ����У�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Process.GetCurrentProcess().Kill();
                }
            }

            //������rclone����

            Process[] rclones = Process.GetProcessesByName("rclone");
            if(rclones.Length > 0)
            {
                DialogResult re= MessageBox.Show("��⵽�����е�rclone���̣�����Ϊ����δ�����˳��������Ľ��̣��Ƿ������","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (re == DialogResult.Yes)
                {
                    foreach (Process process in rclones)
                    {
                        process.Kill();
                    }
                }
            }

            //��ʼ������
            APPDATA = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (File.Exists($"{APPDATA}/Rclone_Daemon/config.json"))
            {
                //���ش���
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
                    MessageBox.Show(ex.Message.ToString(), "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("��ȡ�����ļ����ִ��󣡳����Զ�ɾ�������ļ���", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    File.Delete($"{APPDATA}/Rclone_Daemon/config.json");
                    MessageBox.Show("��������������ɳ�ʼ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.GetCurrentProcess().Kill();
                }
                if (Rclone_Path == null)
                    MessageBox.Show("δ����Rclone�ļ�·�������ڡ����á�������Rclone�����·����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    add_btn.Enabled = true;
                    change_btn.Enabled = true;
                    del_btn.Enabled = true;
                    ed_btn.Enabled = true;
                }

                //��ʼ�����ص�
                foreach (JObject mount in data_json["mount"])
                {
                    //���ع��ص�����
                    string name = mount["name"].Value<String>();
                    string path = mount["path"].Value<String>();
                    string drive = mount["drive"].Value<String>();
                    string status = mount["status"].Value<String>();
                    AddItem(name, path, drive, status);

                    //�������ص�
                    if (status == "������")
                    {
                        Status_Start(name, path, drive);
                    }
                }

            }
            else
            {
                //��ʾ����
                this.Opacity = 1;
                //��ʼ��config
                Directory.CreateDirectory($"{APPDATA}/Rclone_Daemon");
                File.Create($"{APPDATA}/Rclone_Daemon/config.json").Close();
                MessageBox.Show("Ϊ���������У����ڡ����á�������Rclone�����·����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (File.Exists(startpath + "Rclone_Daemon" + ".lnk"))
            {
                ��������.Checked = true;
            }

        }

        private void ��ʾ������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void �˳�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //������ص�����
            save_data_json();

            //�ͷ����й��ص�
            foreach (Rclone_Process rp in rdlist)
            {
                try
                {
                    Process p = Process.GetProcessById(rp.Id);
                    p.Kill();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message,"����",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            //�ر�������
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
                AddItem(args[1], args[2], args[3], "�ѽ���");

                //���������ļ�
                save_data_json();
            }
        }
        /// <summary>
        /// ����ListViewItem����
        /// </summary>
        /// <param name="name">���ص�����</param>
        /// <param name="path">����·��</param>
        /// <param name="drive">�̷�</param>
        /// <param name="status">״̬</param>
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
        /// ���������ļ�����
        /// </summary>
        private void save_data_json()
        {
            JObject data_json = new JObject();
            data_json.Add("Rclone_Path", Rclone_Path);

            //�����ѱ���Ĺ��ص�
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

            //д��config
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

                    //�Զ�����״̬
                    if (mount_box.SelectedItems[0].SubItems["Status"].Text == "������")
                    {
                        Change_Status("disable");
                        Change_Status("enable");
                    }

                    //���������ļ�
                    save_data_json();
                }
            }
            else
            {
                MessageBox.Show("��ѡ��һ������޸ģ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void mount_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mount_box.SelectedItems.Count > 0 && mount_box.SelectedItems[0].SubItems["Status"].Text == "�ѽ���")
            {
                ed_btn.Text = "����";
            }
            else if (mount_box.SelectedItems.Count > 0 && mount_box.SelectedItems[0].SubItems["Status"].Text == "������")
            {
                ed_btn.Text = "����";
            }
        }

        private void ed_btn_Click(object sender, EventArgs e)
        {
            if (ed_btn.Text == "����" && mount_box.SelectedItems.Count > 0)
            {
                if (Change_Status("enable"))
                {
                    mount_box.SelectedItems[0].SubItems["Status"].Text = "������";
                    ed_btn.Text = "����";

                    //���������ļ�
                    save_data_json();
                }
                else
                {
                    MessageBox.Show("����ʧ�ܣ�","����",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (ed_btn.Text == "����" && mount_box.SelectedItems.Count > 0)
            {
                if (Change_Status("disable"))
                {
                    mount_box.SelectedItems[0].SubItems["Status"].Text = "�ѽ���";
                    ed_btn.Text = "����";

                    //���������ļ�
                    save_data_json();
                }
                else
                {
                    MessageBox.Show("����ʧ�ܣ�����rclone����δ���������쳣ֹͣ��","����",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else if (mount_box.SelectedItems.Count < 1)
            {
                MessageBox.Show("��ѡ��һ��������ã�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// �ı����״̬
        /// </summary>
        /// <param name="operation">������</param>
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
        /// �ر�Rclone����
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
            MessageBox.Show("Rclone���̹ر�ʧ�ܣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        /// <summary>
        /// ����Rclone����
        /// </summary>
        /// <param name="name">���ص�����</param>
        /// <param name="path">����·��</param>
        /// <param name="drive">�̷�</param>
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
            if (mount_box.SelectedItems[0].SubItems["Status"].Text == "������")
            {
                if (Change_Status("disable"))
                {
                    mount_box.SelectedItems[0].Remove();
                }
                else
                {
                    MessageBox.Show("ɾ��ʧ�ܣ�����rclone����δ���л����쳣ֹͣ��", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult re= MessageBox.Show("�Ƿ�ǿ��ɾ����","ɾ��",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (re == DialogResult.Yes)
                    {
                        string select = mount_box.SelectedItems[0].Text;
                        mount_box.SelectedItems[0].Remove();
                        if(Status_Stop(select, true))
                            MessageBox.Show("ǿ��ɾ���ɹ���","��ʾ",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                mount_box.SelectedItems[0].Remove();
            }

            //���������ļ�
            save_data_json();
        }

        private void ����Rclone·��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectFile.ShowDialog();
            if (SelectFile.FileName != "")
            {
                Rclone_Path = SelectFile.FileName;
                MessageBox.Show("���óɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                add_btn.Enabled = true;
                change_btn.Enabled = true;
                del_btn.Enabled = true;
                ed_btn.Enabled = true;
            }
        }

        /// <summary>
        /// ������ݷ�ʽ
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

        private void ��������_Click(object sender, EventArgs e)
        {
            if (��������.Checked)
            {
                try
                {
                    File.Delete(startpath + "Rclone_Daemon" + ".lnk");
                    ��������.Checked = false;
                    MessageBox.Show("��ȡ������������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("�����������óɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ��������.Checked = true;
                }
                else
                {
                    MessageBox.Show("������������ʧ�ܣ������Ƿ�ɱ��������ء�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void notify_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about ab =new about();
            ab.ShowDialog();

        }
    }
}
