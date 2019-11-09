using ModelDrivenGUISystem.Attributes;
using ModelDrivenGUISystem.Extensions.FieldInfoExt;
using ModelDrivenGUISystem.Factory;
using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using System.Collections.Generic;
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
            public bool bool0;
            public int[] intArray0;
        }
        [System.Serializable]
        public class InnerModel {
            public int[] int0;
            public InnerInnerModel innerInnerModel0;
        }
        [System.Serializable]
        [Title("Custom model name")]
        [Comment("Comment on model")]
        public class Model {
            public enum SimpleEnum { One, Two }
            [Title("Custum int name")]
            public int int0;
            public SimpleEnum simpleEnum0;
            public string string0;

            public int[] intArray0 = new int[0];
            public InnerModel[] innterModelArray0 = new InnerModel[0];
            public InnerInnerModel[] innerInnerModelArray0 = new InnerInnerModel[0];

            public List<int> intList0 = new List<int>();
            public List<InnerInnerModel> innerInnerModel0 = new List<InnerInnerModel>();
            public StepAndCurveData stepData0 = new StepAndCurveData();
        }


        [System.Serializable]
        public class RouteInfo {
            public List<Vector3> points = new List<Vector3>();
            public List<int> indices = new List<int>();
            public string connections;
        }
        [System.Serializable]
        public class RouteMap {
            public List<RouteInfo> routes = new List<RouteInfo>();
        }
        [System.Serializable]
        public class StepAndCurveData {
            public RouteMap routes = new RouteMap();
        }
    }
}
