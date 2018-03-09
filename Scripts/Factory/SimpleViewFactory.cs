using System.Collections;
using System.Collections.Generic;
using ModelDrivenGUISystem.Accessor;
using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using ModelDrivenGUISystem.ViewModel;
using UnityEngine;

namespace ModelDrivenGUISystem.Factory {

    public class SimpleViewFactory : IViewFactory {
        public BaseView CreateClassView(IFieldValue<object> model) {
            return new ClassView();
        }

        public BaseView CreateStringView(IFieldValue<object> parent, IFieldValue<string> model) {
            var vm = new BypassViewModel<string>(model);
            return new TextFieldView() { Input = vm.Output };
        }
        public BaseView CreateEnumView(IFieldValue<object> parent, IFieldValue<object> model) {
            var accessor = new EnumAccessor(model.Field.FieldType);
            var vm = new NumberViewModel<object>(model, accessor);
            return new EnumView() { Input = vm.Output, EnumType = model.Value.GetType() };
        }

        public BaseView CreateBoolView(IFieldValue<object> parent, IFieldValue<bool> model) {
            var vm = new BypassViewModel<bool>(model);
            return new BoolView() { Input = vm.Output };
        }
        public BaseView CreateIntView(IFieldValue<object> parent, IFieldValue<int> model) {
            var accessor = new IntAccessor();
            var vm = new NumberViewModel<int>(model, accessor);
            return new TextFieldView() { Input = vm.Output };
        }
        public BaseView CreateFloatView(IFieldValue<object> parent, IFieldValue<float> model) {
            var accessor = new FloatAccessor();
            var vm = new NumberViewModel<float>(model, accessor);
            return new TextFieldView() { Input = vm.Output };
        }

        public BaseView CreateVector2View(IFieldValue<object> parent, IFieldValue<Vector2> model) {
            var vm = new VectorViewModel<Vector2, float>(model, new Vector2Accessor());
            return new VectorView() { Input = vm.Output };
        }
        public BaseView CreateVector3View(IFieldValue<object> parent, IFieldValue<Vector3> model) {
            var vm = new VectorViewModel<Vector3, float>(model, new Vector3Accessor());
            return new VectorView() { Input = vm.Output };
        }
        public BaseView CreateVector4View(IFieldValue<object> parent, IFieldValue<Vector4> model) {
            var vm = new VectorViewModel<Vector4, float>(model, new Vector4Accessor());
            return new VectorView() { Input = vm.Output };
        }
        public BaseView CreateColorView(IFieldValue<object> parent, IFieldValue<Color> model) {
            var vm = new VectorViewModel<Color, float>(model, new ColorAccessor());
            return new VectorView() { Input = vm.Output };
        }
    }
}
