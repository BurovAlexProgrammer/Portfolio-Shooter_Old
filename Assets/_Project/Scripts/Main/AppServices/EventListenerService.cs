using System;
using _Project.Scripts.Main.AppServices.Base;
using _Project.Scripts.Main.Game;
using UnityEngine;

namespace _Project.Scripts.Main.AppServices
{
    public class EventListenerService : ServiceBase
    {
        public Action<Character> CharacterDead;
        
        public void SubscribeCharacter(Character character)
        {
            character.Health.OnDead += () =>
            {
                Debug.Log($"Character '{character.gameObject.name}' Dead (click to select)", character);
                CharacterDead?.Invoke(character);
            };
        }

    }
}