using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------------------
//-------------------------------------------------------------------
public static class DebugEx
{
	//-------------------------------------------------------------------
	//-------------------------------------------------------------------
	public static void DrawX(Vector3 v3Pos, Color color, float fSize = 0.1f)
	{
		Debug.DrawLine(v3Pos - Vector3.right * 0.1f, v3Pos + Vector3.right * 0.1f, color);
		Debug.DrawLine(v3Pos - Vector3.forward * 0.1f, v3Pos + Vector3.forward * 0.1f, color);
	}

	//-------------------------------------------------------------------
	//-------------------------------------------------------------------
	public static void DrawX(Vector3 v3Pos)
	{
		DrawX(v3Pos, Color.white);
	}

	//-------------------------------------------------------------------
	//-------------------------------------------------------------------
	public static void DrawBox(Vector3 v3Pos, Vector3 v3Size, Quaternion qRotation, Color color)
	{
		Vector3 v3Halfsize = v3Size * 0.5f;

		Vector3[] av3Points = new Vector3[8];
		av3Points[0] = new Vector3(-v3Halfsize.x, -v3Halfsize.y, -v3Halfsize.z);
		av3Points[1] = new Vector3(-v3Halfsize.x, v3Halfsize.y, -v3Halfsize.z);
		av3Points[2] = new Vector3(v3Halfsize.x, v3Halfsize.y, -v3Halfsize.z);
		av3Points[3] = new Vector3(v3Halfsize.x, -v3Halfsize.y, -v3Halfsize.z);

		av3Points[4] = new Vector3(-v3Halfsize.x, -v3Halfsize.y, v3Halfsize.z);
		av3Points[5] = new Vector3(-v3Halfsize.x, v3Halfsize.y, v3Halfsize.z);
		av3Points[6] = new Vector3(v3Halfsize.x, v3Halfsize.y, v3Halfsize.z);
		av3Points[7] = new Vector3(v3Halfsize.x, -v3Halfsize.y, v3Halfsize.z);

		Matrix4x4 matrix = Matrix4x4.TRS(v3Pos, qRotation, Vector3.one);
		for (int i = 0; i < av3Points.Length; ++i)
		{
			av3Points[i] = matrix.MultiplyPoint3x4(av3Points[i]);
		}

		Debug.DrawLine(av3Points[0], av3Points[1], color);
		Debug.DrawLine(av3Points[1], av3Points[2], color);
		Debug.DrawLine(av3Points[2], av3Points[3], color);
		Debug.DrawLine(av3Points[3], av3Points[0], color);

		Debug.DrawLine(av3Points[4], av3Points[5], color);
		Debug.DrawLine(av3Points[5], av3Points[6], color);
		Debug.DrawLine(av3Points[6], av3Points[7], color);
		Debug.DrawLine(av3Points[7], av3Points[4], color);

		Debug.DrawLine(av3Points[0], av3Points[4], color);
		Debug.DrawLine(av3Points[1], av3Points[5], color);
		Debug.DrawLine(av3Points[2], av3Points[6], color);
		Debug.DrawLine(av3Points[3], av3Points[7], color);
	}

	//-------------------------------------------------------------------
	//-------------------------------------------------------------------
	public static void DrawCircle(Vector3 v3Pos, Quaternion qRotation, float fRadius, int nSegments, Color color)
	{
		int nPointCount = nSegments + 1;

		Vector3[] av3Points = new Vector3[nPointCount];

		for (int i = 0; i < nPointCount; i++)
		{
			float fRadians = Mathf.Deg2Rad * (i * 360f / nSegments);
			av3Points[i] = new Vector3(Mathf.Sin(fRadians) * fRadius, 0, Mathf.Cos(fRadians) * fRadius);
			av3Points[i] = qRotation * av3Points[i];
			av3Points[i] += v3Pos;
		}

		for (int i = 0; i < nSegments; i++)
		{
			Debug.DrawLine(av3Points[i], av3Points[i + 1], color);
		}
	}

	//-------------------------------------------------------------------
	//-------------------------------------------------------------------
	public static void DrawCircle(Vector3 v3Pos, Quaternion qRotation, float fRadius, Color color)
	{
		DrawCircle(v3Pos, qRotation, fRadius, 32, color);
	}

	//-------------------------------------------------------------------
	//-------------------------------------------------------------------
	public static void DrawCircle(Vector3 v3Pos, Quaternion qRotation, float fRadius)
	{
		DrawCircle(v3Pos, qRotation, fRadius, 32, Color.white);
	}

	//-------------------------------------------------------------------
	//-------------------------------------------------------------------
	public static void DrawCircle(Vector3 v3Pos, float fRadius, Color color)
	{
		DrawCircle(v3Pos, Quaternion.identity, fRadius, 32, color);
	}

	//-------------------------------------------------------------------
	//-------------------------------------------------------------------
	public static void DrawCircle(Vector3 v3Pos, float fRadius)
	{
		DrawCircle(v3Pos, Quaternion.identity, fRadius, 32, Color.white);
	}

	//-------------------------------------------------------------------
	//-------------------------------------------------------------------
	public static void DrawSphere(Vector3 v3Pos, float fRadius, Color color)
	{
		DrawCircle(v3Pos, Quaternion.identity, fRadius, color);
		DrawCircle(v3Pos, Quaternion.Euler(90, 0, 0), fRadius, color);
		DrawCircle(v3Pos, Quaternion.Euler(0, 0, 90), fRadius, color);
	}

	//-------------------------------------------------------------------
	//-------------------------------------------------------------------
	public static void DrawViewCone(Vector3 v3Pos, Quaternion qRotation, Vector3 forward, float fAngle, float fLength, Color color)
	{
		float radians = Mathf.Deg2Rad * fAngle;
		Vector3[] lines = {
		new Vector3(-Mathf.Sin(radians), 0, Mathf.Cos(radians)),
		new Vector3(Mathf.Sin(radians),0,Mathf.Cos(radians)),
		new Vector3(0,-Mathf.Sin(radians),Mathf.Cos(radians)),
		new Vector3(0,Mathf.Sin(radians), Mathf.Cos(radians))
		};

		for (int i = 0; i < lines.Length; i++)
		{
			lines[i] = qRotation * lines[i];
			lines[i] *= fLength;
			lines[i] += v3Pos;
			Debug.DrawLine(v3Pos, lines[i], color);
		}

		//Radius
		float radiusDouble = Vector3.Distance(lines[0], lines[1]);

		//Rotation
		var rotation = Quaternion.LookRotation(forward);
		rotation *= Quaternion.Euler(0, 90, 90);

		//Distance
		Vector3 AB = (forward * fLength + v3Pos) - v3Pos;
		Vector3 AP1 = lines[0] - v3Pos;
		float distance = Vector3.Dot(AB, AP1) / AB.magnitude;

		Vector3 finalPosition = v3Pos + (forward * distance);

		DrawCircle(finalPosition, rotation, radiusDouble * 0.5f, color);
	}



	//-------------------------------------------------------------------
	//-------------------------------------------------------------------
	public static void DrawViewArch(Vector3 v3Pos, Quaternion qRotation, float fAngle, float fLength, Color color)
	{
		float radians = Mathf.Deg2Rad * fAngle * 0.5f;
		Vector3[] lines = {
		new Vector3(-Mathf.Sin(radians), 0, Mathf.Cos(radians)),
		new Vector3(Mathf.Sin(radians),0,Mathf.Cos(radians))
		};

		for (int i = 0; i < lines.Length; i++)
		{
			lines[i] = qRotation * lines[i];
			lines[i] *= fLength;
			lines[i] += v3Pos;
			Debug.DrawLine(v3Pos, lines[i], color);
		}
	}

}
