using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public sealed class CollectionManager : MonoBehaviour
{

    private Dictionary<AreaType, bool> _keysCollected;
    private Dictionary<PaperNote, bool> _notesCollected;
    private Dictionary<MiniKeycard, bool> _miniKeycardsCollected;

    private OverlayHandler _overlayHandler;

    public CollectionManager()
    {

        _keysCollected = new Dictionary<AreaType, bool>();
        _notesCollected = new Dictionary<PaperNote, bool>();
        _miniKeycardsCollected = new Dictionary<MiniKeycard, bool>();

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

        _overlayHandler.SetKeycardActive(area, value);
    }

    public void SetNoteValue(PaperNote note, bool value)
    {
        _notesCollected[note] = value;
        if (value)
        {
            _overlayHandler.ReadNote(note);
            Debug.Log($"Note Collected!");
        }
    }

    public void SetMiniKeycardValue(MiniKeycard mini, bool value)
    {
        _miniKeycardsCollected[mini] = value;
        if (value)
            Debug.Log($"Mini Keycard Collected!");

        _overlayHandler.SetMiniKeycardCount(GameManager.Instance.Player.MiniKeycards);
    }

    public bool CheckKeyCollected(AreaType area)
    {
        return _keysCollected[area];
    }

    public bool[] MiniKeycardsCollected()
    {
        bool[] collected = new bool[_miniKeycardsCollected.Count];
        int i = 0;
        foreach (MiniKeycard card in _miniKeycardsCollected.Keys)
        {
            collected[i] = card.Collected;
            i++;
        }

        return collected;
    }

    public Dictionary<AreaType, bool> KeysCollected { get => _keysCollected; }
    public Dictionary<PaperNote, bool> NotesCollected { get => _notesCollected; }

    public OverlayHandler OverlayHandler { get => _overlayHandler; set => _overlayHandler = value; }

}
