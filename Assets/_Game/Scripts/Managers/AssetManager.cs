using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Helpers;
using _Game.Scripts.ObjectPoolSystem.Collectables;
using _Game.Scripts.ObjectPoolSystem.Platforms;
using _Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace _Game.Scripts.Managers
{
    public class AssetManager : GenericSingleton<AssetManager>
    {
        private const string LevelPath = "Levels/Level";
        private const string BallCollectablesPath = "Collectables";
        private const string PlatformPath = "Platforms";

        private List<Platform> _platformList;
        private List<CollectableBallsGroup> _ballCollectableList;
        

        public void LoadPlatforms()
        {
            _platformList = Resources.LoadAll<Platform>(PlatformPath).ToList();
            _ballCollectableList = Resources.LoadAll<CollectableBallsGroup>(BallCollectablesPath).ToList();
        }

        public Platform GetPlatform(PlatformType platformType)
        {
            return _platformList?.FirstOrDefault(x => x.PlatformType == platformType);
        }

        public CollectableBallsGroup GetBallCollectable(BallShapeType ballShapeType)
        {
            return _ballCollectableList?.FirstOrDefault(x => x.ballShapeType == ballShapeType);
        }
        
        public LevelData LoadLevel(int levelIndex)
        {
            return Resources.Load<LevelData>(LevelPath + levelIndex);
        }
    }
}