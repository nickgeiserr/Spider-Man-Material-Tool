using Microsoft.Win32;
using Spider_Man_Material_Tool.Utils;
using Spider_Man_Material_Tool.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Spider_Man_Material_Tool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            Globals.Startup();

            string patch_notes = "- nothing rn \n";

            InitializeComponent();
            TextBlock blk = new TextBlock();
            blk.Text = "You haven't selected a material so how about I show you some patch notes!";
            blk.Foreground = new SolidColorBrush(Colors.White);
            blk.FontSize = 15;
            scoll.Items.Add(blk);
            TextBlock text = new TextBlock();
            text.Text = patch_notes;
            text.Foreground = new SolidColorBrush(Colors.White);
            text.FontSize = 15;
            scoll.Items.Add(text);
            TextBlock refe = new TextBlock();
            refe.Text = "If you encounter any errors or wanna suggest something, hmu on discord xplore#7500";
            refe.Foreground = new SolidColorBrush(Colors.White);
            refe.FontSize = 15;
            scoll.Items.Add(refe);
        }

        private void select_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                Globals.current_file = openFileDialog.FileName;

                HandleFile();

                if (Globals.m_obj.m_graphs.Length == 0 || Globals.m_obj.m_textures.Length == 0)
                {
                    return;
                }

                filterGraphs.IsEnabled = true;
                filterTexture.IsEnabled = true;

                RenderFile();
            }
                
        }

        void HandleFile()
        {
            FileParser parser = new FileParser();
            if (!parser.VerifyFile(Globals.current_file))
            {
                AdonisUI.Controls.MessageBox.Show("Failed to Verify", "Material Header not Found in File.", AdonisUI.Controls.MessageBoxButton.OKCancel);
            }

            parser.ParseMaterialFile(Globals.current_file);
        }

        void RenderFile()
        {
            scoll.Items.Clear();

            if (Globals.m_obj != null)
            {
                Globals.renderTexture = (bool)filterTexture.IsChecked;
                if (Globals.renderTexture)
                {
                    RenderTextures();
                }
                Globals.renderGraphs = (bool)filterGraphs.IsChecked;
                if (Globals.renderGraphs)
                {
                    RenderMaterialGraphs();
                }
            }

        }

        private void RenderMaterialGraphs()
        {
            if (Globals.m_obj.m_graphs.Length == 0) return;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            for (var i = 0; i < Globals.m_obj.m_graphs.Length; i++)
            {

                Button blk = new Button();
                blk.Content = Globals.m_obj.m_graphs[i];
                blk.Name = "listoption" + i;
                blk.Foreground = new SolidColorBrush(Colors.White);
                blk.FontSize = 15;
                blk.FontFamily = new FontFamily("Inter Medium");
                blk.Click += new RoutedEventHandler(btn_Click);

                scoll.Items.Add(blk);

                Separator sep = new Separator();

                scoll.Items.Add(sep);
            }
        }

        private void RenderTextures()
        {

            for (var i = 0; i < Globals.m_obj.m_textures.Length; i++)
            {

                Button blk = new Button();
                blk.Content = Globals.m_obj.m_textures[i];
                blk.Name = "listoption" + i;
                blk.Foreground = new SolidColorBrush(Colors.White);
                blk.FontSize = 15;
                blk.FontFamily = new FontFamily("Inter Medium");
                blk.Click += new RoutedEventHandler(btn_Click);

                scoll.Items.Add(blk);

                Separator sep = new Separator();

                scoll.Items.Add(sep);
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            bool enabled = true;

            if(enabled)
            {
                if(!Globals.hasShownTestingME)
                {
                    AdonisUI.Controls.MessageBox.Show("The Editor is currently In-Development. Your File May Corrupt After You Edit It. Make Sure to Enable the Backup. You Will Only See This Message Once Per Lifetime.", "Warning", AdonisUI.Controls.MessageBoxButton.OK, AdonisUI.Controls.MessageBoxImage.Warning);
                    Globals.hasShownTestingME = true;
                }


                Button b = (Button)sender;

                string text = (string)b.Content;

                MaterialEditorObject meo = new MaterialEditorObject
                {
                    current_file = Globals.current_file,

                    old_text = text
                };

                MaterialEditor me = new MaterialEditor(meo);

                me.Show();

            } else
            {
                AdonisUI.Controls.MessageBox.Show("The Material Editor is Currently Disabled. It has been disabled for this version.", "Module Disabled", AdonisUI.Controls.MessageBoxButton.OK, AdonisUI.Controls.MessageBoxImage.Stop);
            }

        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            bool enabled = false;

            if(enabled)
            {
                MaterialViewer mv = new MaterialViewer();

                mv.Show();
            } 
            else
            {
                AdonisUI.Controls.MessageBox.Show("The Texture Viewer is Currently In a Incomplete State. It has been disabled for this version.", "Module Disabled", AdonisUI.Controls.MessageBoxButton.OK, AdonisUI.Controls.MessageBoxImage.Stop);
            }
        }
        private void Navigate_Click(object sender, RoutedEventArgs e)
        {
            MaterialViewer mv = new MaterialViewer();

            mv.Show();
        }

        private void select_Copy_Click(object sender, RoutedEventArgs e)
        {
            if(Globals.current_file == null)
            {
                AdonisUI.Controls.MessageBox.Show("No File Available To Reload", "Error", AdonisUI.Controls.MessageBoxButton.OKCancel, AdonisUI.Controls.MessageBoxImage.Error);
                return;
            }
            HandleFile();
            RenderFile();
        }

        private void filterGraphs_Checked(object sender, RoutedEventArgs e)
        {
            if(Globals.m_obj != null)
            {
                Globals.renderGraphs = !Globals.renderGraphs;
                if (Globals.renderGraphs)
                {
                    RenderMaterialGraphs();
                }
            }
        }

        private void filterTexture_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void filterClicked_Click(object sender, RoutedEventArgs e)
        {
            RenderFile();
        }
    }
}
