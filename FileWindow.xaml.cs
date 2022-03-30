using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using TheoryOfInformation.lab1.Encryptions.Models;

namespace TheoryOfInformation.lab1
{
    /// <summary>
    /// Логика взаимодействия для FileWindow.xaml
    /// </summary>
    public partial class FileWindow : UserControl
    {
        public FileWindow()
        {
            InitializeComponent();
        }

        private void InputFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    ((Button)sender).Tag = filename;
                }
            }
        }
    }
}
