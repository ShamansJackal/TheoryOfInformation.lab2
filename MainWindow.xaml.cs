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
        private bool readFromFile;
        private bool encode;
        public bool visualisation { get; set; } = true;
        private IEncryption encryption;

        public MainWindow()
        {
            InitializeComponent();
            encryption = new LFRS(new ushort[] { 1,4 }, 4);
            inTextCheck_ib.IsChecked = true;
        }

        private void textBox1_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D0 || e.Key == Key.D1)
            {
                e.Handled = true;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string text;
            Operation operation;

            string key = keyBox.Text;
            var s = encryption.BuildKey(15, 16);

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
    }
}
