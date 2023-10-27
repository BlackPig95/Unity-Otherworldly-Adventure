using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    Dictionary<GameObject, List<GameObject>> pool = new Dictionary<GameObject, List<GameObject>>();
    public virtual GameObject GetObject(GameObject prefab)
    {
        List<GameObject> listObj = new List<GameObject>();
        if (pool.ContainsKey(prefab))
            listObj = pool[prefab];
        else
        {
            pool.Add(prefab, listObj);
        }

        foreach (GameObject g in listObj)
        {
            if (g.activeSelf)
                continue;
            return g;
        }
        GameObject g2 = Instantiate(prefab, this.transform.position, Quaternion.identity);
        listObj.Add(g2);
        return g2;
    }
}
