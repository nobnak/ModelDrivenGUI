using System.Collections;
using System.Collections.Generic;
using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using ModelDrivenGUISystem.ViewModel;
using UnityEngine;

namespace ModelDrivenGUISystem.Factory {

    public class ViewFactory : IViewFactory {
        public BaseView CreateClassView(IFieldValue<object> model) {
            return new ClassView();
        }

        public BaseView CreateFloatView(IFieldValue<float> model, NumberViewModel<float> vm) {
            return new TextFieldView() { Input = vm.Output };
        }
        public BaseView CreateIntView(IFieldValue<int> model, NumberViewModel<int> vm) {
            return new TextFieldView() { Input = vm.Output };
        }

        public BaseView CreateVector2View(IFieldValue<Vector2> model, VectorViewModel<Vector2, float> vm) {
            return new VectorView() { Input = vm.Output };
        }
        public BaseView CreateVector3View(IFieldValue<Vector3> model, VectorViewModel<Vector3, float> vm) {
            return new VectorView() { Input = vm.Output };
        }
        public BaseView CreateVector4View(IFieldValue<Vector4> model, VectorViewModel<Vector4, float> vm) {
            return new VectorView() { Input = vm.Output };
        }
        public BaseView CreateColorView(IFieldValue<Color> model, VectorViewModel<Color, float> vm) {
            return new VectorView() { Input = vm.Output };
        }
    }
}
