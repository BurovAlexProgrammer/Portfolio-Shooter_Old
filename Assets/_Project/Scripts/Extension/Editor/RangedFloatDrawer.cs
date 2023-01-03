using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Extension.Attributes.Editor
{
	[CustomPropertyDrawer(typeof(RangedFloat), true)]
	public class RangedFloatDrawer : PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			label = EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, label);

			SerializedProperty minProp = property.FindPropertyRelative("MinValue");
			SerializedProperty maxProp = property.FindPropertyRelative("MaxValue");
        
			float minValue = minProp.floatValue;
			float maxValue = maxProp.floatValue;

			float rangeMin = 0;
			float rangeMax = 1;

			var ranges = (MinMaxRangeAttribute[])fieldInfo.GetCustomAttributes(typeof (MinMaxRangeAttribute), true);
			if (ranges.Length > 0)
			{
				rangeMin = ranges[0].Min;
				rangeMax = ranges[0].Max;
			}

			const float rangeBoundsLabelWidth = 40f;

			var rangeBound1LabelRect = new Rect(position);
			rangeBound1LabelRect.width = rangeBoundsLabelWidth;
			GUI.Label(rangeBound1LabelRect, new GUIContent(minValue.ToString("F2")));
			position.xMin += rangeBoundsLabelWidth;

			var rangeBound2LabelRect = new Rect(position);
			rangeBound2LabelRect.xMin = rangeBound2LabelRect.xMax - rangeBoundsLabelWidth;
			GUI.Label(rangeBound2LabelRect, new GUIContent(maxValue.ToString("F2")));
			position.xMax -= rangeBoundsLabelWidth;

			EditorGUI.BeginChangeCheck();
			EditorGUI.MinMaxSlider(position, ref minValue, ref maxValue, rangeMin, rangeMax);
			if (EditorGUI.EndChangeCheck())
			{
				minProp.floatValue = minValue;
				maxProp.floatValue = maxValue;
			}

			EditorGUI.EndProperty();
		}
	}
}
