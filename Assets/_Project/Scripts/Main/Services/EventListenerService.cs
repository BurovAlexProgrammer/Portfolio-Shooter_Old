using System;
using _Project.Scripts.Main.Game;
using UnityEngine;

namespace _Project.Scripts.Main.Services
{
    public class EventListenerService : BaseService
    {
        public Action<Character> CharacterDead;

        public void SubscribeCharacter(Character character)
        {
            character.Health.Dead += () =>
            {
                Debug.Log("Character Dead (click to select)", character);
                CharacterDead?.Invoke(character);
            };
        }
    }
}