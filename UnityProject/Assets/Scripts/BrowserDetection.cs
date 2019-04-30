using System.Runtime.InteropServices;
using UnityEngine;

public class BrowserDetection : MonoBehaviour {

    [DllImport ("__Internal")]
    private static extern bool isSafari ();

    void Start () {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
            ControllerInputManager.UseWindows ();
        }
        if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor) {
            ControllerInputManager.UseMac ();
        }
        if (Application.platform == RuntimePlatform.WebGLPlayer) {

            if (isSafari ()) {
                ControllerInputManager.UseMac ();
                Debug.Log ("Safari detected");
            } else {
                ControllerInputManager.UseWindows ();
                Debug.Log ("No Safari detected");
            }
        }
    }

}