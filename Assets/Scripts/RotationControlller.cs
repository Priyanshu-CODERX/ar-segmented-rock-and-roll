using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationControlller : MonoBehaviour
{
    public Vector3 rotation;

    private void Update()
    {
        gameObject.transform.Rotate(rotation * Time.deltaTime);
    }
}
