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
            urlTitle = self.ext.collect_title(url)
            urlMetaDesc = self.ext.collect_metaDescription(url)
            urlH1 = self.ext.collect_h1(url)
            urlRobots = self.ext.collect_robots(url)
            urlHrefs = self.ext.collect_links(url, 'external')
            print(urlTitle, urlMetaDesc, urlH1, urlRobots,urlHrefs ,dateToday, sep='\n')
            #self.db.sql_queries("insert", "INSERT INTO titles(title, url, date) VALUES(%s,%s,%s)", [urlTitle, url,dateToday])
            
        except Exception as e:
            print(f"Erro no metodo process da classe Crawler, ocorreu o seguinte erro: \n {e}")
            self.writer.write_logs("Crawler", "process", e)
            return f"Erro ao coletar titulo"
        
    def process_urls(self, url, typeSearch):
        collectedUrls = []
        try:
            if typeSearch.lower() == 'internal':
                collectedUrls = self.ext.collect_links(url, 'internal')
            elif typeSearch.lower() == 'external':
                collectedUrls = self.ext.collect_links(url, 'external')
            else:
                print('Erro de tipo')
                return []
            return collectedUrls
        except Exception as e:
            self.writer.write_logs('crawler', 'process_urls', e)
            return []