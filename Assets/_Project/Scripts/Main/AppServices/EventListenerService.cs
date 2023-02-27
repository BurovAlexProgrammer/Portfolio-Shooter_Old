using System;
using _Project.Scripts.Main.AppServices.Base;
using _Project.Scripts.Main.Game;
using UnityEngine;
using CharacterController = _Project.Scripts.Main.Game.CharacterController;

namespace _Project.Scripts.Main.AppServices
{
    public class EventListenerService : ServiceBase
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