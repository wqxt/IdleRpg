using UnityEngine;

namespace Assets._IdleRpgGame.Scripts.Core.Utils
{
    public class CameraController : ICameraSetup
    {
        public void SetCameraForCanvas(Canvas canvas, Camera camera)
        {
            if (canvas == null || camera == null)
            {
                Debug.LogError("Canvas or camera is null!");
                return;
            }

            canvas.renderMode = RenderMode.WorldSpace;
            canvas.worldCamera = camera;
        }

    }
}

