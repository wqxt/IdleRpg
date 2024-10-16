using System.Collections;
using UnityEngine;

public interface ICoroutineController
{
    public Coroutine StartCoroutine(IEnumerator coroutine);
    public void StopCoroutine(Coroutine coroutine);
    public void StopAllCoroutines();
}