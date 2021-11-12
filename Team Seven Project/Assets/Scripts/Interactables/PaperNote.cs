using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class PaperNote : MonoBehaviour, ICollectable, IInteractable
{

    [SerializeField] private string _noteTitle = null;
    [SerializeField, TextArea(10,2)] private string _noteBody = null;


    private void Awake()
    {


    }
    public void OnCollect()
    {
        GameManager.Instance.CollectionManager.SetNoteValue(this, true);
    }

    public bool OnInteract(PlayerCharacter playerCharacter)
    {
        OnCollect();
        this.gameObject.SetActive(false);
		//Open up note on canvas UI and set text
		return true;
    }

    public string Title { get => _noteTitle; set => _noteTitle = value; }
    public string Body { get => _noteBody; set => _noteBody = value; }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PaperNote), true)]
public class PaperNoteEditor : Editor
{
    private PaperNote _note;

    private void OnEnable()
    {
        _note = (PaperNote)target;
    }
//    public override void OnInspectorGUI()
//    {
//        OnDrawPaperData();
//    }

//    private void OnDrawPaperData()
//    {
//        GUILayout.BeginVertical();
//        {
//            GUILayout.BeginHorizontal();
//            {
//                GUILayout.Label("Title: ");
//                _note.Title = EditorGUILayout.TextField(_note.Title);
//                GUILayout.FlexibleSpace();
//            }
//            GUILayout.EndHorizontal();

//            GUILayout.BeginHorizontal();
//            {
//                GUILayout.Label("Body: ");
//                _note.Body = EditorGUILayout.TextArea(_note.Body, GUILayout.Height(100));
//                GUILayout.Space(20);
//                GUILayout.FlexibleSpace();
//            }
//            GUILayout.EndHorizontal();
//        }
//        GUILayout.EndVertical();
//    }
}
#endif


