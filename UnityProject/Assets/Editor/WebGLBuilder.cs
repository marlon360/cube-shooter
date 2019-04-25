using UnityEditor;
public class WebGLBuilder {
    [MenuItem("Build/Build WebGL")]
    static void build() {

        // Place all your scenes here
        string[] scenes = {"Assets/main.unity"};

        string pathToDeploy = "builds/WebGLversion";       

        BuildPipeline.BuildPlayer(scenes, pathToDeploy, BuildTarget.WebGL, BuildOptions.None);      
    }
}