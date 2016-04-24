using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class subCameraController : MonoBehaviour
{

    //Musiques
    public AudioClip introMusic;
    public AudioClip ambianceMusic;
    public AudioClip battleMusic;
    public AudioClip preBossMusic;
    public AudioClip bossMusic;

    //Voices
    public AudioClip Intro;
    public AudioClip Preenigme1;
    public AudioClip Postenigme1;
    public AudioClip Preskelette;
    public AudioClip enigme2;
    public AudioClip AvantBoss;
    public AudioClip Boss;


    //Animations
    public AnimationClip introAnim;
    public AnimationClip enigm1Anim;
    public AnimationClip postEnigm1Anim;
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
                AudioSource.PlayClipAtPoint(music, GameObject.Find("LOCAL Player").transform.position, 0.15f);
                AudioSource.PlayClipAtPoint(Intro, GameObject.Find("LOCAL Player").transform.position, 1f);
                break;

            case "ambiance":
                music = ambianceMusic;
                AudioSource.PlayClipAtPoint(music, transform.position, 0.1f);
                break;

            case "battle":
                music = Preskelette;
                AudioSource.PlayClipAtPoint(music, GameObject.Find("LOCAL Player").transform.position, 1f);
                break;

            case "preBoss":
                music = preBossMusic;
                AudioSource.PlayClipAtPoint(music, transform.position, 0.1f);
                break;

            case "prepreBoss":
                music = AvantBoss;
                AudioSource.PlayClipAtPoint(music, GameObject.Find("LOCAL Player").transform.position, 1f);
                break;

            case "boss":
                music = bossMusic;
                AudioSource.PlayClipAtPoint(music, transform.position, 0.1f);
                break;

            case "enigm1":
                music = Preenigme1;
                AudioSource.PlayClipAtPoint(music, GameObject.Find("LOCAL Player").transform.position, 1f);
                break;

            case "postEnigm1":
                music = Postenigme1;
                AudioSource.PlayClipAtPoint(music, GameObject.Find("LOCAL Player").transform.position, 1f);
                break;

            case "enigm2":
                music = enigme2;
                AudioSource.PlayClipAtPoint(music, GameObject.Find("LOCAL Player").transform.position, 1f);
                break;

            default:
                break;
        }
    }
    /*
    public void playAnimation(string nameAnim)
    {
        switch (nameAnim)
        {
            case "intro":
                GetComponent<Animation>().clip = introAnim;
                break;

            case "enigm1":
                GetComponent<Animation>().clip = enigm1Anim;
                break;

            case "postEnigm1":
                GetComponent<Animation>().clip = postEnigm1Anim;
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
    */
}
