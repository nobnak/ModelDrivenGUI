using nobnak.Gist.IMGUI.Scope;
using UnityEngine;

namespace ModelDrivenGUISystem.View {

	public class ClassView : BaseView {

        protected bool visible = false;

		public ClassView(bool visible = false) {
			this.visible = visible;
		}

        public override void Initialize() {
            if (initialized)
                return;
            base.Initialize();

            foreach (var c in Children)
                c.Initialize();
        }
        public override void Draw() {
            Initialize();

            using (new GUILayout.VerticalScope())
            using (new FoldoutScope(ref visible, Title)) {
                using (new IndentScope()) {
                    if (visible && Children != null)
                        foreach (var v in Children)
                            v.Draw();
                }
            }
        }
    }
}
