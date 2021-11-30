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
    private LineRenderer _lineRenderer;

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

            _lineRenderer.SetPositions(new Vector3[] { hookPosition, _attachmentPoint.position });

            var pullDirection = (hookPosition - attacheePosition).normalized;

            _attachedRigidbody.AddForce(pullDirection * Time.deltaTime * PullStrength, ForceMode2D.Impulse);
        }   
    }

    public void Attach(Rigidbody2D rigidbody, Transform attachmentPoint)
    {
        _attachedRigidbody = rigidbody;
        _attachmentPoint = attachmentPoint;

        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.startWidth = 0.05f;
        _lineRenderer.endWidth = 0.05f;
        _lineRenderer.material.color = Color.black;
        _lineRenderer.positionCount = 2;
    }

    public void Detach()
    {
        _isPulling = false;
        _attachedRigidbody = null;

        _lineRenderer.enabled = false;

        Rigidbody.constraints = RigidbodyConstraints2D.None;

        Destroy(gameObject, 10f);
    }
}