import wpf

from System.Windows import Application, Window

class MyWindow(Window):
    def __init__(self):
        wpf.LoadComponent(self, 'WpfApplicationIronPythonTest.xaml')
    for i in range(0, 2):
        for j in range(0, 2):
    
    



if __name__ == '__main__':
    Application().Run(MyWindow())
