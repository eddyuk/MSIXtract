using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSIXtractApplication
{
    public partial class FormMain : Form
    {
        XtractHandler xtractHandler;
        string[] args;
        public FormMain(string[] args)
        {
            InitializeComponent();
            this.args = args;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                xtractHandler = new XtractHandler(args);
                xtractHandler.Extract();
                Process.Start("explorer.exe", xtractHandler.ExtractDirectory);
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Dispose(true);
            }
        }

        void FormMain_Shown(object sender, System.EventArgs e)
        {
            this.Hide();
        }
    }
}
