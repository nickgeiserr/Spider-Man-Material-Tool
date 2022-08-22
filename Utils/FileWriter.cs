using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Spider_Man_Material_Tool.Utils
{
    internal class FileWriter
    {
        public bool UpdateMaterialFile(string file_name, string replacing, string new_s, bool shouldBackup)
        {
            if(!File.Exists(file_name)) {
                return false;
            }

            string file_text = File.ReadAllText(file_name);

            if (shouldBackup)
            {
                File.WriteAllText(file_name + ".backup", file_text);
            }

            File.WriteAllText("log.txt", file_text);

            string newtext = file_text.Replace(replacing, new_s);

            File.WriteAllText("log2.txt", newtext);

            File.WriteAllText(file_name, newtext);

            return true;
        }
    }
}
