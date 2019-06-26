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
        Debug.Log("@@@@@@@@start executed");
    }

    public void Slider_Changed(float newValue)
    {
        Vector3 localScale = cylinder.transform.localScale;
        localScale.x = newValue;
        localScale.z = newValue;
        cylinder.transform.localScale = localScale;
    }

    public void Button_Click()
    {
        Debug.Log("Hello, World!");
        cylinderD.transform.localScale = cylinder.transform.localScale;
        cylinderD.transform.localPosition = cylinder.transform.localPosition;
    }

    public void Button_String(string msg)
    {
        buttonText.text = msg;
    }

    public void Toggle_Changed(bool newValue)
    {
        cube.SetActive(newValue);
        slider.interactable = newValue;
    }

    public void Text_Changed(string newText)
    {
        float temp = float.Parse(newText);
        cube.transform.localScale = new Vector3(temp, temp, temp);
    }

    public void Track_Click()
    {
        Debug.Log("Track clicked");
        if (startTrack == false)
        {
            track.text = "click to fix again";
            startTrack = true;
        }
        
    }
}
