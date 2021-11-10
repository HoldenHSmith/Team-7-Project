using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Checkpoint))]
public class CheckpointEditor : Editor
{
	private Checkpoint _checkpoint;

	private void OnEnable()
	{
		_checkpoint = (Checkpoint)target;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
	}

	private void OnSceneGUI()
	{
		Undo.RecordObject(_checkpoint, _checkpoint.name);
		Vector3 prevSavePos = _checkpoint.Offset;
		Quaternion prevSaveRot = _checkpoint.SaveRotation;
		//_checkpoint.SavePosition = Handles.PositionHandle(_checkpoint.SavePosition, Quaternion.identity);
		_checkpoint.SetOffset(Handles.PositionHandle(_checkpoint.transform.position + _checkpoint.Offset, Quaternion.identity));
		_checkpoint.SaveRotation = Handles.RotationHandle(_checkpoint.SaveRotation, _checkpoint.transform.position + _checkpoint.Offset + (Vector3.up * 0.5f));

		if (prevSavePos != _checkpoint.Offset || prevSaveRot != _checkpoint.SaveRotation)
			EditorUtility.SetDirty(target);


	}
}
