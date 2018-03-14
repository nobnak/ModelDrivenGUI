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
            window = new Rect(10f, 10f, 300f, 300f);
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
            public bool i;
            public int[] j;
        }
        [System.Serializable]
        public class InnerModel {
            public int[] g;
            public InnerInnerModel h;
        }
        [System.Serializable]
        public class Model {
            public enum SimpleEnum { One, Two }
            public int a;
            public SimpleEnum b;
            public string c;

            public int[] d = new int[0];
            public InnerModel[] e = new InnerModel[0];
            public InnerInnerModel[] f = new InnerInnerModel[0];
        }
    }
}
