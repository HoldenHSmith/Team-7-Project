using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif
[Serializable]
public class NoteManager : MonoBehaviour
{

	[SerializeField] private PaperNote[] _noteList;

	//public  void RegisterNote(PaperNote note)
	//{
	//    if (_noteList.Contains(note))
	//        return;

	//    _noteList.Add(note);
	//}

	//public  void RemoveNote(PaperNote note)
	//{
	//    if (!_noteList.Contains(note))
	//        return;

	//    _noteList.Remove(note);
	//}

	public int Notecount { get => _noteList.Length; }
	public PaperNote[] Notes { get => _noteList; set => _noteList = value; }

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		foreach (PaperNote obj in _noteList)
		{
			//Handles.DrawAAPolyLine(transform.position, obj.transform.position);
		}
	}
#endif

}
