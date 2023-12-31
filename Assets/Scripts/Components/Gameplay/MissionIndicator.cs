﻿using Unity.Entities;
using UnityEngine;

namespace Drift
{
    [RequireComponent(typeof(ParticleSystem))]
    public class MissionIndicator : MonoBehaviour, IConvertGameObjectToEntity
    {
        private ParticleSystem particles;

        void Awake()
        {
            particles = GetComponent<ParticleSystem>();
            SetState(false);
        }
        
        public void SetState(bool state)
        {
            if (state)
            {
                particles.gameObject.SetActive(true);
                particles.Play(true);
            }
            else
                particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            conversionSystem.AddHybridComponent(GetComponent<ParticleSystem>());
            conversionSystem.AddHybridComponent(this);
        }
    }
}