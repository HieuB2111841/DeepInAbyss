using Managers.Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Players
{
    public class PlayerUI : PlayerAbstraction
    {
        private UIDocument _document;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadComponent(ref _document);
        }
    }
}