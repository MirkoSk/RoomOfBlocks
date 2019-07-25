using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    Vector3 origin = new Vector3();


    void Awake()
    {
        origin = transform.localPosition;
    }



    public void MoveCube(float offset)
    {
        Vector3 newPosition = origin;
        newPosition.y = origin.y + offset;
        transform.localPosition = newPosition;
    }
}
