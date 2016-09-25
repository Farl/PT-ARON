using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.Callbacks;

public class VersionNumber {

	[InitializeOnLoadMethod]
	static void Start () {
		int versionNum = EditorPrefs.GetInt ("VersionNumber", 0);

		PlayerSettings.productName = "v" + versionNum.ToString();
	}

	[PostProcessBuild]
	static void AfterBuild(BuildTarget target, string pathToBuiltProject)
	{
		EditorPrefs.SetInt ("VersionNumber", EditorPrefs.GetInt ("VersionNumber", 0) + 1);
	}
}
