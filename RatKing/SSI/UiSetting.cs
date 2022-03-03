//#define USE_LOCALISATION

using UnityEngine;

namespace RatKing.SSI {

	public abstract class UiSetting : MonoBehaviour {
		[SerializeField] protected Setting setting = null;
		[SerializeField] protected TMPro.TextMeshProUGUI uiLabel = null;
		//
		public string CurName { get; private set; } // translated

		protected virtual void OnValidate() {
			if (uiLabel == null) { uiLabel = GetComponentInChildren<TMPro.TextMeshProUGUI>(); }
		}

		//

		protected virtual void Start() {
			Settings.AddListener(setting, OnChange);
#if USE_LOCALISATION
			SLH.Localisation.OnLanguageChanged += ChangeLabel;
#endif
		}

		protected virtual void OnDestroy() {
			Settings.RemoveListener(setting, OnChange);
#if USE_LOCALISATION
			SLH.Localisation.OnLanguageChanged -= ChangeLabel;
#endif
		}

		//

		protected virtual void ChangeLabel() {
#if USE_LOCALISATION
			uiLabel.text = SLH.Localisation.Do("settings/" + setting.LabelText);
#else
			uiLabel.text = setting.LabelText;
#endif
		}

		//

		protected abstract void OnChange(Setting setting);
	}

}
