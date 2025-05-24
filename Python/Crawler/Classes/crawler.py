from Classes.extractor import Extractor
from Classes.db_manager import Database
from Classes.text_writter import TxtWriter
from datetime import date


class Crawler:

    def __init__(self):
        self.db = Database('localhost', 'root', '' , 'crawlerpy')
        self.ext = Extractor()
        self.writer = TxtWriter()
        self.visitedUrls = set()
        self.type = 'internal'

    def testProcess(self, urls):
        nextUrls = []

        if isinstance(urls, list):
            nextUrls.extend(urls)
        else:
            nextUrls.append(urls)

        try:
            while True:
                collectedUrls = []
                for url in nextUrls:
                    if url in self.visitedUrls:
                        continue

                    extractedData = self.extractData(url)
                    print(extractedData)
                    self.saveData(url, extractedData)
                    collectedUrls.extend(self.extractUrls(url, self.type))
                    self.visitedUrls.add(url)

                if not collectedUrls:
                    print("Não houve URLs de retorno")
                    break
                elif set(collectedUrls).issubset(self.visitedUrls):
                    print("Todos as URLs retornadas já foram visitadas")
                    break
                else:
                    nextUrls = []
                    nextUrls.extend(collectedUrls)
        except Exception as e:
            print(f"Erro no metodo process da classe Crawler, ocorreu o seguinte erro: \n {e}")
            self.writer.write_logs("Crawler", "process", e)
            return f"Erro ao coletar titulo"
        
    def extractData(self, url):
        try:
            urlTitle = self.ext.collect_title(url)
            urlMetaDesc = self.ext.collect_metaDescription(url)
            urlH1 = self.ext.collect_h1(url)
            urlRobots = self.ext.collect_robots(url)
            return [urlTitle, urlH1, urlMetaDesc, urlRobots]       
        except Exception as e:
            print(f"Erro no metodo process da classe Crawler, ocorreu o seguinte erro: \n {e}")
            self.writer.write_logs("Crawler", "process", e)
            return f"Erro ao coletar titulo"

        
    def extractUrls(self, url, typeSearch):
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
        
    def saveData(self, url, data):
        date1 = date.today()
        try:
            self.db.connect_to()
            verify = self.db.sql_queries('INSERTURL', 'INSERT INTO urls(url, date) VALUES(%s, %s)', [url, date1])
            self.db.commitChanges()

            if not data[2]: 
                data[2] = ["Não há description nessa página"]
            elif data[2][0] == '': 
                data[2][0] = "Não há description nessa página"

            if isinstance(verify, int):
                self.db.sql_queries('INSERT', 'INSERT INTO titles(title, id_url) VALUES(%s, %s)', [data[0], verify])
                self.db.sql_queries('INSERT', 'INSERT INTO h_one_tag(tag_text, id_url) VALUES(%s, %s)', [data[1], verify])
                self.db.sql_queries('INSERT', 'INSERT INTO meta_desc(description, id_url) VALUES(%s, %s)', [data[2][0], verify])
                self.db.sql_queries('INSERT', 'INSERT INTO robots(robots_text, id_url) VALUES(%s, %s)', [data[3], verify])
                self.db.commitChanges()
        except Exception as e:
            print(f"Erro no saveData class Crawler :{e}")
            self.writer.write_logs('Crawler', 'saveData', e)
        finally:
            self.db.close_connection()

