using UnityEngine;

namespace RatKing.SSI {
	
	[CreateAssetMenu(fileName="Setting", menuName="Rat King/New Setting")]
	public class Setting : ScriptableObject {
		[SerializeField] string id = default;
		public string ID => id;
		[SerializeField] string labelText = default;
		public string LabelText => labelText;
		[SerializeField] Base.DynamicVariable defaultValue;
		//
		public System.Action<Setting> OnChange { get; set; }
		//
		Base.DynamicVariable CurValue;

		public void Reset() {
			if (defaultValue != null) { CurValue = defaultValue.GetCopy(); }
			else { CurValue = new Base.DynamicVariable(); }
			OnChange = null;
		}

		/// <summary>
		/// Set the value via this method in order to notify the listeners too!
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		public void SetValue<T>(T value) {
			CurValue.Set(value);
			OnChange?.Invoke(this);
			Save();
		}

		public T Get<T>(T standard = default) {
			return CurValue.Get(standard);
		}

		public float GetNumber(float standard = default) {
			return CurValue.GetNumber(standard);
		}

		public bool TryGet<T>(out T result) {
			return CurValue.TryGet(out result);
		}

		public bool TryGetNumber(out float result) {
			return CurValue.TryGetNumber(out result);
		}

		public bool IsNumber() {
			return CurValue.IsNumber();
		}

		//

		public void Load() {
			switch (defaultValue.Variable) {
				case Base.DynamicVarFloat f: CurValue.Set(PlayerPrefs.GetFloat("SETTING " + id, f.Value)); break;
				case Base.DynamicVarInt i: CurValue.Set(PlayerPrefs.GetInt("SETTING " + id, i.Value)); break;
				case Base.DynamicVarString s: CurValue.Set(PlayerPrefs.GetString("SETTING " + id, s.Value)); break;
				case Base.DynamicVarBool b: CurValue.Set(PlayerPrefs.GetInt("SETTING " + id, b.Value ? 1 : 0) != 0); break;
				default: /* nothing loaded */ break;
			}
		}

		public void Save() {
			switch (CurValue.Variable) {
				case Base.DynamicVarFloat f: PlayerPrefs.SetFloat("SETTING " + id, f.Value); break;
				case Base.DynamicVarInt i: PlayerPrefs.SetInt("SETTING " + id, i.Value); break;
				case Base.DynamicVarString s: PlayerPrefs.SetString("SETTING " + id, s.Value); break;
				case Base.DynamicVarBool b: PlayerPrefs.SetInt("SETTING " + id, b.Value ? 1 : 0); break;
				default: /* nothing saved */ break;
			}
		}
	}

}
