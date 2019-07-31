using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [Space]
    [MinMaxRange(-1f, 1f)]
    [SerializeField] RangedFloat offsetAmount = new RangedFloat();
    [SerializeField] AnimationCurve offsetDistribution = new AnimationCurve();

    [Space]
    [SerializeField] CubeSelector cubeSelector = null;
    [MinMaxRange(-1f, 1f)]
    [SerializeField]
    RangedFloat blueCubeOffsetAmount = new RangedFloat();

    CubeController[] cubes;



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



    void MoveCubes()
    {
        foreach (CubeController cube in cubes)
        {
            if (cubeSelector != null && cube == cubeSelector.BlueCube) cube.MoveCube(Random.Range(blueCubeOffsetAmount.minValue, blueCubeOffsetAmount.maxValue));

            else
            {
                float offsetStrength = offsetDistribution.Evaluate(Random.Range(-1f, 1f));
                if (offsetStrength >= 0) offsetStrength *= offsetAmount.maxValue;
                else offsetStrength *= offsetAmount.minValue;

                cube.MoveCube(offsetStrength);
            }
        }
    }
}
