using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                if(FindObjectOfType<T>() != null)
                    instance = FindObjectOfType<T>();
                else 
                    new GameObject().AddComponent<T>().name = "Singleton_" + typeof(T).ToString();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if(instance != null && instance.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Destroy(this.gameObject);
        }
        else 
            instance = this.GetComponent<T>();
    }
}
