﻿using Unity.Entities;

namespace Drift.Contacts
{
    [UpdateInGroup(typeof(PresentationSystemGroup), OrderFirst = true)]
    public class RegisterNewContactSystem : SystemBase
    {
        private ContactSystem contactSystem;

        protected override void OnCreate()
        {
            base.OnCreate();
            contactSystem = World.GetOrCreateSystem<ContactSystem>();
        }
        
        protected override void OnUpdate()
        {
            var contacts = contactSystem.Contacts;
            Entities.WithAll<Enter>().ForEach((Entity entity, in Contact contact) =>
            {
                contacts[contact.EntityPair] = entity;
            }).Run();
        }
    }
}