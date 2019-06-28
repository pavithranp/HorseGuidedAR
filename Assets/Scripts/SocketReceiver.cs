using System;
//using System.Timers; some issue with unity
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if !UNITY_EDITOR
    using Windows.Networking;
    using Windows.Networking.Sockets;
    using Windows.Storage.Streams;
#endif

//Able to act as a reciever 
public class SocketReceiver : MonoBehaviour
{
    public static String input = "0.5,0.5";
	public String _port = "9090";
    public static float radius;
    public static int SocketStatus = 0;
    //private static System.Timers.Timer aTimer;
    public CapsuleCollider cylinder;
#if !UNITY_EDITOR
        StreamSocket socket;
        StreamSocketListener listener;
        String port; //find IP from Network>Wifi>Additional options
        String message;
#endif
    /* private static void SetTimer()
    {
        // Create a timer with a two second interval.
        aTimer = new System.Timers.Timer(2000);
        // Hook up the Elapsed event for the timer. 
        aTimer.Elapsed += OnEvery2Seconds;
        aTimer.AutoReset = true;
        aTimer.Enabled = true;
    } */
    // Use this for initialization
    void Start()
    {
        //StartCoroutine("MyEvent");
#if !UNITY_EDITOR
        listener = new StreamSocketListener();
        port = _port;
        listener.ConnectionReceived += Listener_ConnectionReceived;
        listener.Control.KeepAlive = false;
        
        Listener_Start();
#endif
    }

     public static void setSocketStatus()
     {
         if (SocketStatus == 1)
         SocketStatus = 0;
     }



#if !UNITY_EDITOR
    private async void Listener_Start()
    {
        Debug.Log("Listener started");
        try
        {
            await listener.BindServiceNameAsync(port);
        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e.Message);
        }

        Debug.Log("Listening");
    }

    private async void Listener_ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
    {
        Debug.Log("Connection received");
        SocketStatus = 1;
        //Invoke("setSocketStatus", 5);
        try
        {
            while (true) {
				
				using (var dw = new DataWriter(args.Socket.OutputStream))
                    {
                        dw.WriteString(radius.ToString());
                        await dw.StoreAsync();
                        dw.DetachStream();
                    }  
				
				using (var dataread = new DataReader(args.Socket.InputStream))
				{
					dataread.InputStreamOptions = InputStreamOptions.Partial;
					await dataread.LoadAsync(200); //Need to make sure the data recieved is greater than 200 characters, add some zeroes to input string from ROS 
					input = dataread.ReadString(200);
					Debug.Log("received: " + input);
                               	//Ninput = input;
				}
            }
        }
        catch (Exception e)
        {
            //SocketStatus.text = "Socket Status: Disconnected";
            Debug.Log("disconnected!!!!!!!! " + e);
        }
   }
#endif
}