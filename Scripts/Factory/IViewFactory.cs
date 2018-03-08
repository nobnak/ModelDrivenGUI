using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using ModelDrivenGUISystem.ViewModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Factory {

    public interface IViewFactory {

        BaseView CreateClassView(IFieldValue<object> model);

        BaseView CreateIntView(IFieldValue<int> model, NumberViewModel<int> vm);
        BaseView CreateFloatView(IFieldValue<float> model, NumberViewModel<float> vm);
        BaseView CreateVector2View(IFieldValue<Vector2> model, VectorViewModel<Vector2, float> vm);
        BaseView CreateVector3View(IFieldValue<Vector3> model, VectorViewModel<Vector3, float> vm);
        BaseView CreateVector4View(IFieldValue<Vector4> model, VectorViewModel<Vector4, float> vm);
        BaseView CreateColorView(IFieldValue<Color> model, VectorViewModel<Color, float> vm);
    }
}
