using System;
using System.Collections.Generic;
using System.Text;

namespace Falcon.Editor.Models
{
    public class EditorComponent
    {
        public string Name { get; set; }

        public List<EditorProperty> Properties { get; set; }= new List<EditorProperty>();
    }
}
