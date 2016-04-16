using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {

    public Transform target;
    public Transform Mycamera;
    public float angularSpeed;

    public AudioClip intro;
    public AudioClip ambiance;
    public AudioClip battle;
    public AudioClip preBoss;
    public AudioClip boss;

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

    public void changeMusic(string nameMusic)
    {
        AudioClip music = intro;
        switch (nameMusic)
        {
            case "intro":
                music = intro;
                AudioSource.PlayClipAtPoint(music, transform.position, 0.1f);
                break;

            case "ambiance":
                music = ambiance;
                AudioSource.PlayClipAtPoint(music, transform.position, 0.1f);
                break;

            case "battle":
                music = battle;
                AudioSource.PlayClipAtPoint(music, transform.position, 0.1f);
                break;

            case "preBoss":
                music = preBoss;
                AudioSource.PlayClipAtPoint(music, transform.position, 0.1f);
                break;

            case "boss":
                music = boss;
                AudioSource.PlayClipAtPoint(music, transform.position, 0.1f);
                break;

            default:
                break;
        }

        GetComponent<AudioSource>().clip = music;
    }
}
