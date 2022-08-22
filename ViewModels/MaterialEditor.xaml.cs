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
using Spider_Man_Material_Tool.Utils;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Spider_Man_Material_Tool.ViewModels
{
    /// <summary>
    /// Interaction logic for MaterialEditor.xaml
    /// </summary>
    public partial class MaterialEditor : Window
    {
        MaterialEditorObject reaf;

        public MaterialEditor(MaterialEditorObject meo)
        {
            InitializeComponent();
            reaf = meo;
            txtn.Text = "Begin Typing Here...";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string new_text = txtn.Text;
            FileWriter w = new FileWriter();
            bool yn = w.UpdateMaterialFile(Globals.current_file, reaf.old_text, new_text, (bool)bakcup.IsChecked);
            if(!yn)
            {
                AdonisUI.Controls.MessageBox.Show("Current File NO Longer Exists", "File Doesn't Exist", AdonisUI.Controls.MessageBoxButton.OKCancel);
            } else
            {
                AdonisUI.Controls.MessageBox.Show("Material Property Modified Succesfully", "Success!", AdonisUI.Controls.MessageBoxButton.OKCancel);
            }

            FileParser fp = new FileParser();

            bool should_throw_error = fp.IsFileCorrupted(Globals.current_file);

            if(should_throw_error) { ThrowCorruptedFileError(); }

        }

        private void ThrowCorruptedFileError()
        {
            AdonisUI.Controls.MessageBox.Show("There was an Error in the Program that Resulted in the material File Corrupting.", "Corrupted File Error", AdonisUI.Controls.MessageBoxButton.OK);
        }
    }
}
