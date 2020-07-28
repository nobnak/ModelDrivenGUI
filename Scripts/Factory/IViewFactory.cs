using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using System.Collections.Generic;
using UnityEngine;

using CustomData = System.Collections.Generic.Dictionary<string, object>;

namespace ModelDrivenGUISystem.Factory {

    public interface IViewFactory {

        BaseView CreateClassView(IValue<object> model, CustomData customData = null);

        BaseView CreateStringView(IValue<string> model, CustomData customData = null);
        BaseView CreateEnumView(IValue<object> modelm, CustomData customData = null);

        BaseView CreateBoolView(IValue<bool> model, CustomData customData = null);
        BaseView CreateIntView(IValue<int> model, CustomData customData = null);
        BaseView CreateFloatView(IValue<float> model, CustomData customData = null);

        BaseView CreateVector2View(IValue<Vector2> model, CustomData customData = null);
        BaseView CreateVector3View(IValue<Vector3> model, CustomData customData = null);
        BaseView CreateVector4View(IValue<Vector4> model, CustomData customData = null);
        BaseView CreateColorView(IValue<Color> model, CustomData customData = null);
		BaseView CreateVector2IntView(IValue<Vector2Int> model, CustomData customData = null);
		BaseView CreateVector3IntView(IValue<Vector3Int> model, CustomData customData = null);

		BaseView CreateArrayView<T>(IValue<T[]> model, CustomData customData = null) where T : new();
        BaseView CreateListView<T>(IValue<List<T>> model, CustomData customData = null) where T : new();

        BaseView CreateLabelView(CustomData customData = null);
    }
}
