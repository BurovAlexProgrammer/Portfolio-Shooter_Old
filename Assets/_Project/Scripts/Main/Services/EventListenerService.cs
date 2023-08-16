using System;
using UnityEngine;
using CharacterController = Main.Game.CharacterController;

namespace Main.Services
{
    public class EventListenerService : IService
    {
        public Action<CharacterController> CharacterDead;
        
        public void SubscribeCharacter(CharacterController characterController)
        {
            characterController.Health.OnDead += () =>
            {
                Debug.Log($"Character '{characterController.gameObject.name}' Dead (click to select)", characterController);
                CharacterDead?.Invoke(characterController);
            };
        }

    }
}