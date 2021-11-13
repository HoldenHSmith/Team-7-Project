#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallColour : MonoBehaviour
{
	private Renderer _renderer;
	private MaterialPropertyBlock _matPropertyBlock;
	[SerializeField] private AreaType _area = AreaType.Containment;

	private void OnEnable()
	{
		_renderer = GetComponentInChildren<Renderer>();

		UpdateMaterialBlock();
	}

	private void OnValidate()
	{
		UpdateMaterialBlock();
	}

	private void UpdateMaterialBlock()
	{
		if (_renderer == null)
			_renderer = GetComponentInChildren<Renderer>();

		_matPropertyBlock = new MaterialPropertyBlock();
		_renderer.GetPropertyBlock(_matPropertyBlock);
		_matPropertyBlock.SetInt("_Value", (int)_area);
		_renderer.SetPropertyBlock(_matPropertyBlock);
	}
}
#endif
