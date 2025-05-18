import mysql.connector
from mysql.connector import Error as sqlErr
from Classes.text_writter import TxtWriter

class Database:

    def __init__(self, host, user, password, db):
        self.writer = TxtWriter()

        self.host = host
        self.user = user
        self.password = password
        self.database = db
        self.connection = None
        self.cursor = None

    def connect_to(self):
        try:
            self.connection = mysql.connector.connect(
                host = self.host,
                user = self.user,
                password = self.password,
                database = self.database,
            )
            if self.connection.is_connected():
                self.cursor = self.connection.cursor()
                print("Conexão Aberta!")       
        except sqlErr as e:
            print(f"Erro no metodo connect_to da classe Database: \n {e}")
            self.writer.write_logs("Database", "connect_to", e)

    def close_connection(self):
        try:
            if self.connection.is_connected():
                if self.cursor:
                    self.cursor.close()            
                self.connection.close()
                print("Conexão Fechada")
        except sqlErr as e:
            print(f"Erro no método close_connection da classe Database: \n {e}")
            self.writer.write_logs("Database", "close_connection", e)

    def commitChanges(self):
        try:
            self.connection.commit()
        except mysql.connector.Error as e:
            print(f"Erro: {e}")
            self.rollbacks()


    def rollbacks(self):
        try:
            self.connection.rollback()
            print("Alterações desfeitas!")
        except mysql.connector.Error as e:
            print(f"Erro: {e}")
    def sql_queries(self, queryType, query, params=None):        
        try:
            self.connect_to()
            if not self.connection or not self.connection.is_connected():
                print("Conexão não estabelecida.")
                return None
                
            if queryType.upper() == "INSERT" or queryType.upper() == "UPDATE" or queryType.upper() == "DELETE":
                if self.connection.is_connected():
                    self.cursor.execute(query, params)
                    self.commitChanges()
                    print("Alteração concluída!")
                    return True
                else:
                    print("Erro em sql_queries ao dar commit")
                    return None         
            elif queryType.upper() == "SELECT":
                self.cursor.execute(query, params)
                results = self.cursor.fetchall()
                return results  
            else:
                print("Tipo de query inválida!")
                return None         
        except sqlErr as e:
            print(f"Erro no método sql_queries da classe Database: \n {e}")
            self.writer.write_logs("Database", "sql_queries", e)
            self.rollbacks()
            return False     
        finally:
            self.close_connection()       