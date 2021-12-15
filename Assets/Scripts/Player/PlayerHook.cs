using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHook : MonoBehaviour
{
    public Rigidbody2D Rigidbody;
    public float PullStrength = 1;

    private Rigidbody2D _attachedRigidbody = null;
    private Transform _attachmentPoint;
    private bool _isPulling = false;
    public LineRenderer LineRenderer;

    private GameObject _attachedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

            _attachedObject = collision.gameObject;

            _isPulling = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPulling && _attachedObject == null)
        {
            Detach();
            return;
        }

        if (_isPulling && _attachedRigidbody != null)
        {
            Vector2 hookPosition = transform.position;
            Vector2 attacheePosition = _attachedRigidbody.position;

            LineRenderer.SetPositions(new Vector3[] { hookPosition, _attachmentPoint.position });

            var pullDirection = (hookPosition - attacheePosition).normalized;

            _attachedRigidbody.AddForce(pullDirection.normalized * Time.deltaTime * PullStrength, ForceMode2D.Impulse);
        }   
    }

    public void Attach(Rigidbody2D rigidbody, Transform attachmentPoint)
    {
        _attachedRigidbody = rigidbody;
        _attachmentPoint = attachmentPoint;
    }

    public void Detach()
    {
        _isPulling = false;
        _attachedRigidbody = null;

        LineRenderer.enabled = false;

        Rigidbody.constraints = RigidbodyConstraints2D.None;

        Destroy(gameObject, 10f);
    }
}
