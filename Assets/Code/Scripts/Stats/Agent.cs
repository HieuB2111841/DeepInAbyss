using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities.Stats
{
    [Serializable]
    public class Agent
    {
        [SerializeField] private Transform _author;
        [SerializeField] private string _reason;
        [SerializeField] private float _value;
        [SerializeField] private float _duration;
        [SerializeField] private float _remaining;


        public Transform Author
        {
            get => _author;
            private set => _author = value;
        }

        public string Reason
        {
            get => _reason;
            set => _reason = value;
        }

        public float Value
        {
            get => _value;
            set => _value = value;
        }

        public float Duration
        {
            get => _duration;
            set => _duration = value;
        }
        public float Remaining
        {
            get => _remaining;
            set => _remaining = Mathf.Clamp(value, 0f, Duration);
        }


        public Agent(Transform author, string reason, float value = 0f, float duration = float.PositiveInfinity)
        {
            this.Author = author;
            this.Reason = reason;
            this.Value = value;

            this.Duration = duration;
            this.Remaining = duration;
        }


        public Agent CopyWith(Transform author = null, string reason = null, float? value = null, float? duration = null)
        {
            return new Agent(author ?? Author, reason ?? Reason, value ?? Value, duration ?? Duration);
        }

        public Agent Set(Transform author = null, string reason = null, float? value = null, float? duration = null)
        {
            this.Author = author ?? this.Author;
            this.Reason = reason ?? this.Reason;
            this.Value = value ?? this.Value;

            this.Duration = duration ?? this.Duration;
            this.Remaining = duration ?? this.Remaining;

            return this;
        }

        public override string ToString()
        {
            return $"{Author?.name ?? "Unknown"}: {Reason ?? "no reason"} - ({Value}) ({Duration}s)";
        }
    }
}