using UnityEngine;
using UnityEngine.UI;

namespace RatKing {

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

		//

		protected override void OnChange(Setting setting) {
			uiToggle.isOn = setting.Get<bool>();
		}
	}

}
