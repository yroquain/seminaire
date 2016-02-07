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


    // Gestion des Sorts
    [ClientRpc]
    public void RpcImmolation(GameObject myPlayer)
    {
        myPlayer.GetComponent<PlayerController>().IsImmolating = !myPlayer.GetComponent<PlayerController>().IsImmolating;

        if (myPlayer.GetComponent<PlayerController>().IsImmolating)
        {
            myPlayer.GetComponent<CapsuleCollider>().enabled = true;
            myPlayer.GetComponent<PlayerController>().Immo.SetActive(true);
        }
        else
        {
            myPlayer.GetComponent<CapsuleCollider>().enabled = false;
            myPlayer.GetComponent<PlayerController>().Immo.SetActive(false);
        }
    }

    [ClientRpc]
    public void RpcMurEole(GameObject myPlayer)
    {
        if (!myPlayer.GetComponent<Sorts_Air>().getIsActivated())
        {
            myPlayer.GetComponent<Sorts_Air>().setMurActif(GameObject.Instantiate(myPlayer.GetComponent<Sorts_Air>().mur));
            myPlayer.GetComponent<Sorts_Air>().getMurActif().transform.position = new Vector3(transform.position.x + myPlayer.GetComponent<Sorts_Air>().cameraa.transform.forward.x * 2,
                transform.position.y,
                transform.position.z + myPlayer.GetComponent<Sorts_Air>().cameraa.transform.forward.z * 2);
            myPlayer.GetComponent<Sorts_Air>().getMurActif().transform.rotation = transform.rotation;

        }
        else
        {
            Destroy(myPlayer.GetComponent<Sorts_Air>().getMurActif().gameObject);
        }
        myPlayer.GetComponent<Sorts_Air>().setIsActivated(!myPlayer.GetComponent<Sorts_Air>().getIsActivated());
    }

    [ClientRpc]
    public void RpcBourrasqueInfernale(GameObject myPlayer)
    {
        Vector3 position = new Vector3(transform.position.x + myPlayer.GetComponent<Sorts_Air>().cameraa.transform.forward.x * 2,
               transform.position.y,
               transform.position.z + myPlayer.GetComponent<Sorts_Air>().cameraa.transform.forward.z * 2);
        Instantiate(myPlayer.GetComponent<Sorts_Air>().wind, position, Quaternion.identity);
    }
   
    [ClientRpc]
    public void RpcPluieDivine(GameObject myPlayer)
    {
        Vector3 position = new Vector3((transform.position.x + myPlayer.GetComponent<Sorts_Eau>().cameraa.transform.forward.x * 2),
               transform.position.y + 2,
               transform.position.z + myPlayer.GetComponent<Sorts_Eau>().cameraa.transform.forward.z * 2);
        Instantiate(myPlayer.GetComponent<Sorts_Eau>().prerain, position, Quaternion.identity);
    }

    [ClientRpc]
    public void RpcChocAquatique(GameObject myPlayer)
    {

        Vector3 position = new Vector3(myPlayer.GetComponent<Sorts_Eau>().pos.position.x,
                myPlayer.GetComponent<Sorts_Eau>().pos.position.y,
                myPlayer.GetComponent<Sorts_Eau>().pos.position.z);
        Instantiate(myPlayer.GetComponent<Sorts_Eau>().trait, position, Quaternion.identity);
    }
}