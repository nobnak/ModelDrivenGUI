using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem.View {

    public class VectorView : BaseView {

        public void Draw(ReactiveCollection<string> view) {
            using (new GUILayout.HorizontalScope()) {
                GUILayout.Label(Title, GUILayout.ExpandWidth(false));
                for (var i = 0; i < view.Count; i++)
                    view[i] = GUILayout.TextField(view[i]);
            }
        }
    }
}
