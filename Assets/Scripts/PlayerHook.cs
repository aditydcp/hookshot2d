using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHook : MonoBehaviour
{
    public Rigidbody2D Rigidbody;
    public float PullStrength = 1;

    private Rigidbody2D _attachedRigidbody = null;
    private bool _isPulling = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

            _isPulling = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPulling && _attachedRigidbody != null)
        {
            Vector2 hookPosition = transform.position;
            Vector2 attacheePosition = _attachedRigidbody.position;

            var pullDirection = (hookPosition - attacheePosition).normalized;

            _attachedRigidbody.AddForce(pullDirection * PullStrength, ForceMode2D.Impulse);
        }   
    }

    public void Attach(Rigidbody2D rigidbody)
    {
        _attachedRigidbody = rigidbody;
    }
}
