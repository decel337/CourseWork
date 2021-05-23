using System;
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
        private Random r = new Random();
        int _textBoxesCount=1;
        int _previousValue = 0;
        private double[,] matrix;
        private double[,] inversionMatrix;
        
        public MainWindow()
        {
            InitializeComponent();
            InitializeOpen();
            InitializeSave();
            
            Methods.SelectedIndex = 0;
            scroll.Value = 1;
            Scroll_OnScroll(scroll, new ScrollEventArgs(ScrollEventType.First, scroll.Value));

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
            matrix = FileReader.Read(filename);
            if (matrix.GetLength(0) <= 15)
            {
                ClearGrid(contGrid);
                ClearGrid(resultGrid);
                
                InitGridValue(contGrid, matrix);
                scroll.Value = matrix.GetLength(0);
                _textBoxesCount = (int)scroll.Value;
                _previousValue = (int) scroll.Value;
                text_scroll.Text = ((int) scroll.Value).ToString();
            }

            infofile.Text = "File appload";
            infofile.Foreground = Brushes.Green;
        }
        
        private void SaveOnExecuted(object sender, ExecutedRoutedEventArgs b)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            if (dialog.ShowDialog() == false) return;

            string filename = dialog.FileName;
            FileWriter.Write(filename, inversionMatrix);
        }

        private void Scroll_OnScroll(object sender, ScrollEventArgs e)
        {
            if (_previousValue != (int)scroll.Value)
            {
                text_scroll.Text = ((int) scroll.Value).ToString();

                int newCount = (int) scroll.Value;
                _textBoxesCount = newCount;

                ClearGrid(contGrid);
                ClearGrid(resultGrid);
                
                InitGrid(contGrid,newCount);

                _previousValue = newCount;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            
            ClearGrid(resultGrid);

            if (infofile.Text == "File not appload")
            {
                matrix = new double[_textBoxesCount, _textBoxesCount];
                for (int i = 0; i < _textBoxesCount; i++)
                {
                    for (int j = 0; j < _textBoxesCount; j++)
                    {
                        matrix[i, j] = double.Parse(((TextBox) contGrid.Children[i * _textBoxesCount + j]).Text);
                    }
                }
            }
            
            if (Determinate.IsZero(matrix))
            {
                inforesult.Text = "Determinate is zero";
                return;
            }

            inversionMatrix = new double[matrix.GetLength(0), matrix.GetLength(0)];
            
            if (Methods.SelectedIndex == 0)
            {
                inversionMatrix = InversionEdging.Inversion(matrix);
            }
            else if (Methods.SelectedIndex == 1)
            {
                inversionMatrix = InversionLU.Inversion(matrix);

                if (inversionMatrix == null)
                {
                    inforesult.Text = "Matrix not decomposition method LU. Try another method";
                    inforesult.Foreground = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 233)));
                }
            }
            else if (Methods.SelectedIndex == 2)
            {
                inversionMatrix = InversionLU.Inversion(matrix);
                
                if (inversionMatrix == null)
                {
                    inversionMatrix = InversionLUP.Inversion(matrix);
                }
            }

            if (inversionMatrix!= null && inversionMatrix.GetLength(0) <= 15)
            {
                InitGridValue(resultGrid, inversionMatrix);
            }
            else if(inversionMatrix!= null && inversionMatrix.GetLength(0) > 15)
            {
                InitializeSave();
            }

            infofile.Text = "File not appload";
            infofile.Foreground=Brushes.Red;
            if (inversionMatrix != null)
            {
                inforesult.Text = "Successfully";
                inforesult.Foreground = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 233)));
            }
        }

        private void InitGridValue(Grid grid, double[,] matrix)
        {
            int length = matrix.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    TextBox box = new TextBox();
                    box.Text = Math.Round(matrix[i, j], 3).ToString();
                    box.FontSize = 50 / length;
                    box.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                    box.Width = contGrid.Width / length;
                    box.Height = contGrid.Height / length;
                    box.VerticalContentAlignment = VerticalAlignment.Center;
                    box.TextAlignment = TextAlignment.Center;
                    box.SetValue(Grid.RowProperty, i);
                    box.SetValue(Grid.ColumnProperty, j);
                    grid.Children.Add(box);
                }
            }
        }

        private void InitGrid(Grid grid, int length)
            {
                for (int i = 0; i < length; i++)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                }

                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        TextBox box = new TextBox();
                        box.FontSize = 50 / length;
                        box.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                        box.Width = contGrid.Width / length;
                        box.Height = contGrid.Height / length;
                        box.VerticalContentAlignment = VerticalAlignment.Center;
                        box.TextAlignment = TextAlignment.Center;
                        box.SetValue(Grid.RowProperty, i);
                        box.SetValue(Grid.ColumnProperty, j);
                        grid.Children.Add(box);
                    }
                }
        }

        private void ClearGrid(Grid grid)
        {
            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();
            inforesult.Text = "";
        }

        private void GridRandom()
        {
            matrix = new double[(int) scroll.Value, (int) scroll.Value];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = r.NextDouble() * 200 - 100;
                }
            }
            
            InitGridValue(contGrid, matrix);
        }
    }
}