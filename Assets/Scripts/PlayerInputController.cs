using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : InputController
{
    [SerializeField] private float inputScale = 10;

    public override void TakeTurn()
    {
        // Debug.Log("Player's turn!");
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin - transform.position;
        trajectory = -inputScale * new Vector2(mousePosition.x, mousePosition.y);

        if (trajectory.magnitude > 1)
        {
            trajectory = trajectory.normalized;
        }

        puck.UpdateVelocityIndicator(trajectory);
    }

    private void OnMouseUp()
    {
        puck.Launch(trajectory);
    }
}
