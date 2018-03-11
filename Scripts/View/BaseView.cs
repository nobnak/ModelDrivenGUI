using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.View {

    public class BaseView : System.IDisposable {
        public virtual string Title { get; set; }
        public virtual IList<BaseView> Children { get; set; }

        public virtual void Draw() { }

        public virtual void Dispose() {
            if (Children != null)
                foreach (var v in Children)
                    v.Dispose();
        }
    }
}
