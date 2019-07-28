using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public bool edgeCube;
    Vector3 origin = new Vector3();


    void Awake()
    {
        origin = transform.localPosition;
    }



    public void MoveCube(float offset)
    {
        if (SpaceAboveCubeEmtpy())
        {
            if (!edgeCube)
            {
                Vector3 newPosition = origin;
                newPosition.y = origin.y + offset;
                transform.localPosition = newPosition;
            }
            else
            {
                Vector3 newPosition = origin;
                newPosition.y = origin.y + Mathf.Abs(offset);
                transform.localPosition = newPosition;
            }
        }
        else
        {
            transform.localPosition = origin;
        }
    }

    bool SpaceAboveCubeEmtpy()
    {
        bool spaceEmtpy = !Physics.BoxCast(transform.position, transform.localScale/2.19f, transform.up, transform.rotation, 1f);

        return spaceEmtpy;
    }
}
