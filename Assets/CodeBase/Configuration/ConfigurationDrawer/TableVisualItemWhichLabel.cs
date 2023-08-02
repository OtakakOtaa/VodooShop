using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeBase.Configuration.ConfigurationDrawer
{
    public sealed class TableVisualItemWhichLabel : VisualElement
    {
        public readonly Label ItemTagField;
        public readonly PropertyField ValueField;
        
        public TableVisualItemWhichLabel()
        {
            style.flexDirection = FlexDirection.Row;
            var borderWidth = 2;
            var borderColor = Color.white;
            
            ItemTagField = new Label
            {
                style =
                {
                    unityTextAlign = TextAnchor.MiddleCenter,
                    width = new StyleLength(new Length(value: 20, LengthUnit.Percent)),
                    borderBottomWidth = borderWidth,
                    borderLeftWidth = borderWidth,
                    borderRightWidth = borderWidth,
                    borderBottomColor = borderColor,
                    borderLeftColor = borderColor,
                    borderRightColor = borderColor,
                }
            };

            ValueField = new PropertyField
            {
                style =
                {
                    width = new StyleLength(new Length(value: 80, LengthUnit.Percent)),
                    borderBottomWidth = borderWidth,
                    borderRightWidth = borderWidth,
                    borderBottomColor = borderColor,
                    borderRightColor = borderColor,
                }
            };

            Add(ItemTagField);
            Add(ValueField);
        }

        public new class UxmlFactory : UxmlFactory<TableVisualItemWhichLabel, UxmlTraits> { }
    }
}