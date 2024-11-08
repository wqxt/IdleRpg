using System.Collections;
using UnityEngine;

namespace Assets._IdleRpgGame.Scripts.Core.Utils
{
    public class CoroutineController : MonoBehaviour, ICoroutineController
    {
        public new Coroutine StartCoroutine(IEnumerator routine)
        {
            return base.StartCoroutine(routine);
        }

        public new void StopCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                base.StopCoroutine(coroutine);
            }
        }

        public new void StopAllCoroutines() => base.StopAllCoroutines();
    }
}