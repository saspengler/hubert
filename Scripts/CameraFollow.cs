using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float xShift;
    public float yShift;
    public GameObject ObjectToFollow;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(ObjectToFollow.transform.position.x + xShift, ObjectToFollow.transform.position.y + yShift, transform.position.z);
    }
}
