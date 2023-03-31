using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    Transform moving = null;
    Vector3 offset;
    [SerializeField] LayerMask movableLayer;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, float.PositiveInfinity, movableLayer);

            if (hit)
            {
                moving = this.transform;
                offset = moving.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            moving = null;
        }

        if(moving != null)
        {
            moving.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

}
