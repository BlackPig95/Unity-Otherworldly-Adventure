using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAbility : MonoBehaviour
{
    public int bulletCount {get; set; }
    public int maxBullet { get; set; }
    [SerializeField] GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletCount == 0)
            return;
        if (Input.GetKeyDown(KeyCode.C))
        {
            bulletCount--;
            GameObject bullet =  ObjectPooling.Instance.GetObj(bulletPrefab);
            bullet.transform.position = this.transform.position;
        }
    }
}
