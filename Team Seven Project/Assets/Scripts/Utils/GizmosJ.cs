using UnityEngine;

public static class GizmosJ
{
	public static void DrawDirectionalLine(Vector3 start, Vector3 end, Color color, int segmentUnitSeparation = 1, float arrowLength = 0.15f, float arrowAngle = 15.0f)
	{
		Gizmos.color = color;

		Gizmos.DrawLine(start, end);

		//Get Direction and Normalize it
		Vector3 direction = end - start;
		direction.Normalize();

		//Calculate arrow directions
		if (direction != Vector3.zero)
		{
			Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowAngle, 0) * Vector3.forward;
			Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowAngle, 0) * Vector3.forward;

			float distance = Vector3.Distance(start, end);

			int segments = (int)(distance / segmentUnitSeparation);
			//Draw Arrows 
			for (int i = 0; i < segments + 1; i++)
			{
				float offset = segmentUnitSeparation * i;
				Vector3 pos = direction * offset;
				Gizmos.DrawRay(start + pos, right * arrowLength);
				Gizmos.DrawRay(start + pos, left * arrowLength);
				Gizmos.DrawLine(start + pos + (right * arrowLength), start + pos + (left * arrowLength));
			}
		}

	}
}
