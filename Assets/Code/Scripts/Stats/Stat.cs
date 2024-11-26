using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Entities.Stats 
{
    [Serializable]
    public class Stat 
    {
        [SerializeField] protected float _value;
        [SerializeField] protected List<Agent> _agents = new();

        public event Action<float> OnValueChanged;
        public event Action<Agent> OnAgentChanged;


        public virtual float Value
        {
            get => _value;
            protected set
            {
                if (_value == value) return; // Nothing changed
                _value = value;
                OnValueChanged?.Invoke(value);
            }
        }

        public virtual void Reset()
        {
            _agents.Clear();
            _value = 0;
        }

        public virtual void Add(Agent agent)
        {
            _agents.Add(agent);
            OnAgentChanged?.Invoke(agent);

            Value += agent.Value;
        }

        public virtual void Add(Transform author, string reason, float value = 0f, float duration = float.PositiveInfinity)
            => this.Add(new Agent(author, reason, value, duration));


        public virtual bool Remove(Agent agent)
        {
            bool isSuccessed = _agents.Remove(agent);
            if (isSuccessed)
            {
                Value -= agent.Value;
                OnAgentChanged?.Invoke(agent);
            }
                
            return isSuccessed;
        }

        public virtual bool Remove(Transform author, string reason = null)
        {
            Agent agent = _agents.Find((a) => this.Match(a, author, reason));

            if (agent == null) return false;
            return this.Remove(agent);
        }

        public virtual bool Contains(Transform author, string reason = null)
        {
            return _agents.Any((a) => this.Match(a, author, reason));
        }
        public virtual bool Contains(Agent agent)
        {
            return _agents.Contains(agent);
        }



        protected virtual bool Match(Agent agent, Transform author, string reason = null)
        {
            bool authorComp = agent.Author == author;
            bool reasonComp = reason == null || reason.Equals(agent.Reason); // reason null or equals a.Reason
            return authorComp && reasonComp;
        }
    }
}