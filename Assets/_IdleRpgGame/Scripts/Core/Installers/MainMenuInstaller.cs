using UnityEngine;
using Zenject;

namespace Assets._IdleRpgGame.Scripts.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private Camera _mainMenuCamera;

        public override void InstallBindings()
        {
            MenuViewInstall();
            InstallCamera();
            SetCamera();
        }

        public void MenuViewInstall() => _mainMenuView = Container.InstantiatePrefabForComponent<MainMenuView>(_mainMenuView);
        public void InstallCamera() => _mainMenuCamera = Container.InstantiatePrefabForComponent<Camera>(_mainMenuCamera);

        public void SetCamera()
        {
            var mainMenuCanvas = _mainMenuView.GetComponent<Canvas>();
            mainMenuCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            mainMenuCanvas.worldCamera = _mainMenuCamera;
        }
    }
}