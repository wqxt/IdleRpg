using UnityEngine;

namespace Assets._IdleRpgGame.Scripts.Core.Utils
{
    public interface ICameraSetup
    {
        public void SetCameraForCanvas(Canvas canvas, Camera camera);
    }
}