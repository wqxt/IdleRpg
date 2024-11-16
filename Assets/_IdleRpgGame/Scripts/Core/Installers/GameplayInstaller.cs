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
            PawnPoolInstall();
            SpawnerInstall();
            CameraInstall();
            GameplayViewInstall();

            HealthViewInstall();
            HealthObserverInstall();

            WeaponObserverInstall();
            WeaponViewInstall();

        }

        private void WeaponObserverInstall()
        {
            // Регистрируем PawnConfiguration из PawnPool для Weapon Observer
            var characterConfiguration = _pawnPoolPrefab.Character.PawnConfiguration;
            Container.Bind<PawnConfiguration>().FromInstance(characterConfiguration).AsSingle();


            Container.Bind<WeaponObserver>()
            .AsSingle()
            .WithArguments(Container.Resolve<PawnConfiguration>());
        }

        private void WeaponViewInstall()
        {
            _switchWeaponViewPrefab = Container.InstantiatePrefabForComponent<WeaponView>(_switchWeaponViewPrefab);
            Container.Bind<WeaponView>().FromInstance(_switchWeaponViewPrefab).AsSingle();

            var canvas = _switchWeaponViewPrefab.GetComponent<Canvas>();
            var cameraController = Container.Resolve<ICameraSetup>();
            cameraController.SetCameraForCanvas(canvas, _cameraPrefab);
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
            for (int i = 0; i < _healthViewPrefabs.Length; i++)
            {
                _healthViewPrefabs[i] = Container.InstantiatePrefabForComponent<HealthView>(_healthViewPrefabs[i]);
                Container.Bind<HealthView>().FromInstance(_healthViewPrefabs[i]).AsCached();

                var canvas = _healthViewPrefabs[i].GetComponent<Canvas>();
                var cameraController = Container.Resolve<ICameraSetup>();
                cameraController.SetCameraForCanvas(canvas, _cameraPrefab);
            }

            Container.Bind<HealthView[]>().FromInstance(_healthViewPrefabs).AsSingle();
        }

        private void HealthObserverInstall()
        {
            var healthObserver = Container.Instantiate<HealthObserver>();
            Container.Bind<HealthObserver>().FromInstance(healthObserver).AsSingle();
        }

        private void SpawnerInstall()
        {
            _spawnerPrefab = Container.InstantiatePrefabForComponent<Spawner>(_spawnerPrefab);
            Container.Bind<Spawner>().FromInstance(_spawnerPrefab).AsSingle();
        }

        private void PawnPoolInstall()
        {
            Container.Bind<PawnPool>().FromInstance(_pawnPoolPrefab).AsSingle();
        }
    }
}