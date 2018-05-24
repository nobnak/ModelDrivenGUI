using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Scope {

    public class GUIChangedScope : System.IDisposable {
        protected bool initialState;
        protected System.Action callIfChnaged;

        public GUIChangedScope(System.Action callIfChnaged) {
            this.callIfChnaged = callIfChnaged;
            initialState = GUI.changed;
            GUI.changed = false;
        }

        public void Dispose() {
            if (GUI.changed)
                callIfChnaged();
            GUI.changed = GUI.changed || initialState;
        }
    }
}