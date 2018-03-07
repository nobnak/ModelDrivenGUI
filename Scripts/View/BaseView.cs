using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.View {

    public abstract class BaseView : System.IDisposable {
        public string Title { get; set; }
        public IList<BaseView> Children { get; set; }

        public abstract void Draw();

        public virtual void Dispose() {}
    }
}