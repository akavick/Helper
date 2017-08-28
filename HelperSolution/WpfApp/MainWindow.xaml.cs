using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private int _side = 3;
        private Button _current;

        public MainWindow()
        {
            InitializeComponent();

            for (var i = 0; i < _side; i++)
            {
                var cd = new ColumnDefinition();
                var rd = new RowDefinition();
                _mainGrid.ColumnDefinitions.Add(cd);
                _mainGrid.RowDefinitions.Add(rd);
            }

            for (var i = 0; i < _side; i++)
            {
                for (var j = 0; j < _side; j++)
                {
                    var b = new Button {FontSize = 50};
                    b.SetValue(Grid.ColumnProperty, j);
                    b.SetValue(Grid.RowProperty, i);
                    b.Click += (s, e) =>
                    {
                        if (_current != null)
                            _current.Content = "";
                        _current = b;
                        b.Content = "Hi!";
                    };
                    _mainGrid.Children.Add(b);
                }
            }



        }
    }
}
