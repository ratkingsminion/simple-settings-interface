using UnityEngine;
using UnityEngine.UI;

namespace RatKing.SSI {

	[ExecuteInEditMode]
	public class UiSetting_Slider : UiSetting {
		[SerializeField] Slider uiSlider = null;
		
#if UNITY_EDITOR
		protected override void OnValidate() {
			base.OnValidate();
			if (uiSlider == null) { uiSlider = GetComponentInChildren<Slider>(); }
		}
#endif

		//

		protected override void Start() {
#if UNITY_EDITOR
			if (!Application.isPlaying) { return; }
#endif
			base.Start();
			if (setting != null) {
				uiSlider.onValueChanged.AddListener(v => {
					if (uiSlider.wholeNumbers) { setting.SetValue((int)v); }
					else { setting.SetValue(v); }
					ChangeLabel();
				});
				var v = setting.GetNumber();
				uiSlider.value = v;
				uiSlider.onValueChanged?.Invoke(v);
			}
		}

		//

		protected override void ChangeLabel() {
			base.ChangeLabel();
			uiLabel.text += ": " + uiSlider.value.ToString("0.#");
		}

		//

		protected override void OnChange(Setting setting) {
			//Debug.Log("Slider " + name + " gets changed");
			 uiSlider.value = uiSlider.wholeNumbers ? setting.Get(0) : setting.Get(0f);
		}
	}

}
