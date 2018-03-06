using ModelDrivenGUISystem.Extensions.TypeExt;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem {

    public abstract class ViewModelPair {
        public virtual System.Action Draw { get; set; }
    }

    public class OneToOne<ModelType, ViewType> : ViewModelPair {
        public virtual ReactiveProperty<ModelType> Input { get; set; }
        public virtual ReactiveProperty<ViewType> Output { get; set; }
    }

    public class OneToList<ModelType, ViewType> : ViewModelPair {
        public virtual ReactiveProperty<ModelType> Input { get; set; }
        public virtual ReactiveCollection<ViewType> Outputs { get; set; }
    }
 }
