using System.Collections;
using System.Collections.Generic;
using ModelDrivenGUISystem.Accessor;
using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using ModelDrivenGUISystem.ViewModel;
using UnityEngine;

namespace ModelDrivenGUISystem.Factory {

    public class SimpleViewFactory : IViewFactory {
        public BaseView CreateClassView(IValue<object> model) {
            return new ClassView();
        }

        public BaseView CreateStringView(IValue<object> parent, IValue<string> model) {
            var vm = new BypassViewModel<string>(model);
            return new TextFieldView() { Input = vm.Output };
        }
        public BaseView CreateEnumView(IValue<object> parent, IValue<object> model) {
            var accessor = new EnumAccessor(model.Value.GetType());
            var vm = new NumberViewModel<object>(model, accessor);
            return new EnumView() { Input = vm.Output, EnumType = model.Value.GetType() };
        }

        public BaseView CreateBoolView(IValue<object> parent, IValue<bool> model) {
            var vm = new BypassViewModel<bool>(model);
            return new BoolView() { Input = vm.Output };
        }
        public BaseView CreateIntView(IValue<object> parent, IValue<int> model) {
            var accessor = new IntAccessor();
            var vm = new NumberViewModel<int>(model, accessor);
            return new TextFieldView() { Input = vm.Output };
        }
        public BaseView CreateFloatView(IValue<object> parent, IValue<float> model) {
            var accessor = new FloatAccessor();
            var vm = new NumberViewModel<float>(model, accessor);
            return new TextFieldView() { Input = vm.Output };
        }

        public BaseView CreateVector2View(IValue<object> parent, IValue<Vector2> model) {
            var vm = new VectorViewModel<Vector2, float>(model, new Vector2Accessor());
            return new VectorView() { Input = vm.Output };
        }
        public BaseView CreateVector3View(IValue<object> parent, IValue<Vector3> model) {
            var vm = new VectorViewModel<Vector3, float>(model, new Vector3Accessor());
            return new VectorView() { Input = vm.Output };
        }
        public BaseView CreateVector4View(IValue<object> parent, IValue<Vector4> model) {
            var vm = new VectorViewModel<Vector4, float>(model, new Vector4Accessor());
            return new VectorView() { Input = vm.Output };
        }
        public BaseView CreateColorView(IValue<object> parent, IValue<Color> model) {
            var vm = new VectorViewModel<Color, float>(model, new ColorAccessor());
            return new VectorView() { Input = vm.Output };
        }

        public BaseView CreateArrayView<T>(IValue<object> parent, IValue<T[]> model) {
            var vm = new ArrayViewModel<T>(model, this);
            return new ArrayView() { Input = vm.Output };
        }
    }
}
