using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Rclone_Daemon
{
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
        }
        string Rclone_Path = "";
        List<Rclone_Process> rdlist;//ά��Rclone���̼���
        JObject data_json;
        private void Rclone_Daemon_Load(object sender, EventArgs e)
        {
            string APPDATA = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (File.Exists($"{APPDATA}/Rclone_Daemon/config.json"))
            {
                //this.BeginInvoke(new Action(() => {
                //    this.Hide();
                //    this.Opacity = 1;
                //}));
                StreamReader sr = new StreamReader($"{APPDATA}/Rclone_Daemon/config.json");
                string text_json=sr.ReadToEnd();
                try
                {
                    data_json = JsonConvert.DeserializeObject<JObject>(text_json);
                    Rclone_Path = data_json["Rclone_Path"].Value<String>();
                    foreach( JObject mount in data_json["mount"])
                    {
                        AddItem(mount["name"].Value<String>(), mount["path"].Value<String>(), mount["drive"].Value<String>(), mount["status"].Value<String>());
                    }
                }
                catch {
                    MessageBox.Show("��ȡ�����ļ����ִ���");
                }
                
            }
            else
            {
                Directory.CreateDirectory($"{APPDATA}/Rclone_Daemon");
                File.Create($"{APPDATA}/Rclone_Daemon/config.json").Close();
                MessageBox.Show("Ϊ���������У����ڡ����á�������Rclone�����·����");
            }
            rdlist = new List<Rclone_Process>();
        }

        private void ��ʾ������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void �˳�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void Rclone_Daemon_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            //this.Hide();
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
            }
        }

        private void AddItem(string name,string path ,string drive,string status)
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
                }
            }
            else
            {
                MessageBox.Show("��ѡ��һ������޸ģ�");
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
            if (ed_btn.Text == "����")
            {
                if (Change_Status("enable"))
                {
                    mount_box.SelectedItems[0].SubItems["Status"].Text = "������";
                    ed_btn.Text = "����";
                }
            }
            else if (ed_btn.Text == "����")
            {
                if (Change_Status("disable"))
                {
                    mount_box.SelectedItems[0].SubItems["Status"].Text = "�ѽ���";
                    ed_btn.Text = "����";
                }
            }
        }

        private bool Change_Status(string operation)
        {
            if (operation == "enable")
            {
                string start_args = "mount" + " " + $"{mount_box.SelectedItems[0].Text}:{mount_box.SelectedItems[0].SubItems["Mount_Path"].Text}" + " " + $"{mount_box.SelectedItems[0].SubItems["Drive"].Text}" + " " + "--vfs-cache-mode writes";
                Process process = new Process();
                process.StartInfo.FileName = Rclone_Path;
                process.StartInfo.Arguments = start_args;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                Rclone_Process rd = new Rclone_Process();
                rd.Mount_Name = mount_box.SelectedItems[0].Text;
                rd.Id = process.Id;
                rdlist.Add(rd);
                return true;
            }
            else if (operation == "disable")
            {
                foreach (Rclone_Process rd in rdlist)
                {
                    if (rd.Mount_Name == mount_box.SelectedItems[0].Text)
                    {
                        rdlist.Remove(rd);
                        Process p= Process.GetProcessById(rd.Id);
                        p.Kill();
                        return true;
                    }
                }
                MessageBox.Show("Rclone���̹ر�ʧ�ܣ�");
                return false;
            }
            return false;
        }
    }
}
