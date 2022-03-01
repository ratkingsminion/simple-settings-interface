using UnityEngine;
using UnityEngine.UI;

namespace RatKing.SSI {

	[ExecuteInEditMode]
	public class UiSetting_Slider : UiSetting {
		[SerializeField] Slider uiSlider;

		protected override void OnValidate() {
			base.OnValidate();
			if (uiSlider == null) { uiSlider = GetComponentInChildren<Slider>(); }
		}

		//

		protected override void Start() {
			base.Start();
			if (setting != null) {
				uiSlider.onValueChanged.AddListener(v => {
					setting.SetValue(v);
					ChangeLabel();
				});
				var v = setting.GetNumber();
				uiSlider.value = v;
				uiSlider.onValueChanged?.Invoke(v);
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

		protected override void ChangeLabel() {
			base.ChangeLabel();
			uiLabel.text += ": " + uiSlider.value.ToString("0.#");
		}

		//

		protected override void OnChange(Setting setting) {
			//Debug.Log("Slider " + name + " gets changed");
			uiSlider.value = setting.Get(0f);
		}
	}

}
