using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.Callbacks;

namespace SS
{
	// This class should not be STATIC
	public class TestBuildPipeline {
		[MenuItem("Editor/Test EditorPrefs")]
		public static void TestEditorPrefs()
		{
			// date time
			string dateTimeNow = "02";//System.DateTime.Now.ToString("dd");
			Debug.Log ("DateTimeNow " + dateTimeNow);

			// check prefs
			string getTest = EditorPrefs.GetString ("Test", "");
			Debug.Log ("Test=" + getTest);


			if (getTest != dateTimeNow) {
				// clear
				EditorPrefs.SetInt ("No", 1);

				Debug.Log ("No(after clear)=" + EditorPrefs.GetInt ("No", 1));

				// set prefs
				EditorPrefs.SetString ("Test", dateTimeNow);

				// check after set
				getTest = EditorPrefs.GetString ("Test", "");
				Debug.Log ("Test(after)=" + getTest);
			}

			int noFinal = EditorPrefs.GetInt ("No", 1);
			Debug.Log ("No(Final)=" + noFinal);


			string[] scenes = new string[] { "Assets/Scenes/test.unity" };
			string path = Application.dataPath + "/../Build/iOS";
			BuildPipeline.BuildPlayer (scenes, path, BuildTarget.iOS, BuildOptions.None);

			// save manual
			EditorApplication.Exit (0);
		}

		[PostProcessBuild()]
		public static void PostProcessBiuld(BuildTarget target, string pathToBuiltProject)
		{
			int no = EditorPrefs.GetInt ("No", 1);
			Debug.Log ("No(post)=" + no);

			EditorPrefs.SetInt ("No", no + 1);

			no = EditorPrefs.GetInt ("No", 1);
			Debug.Log ("No(post, after+1)=" + no);
		}

		[MenuItem("Editor/Clear EditorPrefs")]
		public static void ClearEditorPrefs()
		{
			EditorPrefs.DeleteKey ("Test");
			EditorPrefs.DeleteKey ("No");
		}
	}

}