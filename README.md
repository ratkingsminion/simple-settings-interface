# ssi
Simple settings interface for creating configurable options in Unity/C# games

Usage:

```C#
  [RequireComponent(typeof(Camera))]
  public class ConfigurableFieldOfView : MonoBehaviour {
  
    void Start() {
      // requires Setting assets with the IDs "CAM FOV" and "CAM COLOR" inside Resources/Settings!
      SSI.Settings.AddListener("CAM FOV", OnCamFovChanged);
      SSI.Settings.AddListener("CAM COLOR", OnCamColorChanged);
    }
    
    void OnDestroy() {
      SSI.Settings.RemoveListener("CAM FOV", OnCamFovChanged);
      SSI.Settings.RemoveListener("CAM COLOR", OnCamColorChanged);
    }
    
    void OnCamFovChanged(SSI.Setting setting) {
      GetComponent<Camera>().fieldOfView = setting.GetNumber();
    }
    
    void OnCamColorChanged(SSI.Setting setting) {
      Camera.main.backgroundColor = setting.Get<bool>() ? Color.green : Color.black;
    }
	
  }
```

OnCamFovChanged() and OnCamColorChanged() get automatically called on Start and also anytime the settings CAM FOV and CAM COLOR get changed from the outside (for example via UiSetting_Slider and UiSetting_Toggle).
