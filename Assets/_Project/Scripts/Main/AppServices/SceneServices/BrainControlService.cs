using System.Collections.Generic;
using _Project.Scripts.Main.Game.Brain;

namespace _Project.Scripts.Main.AppServices.SceneServices
{
    public class BrainControlService : MonoServiceBase
    {
        private readonly LinkedList<BrainOwner> _brains = new ();
        private LinkedListNode<BrainOwner> _brainNode;

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