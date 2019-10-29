using ModelDrivenGUISystem.Factory;
using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using UniRx;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using ModelDrivenGUISystem.Extensions.ConstructorExt;

namespace ModelDrivenGUISystem.ViewModel {

    public class ListViewModel<T> : BaseViewModel where T:new() {

        public ReactiveProperty<List<T>> Input { get; set; }
        public IReadOnlyReactiveProperty<string> OutputCount { get; set; }
        public IReadOnlyReactiveProperty<BaseView[]> OutputViews { get; set; }

        public ReactiveCommand CommandAdd { get; set; }
        public ReactiveCommand CommandRemove { get; set; }

        public ListViewModel(IValue<List<T>> model, IViewFactory viewFactory) {
            Input = new ReactiveProperty<List<T>>(model.Value);
            Input.Subscribe(v => model.Value = v);
            
            OutputCount = Input.Select(t => string.Format("{0}", t.Count)).ToReactiveProperty();
            OutputViews = Input.Select(ts => {
                DisposeViews();
                var views = new List<BaseView>();
                try {
                    for (var i = 0; i < ts.Count; i++) {
                        var modelFactory = new ListElementModelFactory(model.Value, i);
                        views.AddRange(
                            ClassConfigurator.GenerateFieldView(
                                modelFactory, viewFactory,
                                typeof(T), string.Format("{0}", i))
                                );
                    }
                }catch(System.Exception e) {
                    Debug.LogWarning(e);
                }
                return views.ToArray();
            }).ToReactiveProperty();

            CommandAdd = Input.Select(ts => ts != null).ToReactiveCommand();
            CommandAdd.Subscribe(u => {
                var currSize = Input.Value.Count;
                var inputList = Input.Value;
                inputList.Add(ConstructorExtension.CreateInstanceHierarchy<T>());
                Input.SetValueAndForceNotify(inputList);
            });

            CommandRemove = Input.Select(ts => ts != null && ts.Count > 0).ToReactiveCommand();
            CommandRemove.Subscribe(u => {
                var currSize = Input.Value.Count;
                var inputList = Input.Value;
                inputList.RemoveAt(currSize - 1);
                Input.SetValueAndForceNotify(inputList);
            });
        }
        public void DisposeViews() {
            if (OutputViews != null && OutputViews.Value != null)
                foreach (var v in OutputViews.Value)
                    v.Dispose();
        }
    }
}
