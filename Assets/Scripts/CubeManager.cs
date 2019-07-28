using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [MinMaxRange(-1f, 1f)]
    [SerializeField] RangedFloat offsetAmount = new RangedFloat();

    CubeController[] cubes;



    void Start()
    {
        cubes = GameObject.FindObjectsOfType<CubeController>();

        MoveCubes();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Fire1")) MoveCubes();
    }



    void MoveCubes()
    {
        foreach (CubeController cube in cubes)
        {
            cube.MoveCube(Random.Range(offsetAmount.minValue, offsetAmount.maxValue));
        }
    }
}
