using UnityEngine;
using UnityEngine.UI;

namespace RatKing.SSI {
	
	[ExecuteInEditMode]
	public class UiSetting_Toggle : UiSetting {
		[SerializeField] Toggle uiToggle = null;
		
#if UNITY_EDITOR
		protected override void OnValidate() {
			base.OnValidate();
			if (uiToggle == null) { uiToggle = GetComponentInChildren<Toggle>(); }
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
