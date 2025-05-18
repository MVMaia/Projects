import os

class TxtWriter:

    def __init__(self):
        self.logPath = './logs/logErrors.txt'
        os.makedirs(os.path.dirname(self.logPath), exist_ok=True)
        

    def write_logs(self, class1, method, msg):
        with open(self.logPath, 'a') as logTXT:
            logTXT.write(f'\n \n Erro no metodo {method} da classe {class1}:\n {msg}')