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
        }
        else if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor) {
            ControllerInputManager.UseMac ();
        }
        else if (Application.platform == RuntimePlatform.WebGLPlayer) {

            if (isSafari ()) {
                ControllerInputManager.UseWebMac ();
                Debug.Log ("Safari detected");
            } else {
                ControllerInputManager.UseWindows ();
                Debug.Log ("No Safari detected");
            }
        }
        SetPlattform();
    }

    public void SetPlattform() {
        if (Input.GetJoystickNames().Length > 0) {
            InputManager.UseController();
            ui.SetGamepadUI();
            isController = true;
        } else {
            InputManager.UseKeyboard();
            ui.SetKeyboardUI();
            isController = false;
        }
    }

    void Update() {
        if (Input.GetJoystickNames().Length > 0 && !isController) {
            SetPlattform();
        }

        if (Input.GetJoystickNames().Length == 0 && isController) {
            SetPlattform();
        }
    }

}