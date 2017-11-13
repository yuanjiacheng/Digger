using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiggerForm
{
    public partial class From : Form
    {
        public From()
        {
            InitializeComponent();
        }
        CreateDiggerTemplateForm cdtf = null;
        DiggerData dd = null;
        ScheduledTask st = null;
        private void button1_Click(object sender, EventArgs e)
        {
            if (cdtf == null || cdtf.IsDisposed)
                cdtf = new CreateDiggerTemplateForm();
            cdtf.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dd == null || dd.IsDisposed)
                dd = new DiggerData();
            dd.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (st == null || st.IsDisposed)
                st = new ScheduledTask();
            st.Show();
        }

        private void MainFrom_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cdtf != null && !cdtf.IsDisposed)
            {
                cdtf.Close();
                cdtf.Dispose();
            }
            if (dd != null && !dd.IsDisposed)
            {
                dd.Close();
                dd.Dispose();
            }
            if (st != null && !st.IsDisposed)
            {
                st.Close();
                st.Dispose();
            }
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
