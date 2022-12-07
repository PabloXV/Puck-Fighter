using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PuckController))]
public class InputController: MonoBehaviour
{
    protected PuckController puck;
    protected Vector2 trajectory;

    private void Start()
    {
        puck = GetComponent<PuckController>();
    }

    public virtual void TakeTurn() {}
}
