using ModelDrivenGUISystem.Factory;
using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using UniRx;
using System.Linq;
using UnityEngine;

namespace ModelDrivenGUISystem.ViewModel {

    public class ArrayViewModel<T> : BaseViewModel {

        public ReactiveProperty<T[]> Input { get; set; }
        public ReactiveProperty<string> OutputCount { get; set; }
        public ReactiveProperty<BaseView[]> OutputViews { get; set; }

        public ArrayViewModel(IValue<T[]> model, IViewFactory viewFactory) {
            Input = new ReactiveProperty<T[]>(model.Value);
            Input.Subscribe(v => model.Value = v);
            
            var views = Input.Value.SelectMany((t, index) => {
                var typeOfField = typeof(T);
                var modelFactory = new ArrayEleentModelFactory(model.Value, index);
                return ClassConfigurator.GenerateFieldView(
                    modelFactory, viewFactory,
                    typeOfField, string.Format("{0}", index));
            });
            OutputCount = new ReactiveProperty<string>(string.Format("{0}", Input.Value.Length));
            OutputViews = new ReactiveProperty<BaseView[]>(views.ToArray());

        }
    }
}
