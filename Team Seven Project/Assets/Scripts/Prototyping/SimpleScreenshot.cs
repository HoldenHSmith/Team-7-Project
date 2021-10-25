using UnityEngine;
using UnityEngine.InputSystem;
public class SimpleScreenshot : MonoBehaviour
{
	void Update()
	{
		if (Keyboard.current.rightBracketKey.wasPressedThisFrame)
		{
			ScreenCapture.CaptureScreenshot("Screenshot " + System.DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".png", 2);
		}
	}
}
