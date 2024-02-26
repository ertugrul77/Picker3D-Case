using System;
using System.Collections.Generic;
using _Game.Scripts.ObjectPoolSystem.Collectables;
using _Game.Scripts.ObjectPoolSystem.Platforms;
using UnityEngine;

namespace _Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Create LevelData", fileName = "LevelData", order = 0)]
    public class LevelData : ScriptableObject
    {
        public List<PlatformData> platformDataList;
    }

    [Serializable]
    public class PlatformData
    {
        public PlatformType platformType;
        
        [Header("Count for Collector")]
        public int collectCount;
        
        [Header("Balls must be added on the Simple Platform")]
        public List<BallCollectableData> ballCollectableDataList;
    }

    [Serializable]
    public class BallCollectableData
    {
        public Vector3 position;
        public BallShapeType ballShapeType;
    }
}