using System.Collections;
using UnityEngine;

namespace Assets._IdleRpgGame.Scripts.Core.Utils
{
    public interface ICoroutineController
    {
        public void StartCoroutine(IEnumerator coroutine);
        public void StopCoroutine(IEnumerator coroutine);
        public void StopAllCoroutines();
    }
}