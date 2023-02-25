using System.Collections.Generic;
using _Project.Scripts.Main.AppServices.Base;
using _Project.Scripts.Main.Contexts;
using _Project.Scripts.Main.Game.Brain;
using Zenject;

namespace _Project.Scripts.Main.AppServices.SceneServices
{
    public class BrainControlService : MonoGamePlayContext
    {
        private readonly LinkedList<BrainOwner> _brains = new ();
        private LinkedListNode<BrainOwner> _brainNode;

        [Inject]
        public void Construct()
        {
            this.RegisterContext();
        }
        
        private void Update()
        {
            if (_brains.Count == 0) return;

            _brainNode = _brainNode?.Next ?? _brains.First;
            _brainNode.Value.Think();
        }

        public void AddBrain(BrainOwner brainOwner)
        {
            _brains.AddLast(brainOwner);
        }

        public void RemoveBrain(BrainOwner brainOwner)
        {
            _brains.Remove(brainOwner);
        }

    }
}