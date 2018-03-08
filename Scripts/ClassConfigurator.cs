using ModelDrivenGUISystem.Accessor;
using ModelDrivenGUISystem.Extensions.TypeExt;
using ModelDrivenGUISystem.Extensions.VectorExt;
using ModelDrivenGUISystem.Factory;
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

        public static BaseView GenerateClassView(
            IFieldValue<object> parentModel, IViewFactory viewFactory) {

            var views = new List<BaseView>();

            foreach (var f in parentModel.Value.Fields()) {
                switch (f.Section()) {
                    case DataSectionEnum.Primitive_Int: {
                            var model = new FieldValue<int>(parentModel.Value, f);
                            var accessor = new IntAccessor();
                            var vm = new NumberViewModel<int>(model, accessor);
                            var view = viewFactory.CreateIntView(model, vm);
                            view.Title = f.Name;
                            views.Add(view);
                            break;
                        }

                    case DataSectionEnum.Primitive_Float: {
                            var model = new FieldValue<float>(parentModel.Value, f);
                            var accessor = new FloatAccessor();
                            var vm = new NumberViewModel<float>(model, accessor);
                            var view = viewFactory.CreateFloatView(model, vm);
                            view.Title = f.Name;
                            views.Add(view);
                            break;
                        }

                    case DataSectionEnum.ValueType_Vector:
                        switch (f.VectorType()) {
                            case VectorTypeEnum.Vector2: {
                                    var model = new FieldValue<Vector2>(parentModel.Value, f);
                                    var vm = new VectorViewModel<Vector2, float>(
                                        model, new Vector2Accessor());
                                    var view = viewFactory.CreateVector2View(model, vm);
                                    view.Title = f.Name;
                                    views.Add(view);
                                    break;
                                }
                            case VectorTypeEnum.Vector3: {
                                    var model = new FieldValue<Vector3>(parentModel.Value, f);
                                    var vm = new VectorViewModel<Vector3, float>(
                                        model, new Vector3Accessor());
                                    var view = viewFactory.CreateVector3View(model, vm);
                                    view.Title = f.Name;
                                    views.Add(view);
                                    break;
                                }
                            case VectorTypeEnum.Vector4: {
                                    var model = new FieldValue<Vector4>(parentModel.Value, f);
                                    var vm = new VectorViewModel<Vector4, float>(
                                        model, new Vector4Accessor());
                                    var view = viewFactory.CreateVector4View(model, vm);
                                    view.Title = f.Name;
                                    views.Add(view);
                                    break;
                                }
                        }
                        break;

                    case DataSectionEnum.Class_UserDefined: {
                            var model = new FieldValue<object>(parentModel.Value, f);
                            var view = GenerateClassView(model, viewFactory);
                            views.Add(view);
                            break;
                        }
                }
            }

            var parentView = viewFactory.CreateClassView(parentModel);
            parentView.Title = parentModel.GetType().Name;
            parentView.Children = views;
            return parentView;
        }
    }
}
