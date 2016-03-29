using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class subCameraController : MonoBehaviour {

    public AudioClip intro;
    public AudioClip ambiance;
    public AudioClip battle;
    public AudioClip preBoss;
    public AudioClip boss;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
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
