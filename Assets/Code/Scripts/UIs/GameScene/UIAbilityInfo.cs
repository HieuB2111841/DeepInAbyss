using System;
using UnityEngine;
using UnityEngine.UIElements;


namespace Game.UIs
{
    public class UIAbilityInfo : VisualElement
    {
        [UnityEngine.Scripting.Preserve]
        public new class UxmlFactory : UxmlFactory<UIAbilityInfo, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            
        }


        private const string styleResourcePath = "UIStyleSheets/UIAbilityInfo.uss";
        VisualElement image = new VisualElement();
        VisualElement filter = new VisualElement();

        public UIAbilityInfo()
        {
            this.LoadStyle();
            this.AddToClassList("ability-info");

            this.LoadImage();
            this.LoadFilter();
        }


        private void LoadStyle()
        {
            StyleSheet style = Resources.Load<StyleSheet>(styleResourcePath);
            if (style) styleSheets.Add(style);
        }

        private void LoadImage()
        {
            image.name = "Image";
            image.AddToClassList("ability-image");
            hierarchy.Add(image);
        }

        private void LoadFilter()
        {
            filter.name = "Filter";
            filter.AddToClassList("ability-filter");
            hierarchy.Add(filter);
        }

        public void SetImage(Sprite sprite)
        {
            image.style.backgroundImage = new StyleBackground(sprite);
        }

        public void ShowFilter(bool isShow = false)
        {
            if(isShow) filter.RemoveFromClassList("hide");
            else filter.AddToClassList("hide");
        }
    }
}