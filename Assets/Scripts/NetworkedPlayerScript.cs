using UnityEngine;
using UnityEngine.Networking;

public class NetworkedPlayerScript : NetworkBehaviour
{
    public PlayerController fpsController;
    public Camera fpsCamera;
    public AudioListener audioListener;
    public cameraController myCameraController;

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

        gameObject.name = "LOCAL Player";
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
            fpsController.enabled = true;
            fpsCamera.enabled = true;
        }
            
            
    }
}