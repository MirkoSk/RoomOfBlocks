using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeSelector : MonoBehaviour
{
    [SerializeField] Color emissiveColor = new Color();
    [SerializeField] CubeController blueCube = null;

    public CubeController BlueCube { get { return blueCube; } }



    void Awake()
    {
        
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100f))
        {
            CubeController hitCube = hit.transform.GetComponentInParent<CubeController>();
            if (hitCube != null)
            {
                if (blueCube != null)
                {
                    blueCube.GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissiveColor", Color.black);
                    blueCube.GetComponentInChildren<Light>().enabled = false;
                }

                blueCube = hitCube;
                blueCube.GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissiveColor", emissiveColor);
                blueCube.GetComponentInChildren<Light>().enabled = true;
            }
        }
    }
}
