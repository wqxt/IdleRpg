using Assets._IdleRpgGame.Scripts.Core.Utils;
using UnityEngine;
using Zenject;

namespace Assets._IdleRpgGame.Scripts.Core.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameplayView _gameplayViewPrefab;
        [SerializeField] private Camera _cameraPrefab;
        [SerializeField] private HealthView[] _healthViewPrefabs;
        [SerializeField] private Spawner _spawnerPrefab;
        [SerializeField] private PawnPool _pawnPoolPrefab;
        [SerializeField] private WeaponView _switchWeaponViewPrefab;

        public override void InstallBindings()
        {
            CameraInstall();
            PawnFactoryInstall();
            PawnPoolInstall();
            SpawnerInstall();

            GameplayViewInstall();

            HealthViewInstall();
            HealthObserverInstall();

            WeaponObserverInstall();
            WeaponViewInstall();

        }

        private void PawnFactoryInstall()
        {
            Container.Bind<IPawnFactory>().To<PawnFactory>().AsSingle();
        }

        private void WeaponObserverInstall()
        {
            Container.Bind<WeaponObserver>()
                .AsSingle()
                .WithArguments(_pawnPoolPrefab._character.PawnConfiguration);
        }

        private void WeaponViewInstall()
        {
            var switchWeaponViewPrefab = Container.InstantiatePrefabForComponent<WeaponView>(_switchWeaponViewPrefab);
            Container.Bind<WeaponView>().FromInstance(switchWeaponViewPrefab).AsSingle();

            var canvas = switchWeaponViewPrefab.GetComponent<Canvas>();
            var camera = Container.Resolve<Camera>();
            var cameraController = Container.Resolve<ICameraSetup>();

            cameraController.SetCameraForCanvas(canvas, camera);
        }


        private void CameraInstall()
        {
            var cameraController = new CameraController();
            Container.Bind<ICameraSetup>().FromInstance(cameraController).AsSingle();

            var camera = Container.InstantiatePrefabForComponent<Camera>(_cameraPrefab);
            Container.Bind<Camera>().FromInstance(camera).AsSingle();
        }

        private void GameplayViewInstall()
        {
            var gameplayView = Container.InstantiatePrefabForComponent<GameplayView>(_gameplayViewPrefab);
            Container.Bind<GameplayView>().FromInstance(gameplayView).AsSingle();

            var canvas = gameplayView.GetComponent<Canvas>();
            var cameraController = Container.Resolve<ICameraSetup>();
            var camera = Container.Resolve<Camera>();

            cameraController.SetCameraForCanvas(canvas, camera);
        }

        private void HealthViewInstall()
        {
            for (int i = 0; i < _healthViewPrefabs.Length; i++)
            {
                var healthView = _healthViewPrefabs[i];
                healthView = Container.InstantiatePrefabForComponent<HealthView>(_healthViewPrefabs[i]);
                Container.Bind<HealthView>().FromInstance(healthView).AsCached();

                var canvas = healthView.GetComponent<Canvas>();
                var camera = Container.Resolve<Camera>();
                var cameraController = Container.Resolve<ICameraSetup>();
                cameraController.SetCameraForCanvas(canvas, camera);
            }
        }

        private void HealthObserverInstall()
        {
            var healthObserver = Container.Instantiate<HealthObserver>();
            Container.Bind<HealthObserver>().FromInstance(healthObserver).AsSingle();
        }

        private void SpawnerInstall()
        {
            var spawner = Container.InstantiatePrefabForComponent<Spawner>(_spawnerPrefab);
            Container.Bind<Spawner>().FromInstance(spawner).AsSingle();
        }

        private void PawnPoolInstall()
        {
            Container.Bind<PawnPool>().FromScriptableObject(_pawnPoolPrefab).AsSingle();
        }
    }
}