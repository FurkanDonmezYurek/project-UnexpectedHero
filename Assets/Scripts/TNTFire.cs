using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTFire : MonoBehaviour
{
    public Vector2 firePoint;
    public Transform dynamiteSpawnPoint;
    public GameObject dynamitePrefb;
    public float dynamiteSpeed;


    void Fire()
    {
        var dynamite = Instantiate(dynamitePrefb, dynamiteSpawnPoint.position, dynamiteSpawnPoint.rotation);
        if (dynamite.transform.rotation == Quaternion.Euler(0f, 0f, 0f))
        {
            firePoint = new Vector2(1f, 0.2f);
        }
        else
        {
            firePoint = new Vector2(-1f, 0.2f);
        }
        dynamite.GetComponent<Rigidbody2D>().AddForce(firePoint * dynamiteSpeed, ForceMode2D.Impulse);
    }
}
