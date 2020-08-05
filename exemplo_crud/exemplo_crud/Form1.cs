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

namespace exemplo_crud
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbState.Items.Add("SP");
            cmbState.Items.Add("PR");
            cmbState.Items.Add("BA");

            cmbCity.Items.Add("Campinas");
            cmbCity.Items.Add("Curitiba");
            cmbCity.Items.Add("Salvador");
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection cn = new MySqlConnection("server=localhost; username=bruno; database=exemplodb; password=dbadmin");
                cn.Open();

                MySqlCommand cmd = new MySqlCommand("insert into tabela (codigo, nome, cidade, estado, endereco) values (null, ?, ?, ?, ?)",cn);

                cmd.Parameters.Add("@nome", MySqlDbType.VarChar, 60).Value = txbName.Text;
                cmd.Parameters.Add("@cidade", MySqlDbType.VarChar, 28).Value = cmbCity.SelectedItem.ToString();
                cmd.Parameters.Add("@estado", MySqlDbType.VarChar, 2).Value = cmbState.SelectedItem.ToString();
                cmd.Parameters.Add("@enderco", MySqlDbType.VarChar, 100).Value = txbAddress.Text;

                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Dados inseridos com sucesso!!");
            }

            catch (Exception error)
            {
                MessageBox.Show("Não connectou. Erro: " + error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection cn = new MySqlConnection("server=localhost; username=bruno; database=exemplodb; password=dbadmin");
                cn.Open();

                MySqlCommand cmd = new MySqlCommand("update tabela set nome = ?, cidade = ?, estado = ?, endereco = ? where codigo = ?", cn);

                cmd.Parameters.Add("@nome", MySqlDbType.VarChar, 60).Value = txbName.Text;
                cmd.Parameters.Add("@cidade", MySqlDbType.VarChar, 28).Value = cmbCity.SelectedItem.ToString();
                cmd.Parameters.Add("@estado", MySqlDbType.VarChar, 2).Value = cmbState.SelectedItem.ToString();
                cmd.Parameters.Add("@endereco", MySqlDbType.VarChar, 100).Value = txbAddress.Text;
                cmd.Parameters.Add("@codigo", MySqlDbType.Int32, 10).Value = txbCode.Text;

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Dados alterados com sucesso!!");
            }

            catch (Exception error)
            {
                MessageBox.Show("Não foi possível alterar este(s) dado(s). \nErro: " + error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection cn = new MySqlConnection("server=localhost; username=bruno; database=exemplodb; password=dbadmin");
                cn.Open();

                MySqlCommand cmd = new MySqlCommand("delete from tabela where codigo = ?", cn);

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@codigo", MySqlDbType.Int32, 10).Value = txbCode.Text;

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                MessageBox.Show("Dados removidos com sucesso!!");
                cn.Close();
            }

            catch (Exception error)
            {
                MessageBox.Show("Erro ao remover um dado. \nErro: " + error);
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection cn = new MySqlConnection("server=localhost; username=bruno; database=exemplodb; password=dbadmin");
                cn.Open();

                MySqlCommand cmd = new MySqlCommand("select nome, cidade, estado, endereco from tabela where codigo = ?", cn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@codigo", MySqlDbType.Int32).Value = txbCode.Text;

                cmd.CommandType = CommandType.Text;

                MySqlDataReader dr;
                dr = cmd.ExecuteReader();
                dr.Read();

                txbName.Text = dr.GetString(0);
                cmbCity.Text = dr.GetString(1);
                cmbState.Text = dr.GetString(2);
                txbAddress.Text = dr.GetString(3);

                cn.Close();
            }

            catch (Exception error)
            {
                MessageBox.Show("Erro na busca do registro. \nErro: " + error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txbCode.Text = "";
            txbAddress.Text = "";
            cmbCity.Text = "";
            cmbState.Text = "";
            txbName.Text = "";
        }
    }
}
