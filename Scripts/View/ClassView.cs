using nobnak.Gist.IMGUI.Scope;
using UnityEngine;

namespace ModelDrivenGUISystem.View {

	public class ClassView : BaseView {

		public const string KEY_EXTRUDE_CHILDREN = "_EXTRUDE_CHILDREN";

        protected bool visible = false;
		protected bool expand = false;

		public ClassView(bool visible = false) {
			this.visible = visible;
		}

        public override void Initialize() {
            if (initialized)
                return;
            base.Initialize();

            foreach (var c in Children)
                c.Initialize();

			expand = CustomData.TryGetValue(KEY_EXTRUDE_CHILDREN, out object v)
				&& v is bool
				&& (bool)v;
			visible = visible || expand;
        }
        public override void Draw() {
            Initialize();

			using (new GUILayout.VerticalScope()) {
				if (expand)
					DrawChildren();
				else {
					using (new FoldoutScope(ref visible, Title, Tooltip))
					using (new IndentScope()) {
						DrawChildren();
					}
				}
			}
        }

		#region member
		protected void DrawChildren() {
			if (visible && Children != null)
				foreach (var v in Children)
					v.Draw();
		}
		#endregion
	}
}
