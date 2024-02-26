using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Managers;
using _Game.Scripts.ObjectPoolSystem.Collectables;
using _Game.Scripts.ObjectPoolSystem.Platforms;
using UnityEngine;

namespace _Game.Scripts.ObjectPoolSystem
{
    public class ObjectPoolManager : MonoBehaviour
    {
        
        private List<Platform> _platformList;
        private List<CollectableBallsGroup> _ballCollectableList;

        public Platform GetPlatform(PlatformType platformType)
        {
            if(_platformList == null)
                _platformList = new List<Platform>();
            
            var platform = _platformList?.FirstOrDefault(x => !x.IsActive && x.PlatformType == platformType);
            if (platform == null)
            {
                platform = AssetManager.Instance.GetPlatform(platformType);
                platform = Instantiate(platform, transform);
                platform.Initialize();
                _platformList?.Add(platform);
            }
            
            platform.SetActivity(true);
            return platform;
        }

        public CollectableBallsGroup GetBall(BallShapeType ballShapeType)
        {
            if(_ballCollectableList == null)
                _ballCollectableList = new List<CollectableBallsGroup>();

            var ball = _ballCollectableList?.FirstOrDefault(x => !x.IsActive && x.ballShapeType == ballShapeType);
            if (ball == null)
            {
                ball = AssetManager.Instance.GetBallCollectable(ballShapeType);
                ball = Instantiate(ball, transform);
                ball.Initialize();
                _ballCollectableList?.Add(ball);
            }
            
            ball.SetActivity(true);
            return ball;
        }
        

        public void DeactivateWholePool()
        {
            if(_platformList.Count <= 0)
                return;
            
            foreach (var platform in _platformList)
            {
                platform.SetActivity(false);
            }

            foreach (var ball in _ballCollectableList)
            {
                ball.SetActivity(false);
            }
        }
    }
}
