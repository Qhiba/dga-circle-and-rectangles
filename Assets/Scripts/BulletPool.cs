using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool _instance = null;
    public static BulletPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BulletPool>();
            }

            return _instance;
        }
    }

    public void InstantiateBullet(GameObject prefab, string objName, List<Bullet> bullets, Transform parent)
    {
        GameObject newBulletObj = bullets.Find(b => b.name == objName && !b.gameObject.activeSelf)?.gameObject;

        if (newBulletObj == null)
        {
            newBulletObj = Instantiate(prefab, new Vector3(parent.position.x, parent.position.y), Quaternion.identity);
            newBulletObj.name = objName;
            newBulletObj.SetActive(false);
        }        

        Bullet newBullet = newBulletObj.GetComponent<Bullet>();
        bullets.Add(newBullet);
    }
}
