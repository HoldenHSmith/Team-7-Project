using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class OverlayHandler : MonoBehaviour
{

    [SerializeField] private Image _beakerIcon = null;

    //Ui Elements for Note Overlay
    [SerializeField] private Canvas _noteOverlay = null;
    [SerializeField] private TextMeshProUGUI _noteTitle = null;
    [SerializeField] private TextMeshProUGUI _noteBody = null;

    private PlayerCharacter _player = null;
    private bool _notePopup = false;
    private void Start()
    {
        _player = GameManager.Instance.Player;
        GameManager.Instance.CollectionManager.OverlayHandler = this;
        _noteOverlay.enabled = false;
    }

    private void Update()
    {
        if (_player != null)
            _beakerIcon.gameObject.SetActive(_player.HasBeaker);

        if(_noteOverlay.enabled && Keyboard.current.eKey.wasReleasedThisFrame && !_notePopup )
        {
           _noteOverlay.enabled = false;
        }

        _notePopup = false;
    }

    public void ReadNote(PaperNote note)
    {
        _noteOverlay.enabled = true;
        _noteTitle.text = note.Title;
        _noteBody.text = note.Body;
        _notePopup = true;

    }


}
