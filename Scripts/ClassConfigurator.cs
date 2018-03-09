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
                    case DataSectionEnum.Primitive_Bool: {
                            var model = new FieldValue<bool>(parentModel.Value, f);
                            var view = viewFactory.CreateBoolView(parentModel, model);
                            view.Title = f.Name;
                            views.Add(view);
                            break;
                        }

                    case DataSectionEnum.Primitive_Int: {
                            var model = new FieldValue<int>(parentModel.Value, f);
                            var view = viewFactory.CreateIntView(parentModel, model);
                            view.Title = f.Name;
                            views.Add(view);
                            break;
                        }

                    case DataSectionEnum.Primitive_Float: {
                            var model = new FieldValue<float>(parentModel.Value, f);
                            var view = viewFactory.CreateFloatView(parentModel, model);
                            view.Title = f.Name;
                            views.Add(view);
                            break;
                        }

                    case DataSectionEnum.Class_String: {
                            var model = new FieldValue<string>(parentModel.Value, f);
                            var view = viewFactory.CreateStringView(parentModel, model);
                            view.Title = f.Name;
                            views.Add(view);
                            break;
                        }

                    case DataSectionEnum.ValueType_Enum: {
                            var model = new FieldValue<object>(parentModel.Value, f);
                            var view = viewFactory.CreateEnumView(parentModel, model);
                            view.Title = f.Name;
                            views.Add(view);
                            break;
                        }


                    case DataSectionEnum.ValueType_Vector:
                        switch (f.VectorType()) {
                            case VectorTypeEnum.Vector2: {
                                    var model = new FieldValue<Vector2>(parentModel.Value, f);
                                    var view = viewFactory.CreateVector2View(parentModel, model);
                                    view.Title = f.Name;
                                    views.Add(view);
                                    break;
                                }
                            case VectorTypeEnum.Vector3: {
                                    var model = new FieldValue<Vector3>(parentModel.Value, f);
                                    var view = viewFactory.CreateVector3View(parentModel, model);
                                    view.Title = f.Name;
                                    views.Add(view);
                                    break;
                                }
                            case VectorTypeEnum.Vector4: {
                                    var model = new FieldValue<Vector4>(parentModel.Value, f);
                                    var view = viewFactory.CreateVector4View(parentModel, model);
                                    view.Title = f.Name;
                                    views.Add(view);
                                    break;
                                }
                            case VectorTypeEnum.Color: {
                                    var model = new FieldValue<Color>(parentModel.Value, f);
                                    var view = viewFactory.CreateColorView(parentModel, model);
                                    view.Title = f.Name;
                                    views.Add(view);
                                    break;
                                }
                        }
                        break;

                    case DataSectionEnum.Class_UserDefined: {
                            var model = new FieldValue<object>(parentModel.Value, f);
                            if (model.Value != null) {
                                var view = GenerateClassView(model, viewFactory);
                                views.Add(view);
                            }
                            break;
                        }

                    case DataSectionEnum.Class_IList: {

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
