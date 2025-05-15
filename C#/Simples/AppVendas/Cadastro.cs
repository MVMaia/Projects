using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppVendas
{
    public partial class Cadastro : Form
    {
        Conexao conn = new Conexao();
        public Cadastro()
        {
            InitializeComponent();
        }

        private void Cadastro_Load(object sender, EventArgs e)
        {
            DataView.DataSource = conn.GetData("SELECT * FROM mer_produtos");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(btnSave.Text == "Salvar")
            {
                string date = DataAmericanaConverter(dateP1.Text.ToString());
                string hourNow = DateTime.Now.ToString("HH:mm");
                bool test = conn.InsertProductData(txtName.Text, txtValue.Text, txtQtd.Text,date,hourNow);
                if (test == true)
                {
                    MessageBox.Show("Salvo com Sucesso", "LEIA ISSOR:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refreshGrid();
                    txtName.Text = "";
                    txtValue.Text = "";
                    txtQtd.Text = "";
                    btnSave.Text = "Salvar";
                    txtName.Focus();
                }
                else
                {
                    MessageBox.Show("Erro", "LEIA ISSOR:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if(btnSave.Text == "Editar")
            {
                string date = DataAmericanaConverter(dateP1.Text.ToString());
                bool testEdit = conn.EditarData(txtName.Text, txtValue.Text, txtQtd.Text, date, int.Parse(lblID.Text));
                if(testEdit == true)
                {
                    MessageBox.Show("Editado com Sucesso", "LEIA ISSOR:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refreshGrid();
                    txtName.Text = "";
                    txtValue.Text = "";
                    txtQtd.Text = "";
                    btnSave.Text = "Salvar";
                }
                else
                {
                    MessageBox.Show("Erro", "LEIA ISSOR:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void refreshGrid()
        {
            DataView.DataSource = conn.GetData("SELECT * FROM mer_produtos");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool testDel = conn.ExecuteQuery($"DELETE FROM mer_produtos WHERE Descricao LIKE '%{txtName.Text}%'");
            if (testDel == true)
            {
                MessageBox.Show("Deletado com Sucesso", "LEIA ISSOR:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshGrid();
                txtName.Text = "";
                txtValue.Text = "";
                txtQtd.Text = "";
                btnSave.Text = "Salvar";
            }
            else
            {
                MessageBox.Show("Erro", "LEIA ISSOR:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DataView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblID.Text = DataView.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = DataView.CurrentRow.Cells[1].Value.ToString();
            txtValue.Text = DataView.CurrentRow.Cells[2].Value.ToString();
            txtQtd.Text = DataView.CurrentRow.Cells[3].Value.ToString();
            btnSave.Text = "Editar";
        }
        private string DataAmericanaConverter(string date)
        {
            string[] dateSp = date.Split('/');
            string dateCor = $"{dateSp[2]}-{dateSp[1]}-{dateSp[0]}";
            return dateCor;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if(txtName.Text == "")
            {
                btnSave.Text = "Salvar";
                txtQtd.Clear();
                txtValue.Clear();
            }
        }
    }
}
