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
                var title = f.Name;
                var fieldType = f.FieldType;
                var modelFactory = new FieldModelFactory(parentModel, f);
                views.AddRange(
                    GenerateFieldView(modelFactory, viewFactory, fieldType, title));
            }

            var parentView = viewFactory.CreateClassView(parentModel);
            parentView.Title = parentModel.Value.GetType().Name;
            parentView.Children = views;
            return parentView;
        }

        public static IEnumerable<BaseView> GenerateFieldView(
                IModelFactory modelFactory, IViewFactory viewFactory, 
                System.Type fieldType, string title) {

            switch (fieldType.Section()) {
                case DataSectionEnum.Primitive_Bool: {
                        var model = modelFactory.CreateValue<bool>();
                        var view = viewFactory.CreateBoolView(model);
                        view.Title = title;
                        yield return view;
                        break;
                    }

                case DataSectionEnum.Primitive_Int: {
                        var model = modelFactory.CreateValue<int>();
                        var view = viewFactory.CreateIntView(model);
                        view.Title = title;
                        yield return view;
                        break;
                    }

                case DataSectionEnum.Primitive_Float: {
                        var model = modelFactory.CreateValue<float>();
                        var view = viewFactory.CreateFloatView(model);
                        view.Title = title;
                        yield return view;
                        break;
                    }

                case DataSectionEnum.Class_String: {
                        var model = modelFactory.CreateValue<string>();
                        var view = viewFactory.CreateStringView(model);
                        view.Title = title;
                        yield return view;
                        break;
                    }

                case DataSectionEnum.ValueType_Enum: {
                        var model = modelFactory.CreateValue<object>();
                        var view = viewFactory.CreateEnumView(model);
                        view.Title = title;
                        yield return view;
                        break;
                    }


                case DataSectionEnum.ValueType_Vector:
                    switch (fieldType.VectorType()) {
                        case VectorTypeEnum.Vector2: {
                                var model = modelFactory.CreateValue<Vector2>();
                                var view = viewFactory.CreateVector2View(model);
                                view.Title = title;
                                yield return view;
                                break;
                            }
                        case VectorTypeEnum.Vector3: {
                                var model = modelFactory.CreateValue<Vector3>();
                                var view = viewFactory.CreateVector3View(model);
                                view.Title = title;
                                yield return view;
                                break;
                            }
                        case VectorTypeEnum.Vector4: {
                                var model = modelFactory.CreateValue<Vector4>();
                                var view = viewFactory.CreateVector4View(model);
                                view.Title = title;
                                yield return view;
                                break;
                            }
                        case VectorTypeEnum.Color: {
                                var model = modelFactory.CreateValue<Color>();
                                var view = viewFactory.CreateColorView(model);
                                view.Title = title;
                                yield return view;
                                break;
                            }
                    }
                    break;

                case DataSectionEnum.Class_UserDefined: {
                        var model = modelFactory.CreateValue<object>();
                        if (model.Value != null) {
                            var view = GenerateClassView(model, viewFactory);
                            yield return view;
                        }
                        break;
                    }

                case DataSectionEnum.Class_Array: {
                        var model = modelFactory.CreateValue(fieldType);
                        var methodCreateView = viewFactory.GetType().GetMethod("CreateArrayView")
                            .MakeGenericMethod(fieldType.GetElementType());
                        var view = (BaseView)methodCreateView.Invoke(
                            viewFactory, new object[] { model });
                        view.Title = title;
                        yield return view;
                        break;
                    }
            }
        }
    }
}
