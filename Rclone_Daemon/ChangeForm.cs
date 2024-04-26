using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rclone_Daemon
{
    public partial class ChangeForm : Form
    {
        public ChangeForm()
        {
            InitializeComponent();
        }
        Rclone_Daemon rd;
        private void ChangeForm_Load(object sender, EventArgs e)
        {
            rd = (Rclone_Daemon)this.Owner;
            if (rd.args[0] == "change")
            {
                mount_name.Text = rd.args[1];
                mount_path.Text = rd.args[2];
                drive.Text = rd.args[3];
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            rd.args[0] = "changed";
            rd.args[1]= mount_name.Text;
            rd.args[2] = mount_path.Text;
            rd.args[3]= drive.Text;
            this.Close();
        }
    }
}
