using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {

    public Transform target;
    public Transform camera;
    public float angularSpeed;
    private bool bQuickSwitch = false;
    private Vector3 initialOffset;

    private Vector3 currentOffset;

	// Use this for initialization
	void Start () {
        currentOffset = transform.position - target.position; ;
	}
	

    private void LateUpdate()
    {
        transform.position = target.position + currentOffset;
        Debug.Log(Input.GetButton("CameraVertical"));
         float movement=0;
         if (Input.GetKeyDown("p"))
        {
            movement = angularSpeed * Time.deltaTime;
        }
        
        if (!Mathf.Approximately(movement, 0f))
        {
            transform.RotateAround(target.position, Vector3.right, movement);
            currentOffset = transform.position - target.position;
        }
    }
}
