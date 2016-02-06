using UnityEngine;
using UnityEngine.Networking;

public class NetworkedPlayerScript : NetworkBehaviour
{
    public PlayerController fpsController;
    public Camera fpsCamera;
    public AudioListener audioListener;
    public cameraController myCameraController;

    //Texture
    public Material texture_air;
    public Material texture_eau;
    public Material texture_feu;

    //constante pour le serveur
    //private int nombreJoueur;
    Renderer[] renderers;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    public override void OnStartLocalPlayer()
    {

        fpsController.enabled = true;
        fpsCamera.enabled = true;
        audioListener.enabled = true;
        myCameraController.enabled = true;

        this.gameObject.name = "LOCAL Player";
        base.OnStartLocalPlayer();
    }


    //Dés
    void ToggleRenderer(bool isAlive)
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].enabled = isAlive;
    }


    [ClientRpc]
    public void RpcResolveDead()
    {
        ToggleRenderer(false);

        if (isLocalPlayer)
        {
            fpsController.enabled = false;
            Transform spawn = NetworkManager.singleton.GetStartPosition();
            transform.position = spawn.position;
            transform.rotation = spawn.rotation;
            fpsCamera.enabled = false;
        }

        Invoke("Respawn", 2f);
    }
    


    void Respawn()
    {
        ToggleRenderer(true);

        if (isLocalPlayer)
        {
            
            this.GetComponent<HealthBar>().completeHealth();
            fpsController.enabled = true;
            fpsCamera.enabled = true;
        }            
    }

    [ClientRpc]
    public void RpcChangerTenue(string newTag, GameObject myPlayer)
    {
        myPlayer.tag = newTag;
        if (newTag == "Mage_Feu")
        {
            myPlayer.transform.Find("Mage").GetComponent<Renderer>().material = texture_feu;   
        }
        if (newTag == "Mage_Eau")
        {
            myPlayer.transform.Find("Mage").GetComponent<Renderer>().material = texture_eau;
        }
        if (newTag == "Mage_Air")
        {
            myPlayer.transform.Find("Mage").GetComponent<Renderer>().material = texture_air;
        }
        
    }
}