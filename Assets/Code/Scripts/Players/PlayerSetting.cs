using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Players
{
    public class PlayerSetting : PlayerAbstraction
    {
        [SerializeField] private float _cameraMoveSpeed = 20f;
        [SerializeField] private float _maxDistanceFromCameraCenter = 0.3f;


        public float CameraMoveSpeed => _cameraMoveSpeed;
        public float MaxDistanceFromCameraCenter => _maxDistanceFromCameraCenter;
    }
}