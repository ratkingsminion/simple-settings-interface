using UnityEngine;
using UnityEngine.UI;

namespace RatKing.SSI {
	
	[ExecuteInEditMode]
	public class UiSetting_Toggle : UiSetting {
		[SerializeField] Toggle uiToggle;

		protected override void OnValidate() {
			base.OnValidate();
			if (uiToggle == null) { uiToggle = GetComponentInChildren<Toggle>(); }
		}

		//

		protected override void Start() {
			base.Start();
			if (setting != null) {
				uiToggle.onValueChanged.AddListener(v => {
					setting.SetValue(v);
				});
				var v = setting.Get<bool>();
				uiToggle.isOn = v;
				uiToggle.onValueChanged?.Invoke(v);
			}
		}

#if UNITY_EDITOR
		void Update() {
			if (!Application.isPlaying) {
				if (uiLabel != null) {
					if (setting != null) { uiLabel.text = setting.ID; }
					else { uiLabel.text = "Missing Setting definition"; }
				}
			}
		}
#endif

		//

		protected override void OnChange(Setting setting) {
			uiToggle.isOn = setting.Get<bool>();
		}
	}

}
