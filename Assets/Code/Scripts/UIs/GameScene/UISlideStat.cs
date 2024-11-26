using System;
using UnityEngine;
using UnityEngine.UIElements;


namespace Game.UIs
{
    public class UISlideStat : VisualElement
    {
        [UnityEngine.Scripting.Preserve]
        public new class UxmlFactory : UxmlFactory<UISlideStat, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            protected UxmlFloatAttributeDescription _currentValue = new UxmlFloatAttributeDescription 
            { 
                name = "current-value", 
                defaultValue = 100f,
                
            };

            protected UxmlFloatAttributeDescription _maxValue = new UxmlFloatAttributeDescription 
            { 
                name = "max-value", 
                defaultValue = 100f 
            };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                UISlideStat slideStat = (UISlideStat)ve;
                slideStat.CurrentValue = _currentValue.GetValueFromBag(bag, cc);
                slideStat.MaxValue = _maxValue.GetValueFromBag(bag, cc);
            }
        }

        private const string styleResourcePath = "UIStyleSheets/UISlideStatStyle.uss";

        VisualElement filter = new VisualElement();
        Label valueText = new Label();

        private float _currentValue = 0f;
        private float _maxValue = 0f;


        public float CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = value;
                this.SetSlide();
            }
        }

        public float MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                this.SetSlide();
            }
        }

        public float Ratio => Mathf.Clamp01(CurrentValue / MaxValue);

        public UISlideStat()
        {
            this.LoadStyle();
            this.AddToClassList("slide-stat");

            this.LoadFilter();
            this.LoadValueText();
        }


        private void LoadStyle()
        {
            StyleSheet style = Resources.Load<StyleSheet>(styleResourcePath);
            if (style) styleSheets.Add(style);
        }

        private void LoadFilter()
        {
            filter.name = "Filter";
            filter.AddToClassList("filter");
            hierarchy.Add(filter);
        }

        private void LoadValueText()
        {
            valueText.name = "Value";
            valueText.AddToClassList("value");
            this.SetSlide();
            hierarchy.Add(valueText);
        }

        private void SetSlide()
        {
            valueText.text = $"{Mathf.Round(CurrentValue)}/{Mathf.Round(MaxValue)}";
            filter.style.width = new StyleLength(new Length(Ratio * 100f, LengthUnit.Percent));
        }
    }
}