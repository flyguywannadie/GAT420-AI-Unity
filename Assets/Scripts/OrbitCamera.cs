using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    //[SerializeField] private Vector3 camOffset;

    [SerializeField] float mouseSensitivity = 0.2f;
    [SerializeField, Range(20, 90)] float defaultPitch = 40;
    [SerializeField, Range(2, 30)] float distance = 5;

    float yaw = 0;
    float pitch = 0;

    bool work = false;

	private void Start()
	{
        pitch = defaultPitch;
	}

	// Update is called once per frame
	void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            work = true;
        }

        if (work)
        {
			yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
			pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;

			Quaternion qyaw = Quaternion.AngleAxis(yaw, Vector3.up);
			Quaternion qpitch = Quaternion.AngleAxis(pitch, Vector3.right);

			Quaternion rotation = qyaw * qpitch;

			//transform.position = target.transform.position + camOffset;
			transform.position = target.transform.position + (rotation * (Vector3.back * distance));
			transform.rotation = rotation;

            work = false;
		}
    }
}
