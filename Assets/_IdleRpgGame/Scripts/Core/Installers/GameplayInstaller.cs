using Assets._IdleRpgGame.Scripts.Core.Utils;
using UnityEngine;
using Zenject;

namespace Assets._IdleRpgGame.Scripts.Core.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameplayView _gameplayViewPrefab;
        [SerializeField] private Camera _cameraPrefab;
        [SerializeField] private HealthObserver _healthObserverPrefab;
        [SerializeField] private Spawner _spawnerPrefab;
        public override void InstallBindings()
        {
            CameraInstall();
            GameplayViewInstall();

            // Spawner первый,  так как HealthSystem требует наличие сущностей на сцене.
            SpawnerInstall();
            HealthViewInstall();

        }

        private void CameraInstall()
        {
            var cameraController = new CameraController();
            // Привязываем созданный экземпляр как ICameraSetup
            Container.Bind<ICameraSetup>().FromInstance(cameraController).AsSingle();

            _cameraPrefab = Container.InstantiatePrefabForComponent<Camera>(_cameraPrefab);
            Container.Bind<Camera>().FromInstance(_cameraPrefab).AsSingle();
        }

        private void GameplayViewInstall()
        {
            _gameplayViewPrefab = Container.InstantiatePrefabForComponent<GameplayView>(_gameplayViewPrefab);
            Container.Bind<GameplayView>().FromInstance(_gameplayViewPrefab).AsSingle();

            var canvas = _gameplayViewPrefab.GetComponent<Canvas>();
            var cameraController = Container.Resolve<ICameraSetup>();
            cameraController.SetCameraForCanvas(canvas, _cameraPrefab);
        }

        private void HealthViewInstall()
        {
            _healthObserverPrefab = Container.InstantiatePrefabForComponent<HealthObserver>(_healthObserverPrefab);
            Container.Bind<HealthObserver>().FromInstance(_healthObserverPrefab).AsSingle();

            var canvas = _healthObserverPrefab.GetComponent<Canvas>();
            var cameraController = Container.Resolve<ICameraSetup>();
            cameraController.SetCameraForCanvas(canvas, _cameraPrefab);
        }

        private void SpawnerInstall()
        {
            _spawnerPrefab = Container.InstantiatePrefabForComponent<Spawner>(_spawnerPrefab);
            Container.Bind<Spawner>().FromInstance(_spawnerPrefab).AsSingle();
        }
    }
}