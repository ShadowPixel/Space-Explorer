using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _objectToFollow = null;

    Vector3 _objectOffset;

    private void Awake()
    {
        // create an offset and keep that offset
        _objectOffset = this.transform.position - _objectToFollow.position;
    }

    // happens after update so that camera always moves last
    private void LateUpdate()
    {
        // apply the offset every frame
        this.transform.position = _objectToFollow.position + _objectOffset;
    }
}
