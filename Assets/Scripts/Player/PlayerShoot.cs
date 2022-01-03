using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Bullet Config")]
    [SerializeField] private float bulletForce = 250;
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private Transform bulletParent = null;

    private List<Bullet> activePlayerBullets = new List<Bullet>();

    private string bulletName = "PlayerBullet";

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;

        Bullet bullet = GetBullet();
        bullet.transform.position = bulletParent.transform.position;
        bullet.gameObject.SetActive(true);
        bullet.ShootAt(direction, bulletForce);
    }

    private Bullet GetBullet()
    {
        Bullet newBullet = activePlayerBullets.Find(b => b.name == bulletName && !b.gameObject.activeSelf)?.GetComponent<Bullet>();

        if (newBullet == null)
        {
            BulletPool.Instance.InstantiateBullet(bulletPrefab, bulletName, activePlayerBullets, bulletParent);
            newBullet = activePlayerBullets.Find(b => b.name == bulletName && !b.gameObject.activeSelf)?.GetComponent<Bullet>();
        }

        return newBullet;
    }
}
