using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.View {

    public abstract class BaseView : System.IDisposable {
        public string Title { get; set; }

        public virtual void Dispose() {}
    }
}