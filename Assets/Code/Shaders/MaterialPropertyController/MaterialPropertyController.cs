using Managers.Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Designs.Shaders
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MaterialPropertyController : MonoBehaviour
    {
        private SpriteRenderer _renderer;

        private void Awake()
        {
            this.LoadComponent(ref _renderer);
        }

        private void Start()
        {
            MaterialPropertyBlock materialPropertyBlock = new();

            Vector2 localScale = new Vector2(transform.localScale.x, transform.localScale.y);
            materialPropertyBlock.SetVector("_Tilling", localScale);
            materialPropertyBlock.SetTexture("_MainTex", _renderer.sprite.texture);

            _renderer.SetPropertyBlock(materialPropertyBlock);
        }
    }
}