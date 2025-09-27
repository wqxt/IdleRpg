using System.Collections;
using UnityEngine;

namespace Assets._IdleRpgGame.Scripts.Core.Utils
{
    public class CoroutineController : MonoBehaviour, ICoroutineController
    {
        public new void StartCoroutine(IEnumerator routine)
        {
            base.StartCoroutine(routine);
        }

        public new void StopCoroutine(IEnumerator coroutine)
        {
            if (coroutine != null)
            {
                base.StopCoroutine(coroutine);
            }
        }

        public new void StopAllCoroutines() => base.StopAllCoroutines();
    }
}