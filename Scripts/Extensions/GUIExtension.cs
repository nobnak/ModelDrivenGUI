using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Extensions.GUIExt {

	public static class GUIExtension {

		public static void AddTooltipForLastGUIELement(this string tooltip) {
			var rectTextField = GUILayoutUtility.GetLastRect();
			GUI.Label(rectTextField, new GUIContent("", tooltip));
		}
	}
}
