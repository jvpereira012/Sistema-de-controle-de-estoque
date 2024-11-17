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
    public partial class frmCadProd : Form
    {

        // Variaveis de controle de conexao
        MySqlConnection conexao;
        MySqlCommand comando;
        MySqlDataAdapter da;
        MySqlDataReader dr;
        string strSQL;

        // Variáveis de Trabalho Global
        decimal vPrcCusto = 0, vMarg_Lucro = 0, vPrcVenda = 0, vEstoq_Min = 0, vQtd_Estoq = 0;
    

        public frmCadProd()
        {
            InitializeComponent();
            // Criando a string de Conexão 
            conexao = new MySqlConnection("Server = localhost; Port = 3306; Database = bd_MinhaEmpresa_ETIM; User = root");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    strSQL = "SELECT * from tb_Produtos WHERE prod_Codigo = @parCodigo";
                    comando = new MySqlCommand(strSQL, conexao);
                    comando.Parameters.Clear();
                    comando.Parameters.AddWithValue("@parCodigo", txtCodigo.Text);
                    conexao.Open();
                    dr = comando.ExecuteReader();

                    if (dr.HasRows)
                    {

                    }
                    else
                    {
                        txtDescricao.Enabled = true;
                        txtQtdEstoq.Enabled = true;
                        txtEstoqMin.Enabled = true;
                        txtLocacao.Enabled = true;
                        txtMargLucro.Enabled = true;
                        txtPrcCusto.Enabled = true;
                        txtPrcVenda.Enabled = true;

                        txtDescricao.Focus();                       
                        toolStripStatuslblMSG.Text = "Informe a desxrição do produto!!";
                    }
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

        private void frmCadProd_Activated(object sender, EventArgs e)
        {
            txtCodigo.Focus();
        }

        private void txtMargLucro_KeyDown(object sender, KeyEventArgs e)
        {
            vPrcCusto = decimal.Parse(txtPrcCusto.Text);
            vMarg_Lucro = decimal.Parse(txtMargLucro.Text);
            vPrcVenda = vPrcCusto * (vMarg_Lucro / 100);
        }

    }

}
