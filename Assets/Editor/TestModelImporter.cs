using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Reflection;

public class TestModelImporter : AssetPostprocessor {


	static MethodInfo updateTransformMaskMethodInfo = typeof(ModelImporter).GetMethod ("UpdateTransformMask", BindingFlags.NonPublic | BindingFlags.Static);

	void OnPreprocessModel()
	{
		var modelImporter = assetImporter as ModelImporter;

		if (modelImporter.clipAnimations.Length == 0 && modelImporter.defaultClipAnimations.Length > 0) {
			ModelImporterClipAnimation[] clipAnims = (ModelImporterClipAnimation[])modelImporter.defaultClipAnimations.Clone();

			var transformMask = new UnityEditor.Animations.AvatarMask();

			transformMask.transformCount = modelImporter.transformPaths.Length;
			int transformIndex = 0;
			foreach (string transformPath in modelImporter.transformPaths) {
				transformMask.SetTransformPath (transformIndex, transformPath);
				transformMask.SetTransformActive (transformIndex, true);
				transformIndex++;
			}
			modelImporter.clipAnimations = clipAnims;

			SerializedObject so = new SerializedObject (assetImporter);
			SerializedProperty transformMaskProperty = so.FindProperty ("m_ClipAnimations").GetArrayElementAtIndex (0).FindPropertyRelative ("transformMask");
			updateTransformMaskMethodInfo.Invoke (clipAnims[0], new System.Object[]{ transformMask, transformMaskProperty });

			so.ApplyModifiedProperties ();
		}

		LogWarning("OnPreprocessModel", modelImporter);
	}

	/*
	void OnPreprocessAnimation()
	{
		var modelImporter = assetImporter as ModelImporter;
		ModelImporterClipAnimation[] clipAnims = (ModelImporterClipAnimation[])modelImporter.defaultClipAnimations.Clone();
		clipAnims [0].name = Path.GetFileNameWithoutExtension (assetPath);
		modelImporter.clipAnimations = clipAnims;
	}

	void OnPostprocessModel(GameObject go)
	{
		go.AddComponent<MeshRenderer> ();
		go.AddComponent<MeshCollider> ();
		var modelImporter = assetImporter as ModelImporter;

		ModelImporterClipAnimation[] clipAnims = modelImporter.clipAnimations;
		clipAnims [0].maskSource = new UnityEditor.Animations.AvatarMask();

		clipAnims [0].maskSource.transformCount = modelImporter.transformPaths.Length;
		int transformIndex = 0;
		foreach (string transformPath in modelImporter.transformPaths) {
			clipAnims [0].maskSource.SetTransformPath (transformIndex, transformPath);
			clipAnims [0].maskSource.SetTransformActive (transformIndex, true);
			transformIndex++;
		}
		modelImporter.clipAnimations = clipAnims;
	}
	*/
}
