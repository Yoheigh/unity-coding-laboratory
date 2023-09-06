using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager instance;
    public static Manager Instance { get { Init(); return instance; } }

    public PoolManager Pool = new PoolManager();
    public ResourceManager Resource = new ResourceManager();
    public ObjectManager Object = new ObjectManager();
    public UITest UI;
    public CoroutineManager Co;

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null)
            {
                go = new GameObject("@Managers");
                go.AddComponent<Manager>();
            }

            instance = go.GetComponent<Manager>();
            DontDestroyOnLoad(go);
        }
    }

    //private static void MonoInit<T>() where T : UnityEngine.Component
    //{
    //    T monoInstance = new GameObject($"@{typeof(T)}").AddComponent<T>();

    //    var _instance = monoInstance.GetComponent<T>();
    //    DontDestroyOnLoad(monoInstance.gameObject);

    //    return _instance;
    //}

    private void Start()
    {
        // °øºÎ¿ë
        // UI.Init();
        // Co = MonoInit<CoroutineManager>();
        Debug.Log(CoroutineManager.Instance);
    }
}
