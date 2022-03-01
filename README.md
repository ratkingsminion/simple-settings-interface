# ssi
Simple settings interface for creating configurable options in Unity/C# games

Usage (Unity):

```C#
  [RequireComponent(typeof(Camera))]
  public class ConfigurableFieldOfView : MonoBehaviour {

    void Start() {
      // requires a Setting asset with the ID "CAM FOV" in the project!
      SSI.Settings.AddListener("CAM FOV", OnCamFovChanged);
    }
    
    void OnDestroy() {
      SSI.Settings.RemoveListener("CAM FOV", OnCamFovChanged);
    }
    
    void OnCamFovChanged(SSI.Setting setting) {
      fov = setting.GetNumber();
      GetComponent<Camera>().fieldOfView = setting.GetNumber();
    }
	
  }
```
