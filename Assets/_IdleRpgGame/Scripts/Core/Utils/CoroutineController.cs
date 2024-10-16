using System;
using System.Collections;
using UnityEngine;

public class CoroutineController : MonoBehaviour, ICoroutineController
{
    public new Coroutine StartCoroutine(IEnumerator routine)
    {
        return base.StartCoroutine(routine);
    }

    public new void StopCoroutine(Coroutine coroutine)
    {
        try
        {
            if (coroutine != null)
            {
                base.StopCoroutine(coroutine);
            }
        }
        catch
        {
            new NullReferenceException();
        }

    }

    public new void StopAllCoroutines()
    {
        base.StopAllCoroutines();
    }
}