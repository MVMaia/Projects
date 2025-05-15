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
    public partial class ConsultaVaIidade : Form
    {
        Conexao conn = new Conexao();
        public ConsultaVaIidade()
        {
            InitializeComponent();
        }

        private void ConsultaVaIidade_Load(object sender, EventArgs e)
        {
            dataGrid.DataSource = conn.GetData("SELECT Descricao,validade FROM mer_produtos");
            dataGrid.Columns[0].Width = 345;
            dataGrid.Columns[1].Width = 80;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string dateC = DataAmericanaConverter(date2.Text);
            string dateCo = DataAmericanaConverter(date3.Text);
            dataGrid.DataSource = conn.GetData($"SELECT Descricao,validade FROM mer_produtos WHERE validade BETWEEN '{dateC}' AND '{dateCo}'");
            dataGrid.Columns[0].Width = 235;
            dataGrid.Columns[1].Width = 200;

        }
        private string DataAmericanaConverter(string date)
        {
            string[] dateSp = date.Split('/');
            string dateCor = $"{dateSp[2]}-{dateSp[1]}-{dateSp[0]}";
            return dateCor;
        }
        private string DataBrazilianConverter(string date)
        {
            string[] dateSp = date.Split('/');
            string dateCor = $"{dateSp[0]}/{dateSp[1]}/{dateSp[2]}";
            return dateCor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dateC = DataAmericanaConverter(date4.Text);
            dataGrid.DataSource = conn.GetData($"SELECT Descricao,validade FROM mer_produtos WHERE data LIKE '{dateC}' ");
            dataGrid.Columns[0].Width = 365;
            dataGrid.Columns[1].Width = 80;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            dataGrid.DataSource = conn.GetData($"SELECT Descricao,validadeFROM mer_produtos WHERE Descricao LIKE '%{txtName.Text}%'");
            dataGrid.Columns[0].Width = 365;
            dataGrid.Columns[1].Width = 80;
        }
    }
}
