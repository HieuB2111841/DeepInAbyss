using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities.Stats
{
    [Serializable]
    public class SlideStat : Stat
    {
        [SerializeField] protected float _remainingValue;

        public event Action<float> OnRemainingValueChanged;

        public virtual float RemainingValue
        {
            get => _remainingValue;
            protected set
            {
                if (_remainingValue == value) return; // Nothing changed
                _remainingValue = Mathf.Clamp(value, 0f, Value);
                OnRemainingValueChanged?.Invoke(value);
            }
        }

        public override void Reset()
        {
            base.Reset();
            _remainingValue = 0f;
        }

        public virtual void AddToRemaining(Agent agent)
        {
            RemainingValue += agent.Value;
        }

        public virtual void AddToRemaining(Transform author, string reason, float value = 0f, float duration = float.PositiveInfinity)
            => this.AddToRemaining(new Agent(author, reason, value, duration));


        public override void Add(Agent agent)
        {
            base.Add(agent);
            RemainingValue += agent.Value;
        }

        public override bool Remove(Agent agent)
        {
            bool isSuccessed = base.Remove(agent);
            if(isSuccessed)
                RemainingValue -= agent.Value;

            return isSuccessed;
        }
    }
}