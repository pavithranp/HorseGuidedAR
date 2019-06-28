using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ViewModel : MonoBehaviour
{
    public Text buttonText;
    public GameObject cube;
    public Slider slider;
    public CapsuleCollider cylinder, cylinderD;
    public Text track;
    public static bool startTrack = false;
    private void Start()
    {
        track = GameObject.Find("Track").GetComponentInChildren<Text>();
        //cylinder = GameObject.Find("Cylinder").GetComponent<CapsuleCollider>();
        cylinderD = GameObject.Find("Cylinder (1)").GetComponent<CapsuleCollider>();
        
    }

    // To change the cylinder radius based on slider
    public void Slider_Changed(float newValue)
    {
        Vector3 localScale = cylinder.transform.localScale;
        localScale.x = newValue;
        localScale.z = newValue;
        SocketReceiver.radius = newValue;
        cylinder.transform.localScale = localScale;
    }

    // To change the inner cylinder to outer cylinder dimensions
    public void Button_Click()
    {

        cylinderD.transform.localScale = cylinder.transform.localScale;
        cylinderD.transform.localPosition = cylinder.transform.localPosition;
    }

    // To change strings in the Button
    public void Button_String(string msg)
    {
        buttonText.text = msg;
    }

    //unused
    public void Toggle_Changed(bool newValue)
    {
        cube.SetActive(newValue);
        slider.interactable = newValue;
    }
    // To change strings in another Button
    public void Text_Changed(string newText)
    {
        float temp = float.Parse(newText);
        cube.transform.localScale = new Vector3(temp, temp, temp);
    }
    //To track the Marker again, the variable is modified in the ARUWPController.cs
    public void Track_Click()
    {
       
        if (startTrack == false)
        {
            track.text = "click to fix again";
            startTrack = true;
        }
        
    }
}
