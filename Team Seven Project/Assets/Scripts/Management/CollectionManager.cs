using System.Collections.Generic;
using UnityEngine;

public sealed class CollectionManager
{
    private static readonly CollectionManager _instance = new CollectionManager();

    private Dictionary<AreaType, bool> _keysCollected;
    private Dictionary<PaperNote, bool> _notesCollected;


    public CollectionManager()
    {
        _keysCollected = new Dictionary<AreaType, bool>();

        for (int i = 0; i < (int)AreaType.Count; i++)
        {
            _keysCollected.Add((AreaType)i, false);
        }

    }

    public void SetKeyValue(AreaType area, bool value)
    {
        _keysCollected[area] = value;
        if (value)
            Debug.Log($"Key Collected: {area}");
    }

    public void SetNotevalue(PaperNote note, bool value)
    {
        _notesCollected[note] = value;
        if (value)
            Debug.Log($"Note Collected!");
    }

    public bool CheckKeyCollected(AreaType area)
    {
        return _keysCollected[area];
    }

    public static CollectionManager Instance { get => _instance; }
    public Dictionary<AreaType, bool> KeysCollected { get => _keysCollected; }
    public Dictionary<PaperNote, bool> NotesCollected { get => _notesCollected; }

}
