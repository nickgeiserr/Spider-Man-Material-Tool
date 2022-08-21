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
using Microsoft.Win32;
using Spider_Man_Material_Tool.Utils;
using System.IO;
using AdonisUI.Controls;

namespace Spider_Man_Material_Tool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TextBlock blk = new TextBlock();
            blk.Text = "Select a Material File to Begin!";
            blk.Foreground = new SolidColorBrush(Colors.White);
            blk.FontSize = 15;
            scoll.Items.Add(blk);
        }

        private void select_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                RenderFile(openFileDialog.FileName);
            }
                
        }

        void RenderFile(string text)
        {
            scoll.Items.Clear();

            FileParser parser = new FileParser();
            if(!parser.VerifyFile(text))
            {
                AdonisUI.Controls.MessageBox.Show("Failed to Verify", "Material Header not Found in File.", AdonisUI.Controls.MessageBoxButton.OKCancel);
            }

            MaterialObject m_obj;

            m_obj = parser.ParseMaterialFile(text);

            if (m_obj.m_graphs.Length == 0)
            {
                return;
            }

            select.IsEnabled = false;
            select.Opacity = 0;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            for (var i = 0; i < m_obj.m_graphs.Length; i++)
            {
                TextBlock blk = new TextBlock();
                blk.Text = m_obj.m_graphs[i];
                blk.Foreground = new SolidColorBrush(Colors.White);
                blk.FontSize = 15;
                scoll.Items.Add(blk);

                Separator sep = new Separator();

                scoll.Items.Add(sep);
            }

            select.IsEnabled = true;
            select.Opacity = 100;

        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
