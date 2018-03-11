using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using ModelDrivenGUISystem.ViewModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Factory {

    public interface IViewFactory {

        BaseView CreateClassView(IValue<object> model);

        BaseView CreateStringView(IValue<object> parent, IValue<string> model);
        BaseView CreateEnumView(IValue<object> parent, IValue<object> modelm);

        BaseView CreateBoolView(IValue<object> parent, IValue<bool> model);
        BaseView CreateIntView(IValue<object> parent, IValue<int> model);
        BaseView CreateFloatView(IValue<object> parent, IValue<float> model);

        BaseView CreateVector2View(IValue<object> parent, IValue<Vector2> model);
        BaseView CreateVector3View(IValue<object> parent, IValue<Vector3> model);
        BaseView CreateVector4View(IValue<object> parent, IValue<Vector4> model);
        BaseView CreateColorView(IValue<object> parent, IValue<Color> model);

        BaseView CreateArrayView<T>(IValue<object> parent, IValue<T[]> model);
    }
}
