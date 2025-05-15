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
    public partial class Vendas : Form
    {
        //Cria um objeto da classe Conexao:
        Conexao conn = new Conexao();
        public Vendas()
        {
            InitializeComponent();
        }
        //Executa a função ao carregar a página:
        private void Vendas_Load(object sender, EventArgs e)
        {
            //Puxa tudo da tabela e a coloca em um DataTable:
            DataTable resultado = conn.GetData("SELECT * FROM mer_produtos");
            //Verifica o banco de dados e adiciona o id da venda a variavel global:
            DataTable vendas = conn.GetData("SELECT * FROM mer_vendas");
            int linhasDT = vendas.Rows.Count;
            if(linhasDT > 0)
            {
                //dá continuação a venda anterior caso não tenha sido concluida
                if (vendas.Rows[linhasDT - 1]["status"].ToString() == "em andamento")
                {
                    string idvenda = vendas.Rows[linhasDT - 1]["idVenda"].ToString();
                    Program.i = int.Parse(idvenda);
                    Program.status = "Andamento";
                }
                else
                {
                    string idvenda = vendas.Rows[linhasDT - 1]["idVenda"].ToString();
                    Program.i = int.Parse(idvenda) + 1;
                }
            }
            else
            {
                Program.i = 1;
            }
            //Verifica se há produtos no banco de dados e se não houver, solicita que cadastre:
            if(resultado.Rows.Count == 0)
            {
                MessageBox.Show("Não há produtos cadastrados no sistema, por gentileza, cadastre produtos");
                Cadastro cadastro = new Cadastro();
                cadastro.ShowDialog();
            }
            DataTable resultado1 = conn.GetData("SELECT * FROM mer_produtos");
            for (int i = 0; i < resultado1.Rows.Count; i++)
            {
                string id = resultado1.Rows[i]["CodigoProduto"].ToString();
                string qtd = "4";
                string idZero = AddZeros(id, qtd);
                LBOX.Items.Add(idZero + "-" + resultado1.Rows[i]["Descricao"]);

            }
            //Seleciona o Caixa:
            CaixaSelect caixa = new CaixaSelect();
            caixa.ShowDialog();
            lblcx.Text = $"Caixa:{Program.caixa}";
            lblIDCAIXA.Text = $"ID caixa:{Program.idcaixa}";
            if(Program.status == "Andamento")
            {
                MessageBox.Show("Você tem uma compra em andamento, conclua ela", "LEIA ISSO:", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                Carrinho car = new Carrinho();
                car.Show();
            }
        }

        private void LBOX_Click(object sender, EventArgs e)
        {
            string line = LBOX.SelectedItem.ToString();
            string[] lineS = line.Split('-');
            string id = lineS[0];
            DataTable info = conn.GetData($"SELECT * FROM mer_produtos WHERE CodigoProduto = '{id}'");
            lblID.Text = info.Rows[0]["CodigoProduto"].ToString();
            lblName.Text = info.Rows[0]["Descricao"].ToString();
            lblValorUnit.Text = info.Rows[0]["ValorUnit"].ToString();
            txtBarras.Text = info.Rows[0]["CodigoProduto"].ToString();
            txtName.Text = info.Rows[0]["Descricao"].ToString();
            txtQtd.Focus();
            if(txtQtd.Text != "")
            {
                lblQTD.Text = txtQtd.Text;
                double num = double.Parse(txtQtd.Text);
                double price = double.Parse(lblValorUnit.Text);
                double total = num * price;
                lblTotalPag.Text = $"R${total}";
                lblsubtotal.Text = $"R${total}";
            }
        }

        private void txtQtd_TextChanged(object sender, EventArgs e)
        {
            if (txtQtd.Text != "" && txtQtd.Text != "0" && txtQtd.Text != null && txtBarras.Text != "" && txtName.Text != "")
            {
                lblQTD.Text = txtQtd.Text;
                double num = double.Parse(txtQtd.Text);
                double price = double.Parse(lblValorUnit.Text);
                double total = num * price;
                lblTotalPag.Text = $"R${total}";
                lblsubtotal.Text = $"R${total}";
            }
        }

        private void txtBarras_TextChanged(object sender, EventArgs e)
        {
            if(txtBarras.Text != "")
            {
                LBOX.Items.Clear();
                DataTable resultado = conn.GetData($"SELECT * FROM mer_produtos WHERE CodigoProduto LIKE '{txtBarras.Text}%'");
                for (int i = 0; i < resultado.Rows.Count; i++)
                {

                    LBOX.Items.Add($"{resultado.Rows[i]["CodigoProduto"]}-{resultado.Rows[i]["Descricao"]}");
                }
                LBOX.Refresh();
            }
            else if (txtBarras.Text == "")
            {
                LBOX.Items.Clear();
                DataTable resultado = conn.GetData("SELECT * FROM mer_produtos");
                for (int i = 0; i < resultado.Rows.Count; i++)
                {
                    LBOX.Items.Add($"{resultado.Rows[i]["CodigoProduto"]}-{resultado.Rows[i]["Descricao"]}");
                }
                LBOX.Refresh();
            }
            //--------------------------------------------------------------------------------------------------
        }
        public string AddZeros(string text, string qtd)
        {
            string quantidade = $"D{qtd}";
            string zeroAdd = int.Parse(text).ToString(quantidade);
            return zeroAdd;
        }


        private void btnADDprod_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string hour = DateTime.Now.ToString("HH:mm");
            string qtd1 = "3";
            string idZero = AddZeros(Program.i.ToString(),qtd1);
            bool cu = conn.InsertSell(idZero,int.Parse(lblID.Text),lblQTD.Text,lblValorUnit.Text,date,hour,Program.idcaixa,"em andamento",lblName.Text);
            if(cu == true)
            {
                MessageBox.Show("Enviado pro carrinho!", "Saber ler? então leia:", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                txtBarras.Clear();
                txtName.Clear();
                txtQtd.Clear();
                lblID.Text = "";
                lblName.Text = "";
                lblQTD.Text = "";
                lblsubtotal.Text = "";
                lblTotalPag.Text = "";
                lblValorUnit.Text = "";
                txtQtd.Focus();
            }
            else if(cu == false)
            {
                MessageBox.Show("Deu ruim colega!", "Saber ler? então leia:", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void btnConclude_Click(object sender, EventArgs e)
        {
            string cu = $"00{Program.i}";
            DataTable resultado = conn.GetData($"SELECT * FROM mer_vendas WHERE IDvenda = {cu}");
            if(resultado.Rows.Count == 0)
            {
                MessageBox.Show("O CARRINHO ESTÁ VAZIO!", "PRESTA ATENÇÃO!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(resultado.Rows.Count > 0)
            {
                Carrinho car = new Carrinho();
                car.Show();
            }
        }
    }
}
