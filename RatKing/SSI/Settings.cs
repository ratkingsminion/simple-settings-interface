using System.Collections.Generic;
using UnityEngine;

namespace RatKing.SSI {

	public class Settings {

		const string settingsFolderInResources = "Settings"; // change this if you put your settings in some other folder

		static readonly Dictionary<string, Setting> byID = new Dictionary<string, Setting>();

		//

		[RuntimeInitializeOnLoadMethod(loadType: RuntimeInitializeLoadType.AfterSceneLoad)]
		static void OnApplicationStart() {
			byID.Clear();
			foreach (var s in Resources.LoadAll<Setting>(settingsFolderInResources)) { // prepare the settings
				s.Load();
				byID[s.ID.ToUpper()] = s;
			}
		}

		//

		public static void AddListener(string ID, System.Action<Setting> onChange) {
			if (onChange == null) { return; }
			if (!byID.TryGetValue(ID.ToUpper(), out var setting)) { Debug.LogError("Setting " + ID + " not found! Did you really put them into Resources/" + settingsFolderInResources + "?"); return; }
			setting.OnChange += onChange;
			onChange.Invoke(setting);
		}

		public static void AddListener(Setting setting, System.Action<Setting> onChange) {
			if (setting == null || onChange == null) { return; }
			setting.OnChange += onChange;
			onChange.Invoke(setting);
		}

		public static void RemoveListener(string ID, System.Action<Setting> onChange) {
			if (byID == null) { return; }
			if (!byID.TryGetValue(ID.ToUpper(), out var setting)) { Debug.LogError("Setting " + ID + " not found! Did you really put them into Resources/" + settingsFolderInResources + "?"); return; }
			setting.OnChange -= onChange;
		}

		public static void RemoveListener(Setting setting, System.Action<Setting> onChange) {
			if (setting == null) { return; }
			setting.OnChange -= onChange;
		}

		// helper stuff

		public static void ToggleBool(string id) {
#if UNITY_EDITOR
			if (byID == null) { Debug.LogError("Can't get settings before initialisation!"); return; }
			if (!byID.ContainsKey(id)) { Debug.LogError("Setting " + id + " does not exist! Did you really put them into Resources/" + settingsFolderInResources + "?"); return; }
#endif
			byID[id].SetValue(!byID[id].Get<bool>());
		}

		public static float GetNumber(string id) {
#if UNITY_EDITOR
			if (byID == null) { Debug.LogError("Can't get settings before initialisation!"); return -1f; }
			if (!byID.ContainsKey(id)) { Debug.LogError("Setting " + id + " does not exist! Did you really put them into Resources/" + settingsFolderInResources + "?"); return -1f; }
#endif
			return byID[id].GetNumber();
		}

		public static T Get<T>(string id) {
#if UNITY_EDITOR
			if (byID == null) { Debug.LogError("Can't get settings before initialisation!"); return default; }
			if (!byID.ContainsKey(id)) { Debug.LogError("Setting " + id + " does not exist! Did you really put them into Resources/" + settingsFolderInResources + "?"); return default; }
#endif
			return byID[id].Get<T>();
		}

		public static void Set<T>(string id, T value) {
#if UNITY_EDITOR
			if (byID == null) { Debug.LogError("Can't get settings before initialisation!"); return; }
			if (!byID.ContainsKey(id)) { Debug.LogError("Setting " + id + " does not exist! Did you really put them into Resources/" + settingsFolderInResources + "?"); return; }
#endif
			byID[id].SetValue(value);
			
		}
	}

}