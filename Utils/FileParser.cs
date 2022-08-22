using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Spider_Man_Material_Tool.Utils;

namespace Spider_Man_Material_Tool.Utils
{
    internal class FileParser
    {
        // return true if valid, false if not
        public bool VerifyFile(string file_path)
        {
            StreamReader reader = new StreamReader(file_path);

            string text = reader.ReadToEnd();

            reader.Close();

            if(text.Contains("Material Built File")) { return true; } 

            return false;
        }

        public bool IsFileCorrupted(string file_path)
        {
            StreamReader reader = new StreamReader(file_path);

            string text = reader.ReadToEnd();

            reader.Close();

            if (text.Contains("Update Material File")) { return true; }

            return false;
        }

        public MaterialObject ParseMaterialFile(string file_path)
        {
            MaterialObject m_obj = new MaterialObject();

            StreamReader reader = new StreamReader(file_path);

            string text = reader.ReadToEnd();

            reader.Close();

            Regex r = new Regex(@"[^a-zA-Z0-9.\/\\ _]");
            string parsed = r.Replace(text, " ");

            string[] arr = parsed.Split();

            List<string> graphs = new List<string>();

            for (var i = 0; i < arr.Length; i++)
            {
                if(arr[i].EndsWith(".texture"))
                {
                    graphs.Add(arr[i]);
                } 
                
                else if (arr[i].EndsWith(".materialgraph"))
                {
                    graphs.Add(arr[i]);
                }
            }

            m_obj.m_graphs = graphs.ToArray();

            return m_obj;
        }
    }
}
