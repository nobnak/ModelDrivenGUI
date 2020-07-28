using ModelDrivenGUISystem.Attributes;
using ModelDrivenGUISystem.Extensions.FieldInfoExt;
using ModelDrivenGUISystem.Factory;
using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using nobnak.Gist.Extensions.CustomAttrExt;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using CustomData = System.Collections.Generic.Dictionary<string, object>;

namespace ModelDrivenGUISystem {

    public static class ClassConfigurator {
        public const string CD_MODEL = "Model";
        public const string CD_ATTRIBUTES = "Attributes";

        public static BaseView GenerateClassView(IValue<object> parentModel, IViewFactory viewFactory) {
            var classType = parentModel.Value.GetType();
            var customData = classType.GenerateCustomData();
            var views = new List<BaseView>();

            views.AddRange(classType.GenerateMemberComment(viewFactory, customData));

            foreach (var f in parentModel.Value.Fields()) {
                var title = f.GetTitle();
                var fieldType = f.FieldType;
                var modelFactory = new FieldModelFactory(parentModel, f);
                var fieldCustomData = f.GenerateCustomData();
                views.AddRange(
                    GenerateFieldView(modelFactory, viewFactory, fieldType, title));
                views.AddRange(f.GenerateMemberComment(viewFactory, fieldCustomData));
            }

            var parentTitle = classType.GetTitle();
            var parentView = viewFactory.CreateClassView(parentModel, customData);
            parentView.Title = parentTitle;
            parentView.Children = views;
            return parentView;
        }

        public static IEnumerable<BaseView> GenerateFieldView(
                IModelFactory modelFactory, IViewFactory viewFactory,
                System.Type fieldType, string title) {

            var customData = fieldType.GenerateCustomData();

            switch (fieldType.Section()) {
                case DataSectionEnum.Primitive_Bool: {
                        var model = modelFactory.CreateValue<bool>();
                        var view = viewFactory.CreateBoolView(model, customData);
                        view.Title = title;
                        yield return view;
                        break;
                    }

                case DataSectionEnum.Primitive_Int: {
                        var model = modelFactory.CreateValue<int>();
                        var view = viewFactory.CreateIntView(model, customData);
                        view.Title = title;
                        yield return view;
                        break;
                    }

                case DataSectionEnum.Primitive_Float: {
                        var model = modelFactory.CreateValue<float>();
                        var view = viewFactory.CreateFloatView(model, customData);
                        view.Title = title;
                        yield return view;
                        break;
                    }

                case DataSectionEnum.Class_String: {
                        var model = modelFactory.CreateValue<string>();
                        var view = viewFactory.CreateStringView(model, customData);
                        view.Title = title;
                        yield return view;
                        break;
                    }

                case DataSectionEnum.ValueType_Enum: {
                        var model = modelFactory.CreateValue<object>();
                        var view = viewFactory.CreateEnumView(model, customData);
                        view.Title = title;
                        yield return view;
                        break;
                    }


                case DataSectionEnum.ValueType_Vector:
					switch (fieldType.VectorType()) {
						case VectorTypeEnum.Vector2: {
								var model = modelFactory.CreateValue<Vector2>();
								var view = viewFactory.CreateVector2View(model, customData);
								view.Title = title;
								yield return view;
								break;
							}
						case VectorTypeEnum.Vector3: {
								var model = modelFactory.CreateValue<Vector3>();
								var view = viewFactory.CreateVector3View(model, customData);
								view.Title = title;
								yield return view;
								break;
							}
						case VectorTypeEnum.Vector4: {
								var model = modelFactory.CreateValue<Vector4>();
								var view = viewFactory.CreateVector4View(model, customData);
								view.Title = title;
								yield return view;
								break;
							}
						case VectorTypeEnum.Color: {
								var model = modelFactory.CreateValue<Color>();
								var view = viewFactory.CreateColorView(model, customData);
								view.Title = title;
								yield return view;
								break;
							}
					}
					break;
				case DataSectionEnum.ValueType_VectorInt:
					switch (fieldType.VectorType()) {
						case VectorTypeEnum.Vector2Int: {
								var model = modelFactory.CreateValue<Vector2Int>();
								var view = viewFactory.CreateVector2IntView(model, customData);
								view.Title = title;
								yield return view;
								break;
							}
						case VectorTypeEnum.Vector3Int: {
								var model = modelFactory.CreateValue<Vector3Int>();
								var view = viewFactory.CreateVector3IntView(model, customData);
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
							view.Title = title;
                            yield return view;
                        }
                        break;
                    }

                case DataSectionEnum.Class_Array: {
                        var model = modelFactory.CreateValue(fieldType);
                        var methodCreateView = viewFactory.GetType().GetMethod("CreateArrayView")
                            .MakeGenericMethod(fieldType.GetElementType());
                        var view = (BaseView)methodCreateView.Invoke(
                            viewFactory, new object[] { model, customData });
                        view.Title = title;
                        yield return view;
                        break;
                    }
                case DataSectionEnum.Class_ListGeneric: {
                        var model = modelFactory.CreateValue(fieldType);
                        var elementType = fieldType.GetGenericArguments()[0];
                        var methodCreateView = viewFactory.GetType().GetMethod("CreateListView");
                        methodCreateView = methodCreateView.MakeGenericMethod(elementType);
                        var view = (BaseView)methodCreateView.Invoke(
                            viewFactory, new object[] { model, customData });
                        view.Title = title;
                        yield return view;
                        break;
                    }
            }
        }
        public static CustomData GenerateCustomData(this MemberInfo info) {
            var attributes = info.GetCustomAttributes();
            var result = new CustomData();
            result[CD_ATTRIBUTES] = attributes;
            return result;
        }
        public static string GetTitle(this MemberInfo info) {
            var titleAttr = info.GetCustomAttribute<TitleAttribute>();
            var title = (titleAttr == null) ? info.Name : titleAttr.title;
            return title;
        }
        public static IEnumerable<BaseView> GenerateMemberComment(
            this MemberInfo info, IViewFactory viewFactory, CustomData customData) {

            var attr = default(CommentAttribute);
            if (info.TryGetAttribute<CommentAttribute>(out attr)) {
                customData[LabelView.CD_USAGE] = LabelView.UsageEnum.Comment;
                var comment = viewFactory.CreateLabelView(customData);
                comment.Title = attr.text;
                yield return comment;
            }
        }
    }
}
