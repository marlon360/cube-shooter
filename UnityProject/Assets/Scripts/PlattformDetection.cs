using System.Runtime.InteropServices;
using UnityEngine;

public class PlattformDetection : MonoBehaviour {

    public UI ui;

    [DllImport ("__Internal")]
    private static extern bool isSafari ();

    private bool isController;

    void Start () {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
            ControllerInputManager.UseWindows ();
        } else if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor) {
            ControllerInputManager.UseMac ();
        } else if (Application.platform == RuntimePlatform.WebGLPlayer) {

            if (isSafari ()) {
                ControllerInputManager.UseWebMac ();
                Debug.Log ("Safari detected");
            } else {
                ControllerInputManager.UseWindows ();
                Debug.Log ("No Safari detected");
            }
        }
        if (isOneGamepadConnected()) {
            InputManager.UseController ();
            ui.SetGamepadUI ();
            isController = true;
        } else {
            InputManager.UseKeyboard ();
            ui.SetKeyboardUI ();
            isController = false;
        }
        
        InvokeRepeating("CheckPlattform", 0.2f, 3f);
    }

    public void CheckPlattform () {
        if (isOneGamepadConnected() && !isController) {
            InputManager.UseController ();
            ui.SetGamepadUI ();
            isController = true;
        }
        if (!isOneGamepadConnected() && isController) {
            InputManager.UseKeyboard ();
            ui.SetKeyboardUI ();
            isController = false;
        }
    }

    bool isOneGamepadConnected () {

        bool connected = false;

        //Get Joystick Names
        string[] temp = Input.GetJoystickNames ();

        //Check whether array contains anything
        if (temp.Length > 0) {
            //Iterate over every element
            for (int i = 0; i < temp.Length; ++i) {
                //Check if the string is empty or not
                if (!string.IsNullOrEmpty (temp[i])) {
                    //Not empty, controller temp[i] is connected
                    connected = true;
                }
            }
        }
        return connected;
    }

}