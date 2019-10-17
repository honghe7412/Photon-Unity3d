using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T instance;

    [Header("Singleton")]
    [SerializeField] private bool _IsPersistent;

    public T InstanceAwake()
    {
        if(ReferenceEquals(instance,null))
            instance = FindObjectOfType(typeof(T)) as T; 
        return instance;
    }

    protected virtual void Awake()
    {
        if (ReferenceEquals(instance, null))
            instance = this as T;
        else if (instance != this as T)
        {
            Destroy(this.gameObject);
        }

        if (_IsPersistent)
        {
            DontDestroyOnLoad(instance);
        }
    }

    protected virtual void OnDestroy()
    {
        if (_IsPersistent == false)
        {
            instance = null;
        }
    }
}
