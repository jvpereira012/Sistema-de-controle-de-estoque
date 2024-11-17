using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace prjMinhaEmpresa
{
    public partial class frmLogin : Form
    {
        MySqlConnection conexao;
        MySqlCommand comando;
        MySqlDataAdapter da;
        MySqlDataReader dr;
        string strSQL;

        public frmLogin()
        {
            InitializeComponent();
            conexao = new MySqlConnection("Server = localhost; Port = 3306; Database = bd_MinhaEmpresa_ETIM; User = root");
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblNovoUsuario_Click(object sender, EventArgs e)
        {
            frmNovoUsuario fcnu = new frmNovoUsuario();
            fcnu.Show();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Confirma a saída da aplicação ?", "Mensagem", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                strSQL = "SELECT log_senha from tb_Login WHERE log_usuario = @parUsuario";
                comando = new MySqlCommand(strSQL, conexao);
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@parUsuario", txtUsuario.Text);
                conexao.Open();
                comando.ExecuteScalar();
            }
            catch (MySqlException Erro)
            {
                MessageBox.Show("Erro ==> " + Erro.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                if (comando.ExecuteScalar() == null)
                {
                    MessageBox.Show("Usuário não cadastrado!!! ", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (Convert.ToString(comando.ExecuteScalar()) != txtSenha.Text)
                    {
                        MessageBox.Show("Senha Inválida", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSenha.Focus();
                    }
                    else
                    {
                        this.Visible = false;
                        frmPrincipal objFPrinc = new frmPrincipal();
                        objFPrinc.ShowDialog(); 
                    }
                }
                conexao.Close();
                comando = null;
            }
        }
    }
}
