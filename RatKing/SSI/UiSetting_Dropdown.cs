using UnityEngine;
using UnityEngine.UI;

namespace RatKing {

	public class UiSetting_Dropdown : UiSetting {
		[SerializeField] TMPro.TMP_Dropdown uiDropdown;

		protected override void OnValidate() {
			base.OnValidate();
			if (uiDropdown == null) { uiDropdown = GetComponentInChildren<TMPro.TMP_Dropdown>(); }
		}

		//

		protected override void Start() {
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

		//

		protected override void OnChange(Setting setting) {
			uiDropdown.value = (int)setting.GetNumber();
		}
	}

}
