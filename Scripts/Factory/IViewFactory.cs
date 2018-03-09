using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using ModelDrivenGUISystem.ViewModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Factory {

    public interface IViewFactory {

        BaseView CreateClassView(IFieldValue<object> model);

        BaseView CreateStringView(IFieldValue<object> parent, IFieldValue<string> model);
        BaseView CreateEnumView(IFieldValue<object> parent, IFieldValue<object> modelm);

        BaseView CreateBoolView(IFieldValue<object> parent, IFieldValue<bool> model);
        BaseView CreateIntView(IFieldValue<object> parent, IFieldValue<int> model);
        BaseView CreateFloatView(IFieldValue<object> parent, IFieldValue<float> model);

        BaseView CreateVector2View(IFieldValue<object> parent, IFieldValue<Vector2> model);
        BaseView CreateVector3View(IFieldValue<object> parent, IFieldValue<Vector3> model);
        BaseView CreateVector4View(IFieldValue<object> parent, IFieldValue<Vector4> model);
        BaseView CreateColorView(IFieldValue<object> parent, IFieldValue<Color> model);
    }
}
