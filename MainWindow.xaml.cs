 using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TheoryOfInformation.lab1.Encryptions;
using TheoryOfInformation.lab1.Encryptions.Models;
using static TheoryOfInformation.lab1.Encryptions.TextWorker;

namespace TheoryOfInformation.lab1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _readFromFile = true;
        private ushort _polynomePower = 4;

        private IEncryption _encryption;

        private bool _encode = false;
        private bool Encode { get => _encode; set { if (fileUnit_in != null) fileUnit_in.encrypt = value; _encode = value; } }
        public bool _visualisation { get; set; }


        public MainWindow()
        {
            _encryption = new LFRS(new ushort[] { 1, 4 }, _polynomePower);

            InitializeComponent();
            keyBox.Text = "Ключ";
            keyBox.MaxLength = _polynomePower;
            inTextCheck_ib.IsChecked = true;
            encCheck.IsChecked = true;
        }

        private void inFileCheck_Checked(object sender, RoutedEventArgs e)
        {
            if (inFileCheck_in.IsChecked.Value)
            {
                fileUnit_in.Visibility = Visibility.Visible;
                textUnit_in.Visibility = Visibility.Hidden;
                _readFromFile = true;
            }
            else
            {
                fileUnit_in.Visibility = Visibility.Hidden;
                textUnit_in.Visibility = Visibility.Visible;
                _readFromFile = false;
            }
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e) => Encode = encCheck.IsChecked.Value;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string text;

            string key = keyBox.Text;
            var s = _encryption.BuildKey(15, 16);

            if (_readFromFile)
            {
                string path = fileUnit_in.OutputFile.Text;
                text = File.ReadAllText(path);
            }
            else
            {
                text = textUnit_in.outputText.Text;
            }

            if (Encode) ;

        }

        private void keyBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsGood);
        }

        private void OnPasting(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(IsGood))
                e.CancelCommand();
        }

        bool IsGood(char c)
        {
            if (c == '0' || c == '1')
                return true;
            else
                return false;
        }

        private void keyBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            KeyLengthLabel.Content = $"Длина ключа {((TextBox)sender).Text.Length}/{_polynomePower}";
        }
    }
}
