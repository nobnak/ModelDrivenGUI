using System.Collections;
using System.Collections.Generic;
using ModelDrivenGUISystem.Accessor;
using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using ModelDrivenGUISystem.ViewModel;
using UnityEngine;

using CustomData = System.Collections.Generic.Dictionary<string, object>;

namespace ModelDrivenGUISystem.Factory {

    public class SimpleViewFactory : IViewFactory {
        public virtual BaseView CreateClassView(IValue<object> model, CustomData customData = null) {
            return new ClassView();
        }

        public virtual BaseView CreateStringView(IValue<string> model, CustomData customData = null) {
            var vm = new BypassViewModel<string>(model);
            return new TextFieldView() {
                Input = vm.Output,
                CustomData = customData
            };
        }
        public virtual BaseView CreateEnumView(IValue<object> model, CustomData customData = null) {
            var accessor = new EnumAccessor(model.Value.GetType());
            var vm = new NumberViewModel<object>(model, accessor);
            return new EnumView() {
                Input = vm.Output,
                EnumType = model.Value.GetType(),
                CustomData = customData
            };
        }

        public virtual BaseView CreateBoolView(IValue<bool> model, CustomData customData = null) {
            var vm = new BypassViewModel<bool>(model);
            return new BoolView() {
                Input = vm.Output,
                CustomData = customData
            };
        }
        public virtual BaseView CreateIntView(IValue<int> model, CustomData customData = null) {
            var accessor = new IntAccessor();
            var vm = new NumberViewModel<int>(model, accessor);
            return new TextFieldView() {
                Input = vm.Output,
                CustomData = customData
            };
        }
        public virtual BaseView CreateFloatView(IValue<float> model, CustomData customData = null) {
            var accessor = new FloatAccessor();
            var vm = new NumberViewModel<float>(model, accessor);
            return new TextFieldView() {
                Input = vm.Output,
                CustomData = customData
            };
        }

        public virtual BaseView CreateVector2View(IValue<Vector2> model, CustomData customData = null) {
            var vm = new VectorViewModel<Vector2, float>(model, new Vector2Accessor());
            return new VectorView() {
                Input = vm.Output,
                CustomData = customData
            };
        }
        public virtual BaseView CreateVector3View(IValue<Vector3> model, CustomData customData = null) {
            var vm = new VectorViewModel<Vector3, float>(model, new Vector3Accessor());
            return new VectorView() {
                Input = vm.Output,
                CustomData = customData
            };
        }
        public virtual BaseView CreateVector4View(IValue<Vector4> model, CustomData customData = null) {
            var vm = new VectorViewModel<Vector4, float>(model, new Vector4Accessor());
            return new VectorView() {
                Input = vm.Output,
                CustomData = customData
            };
        }
        public virtual BaseView CreateColorView(IValue<Color> model, CustomData customData = null) {
            var vm = new VectorViewModel<Color, float>(model, new ColorAccessor());
            return new VectorView() {
                Input = vm.Output,
                CustomData = customData
            };
        }

        public virtual BaseView CreateArrayView<T>(IValue<T[]> model, CustomData customData = null) where T : new() {
            var vm = new ArrayViewModel<T>(model, this);
            return new ArrayView() {
                Views = vm.OutputViews,
                Count = vm.OutputCount,
                CommandAdd = vm.CommandAdd,
                CommandRemove = vm.CommandRemove,
                CustomData = customData
            };
        }
        public virtual BaseView CreateListView<T>(IValue<List<T>> model, CustomData customData = null) where T : new() {
            var vm = new ListViewModel<T>(model, this);
            return new ListView() {
                Views = vm.OutputViews,
                Count = vm.OutputCount,
                CommandAdd = vm.CommandAdd,
                CommandRemove = vm.CommandRemove,
                CustomData = customData
            };
        }
    }
}
