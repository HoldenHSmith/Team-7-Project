using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class NoteManager : MonoBehaviour
{
    private static NoteManager _instance;
    private static readonly List<PaperNote> _noteList = new List<PaperNote>();

    public static void RegisterNote(PaperNote note)
    {
        if (_noteList.Contains(note))
            return;

        _noteList.Add(note);
    }

    public static void RemoveNote(PaperNote note)
    {
        if (!_noteList.Contains(note))
            return;

        _noteList.Remove(note);
    }

    public int Notecount { get => _noteList.Count; }
    public static List<PaperNote> Notes { get => _noteList; }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        foreach (PaperNote obj in _noteList)
        {
            Handles.DrawAAPolyLine(transform.position, obj.transform.position);
            //Vector3 managerPos = transform.position;
            //Vector3 objPos = obj.transform.position;
            //float halfHeight = (managerPos.y - objPos.y) * 0.5f;
            //Vector3 offset = Vector3.up * halfHeight;

            //Handles.DrawBezier(managerPos, objPos, managerPos - offset, objPos + offset, Color.red, EditorGUIUtility.whiteTexture, 1f);
        }
    }
#endif

    public static NoteManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject();
                _instance = go.AddComponent<NoteManager>();
            }
            return _instance;
        }
    }
}
