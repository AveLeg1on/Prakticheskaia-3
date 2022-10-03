using Lib_11;
using Libmas;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace Z3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[,] mas = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Do(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(Txt_Row.Text, out int rows) && int.TryParse(Txt_Column.Text, out int column))
            {
                mas = new int[rows, column];
                grider.ItemsSource = VisualArray.ToDataTable(ClassForProgram.ZMass(mas, 10)).DefaultView;
                ClassP.MaxArraoundMin(mas, out int res);
                Answer.Text = $"{res}";
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = ".txt";
            open.Filter = "Все файлы (*.*)|*.*| Текстовые файлы |*.txt";
            open.FilterIndex = 2;
            open.Title = "Открытие таблицы";
            if (open.ShowDialog() == true)
            {
                mas = ClassForProgram.Openmas(open.FileName, mas);
                grider.ItemsSource = VisualArray.ToDataTable(mas).DefaultView;
                ClassP.MaxArraoundMin(mas, out int res);
                Answer.Text = $"{res}";
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "Все файлы |*.*| Текстовые файлы |*.txt";
            save.FilterIndex = 2;
            save.Title = "Сохранение таблицы";
            if (save.ShowDialog() == true)
            {
                ClassForProgram.SMass(save.FileName, mas);
            }
        }

        private void FAQ(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Разработчик: Денисов Олег Андреевич\n Задание: Дана матрица размера M × N. Найти максимальный среди минимальных элементов\r\n", "FAQ", MessageBoxButton.OKCancel, MessageBoxImage.None);
        }
        private void Do1(object sender, DataGridCellEditEndingEventArgs e)
        {
            var el = e.EditingElement as TextBox; // получаем значения из измененной ячейки как элемент TextBox'a
            if (mas != null)
            {
                for (int i = 0; i < mas.GetLength(0); i++)
                {
                    for (int j = 0; j < mas.GetLength(1); j++)
                    {
                        int row = e.Row.GetIndex(); // получаем индекс строки, где произошло изменение
                        int column = e.Column.DisplayIndex; // получаем индекс столбца, где произошло изменение
                        int.TryParse(el.Text, out int value);
                        mas[row, column] = value;
                    }
                }
                ClassP.MaxArraoundMin(mas, out int res);
                Answer.Text = $"{res}";
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            mas = null;
            grider.ItemsSource = null;
            Txt_Column.Clear();
            Txt_Row.Clear();
            Txt_Row.Focus();
        }
    }
}