using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaterialBlockHandler : MonoBehaviour
{

	[SerializeField] private Renderer m_Renderer;
	[SerializeField] private MaterialPropertyBlock m_MaterialPropertyBlock;

	private void Awake()
	{
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
		m_Renderer = GetComponent<Renderer>();
	}

	public void SetProperties(float evaluation, float speed)
	{
		m_Renderer.GetPropertyBlock(m_MaterialPropertyBlock);
		m_MaterialPropertyBlock.SetFloat("_Evaluation", evaluation);
		m_MaterialPropertyBlock.SetFloat("_Speed", speed);
		m_Renderer.SetPropertyBlock(m_MaterialPropertyBlock);
	}

}
