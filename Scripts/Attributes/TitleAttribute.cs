using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Attributes {
    
    public class TitleAttribute : System.Attribute {
        public readonly string title;

        public TitleAttribute(string title) {
            this.title = title;
        }
    }
}
