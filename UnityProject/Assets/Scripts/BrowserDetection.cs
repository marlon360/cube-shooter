using System.Runtime.InteropServices;
using UnityEngine;

public class BrowserDetection : MonoBehaviour {

    [DllImport ("__Internal")]
    private static extern bool isSafari ();

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
    }

}