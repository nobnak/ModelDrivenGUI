using ModelDrivenGUISystem.Extensions.FieldInfoExt;
using ModelDrivenGUISystem.Factory;
using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem {

    public static class ClassConfigurator {

        public static BaseView GenerateClassView(IValue<object> parentModel, IViewFactory viewFactory) {

            var views = new List<BaseView>();

            foreach (var f in parentModel.Value.Fields()) {
                switch (f.Section()) {
                    case DataSectionEnum.Primitive_Bool: {
                            var model = new FieldValue<bool>(parentModel, f);
                            var view = viewFactory.CreateBoolView(parentModel, model);
                            view.Title = f.Name;
                            views.Add(view);
                            break;
                        }

                    case DataSectionEnum.Primitive_Int: {
                            var model = new FieldValue<int>(parentModel, f);
                            var view = viewFactory.CreateIntView(parentModel, model);
                            view.Title = f.Name;
                            views.Add(view);
                            break;
                        }

                    case DataSectionEnum.Primitive_Float: {
                            var model = new FieldValue<float>(parentModel, f);
                            var view = viewFactory.CreateFloatView(parentModel, model);
                            view.Title = f.Name;
                            views.Add(view);
                            break;
                        }

                    case DataSectionEnum.Class_String: {
                            var model = new FieldValue<string>(parentModel, f);
                            var view = viewFactory.CreateStringView(parentModel, model);
                            view.Title = f.Name;
                            views.Add(view);
                            break;
                        }

                    case DataSectionEnum.ValueType_Enum: {
                            var model = new FieldValue<object>(parentModel, f);
                            var view = viewFactory.CreateEnumView(parentModel, model);
                            view.Title = f.Name;
                            views.Add(view);
                            break;
                        }


                    case DataSectionEnum.ValueType_Vector:
                        switch (f.VectorType()) {
                            case VectorTypeEnum.Vector2: {
                                    var model = new FieldValue<Vector2>(parentModel, f);
                                    var view = viewFactory.CreateVector2View(parentModel, model);
                                    view.Title = f.Name;
                                    views.Add(view);
                                    break;
                                }
                            case VectorTypeEnum.Vector3: {
                                    var model = new FieldValue<Vector3>(parentModel, f);
                                    var view = viewFactory.CreateVector3View(parentModel, model);
                                    view.Title = f.Name;
                                    views.Add(view);
                                    break;
                                }
                            case VectorTypeEnum.Vector4: {
                                    var model = new FieldValue<Vector4>(parentModel, f);
                                    var view = viewFactory.CreateVector4View(parentModel, model);
                                    view.Title = f.Name;
                                    views.Add(view);
                                    break;
                                }
                            case VectorTypeEnum.Color: {
                                    var model = new FieldValue<Color>(parentModel, f);
                                    var view = viewFactory.CreateColorView(parentModel, model);
                                    view.Title = f.Name;
                                    views.Add(view);
                                    break;
                                }
                        }
                        break;

                    case DataSectionEnum.Class_UserDefined: {
                            var model = new FieldValue<object>(parentModel, f);
                            if (model.Value != null) {
                                var view = GenerateClassView(model, viewFactory);
                                views.Add(view);
                            }
                            break;
                        }

                    case DataSectionEnum.Class_Array: {
                            var modelType = typeof(FieldValue<>).MakeGenericType(f.FieldType);
                            var model = System.Activator.CreateInstance(
                                modelType, new object[] { parentModel, f });
                            var methodCreateView = viewFactory.GetType().GetMethod("CreateArrayView")
                                .MakeGenericMethod(f.FieldType.GetElementType());
                            var view = (BaseView)methodCreateView.Invoke(
                                viewFactory, new object[] { parentModel, model });
                            view.Title = f.Name;
                            views.Add(view);
                            break;
                        }
                }
            }

            var parentView = viewFactory.CreateClassView(parentModel);
            parentView.Title = parentModel.Value.GetType().Name;
            parentView.Children = views;
            return parentView;
        }
    }
}
