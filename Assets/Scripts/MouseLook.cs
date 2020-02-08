// credit to FatiguedArtist on the Unity forums
// https://forum.unity.com/threads/a-free-simple-smooth-mouselook.73117/

using UnityEngine;

public class MouseLook : MonoBehaviour
{
	public Vector2 ClampInDegrees = new Vector2(360, 180);
	public bool LockCursor;
	public Vector2 Sensitivity = new Vector2(2, 2);

	Vector2 mousePosition;
	Vector2 targetCharacterDirection;

	CursorLockMode previousLockMode;

	void Start ()
	{
        targetCharacterDirection = transform.localRotation.eulerAngles;

		previousLockMode = Cursor.lockState;
	}

	void Update ()
	{
		if (LockCursor)
			Cursor.lockState = CursorLockMode.Locked;

		var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);

		var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		mouseDelta = Vector2.Scale(mouseDelta, new Vector2(Sensitivity.x, Sensitivity.y));

		mousePosition += mouseDelta;

		if (ClampInDegrees.x < 360)
			mousePosition.x = Mathf.Clamp(mousePosition.x, -ClampInDegrees.x * 0.5f, ClampInDegrees.x * 0.5f);

		if (ClampInDegrees.y < 360)
			mousePosition.y = Mathf.Clamp(mousePosition.y, -ClampInDegrees.y * 0.5f, ClampInDegrees.y * 0.5f);

        transform.localEulerAngles = new Vector3(-mousePosition.y, mousePosition.x);
	}

	void OnDestroy ()
	{
		Cursor.lockState = previousLockMode;
	}
}
