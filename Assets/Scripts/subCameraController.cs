using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class subCameraController : MonoBehaviour
{

    public AudioClip introMusic;
    public AudioClip ambianceMusic;
    public AudioClip battleMusic;
    public AudioClip preBossMusic;
    public AudioClip bossMusic;

    public AnimationClip introAnim;
    public AnimationClip enigm1Anim;
    public AnimationClip skeletonAnim;
    public AnimationClip enigm2Anim;
    public AnimationClip preBossAnim;
    public AnimationClip bossAnim;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void changeMusic(string nameMusic)
    {
        AudioClip music = introMusic;
        switch (nameMusic)
        {
            case "intro":
                music = introMusic;
                AudioSource.PlayClipAtPoint(music, transform.position, 0.1f);
                break;

            case "ambiance":
                music = ambianceMusic;
                AudioSource.PlayClipAtPoint(music, transform.position, 0.1f);
                break;

            case "battle":
                music = battleMusic;
                AudioSource.PlayClipAtPoint(music, transform.position, 0.1f);
                break;

            case "preBoss":
                music = preBossMusic;
                AudioSource.PlayClipAtPoint(music, transform.position, 0.1f);
                break;

            case "boss":
                music = bossMusic;
                AudioSource.PlayClipAtPoint(music, transform.position, 0.1f);
                break;

            default:
                break;
        }

        //GetComponent<AudioSource>().clip = music;
    }

    public void playAnimation(string nameAnim)
    {
        //AnimationClip anim = introAnim;
        switch (nameAnim)
        {
            case "intro":
                GetComponent<Animation>().clip = introAnim;
                break;

            case "enigm1":
                GetComponent<Animation>().clip = enigm1Anim;
                break;

            case "skeleton":
                GetComponent<Animation>().clip = skeletonAnim;
                break;

            case "enigm2":
                GetComponent<Animation>().clip = enigm2Anim;
                break;

            case "preBoss":
                GetComponent<Animation>().clip = preBossAnim;
                break;

            case "boss":
                GetComponent<Animation>().clip = preBossAnim;
                break;

            default:
                break;
        }
    }
}
