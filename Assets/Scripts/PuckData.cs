using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckData
{
    

    public float size;
    public float mass;
    public float speed;
    public Sprite icon;

    public PuckData(float _size, float _mass, float _speed, Sprite _icon)
    {
        size = _size;
        mass = _mass;
        speed = _speed;
        icon = _icon;
    }

    public PuckData(float _size, float _mass, float _speed, int _icon)
    {
        size = _size;
        mass = _mass;
        speed = _speed;
        icon = EntitySpawner.instance.icons[_icon];
    }

    public PuckData()
    {
        size = Random.Range(0.5f, 1.5f);
        mass = Random.Range(0.25f, 4f);
        speed = Random.Range(10f, 30f);
        icon = EntitySpawner.instance.icons[Random.Range(0, EntitySpawner.instance.icons.Count)];
    }
}
