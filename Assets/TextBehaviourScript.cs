using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextBehaviourScript : MonoBehaviour
{
    public Text SocketStatus;
    public int temp;
    // Start is called before the first frame update
    void Start()
    {
        SocketStatus = GameObject.Find("SocketText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
            if (SocketReceiver.SocketStatus == 1)
            {
                temp = 1;
                SocketStatus.text = "Socket Status: New Data";
                SocketStatus.color = new Color(0.0f / 255.0f, 255.0f / 255.0f, 65.0f / 255.0f);
            //Debug.Log("Changed color to green");
            }
            else
            {
            if (temp == 1)
            {
                SocketStatus.text = "Socket Status: Connected";
                SocketStatus.color = new Color(50.0f / 255.0f, 255.0f / 255.0f, 115.0f / 255.0f);
            }
            //Debug.Log("Changed color to red");
        }
        
    }
}
