﻿using Drift.Contacts;
using Unity.Entities;

namespace Drift
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(TriggerSystem))]
    [UpdateBefore(typeof(ReactiveTriggerEnterSystem))]
    public class ReactiveTriggerCompleteEnterSystem : SystemBase
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
            
            Entities.WithAll<Trigger, Enter>().ForEach((Entity entity) =>
            {
                commands.RemoveComponent<Enter>(entity);
            }).Run();
        }
    }
}