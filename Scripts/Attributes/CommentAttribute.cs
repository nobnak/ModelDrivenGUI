using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Attributes {
    
    public class CommentAttribute : System.Attribute {
        public readonly string text;

        public CommentAttribute(string text) {
            this.text = text;
        }
    }
}
