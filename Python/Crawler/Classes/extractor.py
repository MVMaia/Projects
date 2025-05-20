from bs4 import BeautifulSoup
from Classes.text_writter import TxtWriter
from urllib.parse import urlparse, urljoin
import requests


class Extractor:

    def __init__(self):
        self.writer = TxtWriter()


    def collect_title(self, url):
        try:
            response = requests.get(url)
            response.raise_for_status()

            soup = BeautifulSoup(response.text, 'lxml')
            title = soup.title.string if soup.title else "essa página não tem título"
            return title.strip()
        
        except requests.RequestException as e:
            print(f"Erro ao acessar a url {url} no metodo Extractor.collect_title, ocorreu o seguinte erro: \n {e}")
            error = f"Erro ao acessar a url {url}: \n {e}"
            self.writer.write_logs("Extractor", "collect_title", error)
            return f"Erro ao coletar titulo"
        
        
    def collect_links(self, url, type):
        try:
            externalLinks = []
            internalLinks = []
            actualDomain = urlparse(url).netloc
            response = requests.get(url)
            response.raise_for_status()

            if response.status_code == 200:
                soup = BeautifulSoup(response.text, 'lxml')
                linksTags = soup.find_all('a', href=True)

                for link in linksTags:
                    href = link['href']
                    fullUrl = urljoin(url, href)
                    linkDomain = urlparse(fullUrl).netloc

                    if(fullUrl.startswith('http') and linkDomain != actualDomain):
                        externalLinks.append(fullUrl)
                    elif(fullUrl.startswith('http') and linkDomain == actualDomain):
                        internalLinks.append(fullUrl)
                
                if type.lower() == 'external':
                    externalLinks = list(set(externalLinks))
                    return externalLinks
                elif type.lower() == 'internal':
                    internalLinks = list(set(internalLinks))
                    return internalLinks
                else:
                    print("Tipo inválido")
                    return []
            else:
                print(f"Erro de requisição: {response.status_code}")
        except requests.HTTPError as e:
            msg = f"Erro HTTP ao acessar {url}: {e}"
        except requests.Timeout as e:
            msg = f"Timeout ao acessar {url}: {e}"
        except requests.RequestException as e:
            msg = f"Erro de requisição ao acessar {url}: {e}"
        except Exception as e:
            msg = f"Erro inesperado ao acessar {url}: {e}"

        self.writer.write_logs("Extractor", "collect_links", msg)
        return []

