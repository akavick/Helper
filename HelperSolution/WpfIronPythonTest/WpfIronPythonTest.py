import wpf

from System.Windows import Application, Window

class MyWindow(Window):
    def __init__(self):
        wpf.LoadComponent(self, 'WpfIronPythonTest.xaml')

    _side = 3
    _current = None
    for i in range(0, 2):
        cd = ColumnDefinition()
        rd = RowDefinition()
        _mainGrid.ColumnDefinitions.Add(cd)
        _mainGrid.RowDefinitions.Add(rd)

    for i in range(0, 2):
        for j in range(0, 2):               
            b = Button()
            b.FontSize = 50
            b.SetValue(Grid.ColumnProperty, j)
            b.SetValue(Grid.RowProperty, i)
            def onClick(s, e):
                if _current != None:
                    _current.Content = ""
                _current = b
                b.Content = "Hi!"
            b.Click += onClick
            _mainGrid.Children.Add(b)

if __name__ == '__main__':
    Application().Run(MyWindow())       