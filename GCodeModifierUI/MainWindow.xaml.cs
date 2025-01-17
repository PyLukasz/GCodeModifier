﻿using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using GCodeModifier;

namespace GCodeModifierUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string RemoveExtensionFile(string fileName)
        {
            int indexOfDot = fileName.IndexOf('.');
            string f = fileName.Remove(indexOfDot);
            return f;
        }
        public static string ModifyFileName(string fileName)
        {
            string modifiedFileName = RemoveExtensionFile(fileName) + "_modify.min";
            return modifiedFileName;
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            var dialog = new OpenFileDialog();
            GCodeModify gCodeModify = new GCodeModify();
            dialog.FileName = "Document"; // Default file name
            //dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.min)|*.min"; // Filter files by extension
            //dialog.InitialDirectory = @"C:\";

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                //string fileName = dialog.FileName;
                string modifiedFileName = ModifyFileName(dialog.FileName);
                string[] readFile = File.ReadAllLines(dialog.FileName);
                List<string> getListOfTools = gCodeModify.GetListOfTools(dialog.FileName);
                string[] newFile = gCodeModify.PutToolChangesToFile(getListOfTools, readFile);
                File.WriteAllLines(modifiedFileName, newFile);
                textBox.Text = modifiedFileName;
            }
        }
    }
}
