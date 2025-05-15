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
    public partial class CaixaSelect : Form
    {
        Conexao conn = new Conexao();
        public CaixaSelect()
        {
            InitializeComponent();
        }

        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblName.Text = dataGrid.CurrentRow.Cells[1].Value.ToString();
            DataTable dt = conn.GetData($"SELECT * FROM mer_caixa WHERE nome = '{lblName.Text}'");
            Program.caixa = dt.Rows[0]["nome"].ToString();
            string algo = dt.Rows[0]["IDcaixa"].ToString();
            Program.idcaixa = int.Parse(algo); 
            this.Close();
        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CaixaSelect_Load(object sender, EventArgs e)
        {
            dataGrid.DataSource = conn.GetData("SELECT * FROM mer_caixa");
            dataGrid.Columns[0].Width = 65;
            dataGrid.Columns[1].Width = 350;
        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }
    }
}
