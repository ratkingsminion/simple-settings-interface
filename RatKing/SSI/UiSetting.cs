//#define USE_LOCALISATION

using UnityEngine;

namespace RatKing {

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
			Base.Localisation.LANGUAGE_CHANGED.Register(ChangeLabel);
#endif
		}

		protected virtual void OnDestroy() {
			Settings.RemoveListener(setting, OnChange);
#if USE_LOCALISATION
			Base.Localisation.LANGUAGE_CHANGED.Unregister(ChangeLabel);
#endif
		}

		//

		protected virtual void ChangeLabel() {
#if USE_LOCALISATION
			uiLabel.text = Base.Localisation.Do("settings/" + setting.LabelText);
#else
			uiLabel.text = setting.LabelText;
#endif
		}

		//

		protected abstract void OnChange(Setting setting);
	}

}
