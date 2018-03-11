using ModelDrivenGUISystem.Extensions.FieldInfoExt;
using ModelDrivenGUISystem.Factory;
using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using UnityEngine;

namespace ModelDrivenGUISystem.Examples {

    public class TestViewModel : MonoBehaviour {
        public Model data;

        protected Rect window;
        protected BaseView view;

        private void OnEnable() {
            window = new Rect(10f, 10f, 200f, 300f);
            var viewFactory = new SimpleViewFactory();
            view = ClassConfigurator.GenerateClassView(new BaseValue<object>(data), viewFactory);
        }
        private void OnDisable() {
            if (view != null) {
                view.Dispose();
                view = null;
            }
        }
        private void OnGUI() {
            window = GUILayout.Window(GetInstanceID(), window, Window, name);
        }
        private void Window(int id) {
            view.Draw();
            GUI.DragWindow();
        }

        [System.Serializable]
        public class InnerInnerModel {
            public bool bool01;
            public string string01;
            public int int01;
            public float float01;
        }
        [System.Serializable]
        public class InnerModel {
            public bool bool01;
            public string string01;
            public int int01;
            public float float01;

            public InnerInnerModel innerInner;
        }
        [System.Serializable]
        public class Model {
            public enum SimpleEnum { One, Two }

            public bool bool01;
            public SimpleEnum enum01;
            public string string01;
            public int int01;
            public float float01;
            public Vector2 vec2_01;
            public Vector3 vec3_01;
            public Vector4 vec4_01;
            public Vector2Int vec2int01;

            public int[] intArray01;

            public InnerModel innerClass01;
        }
    }
}
