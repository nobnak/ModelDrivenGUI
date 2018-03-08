using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using ModelDrivenGUISystem.ViewModel;
using System.Linq.Expressions;
using UnityEngine;

namespace ModelDrivenGUISystem.Examples {

    public class TestViewModel : MonoBehaviour {
        public Model model;

        protected Rect window;
        protected BaseView view;

        private void OnEnable() {
            window = new Rect(10f, 10f, 200f, 300f);
            var f = new FieldValue<object>(this, this.GetType().GetField(MemberName(() => model)));
            view = ClassConfigurator.GenerateClassView(f);
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

        public static string MemberName<T>(Expression<System.Func<T>> memberAccess) {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }

        [System.Serializable]
        public class InnerModel {
            public int intfield02;
        }
        [System.Serializable]
        public class Model {
            public int intfield01;
            public Vector4 vec4field01;
            public InnerModel innerModel;
        }
    }
}
