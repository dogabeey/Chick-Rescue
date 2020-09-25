using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCurve : MonoBehaviour
{
    [SerializeField] AnimationCurve curveX;
    [SerializeField] AnimationCurve curveY;
    [SerializeField] AnimationCurve curveZ;

    float width = 180;
    Vector3 startingAngle;

    // Start is called before the first frame update
    void Start()
    {
        startingAngle = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(
            curveX.Evaluate(Time.timeSinceLevelLoad) * width,
            curveY.Evaluate(Time.timeSinceLevelLoad) * width,
            curveZ.Evaluate(Time.timeSinceLevelLoad) * width
            ) - startingAngle ;
    }
}
