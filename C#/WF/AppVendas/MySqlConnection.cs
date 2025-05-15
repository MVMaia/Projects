using MySql.Data.MySqlClient;
using System;
using System.Data;

public class MySqlConnection
{
    private string connectionString;

    public MySqlConnection(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public ConnectionState State { get; internal set; }

    internal void Open()
    {
        throw new NotImplementedException();
    }

    internal void Close()
    {
        throw new NotImplementedException();
    }

    internal MySqlCommand CreateCommand()
    {
        throw new NotImplementedException();
    }
}