using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Attributes {

	[System.AttributeUsage(System.AttributeTargets.Class)]
	public class ClassTooltipAttribute : TooltipAttribute {

		public ClassTooltipAttribute(string tooltip) : base(tooltip) {
		}
	}
}
