using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    public List<GameObject> entities = new List<GameObject>();
    public int currentTurn { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(instance);
        }
    }

    public void EndTurn()
    {
        if (currentTurn >= entities.Count - 1)
        {
            currentTurn = 0;
        }
        else
        {
            currentTurn += 1;
        }

        entities[TurnManager.instance.currentTurn].GetComponent<InputController>().TakeTurn();
    }
}
