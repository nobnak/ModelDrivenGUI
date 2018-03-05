using ModelDrivenGUISystem.Scope;
using ModelDrivenGUISystem.Visualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Examples {

    [ExecuteInEditMode]
    public class Example_IntField : MonoBehaviour {
        public Data data;

        protected Rect window;
        protected FoldoutVisual foldout;
        protected ClassVisualizer visualizer;

        private void OnEnable() {
            window = new Rect(10f, 10f, 200f, 300f);
            foldout = new FoldoutVisual("Foldout");
        }
        private void OnGUI() {
            if (!this.isActiveAndEnabled)
                return;

            window = GUILayout.Window(GetInstanceID(), window, Window, name);
        }

        protected void Window(int id) {
            using (foldout.DrawScope())
            using (new IndentScope(10)) {
            }
            GUI.DragWindow();
        }

        [System.Serializable]
        public class Data {
            public int intField01;
            public int intField02;
            public float floatField01;
            public string textField01;
        }
    }
}