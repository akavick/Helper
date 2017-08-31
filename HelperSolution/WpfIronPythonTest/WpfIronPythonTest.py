import wpf

from System.Windows import Application, Window
from System.Windows.Controls import ColumnDefinition
from System.Windows.Controls import RowDefinition
from System.Windows.Controls import Button
from System.Windows.Controls import Grid

class MyWindow(Window):
	
	side = 3
	current = None
	
	def FillField(): 
		for i in range(0, side):
			cd = ColumnDefinition()
			rd = RowDefinition()
			_mainGrid.ColumnDefinitions.Add(cd)
			_mainGrid.RowDefinitions.Add(rd)
	
		for i in range(0, side):
			for j in range(0, side):               
				b = Button()
				b.FontSize = 50
				b.SetValue(Grid.ColumnProperty, j)
				b.SetValue(Grid.RowProperty, i)
				def onClick(s, e):
					if current != None:
						current.Content = ""
					current = b
					b.Content = "Hi!"
				b.Click += onClick
				_mainGrid.Children.Add(b)

	def __init__(self):
		wpf.LoadComponent(self, 'WpfIronPythonTest.xaml')
		FillField()

	


if __name__ == '__main__':
    Application().Run(MyWindow())