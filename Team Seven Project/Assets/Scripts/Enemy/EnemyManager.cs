using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnemyManager : MonoBehaviour
{
	public static readonly List<Enemy> _enemyList = new List<Enemy>();

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		foreach (Enemy obj in _enemyList)
		{
			//Handles.DrawAAPolyLine(transform.position, obj.transform.position);
			Vector3 managerPos = transform.position;
			Vector3 objPos = obj.transform.position;
			float halfHeight = (managerPos.y - objPos.y) * 0.5f;
			Vector3 offset = Vector3.up * halfHeight;

			Handles.DrawBezier(managerPos, objPos, managerPos - offset, objPos + offset, Color.red, EditorGUIUtility.whiteTexture, 1f);
		}
	}
#endif

	public List<Enemy> Enemies { get => _enemyList; }
	public int EnemyCount { get => _enemyList.Count; }
}
