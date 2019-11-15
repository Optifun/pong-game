using UnityEngine;

public class BasicCamera : Singleton<BasicCamera>
{
	Camera cam;
	Vector3 position;
	Vector3 rotation;

	void Awake ()
		{
		cam = gameObject.GetComponent<Camera>();
		}

	/// <summary>
	/// Устанавливает поворот камеры
	/// </summary>
	/// <param name="rot"></param>
	public void Rotate(Vector3 rot)
		{
		rotation = rot;
		var qRot = new Quaternion();
		qRot.eulerAngles = rotation;
		}

	void Start()
    {
		transform.position = position;
		Rotate(rotation);
    }
}
