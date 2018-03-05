using ModelDrivenGUISystem.Scope;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem {

    public class FoldoutVisual {
        protected string text;
        protected bool visible;

        public FoldoutVisual(string text, bool initVisible = false) {
            this.text = text;
            this.visible = initVisible;
        }

        public System.IDisposable DrawScope() {
            var style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleLeft;
            visible = GUILayout.Toggle(visible,
                string.Format("{0} {1}", (visible ? '▼' : '▶'), text), 
                style,
                GUILayout.ExpandWidth(true));
            return new FoldoutScope(visible);
        }
    }
}
