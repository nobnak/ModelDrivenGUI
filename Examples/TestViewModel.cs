using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Examples {

    public class TestViewModel : MonoBehaviour {
        public Model model;

        protected Rect window;
        protected ViewModel<Model> viewmodel;

        private void OnEnable() {
            window = new Rect(10f, 10f, 200f, 300f);
            viewmodel = new ViewModel<Model>() {
                Model = model
            };
        }
        private void OnGUI() {
            window = GUILayout.Window(GetInstanceID(), window, Window, name);
        }
        private void Window(int id) {
            using (new GUILayout.VerticalScope()) {
                viewmodel.Draw();
            }
            GUI.DragWindow();
        }

        [System.Serializable]
        public class Model {
            public int intfield01;
            public Vector4 vec4field01;
        }
    }
}
