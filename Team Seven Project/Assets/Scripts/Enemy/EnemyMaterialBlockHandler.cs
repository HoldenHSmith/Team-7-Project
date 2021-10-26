using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaterialBlockHandler : MonoBehaviour
{

	[SerializeField] private Renderer _renderer;
	[SerializeField] private MaterialPropertyBlock _materialPropertyBlock;

	private void Awake()
	{
		_materialPropertyBlock = new MaterialPropertyBlock();
		_renderer = GetComponent<Renderer>();
	}

	public void SetProperties(float evaluation, float speed)
	{
		_renderer.GetPropertyBlock(_materialPropertyBlock);
		_materialPropertyBlock.SetFloat("_Evaluation", evaluation);
		_materialPropertyBlock.SetFloat("_Speed", speed);
		_renderer.SetPropertyBlock(_materialPropertyBlock);
	}

}
