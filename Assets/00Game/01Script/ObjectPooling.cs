using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    Dictionary<GameObject, List<GameObject>> pool = new Dictionary<GameObject, List<GameObject>>();

    public virtual GameObject GetObj(GameObject prefab)
    {
        List<GameObject> listObj = new List<GameObject>();
        if (pool.ContainsKey(prefab))
            listObj = pool[prefab];
        else
        {
            pool.Add(prefab, listObj);
        }
        foreach (GameObject obj in listObj)
        {
            if (obj.activeSelf)
                continue;
            return obj;
        }

        GameObject obj2 = Instantiate(prefab, this.transform.position, Quaternion.identity);
        listObj.Add(obj2);
        return obj2;
    }

}
