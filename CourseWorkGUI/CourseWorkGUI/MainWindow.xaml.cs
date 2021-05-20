using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace CourseWorkGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        int _textBoxesCount=0;
        private double[,] matrix;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Scroll_OnScroll(object sender, ScrollEventArgs e)
        {
            text_scroll.Text = ((int)scroll.Value).ToString();

            int newCount = (int)scroll.Value;
            _textBoxesCount=newCount;

            contGrid.Children.Clear();
            contGrid.RowDefinitions.Clear();
            contGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < newCount; i++)
            {
                contGrid.RowDefinitions.Add(new RowDefinition());
                contGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < newCount; i++)
            {
                for (int j = 0; j < newCount; j++)
                {
                    TextBox box = new TextBox();
                    box.FontSize = 250 / newCount;
                    box.VerticalAlignment = VerticalAlignment.Center;
                    box.TextAlignment=TextAlignment.Center;
                    box.SetValue(Grid.RowProperty, i);
                    box.SetValue(Grid.ColumnProperty, j);
                    contGrid.Children.Add(box);
                }
            }

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            matrix = new double[_textBoxesCount, _textBoxesCount];
            for (int i = 0; i < _textBoxesCount; i++)
            {
                for (int j = 0; j < _textBoxesCount; j++)
                {
                    matrix[i, j] = double.Parse(((TextBox)contGrid.Children[i*_textBoxesCount+j]).Text);
                }
            }
        }
    }
}