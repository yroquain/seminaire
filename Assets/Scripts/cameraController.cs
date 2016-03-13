using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {

    public Transform target;
    public Transform Mycamera;
    public float angularSpeed;

	// Use this for initialization
	void Start () {
       
	}
	

    private void LateUpdate()
    {
        float movement=  angularSpeed * Time.deltaTime * Input.GetAxis("CameraVertical");

        if (!Mathf.Approximately(movement, 0f))
        {
            if (movement > 0)
            {
                if (Quaternion.Angle(transform.rotation, target.rotation) < 25 || transform.rotation.x < 0) //on limite vers la haut
                {
                    transform.RotateAround(target.position, target.transform.right, movement);                    
                }
            }
            else
            {
                if (Quaternion.Angle(transform.rotation, target.rotation) < 25 || transform.rotation.x > 0) // on limite vers le bas
                {
                    transform.RotateAround(target.position, target.transform.right, movement);
                }
            }
        }
     }
}
