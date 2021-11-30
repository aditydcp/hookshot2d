using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform FiringPoint;
    public Rigidbody2D PlayerRigidbody;
    public GameObject BulletPrefab;
    public GameObject GrapplingHookPrefab;

    public float BulletForce = 10f;
    public float GrapplingHookForce = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        var grapplingHook = Instantiate(GrapplingHookPrefab, FiringPoint.position, FiringPoint.rotation);

        grapplingHook.GetComponent<Rigidbody2D>().AddForce(FiringPoint.up * GrapplingHookForce, ForceMode2D.Impulse);

        grapplingHook.GetComponent<PlayerHook>().Attach(PlayerRigidbody);

        /*var bullet = Instantiate(BulletPrefab, FiringPoint.position, FiringPoint.rotation);

        var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        bulletRigidbody.AddForce(FiringPoint.up * BulletForce, ForceMode2D.Impulse);*/
    }
}
