using UnityEngine;
using UnityEngine.UI;

public class OverlayHandler : MonoBehaviour
{
	[SerializeField] private Image _beakerIcon = null;

	private PlayerCharacter _player = null;

	private void Start()
	{
		_player = GameManager.Instance.Player;
	}

	private void Update()
	{
		if (_player != null)
			_beakerIcon.gameObject.SetActive(_player.HasBeaker);
	}


}
