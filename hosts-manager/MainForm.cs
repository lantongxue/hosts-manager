using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using hosts_manager.Parse;

namespace hosts_manager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!HostsParse.HostsExists())
            {
                MessageBox.Show("系统hosts文件不存在！", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            foreach(Host host in HostsParse.ParseList())
            {
                ListViewItem listViewItem = new ListViewItem(new string[] { host.IP, host.Domain });
                listViewItem.Tag = host;
                listView1.Items.Add(listViewItem);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count > 0)
            {
                EditHost editHost = new EditHost((Host)listView1.SelectedItems[0].Tag);
                if(editHost.ShowDialog() == DialogResult.OK)
                {
                    ListViewItem listViewItem = new ListViewItem(new string[] { editHost.host.IP, editHost.host.Domain });
                    listViewItem.Tag = editHost.host;
                    listView1.Items[listView1.SelectedItems[0].Index] = listViewItem;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            EditHost editHost = new EditHost();
            if (editHost.ShowDialog() == DialogResult.OK)
            {
                ListViewItem listViewItem = new ListViewItem(new string[] { editHost.host.IP, editHost.host.Domain });
                listViewItem.Tag = editHost.host;
                listView1.Items.Add(listViewItem);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem listViewItem in listView1.SelectedItems)
            {
                listView1.Items.Remove(listViewItem);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            foreach (Host host in HostsParse.ParseList())
            {
                ListViewItem listViewItem = new ListViewItem(new string[] { host.IP, host.Domain });
                listViewItem.Tag = host;
                listView1.Items.Add(listViewItem);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try
            {
                List<Host> hosts = new List<Host>();
                foreach (ListViewItem listViewItem in listView1.Items)
                {
                    hosts.Add((Host)listViewItem.Tag);
                }
                string hosts_string = HostsParse.ListToString(hosts);
                HostsParse.ReWrite(hosts_string);
            }
            catch (UnauthorizedAccessException exception)
            {
                MessageBox.Show(exception.Message+"\r\n请以管理员的身份运行程序！", "UnauthorizedAccessException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message, "exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
