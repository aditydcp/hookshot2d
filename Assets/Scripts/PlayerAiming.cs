using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    public Camera Camera;
    public Transform GunPivotPoint;

    void Start()
    {
        
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        RotateGun();
        FlipPlayer();
    }

    private void RotateGun()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(GunPivotPoint.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        GunPivotPoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void FlipPlayer()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // flips the character
        var scale = transform.localScale;
        transform.localScale = new Vector3(
            (mousePosition.x - transform.position.x) > 0 ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x), 
            scale.y, 
            scale.z
        );
    }
}
