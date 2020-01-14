using System;
using System.Collections.Generic;
using System.Text;

namespace Editor.Models
{
    public class EditorEntity
    {
        public string Name { get; set; }

        public List<EditorComponent> Components { get; set; } = new List<EditorComponent>();
    }
}
