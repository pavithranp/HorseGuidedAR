using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderModifier : MonoBehaviour
{
    public double cylinderRadius = 0.5;
    public double cylinderHeight = 2.0;
    public CapsuleCollider cylinder;
    // Start is called before the first frame update
    void Start()
    {
        cylinder = GameObject.Find("Cylinder").GetComponent<CapsuleCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        if (SocketReceiver.SocketStatus == 1)
        {
            var numbers = SocketReceiver.input.Split(',');
            if (double.TryParse(numbers[0], out cylinderRadius) && double.TryParse(numbers[1], out cylinderHeight))
            {
                Debug.Log("parse success" + cylinderRadius + "," + cylinderHeight);
                Vector3 localScale = cylinder.transform.localScale;
                localScale.x = (float)cylinderRadius;
                localScale.z = (float)cylinderRadius;
                localScale.y = (float)cylinderHeight;
                Vector3 localPosition = cylinder.transform.localPosition;
                localPosition.z = (float)cylinderHeight;
                cylinder.transform.localScale = localScale;
                cylinder.transform.localPosition = localPosition;
                //cylinder.height = (float)cylinderHeight;
                //cylinder.radius = (float)cylinderRadius;
                // you know that the parsing attempt
                // was successful
                SocketReceiver.SocketStatus = 0;
            }
            else
            {
                Debug.Log("parse fail");
            }
            
        }
    }
}
