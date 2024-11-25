using Managers.Extension;
using UnityEngine;

namespace Game.Objects
{
    public class TextPopup : SpawnedObject
    {
        [SerializeField] private TextMesh _textMesh;

        public TextMesh TextMesh => _textMesh;


        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadComponent(ref _textMesh);
        }


        public void SetUp(string text)
        {
            this.SetUp(text, Color.white);
        }

        public void SetUp(string text, Color color)
        {
            TextMesh.text = text;
            TextMesh.color = color;
        }
    }
}