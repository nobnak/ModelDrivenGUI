using ModelDrivenGUISystem.Extensions.TypeExt;
using ModelDrivenGUISystem.Extensions.VectorExt;
using ModelDrivenGUISystem.View;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem {

    public class ViewModel<ModelType> {

        protected ModelType model;
        protected List<ViewModelPair> pairs = new List<ViewModelPair>();

        public virtual ModelType Model {
            get { return model; }
            set {
                model = value;
                Build(model);
            }
        }

        public virtual void Build(ModelType model) {
            pairs.Clear();
            foreach (var f in model.Fields()) {
                switch (f.Section()) {
                    case DataSectionEnum.Primitive_Int: {
                            var mr = new ReactiveProperty<int>(GetValue<int>(f, model));
                            mr.Subscribe(v => {
                                SetValue(f, model, v);
                            });
                            var vr = mr.Select(v => v.ToString()).ToReactiveProperty();
                            vr.Subscribe(v => {
                                int nextValue;
                                if (int.TryParse(v, out nextValue))
                                    mr.Value = nextValue;
                            });
                            var view = new IntView() { Title = f.Name };
                            var p = new OneToOne<int, string>() {
                                Input = mr,
                                Output = vr,
                                Draw = () => view.Draw(vr)
                            };
                            pairs.Add(p);
                            break;
                        }

                    case DataSectionEnum.ValueType_Vector:
                        switch (f.VectorType()) {
                            case VectorTypeEnum.Vector4: {
                                    var mr = new ReactiveProperty<Vector4>(GetValue<Vector4>(f, model));
                                    var vr = new ReactiveCollection<string>(mr.Value.Split().Select(v => v.ToString()));
                                    mr.Subscribe(vec => {
                                        SetValue(f, model, vec);
                                        for (var i = 0; i < 4; i++)
                                            vr[i] = vec[i].ToString();
                                    });
                                    vr.ObserveReplace().Subscribe(e => {
                                        float next;
                                        if (float.TryParse(e.NewValue, out next)) {
                                            var vec = mr.Value;
                                            vec[e.Index] = next;
                                        }
                                    });
                                    var view = new VectorView() { Title = f.Name };
                                    var p = new OneToList<Vector4, string>() {
                                        Input = mr,
                                        Outputs = vr,
                                        Draw = () => view.Draw(vr)
                                    };
                                pairs.Add(p);
                                break;
                                }
                        }
                        break;
                }
            }
        }
        public virtual void Draw() {
            foreach (var p in pairs) {
                p.Draw();
            }
        }

        protected virtual ModelFieldType GetValue<ModelFieldType>(FieldInfo field, ModelType parent) {
            return (ModelFieldType)field.GetValue(parent);
        }
        protected virtual void SetValue<ModelFieldType>(FieldInfo field, ModelType parent, ModelFieldType value) {
            field.SetValue(parent, value);
        }
    }
}
