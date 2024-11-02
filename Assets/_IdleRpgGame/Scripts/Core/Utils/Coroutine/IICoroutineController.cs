using System.Collections;
using UnityEngine;

namespace Assets._IdleRpgGame.Scripts.Core.Utils
{
    public interface ICoroutineController
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
        public void StopCoroutine(Coroutine coroutine);
        public void StopAllCoroutines();
    }
}