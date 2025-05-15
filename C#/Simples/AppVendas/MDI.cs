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
    public partial class MDI : Form
    {
        Vendas vendas = new Vendas();
        Cadastro cadastro = new Cadastro();
        ConsultaVaIidade validade = new ConsultaVaIidade();
        CaixaSelect caixa = new CaixaSelect();
        public MDI()
        {
            InitializeComponent();
        }

        private void vendasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vendas.MdiParent = this;
            vendas.Show();
        }

        private void cadastroDeProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cadastro.MdiParent = this;
            cadastro.Show();
        }

        private void validadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            validade.MdiParent = this;
            validade.Show();
        }

        private void MDI_Load(object sender, EventArgs e)
        {

        }
    }
}
