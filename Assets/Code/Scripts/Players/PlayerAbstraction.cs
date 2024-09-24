using Managers.Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Players
{
    public abstract class PlayerAbstraction : MonoBehaviour
    {
        private Player _player;


        public Player Player => _player;



        protected virtual void Awake()
        {
            this.LoadComponents();
        }

        protected virtual void LoadComponents()
        {
            this.LoadComponent(ref _player);
        }

    }
}