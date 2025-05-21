from Classes.extractor import Extractor
from Classes.db_manager import Database
from Classes.text_writter import TxtWriter
from datetime import date


class Crawler:

    def __init__(self):
        self.db = Database('localhost', 'root', '' , 'crawlerpy')
        self.ext = Extractor()
        self.writer = TxtWriter()

    def process(self, url):
        try:
            dateToday = date.today()
            title = self.ext.collect_title(url)
            self.db.sql_queries("insert", "INSERT INTO titles(title, url, date) VALUES(%s,%s,%s)", [title, url,dateToday])
            
        except Exception as e:
            print(f"Erro no metodo process da classe Crawler, ocorreu o seguinte erro: \n {e}")
            self.writer.write_logs("Crawler", "process", e)
            return f"Erro ao coletar titulo"