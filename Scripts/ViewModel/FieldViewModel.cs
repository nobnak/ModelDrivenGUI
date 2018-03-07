using UniRx;

namespace ModelDrivenGUISystem.ViewModel {

    public class OneToOneViewModel<ModelType, ViewType> : BaseViewModel {
        public virtual ReactiveProperty<ModelType> Input { get; set; }
        public virtual ReactiveProperty<ViewType> Output { get; set; }
    }

    public class OneToList<ModelType, ViewType> : BaseViewModel {
        public virtual ReactiveProperty<ModelType> Input { get; set; }
        public virtual ReactiveCollection<ViewType> Outputs { get; set; }
    }
 }
