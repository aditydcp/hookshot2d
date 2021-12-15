using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    private Transform _target = null;
    
    public float Speed = 10f;
    public float RotationSpeed = 5f;
    public Rigidbody2D MissileRigidBody;
    public int SelfDestructDelaySeconds = 5;

    void Start()
    {
        AcquireTarget();
        Destroy(gameObject, SelfDestructDelaySeconds);

        FindObjectOfType<AudioManager>().Play("EnemyMissileFire");
        // FindObjectOfType<AudioManager>().Play("Explosion", SelfDestructDelaySeconds);
    }

    void FixedUpdate()
    {
        if (_target != null)
        {
            var turnDirection = (Vector2)_target.position - MissileRigidBody.position;

            var rotation = Vector3.Cross(turnDirection.normalized, transform.up).z;

            MissileRigidBody.angularVelocity = -rotation * RotationSpeed;
        }

        MissileRigidBody.velocity = transform.up * Speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerManager>();

        if (player != null)
        {
            player.TakeDamage(3);

            Destroy(gameObject);

            FindObjectOfType<AudioManager>().Play("Explosion");
        }
    }

    private void AcquireTarget()
    {
        var player = GameObject.Find("Player");
        _target = player.transform;
    }
}
