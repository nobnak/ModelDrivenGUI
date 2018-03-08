using ModelDrivenGUISystem.Accessor;
using ModelDrivenGUISystem.Extensions.VectorExt;
using ModelDrivenGUISystem.ValueWrapper;
using System.Linq;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem.ViewModel {

    public class VectorViewModel<VectorType, VectorElementType>: BaseViewModel {
        public ReactiveProperty<VectorType> Input { get; protected set; }
        public ReactiveCollection<string> Output { get; protected set; }

        public IVectorAccessor<VectorType, VectorElementType> Accessor;

        public VectorViewModel(IValue<VectorType> model, 
            IVectorAccessor<VectorType, VectorElementType> accessor) {
            this.Accessor = accessor;

            Input = new ReactiveProperty<VectorType>(model.Value);
            Output = new ReactiveCollection<string>(
                accessor.Split(Input.Value).Select(v => v.ToString()));

            Input.Subscribe(vec => {
                model.Value =  vec;
                for (var i = 0; i < accessor.Count; i++)
                    Output[i] = accessor.GetElement(ref vec, i).ToString();
            });
            Output.ObserveReplace().Subscribe(e => {
                VectorElementType next;
                if (accessor.TryParse(e.NewValue, out next)) {
                    var vec = Input.Value;
                    accessor.SetElement(ref vec, e.Index, next);
                    Input.Value = vec;
                }
            });
        }
    }
}
