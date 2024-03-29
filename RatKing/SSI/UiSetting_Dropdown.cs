using UnityEngine;

namespace RatKing.SSI {
	
	[ExecuteInEditMode]
	public class UiSetting_Dropdown : UiSetting {
		[SerializeField] TMPro.TMP_Dropdown uiDropdown = null;

#if UNITY_EDITOR
		protected override void OnValidate() {
			base.OnValidate();
			if (uiDropdown == null) { uiDropdown = GetComponentInChildren<TMPro.TMP_Dropdown>(); }
		}
#endif

		//

		protected override void Start() {
#if UNITY_EDITOR
			if (!Application.isPlaying) { return; }
#endif
			base.Start();
			if (setting != null) {
				ChangeLabel();
				uiDropdown.onValueChanged.AddListener(v => {
					setting.SetValue(v);
				});
				var v = (int)setting.GetNumber();
				uiDropdown.value = v;
				uiDropdown.onValueChanged?.Invoke(v);
			}
		}

		//

		protected override void OnChange(Setting setting) {
			uiDropdown.value = (int)setting.GetNumber();
		}
	}

}
