using Assets._IdleRpgGame.Scripts.Core.Utils;
using UnityEngine;
using Zenject;

namespace Assets._IdleRpgGame.Scripts.Core.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuView _mainMenuPrefab;
        [SerializeField] private Camera _mainMenuCameraPrefab;

        public override void InstallBindings()
        {
            InstallCamera();
            MenuViewInstall();
        }

        private void InstallCamera()
        {
            var cameraController = new CameraController();
            // Привязываем созданный экземпляр как ICameraSetup
            Container.Bind<ICameraSetup>().FromInstance(cameraController).AsSingle();

            _mainMenuCameraPrefab = Container.InstantiatePrefabForComponent<Camera>(_mainMenuCameraPrefab);
            Container.Bind<Camera>().FromInstance(_mainMenuCameraPrefab).AsSingle();
        }

        private void MenuViewInstall()
        {
            _mainMenuPrefab = Container.InstantiatePrefabForComponent<MainMenuView>(_mainMenuPrefab);
            Container.Bind<MainMenuView>().FromInstance(_mainMenuPrefab).AsSingle();

            // Получаем Canvas и ICameraSetup из контейнера, настраиваем камеру для Canvas
            var canvas = _mainMenuPrefab.GetComponent<Canvas>();
            var cameraController = Container.Resolve<ICameraSetup>();
            cameraController.SetCameraForCanvas(canvas, _mainMenuCameraPrefab);
        }
    }
}