using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider_Man_Material_Tool.Utils
{
    class MaterialObject
    {
        public string[]? m_graphs { get; set; }
        public string[]? m_materials { get; set; }
    }

    public class MaterialEditorObject
    {
        public string? current_file { get; set; }
        public string? old_text { get; set; }
    }

}
