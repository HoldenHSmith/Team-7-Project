using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaterialBlockHandler : MonoBehaviour
{

	[SerializeField] private Renderer _renderer = null;
	[SerializeField] private MaterialPropertyBlock _materialPropertyBlock = null;
	[SerializeField] private Light _glowLight = null;
	[SerializeField] private Gradient _colorGradient;
	private void Awake()
	{
		_materialPropertyBlock = new MaterialPropertyBlock();
		//_renderer = GetComponent<Renderer>();
	}

	public void SetProperties(float evaluation, float speed)
	{
		if (speed <= 0.15f)
			speed = 0f;
		if (speed > 15)
			speed = 15;
		_renderer.GetPropertyBlock(_materialPropertyBlock);
		_materialPropertyBlock.SetFloat("_Evaluation", evaluation);
		_materialPropertyBlock.SetFloat("_Speed", speed);
		_renderer.SetPropertyBlock(_materialPropertyBlock);
		_glowLight.color = _colorGradient.Evaluate(evaluation);
	}

}
