using System;
using UnityEngine;
using UnityEngine.UIElements;


namespace Game.UIs
{
    public class UIStatInfo : VisualElement
    {
        [UnityEngine.Scripting.Preserve]
        public new class UxmlFactory : UxmlFactory<UIStatInfo, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            protected UxmlStringAttributeDescription _type = new UxmlStringAttributeDescription
            {
                name = "type",
                defaultValue = "Type"
            };

            protected UxmlFloatAttributeDescription _value = new UxmlFloatAttributeDescription
            {
                name = "value",
                defaultValue = 0f
            };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                UIStatInfo stat = (UIStatInfo)ve;
                stat.Type = _type.GetValueFromBag(bag, cc);
                stat.Value = _value.GetValueFromBag(bag, cc);
            }
        }

        private const string styleResourcePath = "UIStyleSheets/UIStatInfoStyle.uss";

        private Label _typeText = new Label();
        private Label _valueText = new Label();

        public string Type
        {
            get => _typeText.text;
            set => _typeText.text = value;
        }

        public float Value
        {
            get => float.TryParse(_valueText.text, out var value) ? value : 0f;
            set => _valueText.text = value.ToString();
        }

        public UIStatInfo()
        {
            this.LoadStyle();
            this.AddToClassList("stat-info");

            this.LoadTypeText();
            this.LoadValueText();
        }

        private void LoadStyle()
        {
            StyleSheet style = Resources.Load<StyleSheet>(styleResourcePath);
            if (style) styleSheets.Add(style);
        }

        private void LoadTypeText()
        {
            _typeText.name = "Value";
            _typeText.AddToClassList("type-text");
            hierarchy.Add(_typeText);
        }

        private void LoadValueText()
        {
            _valueText.name = "Value";
            _valueText.AddToClassList("value-text");
            hierarchy.Add(_valueText);
        }
    }
}