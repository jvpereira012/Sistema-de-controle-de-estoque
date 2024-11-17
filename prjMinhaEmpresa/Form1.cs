using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjMinhaEmpresa
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer_Tick(object sender, EventArgs e)
        {
             if (progressBar.Value < 100)
            {
                progressBar.Value = progressBar.Value + 1;
            }

            else
            {
                timer.Enabled = false;
                this.Visible = false;

                //Chamada da Tela de Login
                frmLogin frmLog = new frmLogin();
                frmLog.Show();
            }

        }
        
    }
}
