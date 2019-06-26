using System;
using UnityEngine;

namespace HoloToolkit.Unity.InputModule.Tests
{
    /// <summary>
    /// This class implements IInputClickHandler to handle the tap gesture.
    /// It increases the scale of the object when tapped.
    /// </summary>
    public class TapResponder : MonoBehaviour, IInputClickHandler
    {
        public void OnInputClicked(InputClickedEventData eventData)
        {
            if (ARUWPController.ToggleTap == false)
                ARUWPController.ToggleTap = true;
            else
                ARUWPController.ToggleTap = false;
           
        }
    }
}