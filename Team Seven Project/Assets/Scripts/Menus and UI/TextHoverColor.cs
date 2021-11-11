using UnityEngine;
using TMPro;

public class TextHoverColor : MonoBehaviour
{
	[SerializeField] private Color _mouseOverColor;

	private TextMeshProUGUI _text;
	private Color _defaultColor;

	private void Awake()
	{
		_text = GetComponentInChildren<TextMeshProUGUI>();
		_defaultColor = _text.color;
	}

	private void OnMouseOver()
	{
		Debug.Log("Mouse over Mother Fucker!");
		_text.color = _mouseOverColor;
	}

	private void OnMouseExit()
	{
		_text.color = _defaultColor;
	}
}
