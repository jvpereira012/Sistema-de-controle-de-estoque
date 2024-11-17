using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // <-- Adicionar o MySql

namespace prjMinhaEmpresa
{
    public partial class frmNovoUsuario : Form
    {

        // Variaveis de controle de conexao
        MySqlConnection conexao;
        MySqlCommand comando;
        MySqlDataAdapter da;
        MySqlDataReader dr;
        string strSQL;

        public frmNovoUsuario()
        {
            InitializeComponent();
            // Criando a string de Conexão 
            conexao = new MySqlConnection("Server = localhost; Port = 3306; Database = bd_MinhaEmpresa_ETIM; User = root");
        }

       

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

     

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    strSQL = "SELECT * from tb_Login WHERE log_usuario = @parUsuario";
                    comando = new MySqlCommand(strSQL, conexao);
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@parUsuario", txtUsuario.Text);
                    conexao.Open();
                    dr = comando.ExecuteReader();

                    if (!dr.HasRows)
                    {
                        textNomeUsuario.Enabled = true;
                        txtSenha.Enabled = true;
                        btnCadastrar.Enabled = true;
                        textNomeUsuario.Clear();
                        txtSenha.Clear();
                        textNomeUsuario.Focus();
                        toolStripStatusLabel.Text = "Digite o nome do usuário.";
                    }
                    else 
                    {
                        while (dr.Read())
                            textNomeUsuario.Text = Convert.ToString(dr["log_nome"]);
                            txtSenha.Text = Convert.ToString(dr["log_senha"]);
                    }
                    toolStripStatusLabel.Text = "Usuário já Cadastrado!!!";
                }
                catch (MySqlException Erro)
                {
                    MessageBox.Show("Erro ==> " + Erro.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                finally
                {
                    conexao.Close();
                    comando = null;
                }
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                strSQL = "INSERT INTO tb_Login (log_usuario, log_senha, log_nome, log_ult_Atualizacao) values (@parUsuario, @parSenha, @parNomeUsuario, CURRENT_TIMESTAMP)";
                comando = new MySqlCommand(strSQL, conexao);
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@parUsuario", txtUsuario.Text);
                comando.Parameters.AddWithValue("@parSenha", txtSenha.Text);
                comando.Parameters.AddWithValue("@parNomeUsuario", textNomeUsuario.Text);
                conexao.Open();
                comando.ExecuteNonQuery();
                toolStripStatuslblMSG.Text = "Cadastro efetuado com Sucesso!!";

                textNomeUsuario.Enabled = true;
                txtSenha.Enabled = true;
                btnCadastrar.Enabled = true;
            }
            catch (MySqlException Erro)
            {
                MessageBox.Show("Erro ==> " + Erro.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally 
            {
                conexao.Close();
                comando = null;
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtUsuario.Clear();
            textNomeUsuario.Clear();
            txtSenha.Clear();
        }

        private void frmNovoUsuario_Load(object sender, EventArgs e)
        {

        }
    }
}
