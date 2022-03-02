using UnityEngine;
using UnityEngine.UI;

namespace RatKing.SSI {
	
	[ExecuteInEditMode]
	public class UiSetting_Dropdown : UiSetting {
		[SerializeField] TMPro.TMP_Dropdown uiDropdown;

		protected override void OnValidate() {
			base.OnValidate();
			if (uiDropdown == null) { uiDropdown = GetComponentInChildren<TMPro.TMP_Dropdown>(); }
		}

		//

		protected override void Start() {
#if UNITY_EDITOR
			if (!Application.isPlaying) { return; }
#endif
			base.Start();
			if (setting != null) {
				uiDropdown.onValueChanged.AddListener(v => {
					setting.SetValue(v);
				});
				var v = (int)setting.GetNumber();
				uiDropdown.value = v;
				uiDropdown.onValueChanged?.Invoke(v);
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
			uiDropdown.value = (int)setting.GetNumber();
		}
	}

}
