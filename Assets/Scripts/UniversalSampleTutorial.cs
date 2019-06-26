using System;
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
public class UniversalSampleTutorial : MonoBehaviour
{
    public String _input = "Waiting";
    public Text SocketStatus;

#if !UNITY_EDITOR
        StreamSocket socket;
        StreamSocketListener listener;
        String port;
        String message;
#endif

    // Use this for initialization
    void Start()
    {
        SocketStatus = GameObject.Find("SocketText").GetComponent<Text>();

#if !UNITY_EDITOR
        listener = new StreamSocketListener();
        port = "9090";
        listener.ConnectionReceived += Listener_ConnectionReceived;
        listener.Control.KeepAlive = false;

        Listener_Start();
#endif
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

        try
        {
          /*  while (true) {

                     using (var dw = new DataWriter(args.Socket.OutputStream))
                    {
                        dw.WriteString("Hello There");
                        await dw.StoreAsync();
                        dw.DetachStream();
                    }  */

                    using (var dr = new DataReader(args.Socket.InputStream))
                    {
                        dr.InputStreamOptions = InputStreamOptions.Partial;
                        await dr.LoadAsync(12);
                        var receivedStrings = "";
                        while (dr.UnconsumedBufferLength > 0)
                            {
                                uint bytesToRead = dr.ReadUInt32();
                                receivedStrings += dr.ReadString(bytesToRead) + "\n";
                                }
            var input = receivedStrings; 
                                Debug.Log("received: " + input);
                                _input = input;
                        
                        SocketStatus.text = "Socket Status: Received Data";
                        SocketStatus.color = new Color(0.0f/255.0f, 255.0f/255.0f, 65.0f/255.0f);

                    }
            //}
        }
        catch (Exception e)
        {
            Debug.Log("disconnected!!!!!!!! " + e);
        }

   }
#endif
}