using UnityEngine;

namespace RatKing.SSI {
	
	[ExecuteInEditMode]
	public class UiSetting_TextInput : UiSetting {
		[SerializeField] TMPro.TMP_InputField uiInputField = null;

#if UNITY_EDITOR
		protected override void OnValidate() {
			base.OnValidate();
			if (uiInputField == null) { uiInputField = GetComponentInChildren<TMPro.TMP_InputField>(); }
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
				uiInputField.onValueChanged.AddListener(v => {
					setting.SetValue(v);
				});
				var v = setting.Get("");
				uiInputField.text = v;
				uiInputField.onValueChanged?.Invoke(v);
			}
		}

		//

		protected override void OnChange(Setting setting) {
			uiInputField.text = setting.Get("");
		}
	}

}
