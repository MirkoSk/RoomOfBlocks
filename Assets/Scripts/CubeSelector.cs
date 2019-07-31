using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeSelector : MonoBehaviour
{
    [SerializeField] Color emissiveColor = new Color();
    [SerializeField] float tweenDuration = 2f;
    [SerializeField] CubeController blueCube = null;

    CubeController currentTarget = null;

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

            // Target switched
            if (hitCube != null && hitCube != currentTarget)
            {
                if (hitCube != blueCube)
                {
                    // Light up new target
                    hitCube.GetComponentInChildren<MeshRenderer>().material.DOColor(emissiveColor, "_EmissiveColor", tweenDuration).SetId(hitCube);

                    Light light = hitCube.GetComponentInChildren<Light>();
                    float lightIntensity = light.intensity;
                    light.enabled = true;
                    light.DOIntensity(0f, tweenDuration).From().SetId(hitCube)
                        .OnComplete(() =>
                        {
                            AssignBlueCube(hitCube);
                        })
                        .OnRewind(() =>
                        {
                            if (light.intensity == 0f)
                            {
                                light.enabled = false;
                                light.intensity = lightIntensity;
                            }
                        });
                }


                // Unlight the previous target
                if (currentTarget != null)
                {
                    DOTween.PlayBackwards(currentTarget);
                }


                // Update reference to currentTarget
                currentTarget = hitCube;
            }
        }
    }

    void AssignBlueCube(CubeController cube)
    {
        if (blueCube != null)
        {
            blueCube.GetComponentInChildren<MeshRenderer>().material.DOColor(Color.black, "_EmissiveColor", tweenDuration).SetId(blueCube);
            Light light = blueCube.GetComponentInChildren<Light>();
            float lightIntensity = light.intensity;
            light.DOIntensity(0f, tweenDuration).SetId(blueCube)
                .OnComplete(() =>
                {
                    light.enabled = false;
                    light.intensity = lightIntensity;
                });
        }

        blueCube = cube;
    }
}
