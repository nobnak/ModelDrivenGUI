using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Scope {

    public class FoldoutScope : System.IDisposable {
        protected bool prevVisibility;

        public FoldoutScope(bool visibility) {
            prevVisibility = SharedParams.visible;
            SharedParams.visible = visibility;
        }

        public void Dispose() {
            SharedParams.visible = prevVisibility;
        }
    }
}
