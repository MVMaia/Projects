using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace AppVendas
{
    public partial class Carrinho : Form
    {
        Conexao conn = new Conexao();
        public Carrinho()
        {
            InitializeComponent();
        }

        private void Carrinho_Load(object sender, EventArgs e)
        {
            string cu = $"00{Program.i}";
            DataTable resultado = conn.GetData($"SELECT * FROM mer_vendas WHERE IDvenda = {cu}");
            for (int i = 0; i < resultado.Rows.Count; i++)
            {
                string id = resultado.Rows[i]["CodigoProduto"].ToString();
                string qtd = "4";
                string idZero = AddZeros(id, qtd);
                lbox.Items.Add(idZero + "-" + resultado.Rows[i]["NomeProduto"] + "-" + resultado.Rows[i]["Quantidade"].ToString() + "- " + "R$" + resultado.Rows[i]["valorUnit"]);

            }
            lblTotal.Text = TotalPrice();
        }
        public string AddZeros(string text, string qtd)
        {
            string quantidade = $"D{qtd}";
            string zeroAdd = int.Parse(text).ToString(quantidade);
            return zeroAdd;
        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            //Adiciona 0 aos ids:
            string idvenda;
            if (Program.i > 9)
            {
                idvenda = $"0{Program.i}";
            }
            else if (Program.i > 99)
            {
                idvenda = $"{Program.i}";
            }
            else
            {
                idvenda = $"00{Program.i}";
            }
            //remove itens comprados do Estoque:
            List<string> list = new List<string>();
            for(int i = 0; i <= lbox.Items.Count-1; i++)
            {
                string algo = lbox.Items[i].ToString();
                list.Add(algo);
            }
            for (int v = 0; v <= lbox.Items.Count - 1; v++)
            {
                string[] removeQTD = list[v].Split('-');
                int qtdcomprada = int.Parse(removeQTD[2]);
                string idNoZero = removeQTD[0].TrimStart('0');
                DataTable all = conn.GetData($"SELECT * FROM mer_produtos WHERE CodigoProduto = {idNoZero}");
                int qtdTotal = int.Parse(all.Rows[0]["Quantidade"].ToString());
                int calc = qtdTotal - qtdcomprada;
                string calcTring = calc.ToString();
                conn.ExecuteQuery($"UPDATE mer_produtos SET Quantidade = '{calcTring}' WHERE CodigoProduto = {int.Parse(idNoZero)}");
            }
            //Atualiza a compra para concluido:
            string status1 = "Concluido";
            conn.ExecuteQuery($"UPDATE mer_vendas SET status = '{status1}' WHERE IDvenda = '{idvenda}'");
            MessageBox.Show("Compra concluida", "Leia isso:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Program.i = Program.i + 1;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string idvenda;
            if(Program.i > 9)
            {
                idvenda = $"0{Program.i}";
            }
            else if(Program.i > 999)
            {
                idvenda = $"{Program.i}";
            }
            else
            {
                idvenda = $"00{Program.i}";
            }
            lbox.Items.Clear();
            string status2 = "Cancelado";
            string sql = $"UPDATE mer_vendas SET status = '{status2}' WHERE IDvenda = '{idvenda}'";
            conn.ExecuteQuery(sql);
            Program.i = Program.i + 1;
            MessageBox.Show("Compra cancelada", "Leia isso:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            this.Close();
        }

        private void lbox_Click(object sender, EventArgs e)
        {
            DialogResult box1 = MessageBox.Show("Tem certeza que quer remover esse item do carrinho?", "Leia issor:", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if( box1 == DialogResult.OK)
            {
                string item = lbox.SelectedItem.ToString();
                string[] ui = item.Split('-');
                string num = ui[0].TrimStart('0');
                string andante = "em andamento";
                bool test = conn.ExecuteQuery($"DELETE FROM mer_vendas WHERE CodigoProduto = '{num}'AND status = '{andante}'");
                if( test == true )
                {
                    lbox.Items.Remove(item);
                }
            }
        }
        private string TotalPrice()
        {
            int total = 0;
            List<string> list1 = new List<string>();
            for (int i = 0; i <= lbox.Items.Count - 1; i++)
            {
                string algo = lbox.Items[i].ToString();
                list1.Add(algo);
            }
            for (int v = 0; v <= lbox.Items.Count - 1; v++)
            {
                string[] removeQTD = list1[v].Split('-');
                string[] unitPrice = removeQTD[3].Split('$',',');
                string unitPrice1 = unitPrice[1];
                int intUnit = int.Parse(unitPrice1);
                total = total + intUnit;
            }
            return $"R${total},00";
        }
    }
}
