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
        
        
    def collect_links(self, url, typeLink):
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
                
                if typeLink.lower() == 'external':
                    externalLinks = list(set(externalLinks))
                    return externalLinks
                elif typeLink.lower() == 'internal':
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
    
    def collect_metaDescription(self, url):
        try:
            response = requests.get(url)
            response.raise_for_status()
            if response.status_code == 200:
                soup = BeautifulSoup(response.text, 'lxml')
                metaTags = soup.find_all('meta', attrs={'name':'description'})

                if metaTags:
                    tags = []
                    for meta in metaTags:
                        if meta.get('content'):
                            tags.append(meta['content'].strip())    
                    return tags   
                else:
                    return ['Essa página não possui <meta name="description">']           
        except requests.exceptions.HTTPError as e:
            print(f"Erro na classe collect_metaDescription: {e}")
            self.writer.write_logs('extractor', 'collect_metaData', e)
            return ['Erro ao coletar metaDescription']  
        

    def collect_h1(self, url):
        try:
            response = requests.get(url)
            response.raise_for_status()

            if response.status_code == 200:
                soup = BeautifulSoup(response.text, 'lxml')
                h1 = soup.find('h1')
                if h1:
                    return h1.text
                else:
                    return "não há <h1> nessa página!"
        except requests.exceptions.HTTPError as e:
            print(f"Erro no método collect_h1 da classe extractor: {e}")
            self.writer.write_logs('extractor', 'collect_h1', e)
            return ["erro ao coletar o h1"]
        
    def collect_robots(self, url):
        try:
            response = requests.get(url)
            response.raise_for_status()

            if response.status_code == 200:
                soup = BeautifulSoup(response.text, 'lxml')
                robots = soup.find('mega', attrs={'name':'robots'})
                if robots:
                    return robots.text
                else:
                    return "Não há robots nessa página!"
        except Exception as e:
            print(f"erro na classe extractor, metodo robots: {e}")
            self.writer.write_logs('extractor', 'collect_robots', e)
            return ["Erro ao coletar robots"]