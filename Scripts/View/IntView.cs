using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem.View {

    public class IntView : BaseView {

        public void Draw(ReactiveProperty<string> view) {
            using (new GUILayout.HorizontalScope()) {
                GUILayout.Label(Title, GUILayout.ExpandWidth(false));
                view.Value = GUILayout.TextField(view.Value);
            }
        }
    }
}
