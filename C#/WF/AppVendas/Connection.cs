using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace AppVendas
{
    internal class Conexao
    {
        public string connectionString = null;
        public MySqlConnection connection = null;
        public string sql;
        public string server = "127.0.0.1";
        public string database = "pdv";
        public string porta = "3306";
        public string username = "root";
        public string password = "";


        public void MySQLCRUD()
        {
            connectionString = $"Server={server};Database={database};UID={username};PWD={password};PORT={porta}";
            connection = new MySqlConnection(connectionString);
        }
        //Abrir conexao

        public Boolean AbrirConexao()
        {
            try
            {
                MySQLCRUD();
                if (connection.State == ConnectionState.Closed)
                {
                    connection = new MySqlConnection(connectionString);
                    connection.Open();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar Conectar ao Banco: " + ex.Message);
                return false;
            }
        }
        public void FecharConexao()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection = new MySqlConnection(connectionString);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Método que retorna uma leitura do Banco de dados;
        public DataTable GetData(string query)
        {
            try
            {
                AbrirConexao();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
                return null;
            }
            finally
            {
                FecharConexao();
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //MÉTODO QUE EXECUTA UMA SQL:
        public bool ExecuteQuery(string query)
        {
            try
            {
                AbrirConexao();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
                return false;
            }
            finally
            {
                FecharConexao();
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------
        //MÉTODO QUE ADICIONA NO BANCO DE DADOS:
        public bool InsertProductData(string Descricao, string Valor, string QTD, string vld,string hour)
        {
            try
            {
                AbrirConexao();

                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO mer_produtos(Descricao,ValorUnit,Quantidade,validade,horaCadastro) VALUES(@Descricao,@Valor,@Quantidade,@vld,@hora)";
                cmd.Parameters.AddWithValue("@Descricao", Descricao);
                cmd.Parameters.AddWithValue("@Valor", Valor);
                cmd.Parameters.AddWithValue("@Quantidade", QTD);
                cmd.Parameters.AddWithValue("@vld", vld);
                cmd.Parameters.AddWithValue("@hora", hour);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
                return false;
            }
            finally
            {
                FecharConexao();
            }
        }
        public bool InsertSell(string idVenda, int codProd, string QTD, string valor, string date,string hour,int idCaixa,string status,string nomeProd)
        {
            try
            {
                AbrirConexao();

                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO mer_vendas(IDvenda,CodigoProduto,Quantidade,valorUnit,data,hora,IDcaixa,status,NomeProduto) VALUES(@IDvenda,@CodigoProduto,@Quantidade,@valorUnit,@data,@hora,@IDcaixa,@status,@name)";
                cmd.Parameters.AddWithValue("@IDvenda", idVenda);
                cmd.Parameters.AddWithValue("@CodigoProduto", codProd);
                cmd.Parameters.AddWithValue("@Quantidade", QTD);
                cmd.Parameters.AddWithValue("@valorUnit", valor);
                cmd.Parameters.AddWithValue("@data", date);
                cmd.Parameters.AddWithValue("@hora", hour);
                cmd.Parameters.AddWithValue("@IDcaixa", idCaixa);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@name", nomeProd);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
                return false;
            }
            finally
            {
                FecharConexao();
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------
        //MÉTODO QUE EDITA INFORMAÇÕES:
        public bool EditarData(string Descricao1, string Valor1, string Quantidade, string data, int ID)
        {
                try
                {
                    AbrirConexao();

                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "UPDATE mer_produtos SET Descricao = @Descricao, ValorUnit = @Valor, Quantidade = @Quantidade,validade = @data WHERE CodigoProduto = @ID";
                    cmd.Parameters.AddWithValue("@Descricao", Descricao1);
                    cmd.Parameters.AddWithValue("@Valor", Valor1);
                    cmd.Parameters.AddWithValue("@Quantidade", Quantidade);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("data", data);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                    return false;
                }
                finally
                {
                    FecharConexao();
                }
            
        }
    }     
}

