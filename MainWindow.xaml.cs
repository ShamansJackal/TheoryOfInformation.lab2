using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using TheoryOfInformation.lab1.Encryptions;
using TheoryOfInformation.lab1.Encryptions.Models;
using static TheoryOfInformation.lab1.Encryptions.TextCleaner;

namespace TheoryOfInformation.lab1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool readFromFile;
        private bool writeToFile;
        private bool encode;
        private IEnumerable<IEncryption> ecncryptions;
        private IEncryption encryption;

        public MainWindow()
        {
            ecncryptions = new List<IEncryption>() { };
            InitializeComponent();
            encryptionsBox.ItemsSource = ecncryptions;
            encryptionsBox.SelectedIndex = 1;
            inTextCheck_ib.IsChecked = true;
            inTextCheck_out.IsChecked = true;
        }

        private void outFileCheck_Checked(object sender, RoutedEventArgs e)
        {
            if (inFileCheck_out.IsChecked.Value)
            {
                fileUnit_out.Visibility = Visibility.Visible;
                textUnit_out.Visibility = Visibility.Hidden;
                writeToFile = true;
            }
            else
            {
                fileUnit_out.Visibility = Visibility.Hidden;
                textUnit_out.Visibility = Visibility.Visible;
                writeToFile = false;
            }
        }

        private void inFileCheck_Checked(object sender, RoutedEventArgs e)
        {
            if (inFileCheck_in.IsChecked.Value)
            {
                fileUnit_in.Visibility = Visibility.Visible;
                textUnit_in.Visibility = Visibility.Hidden;
                readFromFile = true;
            }
            else
            {
                fileUnit_in.Visibility = Visibility.Hidden;
                textUnit_in.Visibility = Visibility.Visible;
                readFromFile = false;
            }
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e) => encode = encCheck.IsChecked.Value;

        private void encryptionsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            IEncryption selected = cmb.SelectedItem as IEncryption;
            encryption = selected;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string text;
            Operation operation;

            string key = keyBox.Text;

            if (readFromFile)
            {
                string path = fileUnit_in.OutputFile.Text;
                text = File.ReadAllText(path);
            }
            else
            {
                text = textUnit_in.outputText.Text;
            }

            if (encode) operation = encryption.Encrypte;
            else operation = encryption.Decrypte;

            CleanText(ref key, encryption.KeyLang);

            string result = WorkWithText(key, text, operation, encryption.Lang);
            result = string.IsNullOrWhiteSpace(result) ? "Не валидные данные" : result;

            if (writeToFile)
            {
                string path = fileUnit_out.OutputFile.Text;
                File.WriteAllText(path, result);
            }
            else
            {
                textUnit_out.outputText.Text = result;
            }
        }
    }
}
