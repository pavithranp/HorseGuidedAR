using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;


/* This code is used to instantiate Plane objects for every angle to represent the protection region in the real world
 * A plane prefab named "Angle" is present in the Resources folder in Asset directory 
 * The prefab is instantiated multiple times in a loop to create the effect of protection field on this Game Object.
 * 
 *Cylinders can be thought of, as the center for these projections
 * 
 */
namespace ScanOut
{

    public class ScannerRange : MonoBehaviour
    {
        public Scan message = new Scan();
        public const double Pi = 3.14159;
        public GameObject[] Angles;
        public static String values;
       
        public double[] parseInput;
        //int once = 1;
        // Start is called before the first frame update
        void Start()
        {
            //init values sample
           

            Angles = new GameObject[270];
            GameObject resource = new GameObject();
            GameObject Scanner = GameObject.Find("Kanji Event");
            if(SocketReceiver.SocketStatus == 0)//initial value for the array before data from ROS
                values = "0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0";
                //values = "0.0,0.0,0.0,0.0,1.383,1.421,1.509,1.647,1.598,1.283,1.101,0.991,0.928,0.81,0.836,0.894,0.993,1.159,1.446,2.006,3.291,3.28,2.877,0.0,0.0,0.0,0.0";
            string[] a_temp = values.Split(',');
            parseInput = a_temp.Select(s => Double.Parse(s)).ToArray();


            Debug.Log(parseInput.Length);
            resource = Resources.Load("Angle", typeof(GameObject)) as GameObject;
            for (int i = 0; i < 27; i++)
            {
                    Vector3 position = new Vector3(0, 0, 0);
                    Angles[i] = Instantiate(resource, position, Quaternion.Euler(0, 0, ((27-i)*10 - 225))); // Used to align unity coordinates with real world values from ROS
                    Angles[i].transform.localScale = new Vector3((float)parseInput[i], (float)(0.1), (float)0.5);
                    Angles[i].transform.SetParent(Scanner.transform, false);
                
            }


        }

        // Update is called once per frame
        void Update()
        {
           
                values = SocketReceiver.input; // The value recieved is a String in the Socket Reciever code
                string[] a_temp = values.Split(','); // need to convert it to Array for further processing
            parseInput = a_temp.Select(s => Double.Parse(s)).ToArray();
                GameObject Scanner = GameObject.Find("Kanji Event");
                Debug.Log("The length is " + parseInput.Length); 
                if (parseInput.Length == 27) // The array must contain 27 Values, one value for every 10 degrees :. 27*10 = 270 degrees
                {

                    for (int i = 0; i < 27; i++)
                    {
 
                        Angles[i].transform.localScale = new Vector3((float)parseInput[i], (float)(0.1), (float)0.5);
                        Angles[i].transform.SetParent(Scanner.transform, false);

                    }

                }
                else
                {
                    Debug.Log("The length is " + parseInput.Length); //Debugging purpose
                }
                //method to read int
            
        }

        double ToDegrees(double Radians) 
        {
            return (Radians / Pi) * 180.0;
        }

    }
}