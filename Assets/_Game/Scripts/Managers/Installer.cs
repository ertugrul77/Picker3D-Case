using _Game.Scripts.ObjectPoolSystem;
using _Game.Scripts.Player;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Managers
{
    public class Installer : MonoInstaller
    {
        [SerializeField] private PlayerBase playerBase;
        [SerializeField] private ObjectPoolManager objectPoolManager;
        [SerializeField] private LevelGenerator levelGenerator;
        

        public override void InstallBindings()
        {
            Container.BindInstance(playerBase);
            Container.BindInstance(levelGenerator);
            Container.BindInstance(objectPoolManager);
        }
    }
}
