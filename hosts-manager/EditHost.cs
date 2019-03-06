using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace hosts_manager
{
    public partial class EditHost : Form
    {
        public Host host;

        public EditHost()
        {
            InitializeComponent();
            this.host = new Host();
        }

        public EditHost(Host _host)
        {
            InitializeComponent();
            this.host = _host;
            this.textBox1.Text = this.host.IP;
            this.textBox2.Text = this.host.Domain;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.host.IP = this.textBox1.Text;
            this.host.Domain = this.textBox2.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
