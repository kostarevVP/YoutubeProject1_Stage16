﻿using Drift.Contacts;
using Unity.Entities;

namespace Drift
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(TriggerSystem))]
    public class ReactiveTriggerExitSystem : SystemBase
    {
        private EntityCommandBufferSystem entityCommandBufferSystem;

        protected override void OnCreate()
        {
            base.OnCreate();
            entityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
        }
        
        protected override void OnUpdate()
        {
            var commands = entityCommandBufferSystem.CreateCommandBuffer();
            
            Entities.WithAll<Exit>().ForEach((in TriggerEvent triggerEvent) =>
            {
                if (!HasComponent<Reactive>(triggerEvent.Trigger)
                    || !HasComponent<Triggered>(triggerEvent.Trigger)) return;
                
                commands.AddComponent<Exit>(triggerEvent.Trigger);
                
            }).Run();
        }
    }
}