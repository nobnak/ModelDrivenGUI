using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Scope {

    public class IndentScope : System.IDisposable {
        protected float prevIndent;

        public IndentScope(float indent) {
            prevIndent = SharedParams.indent;
            SharedParams.indent = indent;
        }

        public void Dispose() {
            SharedParams.indent = prevIndent;
        }
    }
}
