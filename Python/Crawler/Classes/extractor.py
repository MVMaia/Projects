import requests
from bs4 import BeautifulSoup
from Classes.text_writter import TxtWriter


class Extractor:

    def __init__(self):
        self.writer = TxtWriter()

    def collect_title(self, url):
        try:
            response = requests.get(url)
            response.raise_for_status()

            soup = BeautifulSoup(response.content, 'lxml')
            title = soup.title.string if soup.title else "essa página não tem título"
            return title
        
        except requests.RequestException as e:
            print(f"Erro ao acessar a url {url} no metodo Extractor.collect_title, ocorreu o seguinte erro: \n {e}")
            error = f"Erro ao acessar a url {url}: \n {e}"
            self.writer.write_logs("Extractor", "collect_title", error)
            return f"Erro ao coletar titulo"

