using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class OverlayHandler : MonoBehaviour
{

    [SerializeField] private Image _beakerIcon = null;

    //Ui Elements for Note Overlay
    [SerializeField] private GameObject _noteOverlay = null;
    [SerializeField] private TextMeshProUGUI _noteTitle = null;
    [SerializeField] private TextMeshProUGUI _noteBody = null;

    private PlayerCharacter _player = null;

    private void Start()
    {
        _player = GameManager.Instance.Player;
        _noteOverlay.SetActive(false);
        CollectionManager.Instance.OverlayHandler = this;
    }

    private void Update()
    {
        if (_player != null)
            _beakerIcon.gameObject.SetActive(_player.HasBeaker);

        if(_noteOverlay.activeInHierarchy && Keyboard.current.eKey.wasReleasedThisFrame )
        {
            _noteOverlay.SetActive(false);
        }
    }

    public void ReadNote(PaperNote note)
    {
        _noteOverlay.SetActive(true);
        _noteTitle.text = note.Title;
        _noteBody.text = note.Body;

    }


}
