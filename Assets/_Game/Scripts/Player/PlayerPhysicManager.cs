using System.Collections.Generic;

namespace _Game.Scripts.Player
{
    public class PlayerPhysicManager 
    {
        private readonly List<CollectableBase> _collectables = new List<CollectableBase>();

        public void AddCollectable(CollectableBase collectableBase)
        {
            _collectables.Add(collectableBase);
        }

        public void RemoveCollectable(CollectableBase collectableBase)
        {
            _collectables.Remove(collectableBase);
        }

        public List<CollectableBase> GetCollectables()
        {
            return _collectables;
        }

        public void Clear()
        {
            _collectables.Clear();
        }
    }
}