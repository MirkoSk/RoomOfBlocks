using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeController : MonoBehaviour
{
    public bool edgeCube;
    Vector3 origin = new Vector3();
    bool idle = true;

    public bool Idle { get { return idle; } }

    void Awake()
    {
        origin = transform.localPosition;
    }



    public void MoveCube(float offset, float duration)
    {
        idle = false;

        if (SpaceAboveCubeEmtpy())
        {
            if (!edgeCube)
            {
                Vector3 newPosition = origin;
                newPosition.y = origin.y + offset;
                transform.DOLocalMove(newPosition, duration).SetEase(Ease.InOutQuad).OnComplete(() => idle = true);
            }
            else
            {
                Vector3 newPosition = origin;
                newPosition.y = origin.y + Mathf.Abs(offset);
                transform.DOLocalMove(newPosition, duration).SetEase(Ease.InOutQuad).OnComplete(() => idle = true);
            }
        }
        else
        {
            transform.DOLocalMove(origin, duration).SetEase(Ease.InOutQuad).OnComplete(() => idle = true);
        }
    }

    bool SpaceAboveCubeEmtpy()
    {
        bool spaceEmtpy = !Physics.BoxCast(transform.position, transform.localScale/2.19f, transform.up, transform.rotation, 1f);

        return spaceEmtpy;
    }
}
