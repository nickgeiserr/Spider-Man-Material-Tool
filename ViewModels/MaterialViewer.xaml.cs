using Microsoft.Win32;
using Spider_Man_Material_Tool.TextureViewer;
using System;
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
using System.Windows.Shapes;

namespace Spider_Man_Material_Tool.ViewModels
{
    /// <summary>
    /// Interaction logic for MaterialViewer.xaml
    /// </summary>
    public partial class MaterialViewer : Window
    {
        public MaterialViewer()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                Globals.image = openFileDialog.FileName;
                Convertor conv = new Convertor();

                bool valid = conv.IsTextureFileValid(Globals.image);

                if (!valid) { return; }

                byte[] allBytes = File.ReadAllBytes(Globals.image);

                byte[] resultBytes = BytePatternUtilities.ReplaceBytes(allBytes, Globals.textureByteHeader, Globals.ddsByteHeader);

                File.WriteAllBytes("textures/current.dds", resultBytes);

            }
        }
    }
}
