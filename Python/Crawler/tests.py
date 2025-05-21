from Classes.extractor import Extractor

Ext = Extractor()
url = input("Digite uma url\n")
print(Ext.collect_robots(url))