using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using ModelDrivenGUISystem.ViewModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Factory {

    public interface IViewFactory {

        BaseView CreateClassView(IValue<object> model);

        BaseView CreateStringView(IValue<string> model);
        BaseView CreateEnumView(IValue<object> modelm);

        BaseView CreateBoolView(IValue<bool> model);
        BaseView CreateIntView(IValue<int> model);
        BaseView CreateFloatView(IValue<float> model);

        BaseView CreateVector2View(IValue<Vector2> model);
        BaseView CreateVector3View(IValue<Vector3> model);
        BaseView CreateVector4View(IValue<Vector4> model);
        BaseView CreateColorView(IValue<Color> model);

        BaseView CreateArrayView<T>(IValue<T[]> model) where T:new();
    }
}
