using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace ScanOut
{
   
    public class ScannerRange : MonoBehaviour
    {
        public Scan message = new Scan();
        public const double Pi = 3.14159;
        public GameObject[] Angles;
        public String input;
        double angle_min;        // start angle of the scan [rad]
        double angle_max;        // end angle of the scan [rad]
        double angle_increment;  // angular distance between measurements [rad]

        double time_increment;   // time between measurements [seconds] - if your scanner
                                 // is moving, this will be used in interpolating position
                                 // of 3d points
        double scan_time;        // time between scans [seconds]

        double range_min;        // minimum range value [m]
        double range_max;        // maximum range value [m]
        double[] ranges;         // range data [m] (Note: values < range_min or > range_max should be discarded)
        double[] intensities;    // intensity data [device-specific units].  If your
                                 // device does not provide intensities, please leave
                                 // the array empty.
        int HolographicResolution = 10;  // raycast resolution (or) no.of angles per raycast
        public double[] parseInput;
        //int once = 1;
        // Start is called before the first frame update
        void Start()
        {
            //init values sample
            angle_max = ToDegrees(-1.57079994678);
            angle_min = ToDegrees(1.57079994678);
            angle_increment = ToDegrees(0.00436940183863);
            int angleCount = (int)((angle_max - angle_min) / angle_increment);



            Angles = new GameObject[270];
            GameObject resource = new GameObject();
            GameObject Scanner = GameObject.Find("Kanji Event");
            input = "2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0," +
                "2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0," +
                "2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0," +
                "2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,3.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0," +
                "2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0," +
                "2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0," +
                "2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,3.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0," +
                "2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0," +
                "2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0," +
                "2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0,2.0," +
                "2.0,2.0,2.0,2.0";
            string[] a_temp = input.Split(',');
            ranges = a_temp.Select(s => Double.Parse(s)).ToArray();
            //parseInput = Array.ConvertAll(input.Split(','), Double.Parse);
            Debug.Log(ranges.Length);
            int i;
            double sum = 0;
            double avg = 0;
            resource = Resources.Load("Angle", typeof(GameObject)) as GameObject;
            for (i = 0; i < 180; i++)
            {
                if (i % HolographicResolution == 0 && i != 0)
                {
                    avg = sum / HolographicResolution;
                    Vector3 position = new Vector3(0, 0, 0);
                    Angles[i] = Instantiate(resource, position, Quaternion.Euler(0, 0, i));
                    Angles[i].transform.localScale = new Vector3((float)0.1, (float)(0.1), (float)1);
                    Angles[i].transform.SetParent(Scanner.transform, false);
                    sum = 0;
                }
                sum += ranges[i];

            }


        }

        // Update is called once per frame
        void Update()
        {


            //method to read int


        }

        double ToDegrees(double Radians)
        {
            return (Radians / Pi) * 180.0;
        }

    }
}
