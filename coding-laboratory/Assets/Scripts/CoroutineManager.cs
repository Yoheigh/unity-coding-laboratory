using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    static MonoBehaviour monoInstance;

    private static CoroutineManager instance;
    public static CoroutineManager Instance { get { Init(); return instance; } }
    
    private static void Init()
    {
        if(monoInstance == null)
        {
            monoInstance = new GameObject($"{nameof(CoroutineManager)}").AddComponent<CoroutineManager>();
            instance = monoInstance.GetComponent<CoroutineManager>();
            DontDestroyOnLoad(monoInstance.gameObject);
        }
    }

    public new static Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return monoInstance.StartCoroutine(coroutine);
    }

    public new static void StopCoroutine(Coroutine coroutine)
    {
        monoInstance.StopCoroutine(coroutine);
    }
}
