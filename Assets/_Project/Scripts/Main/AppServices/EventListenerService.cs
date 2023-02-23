using System;
using _Project.Scripts.Main.AppServices.Base;
using _Project.Scripts.Main.Game;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.AppServices
{
    public class EventListenerService : MonoServiceBase
    {
        public Action<Character> CharacterDead;

        [Inject]
        public void Construct()
        {
            this.RegisterService();
        }
        
        public void SubscribeCharacter(Character character)
        {
            character.Health.Dead += () =>
            {
                Debug.Log($"Character '{character.gameObject.name}' Dead (click to select)", character);
                CharacterDead?.Invoke(character);
            };
        }

    }
}