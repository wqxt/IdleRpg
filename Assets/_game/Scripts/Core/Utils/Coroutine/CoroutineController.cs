
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._IdleRpgGame.Scripts.Core.Utils
{
    public class CoroutineController : MonoBehaviour, ICoroutineController
    {
        public new void StartCoroutine(IEnumerator routine)
        {
            if (routine == null)
            {
                Debug.LogError("[CoroutineController] ");
                return;
            }

            base.StartCoroutine(routine);
        }

        public new void StopCoroutine(IEnumerator routine)
        {
            if (routine == null)
            {
                Debug.LogError("[CoroutineController] ");
                return;
            }

            base.StopCoroutine(routine);
        }

        public new void StopAllCoroutines() => base.StopAllCoroutines();
    }
}