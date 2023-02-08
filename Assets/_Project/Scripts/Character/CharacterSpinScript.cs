using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpinScript : MonoBehaviour
{
    public float _angleOffset = 0f;
    public float _speed = 0f;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, _angleOffset);
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
    }
}
