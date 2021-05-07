using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Extensions.GUIExt {

	public static class GUIExtension {

		public static void AddTooltipForLastGUIELement(this string tooltip) {
			var rectTextField = GUILayoutUtility.GetLastRect();
			GUI.Label(rectTextField, new GUIContent("", tooltip));
		}

		public static void DrawTooltips() {
			if (!string.IsNullOrWhiteSpace(GUI.tooltip)) {
				var cont = new GUIContent(GUI.tooltip);
				var size = GUI.skin.label.CalcSize(cont);
				var pos = Event.current.mousePosition;
				pos.y -= size.y;
				var rectTooltip = new Rect(pos, size);

				var bgcolor = GUI.backgroundColor;
				GUI.backgroundColor = new Color(0f, 0f, 0f, 0.5f);
				var style = new GUIStyle(GUI.skin.label);
				style.normal.background = Texture2D.whiteTexture;
				GUI.Label(rectTooltip, cont, style);
				GUI.backgroundColor = bgcolor;
			}
		}
	}
}
