using System;
using System.Data.SQLite;

class Program
{
    static void Main()
    {
        string connectionString = "Data Source=./database/database.db";
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Conexão aberta com sucesso!");

                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Clientes (
                        Id INTEGER PRIMARY KEY,
                        Nome TEXT NOT NULL,
                        Email TEXT UNIQUE,
                        DataDeNascimento DATE,
                        Saldo DECIMAL(10, 2)
                    );";

                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Tabela 'Clientes' criada com sucesso!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
    }
}
