using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeSelector : MonoBehaviour
{
    [SerializeField] Color emissiveColor = new Color();
    [SerializeField] float lightIntensity = 1.59f;

    [Space]
    [SerializeField] float tweenDuration = 2f;
    [SerializeField] float tweenFadeOutMultiplier = 2f;
    [SerializeField] Ease tweenEaseType = Ease.InOutQuad;

    [Space]
    [SerializeField] CubeController blueCube = null;
    [SerializeField] CubeManager cubeManager = null;

    CubeController previousTarget = null;

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
            if (hitCube != null && hitCube != previousTarget)
            {
                if (hitCube != blueCube)
                {
                    // Light up new target
                    hitCube.GetComponentInChildren<MeshRenderer>().material.DOColor(emissiveColor, "_EmissiveColor", tweenDuration).SetId(hitCube).SetEase(tweenEaseType);

                    Light light = hitCube.GetComponentInChildren<Light>();
                    light.enabled = true;
                    light.DOIntensity(0f, tweenDuration).From().SetId(hitCube).SetEase(tweenEaseType)
                        .OnComplete(() =>
                        {
                            AssignBlueCube(hitCube);
                        });
                }


                // Unlight the previous target
                if (previousTarget != null && previousTarget != blueCube)
                {
                    List<Tween> tweens = DOTween.TweensById(previousTarget);
                    float tweenDuration = tweens[0].Elapsed();
                    DOTween.Kill(previousTarget);

                    previousTarget.GetComponentInChildren<MeshRenderer>().material.DOColor(Color.black, "_EmissiveColor", tweenDuration * tweenFadeOutMultiplier).SetId(previousTarget).SetEase(Ease.InSine);
                    Light light = previousTarget.GetComponentInChildren<Light>();
                    light.DOIntensity(0f, tweenDuration * tweenFadeOutMultiplier).From().SetId(previousTarget).SetEase(Ease.InSine)
                        .OnComplete(() =>
                        {
                            light.enabled = false;
                            light.intensity = lightIntensity;
                        });
                }


                // Update reference to currentTarget
                previousTarget = hitCube;
            }
        }
    }

    void AssignBlueCube(CubeController cube)
    {
        if (blueCube != null)
        {
            blueCube.GetComponentInChildren<MeshRenderer>().material.DOColor(Color.black, "_EmissiveColor", tweenDuration * tweenFadeOutMultiplier).SetId(blueCube).SetEase(tweenEaseType);
            Light light = blueCube.GetComponentInChildren<Light>();
            light.DOIntensity(0f, tweenDuration * tweenFadeOutMultiplier).SetId(blueCube).SetEase(tweenEaseType)
                .OnComplete(() =>
                {
                    light.enabled = false;
                    light.intensity = lightIntensity;
                });
        }

        cube.GetComponentInChildren<ParticleSystem>().Play();
        blueCube = cube;
        if (cubeManager != null) cubeManager.MoveCubes();
    }
}
