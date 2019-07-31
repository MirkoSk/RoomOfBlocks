using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeManager : MonoBehaviour
{
    [Space]
    [SerializeField] float tweenDuration = 2f;
    [MinMaxRange(-1f, 1f)]
    [SerializeField] RangedFloat offsetAmount = new RangedFloat();
    [SerializeField] AnimationCurve offsetDistribution = new AnimationCurve();
    [MinMaxRange(-1f, 1f)]
    [SerializeField]
    RangedFloat blueCubeOffsetAmount = new RangedFloat();

    [Header("References")]
    [SerializeField] CubeSelector cubeSelector = null;

    CubeController[] cubes;



    private void Awake()
    {
        DOTween.Init(recycleAllByDefault: true, logBehaviour: LogBehaviour.Default).SetCapacity(1000, 0);
        Cursor.visible = false;
    }

    void Start()
    {
        cubes = GameObject.FindObjectsOfType<CubeController>();

        MoveCubes();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Fire1"))
        {
            bool cubesIdle = true;
            foreach (CubeController cube in cubes)
            {
                if (!cube.Idle) cubesIdle = false;
            }

            if (cubesIdle == true) MoveCubes();
        }
    }



    public void MoveCubes()
    {
        foreach (CubeController cube in cubes)
        {
            if (cubeSelector != null && cube == cubeSelector.BlueCube) cube.MoveCube(Random.Range(blueCubeOffsetAmount.minValue, blueCubeOffsetAmount.maxValue), tweenDuration);

            else
            {
                float offsetStrength = offsetDistribution.Evaluate(Random.Range(-1f, 1f));
                if (offsetStrength >= 0) offsetStrength *= offsetAmount.maxValue;
                else offsetStrength *= offsetAmount.minValue;

                cube.MoveCube(offsetStrength, tweenDuration);
            }
        }
    }
}
