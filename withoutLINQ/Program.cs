using System;
using System.Data.SQLite;

class Program{
    static void Main(){
        string connectionString = "Data source=../database/without.db";
        using (SQLiteConnection connection = new SQLiteConnection(connectionString)){
            try{
                connection.Open();
                Console.WriteLine("Conexão aberta com sucesso!");

                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Clientes(
                        Id INTEGER PRIMARY KEY NOT NULL,
                        Nome TEXT NOT NULL,
                        Saldo DECIMAL(10,2) NOT NULL
                    );";

                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection)){
                    command.ExecuteNonQuery();
                    Console.WriteLine("Tabela Clientes gerada com sucesso!");
                }
                
                string createFourUsers = @"
                    INSERT INTO Clientes(Id, Nome, Saldo) VALUES 
                    (1, 'Joao Arruda', '13.95'),
                    (2, 'Lucas Tomaz', '18.99'),
                    (3, 'Mateus Flosi', '195038.27'),
                    (4, 'Rodrigo Mendonça', '845011.83');";
                
                using (SQLiteCommand command = new SQLiteCommand(createFourUsers, connection)){
                    command.ExecuteNonQuery();
                    Console.WriteLine("Quatro clientes inseridos na tabela clientes com sucesso!");
                }

                string selectingUsers = @"SELECT * FROM Clientes WHERE Saldo > 100;";
                using (SQLiteCommand command = new SQLiteCommand(selectingUsers, connection)){
                    using (SQLiteDataReader reader = command.ExecuteReader()){
                        while (reader.Read()){
                            int id = Convert.ToInt32(reader["Id"]);
                            string nome = Convert.ToString(reader["Nome"]);
                            decimal saldo = Convert.ToDecimal(reader["Saldo"]);
                            Console.WriteLine($"Id: {id}, Nome: {nome}, Saldo: {saldo}");
                        }
                    }
                    Console.WriteLine("Seleção de clientes feita com sucesso!");
                }
                
                    
            }
            catch (Exception ex){
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
    }
}
