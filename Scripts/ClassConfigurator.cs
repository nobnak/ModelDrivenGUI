using ModelDrivenGUISystem.Extensions.TypeExt;
using ModelDrivenGUISystem.Extensions.VectorExt;
using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using ModelDrivenGUISystem.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem {

    public static class ClassConfigurator {

        public static BaseView GenerateClassView(object parent) {
            var views = new List<BaseView>();

            foreach (var f in parent.Fields()) {
                switch (f.Section()) {
                    case DataSectionEnum.Primitive_Int: {
                            var model = new FieldValue<int>(parent, f);
                            var vm = new IntViewModel(model);
                            var view = new IntView() { Title = f.Name };
                            view.Input = vm.Output;
                            views.Add(view);
                            break;
                        }

                    case DataSectionEnum.ValueType_Vector:
                        switch (f.VectorType()) {
                            case VectorTypeEnum.Vector4: {
                                    var model = new FieldValue<Vector4>(parent, f);
                                    var vm = new Vector4ViewModel(model);
                                    var view = new VectorView() { Title = f.Name };
                                    view.Input = vm.Output;
                                    views.Add(view);
                                    break;
                                }
                        }
                        break;

                    case DataSectionEnum.Class_UserDefined: {
                            var model = new FieldValue<object>(parent, f);
                            var view = GenerateClassView(model.Value);
                            views.Add(view);
                            break;
                        }
                }
            }

            return new ClassView() {
                Title = parent.GetType().Name,
                Children = views
            };
        }
    }
}
