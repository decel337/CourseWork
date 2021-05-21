using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;

namespace CourseWorkGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        int _textBoxesCount=0;
        int _previousValue = 0;
        private double[,] matrix;
        
        public MainWindow()
        {
            InitializeComponent();
            InitializeOpen();
            InitializeSave();
            Methods.SelectedIndex = 0;
            //Methods.SelectedIndex = (int)Methods.FindName("Method edging minor");

        }

        private void InitializeOpen()
        {
            CommandBinding open = new CommandBinding(ApplicationCommands.Open);
            open.Executed += OpenOnExecuted;
            CommandBindings.Add(open);
        }
        
        private void InitializeSave()
        {
            CommandBinding save = new CommandBinding(ApplicationCommands.Save);
            save.Executed += SaveOnExecuted;
            CommandBindings.Add(save);
        }

        private void OpenOnExecuted(object sender, ExecutedRoutedEventArgs b)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == false) return;

            string filename = dialog.FileName;
            double[,] matrix = FileReader.Read(filename);
        }
        
        private void SaveOnExecuted(object sender, ExecutedRoutedEventArgs b)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == false) return;

            string filename = dialog.FileName;
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

            double[,] inversionMatrix = new double[matrix.GetLength(0), matrix.GetLength(0)];
            
            if (Methods.SelectedIndex == 0)
            {
                inversionMatrix = InversionEdging.Inversion(matrix);
            }
            else if (Methods.SelectedIndex == 1)
            {
                inversionMatrix = InversionLU.Inversion(matrix);
            }
            else if (Methods.SelectedIndex == 2)
            {
                inversionMatrix = InversionLUP.Inversion(matrix);
            }
            
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    TextBox box = new TextBox();
                    box.Text = inversionMatrix[i, j].ToString();
                    box.FontSize = 250 / matrix.GetLength(0);
                    box.VerticalAlignment = VerticalAlignment.Center;
                    box.TextAlignment=TextAlignment.Center;
                    box.SetValue(Grid.RowProperty, i);
                    box.SetValue(Grid.ColumnProperty, j);
                    resultGrid.Children.Add(box);
                }
            }
        }

        private void Methods_OnSelected(object sender, RoutedEventArgs e)
        {
            
        }
    }
}