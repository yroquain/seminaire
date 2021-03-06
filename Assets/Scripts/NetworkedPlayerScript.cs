﻿using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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

        Destroy(GameObject.Find("CameraDebut"));

        this.gameObject.name = "LOCAL Player";
        base.OnStartLocalPlayer();
    }

    public void ajouterHpMana(){
        if (isLocalPlayer)
        {
            this.gameObject.GetComponent<ManagementHpMana>().addMaxMana(10);
            this.gameObject.GetComponent<ManagementHpMana>().addMaxHp(10);
        } 
    }

    void ToggleRenderer(bool isAlive)
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].enabled = isAlive;
    }

    [ClientRpc]
    public void RpcSynchronizeManaHp(int _numberPlayer, float _currentHp, float _currentMana, float _maxHp, float _maxMana)
    {
        GameObject.Find("networkManager").GetComponent<GameController>().setHpManaActual(_numberPlayer, _currentHp, _currentMana, _maxHp, _maxMana);
    }
    [ClientRpc]
    public void RpcSetIsAttacking(int _numberPlayer, bool isAttacking)
    {
        GameObject.Find("networkManager").GetComponent<GameController>().isAttacking[_numberPlayer]=isAttacking;
    }

    [ClientRpc]
    public void RpcSetIsCasting(bool _isCasting, int _numberPlayer, int _numberSpell)
    {
        GameObject.Find("networkManager").GetComponent<GameController>().SetIsCasting(_isCasting, _numberPlayer, _numberSpell);
    }
    [ClientRpc]
    public void RpcResetVarSpell(int _numberPlayer)
    {
        GameObject.Find("networkManager").GetComponent<GameController>().ResetVarSpell(_numberPlayer);
    }
    [ClientRpc]
    public void RpcIsReady(int _numberPlayer)
    {
        GameObject.Find("networkManager").GetComponent<GameController>().Ready(_numberPlayer);
    }
    [ClientRpc]
    public void RpcIsReadyRefresh(int _numberPlayer, bool ready)
    {
        GameObject.Find("networkManager").GetComponent<GameController>().isReady[_numberPlayer]=ready;
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
            this.GetComponent<PlayerController>().qTo = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            this.GetComponent<PlayerController>().rotate = 0.0f;
            GameObject.Find("CanvasJ1(Clone)").GetComponent<scriptHUD>().setPlayerDead(true);
        }
        Invoke("RpcRespawn", 2f);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        ToggleRenderer(true);
        if (isLocalPlayer)
        {
            this.GetComponent<ManagementHpMana>().setFullMana();
            this.GetComponent<ManagementHpMana>().setFullHp();
            fpsController.enabled = true;
            GameObject.Find("CanvasJ1(Clone)").GetComponent<scriptHUD>().setPlayerDead(false);
            fpsCamera.enabled = true;
        }            
    }
    [ClientRpc]
    public void RpcStartGame(GameObject myPlayer)
    {
        if(myPlayer.GetComponent<Sorts_simple>().numeroJoueur==0)
        {
            myPlayer.transform.position = new Vector3(0, -0.78f, -450);
        }
        if (myPlayer.GetComponent<Sorts_simple>().numeroJoueur == 1)
        {
            myPlayer.transform.position = new Vector3(4, -0.79f, -450);
        }
        this.GetComponent<PlayerController>().qTo = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        this.GetComponent<PlayerController>().rotate = 0.0f;
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
        GameObject.Find("networkManager").GetComponent<GameController>().SetElement(myPlayer.GetComponent<Sorts_simple>().numeroJoueur, newTag);
        
    }

    [ClientRpc]
    public void RpcSwitchRespawnBegin()
    {
        GameObject.Find("Spawn_joueur1").transform.position = new Vector3(0, -0.78f, -450);
        GameObject.Find("Spawn_joueur2").transform.position = new Vector3(4, -0.79f, -450);
        
    }

    [ClientRpc]
    public void RpcSwitchRespawn(GameObject triggerRespawn)
    {
        triggerRespawn.GetComponent<triggerEnigme1>().spawnPrecedent1.SetActive(false);
        triggerRespawn.GetComponent<triggerEnigme1>().spawnPrecedent2.SetActive(false);
        triggerRespawn.GetComponent<triggerEnigme1>().newRespawn.SetActive(true);
        Destroy(triggerRespawn.GetComponent<triggerEnigme1>().spawnPrecedent1);
        Destroy(triggerRespawn.GetComponent<triggerEnigme1>().spawnPrecedent2);
        Destroy(triggerRespawn);
        if (GameObject.FindGameObjectWithTag("Mage_Feu") != null)
        {
            GameObject.FindGameObjectWithTag("Mage_Feu").GetComponent<ManagementHpMana>().addMaxMana(10);
            GameObject.FindGameObjectWithTag("Mage_Feu").GetComponent<ManagementHpMana>().addMaxHp(10);
        }
        if (GameObject.FindGameObjectWithTag("Mage_Eau") != null)
        {
            GameObject.FindGameObjectWithTag("Mage_Eau").GetComponent<ManagementHpMana>().addMaxMana(10);
            GameObject.FindGameObjectWithTag("Mage_Eau").GetComponent<ManagementHpMana>().addMaxHp(10);
        }
        if (GameObject.FindGameObjectWithTag("Mage_Air") != null)
        {
            GameObject.FindGameObjectWithTag("Mage_Air").GetComponent<ManagementHpMana>().addMaxMana(10);
            GameObject.FindGameObjectWithTag("Mage_Air").GetComponent<ManagementHpMana>().addMaxHp(10);
        }
        
    }

    #region Gestion sorts
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
    public void RpcTraitDeFeu(GameObject myPlayer)
    {

        GameObject player = GameObject.FindGameObjectWithTag("Mage_Feu");
        Vector3 position = new Vector3(player.transform.position.x + player.transform.forward.x * 2,
            player.transform.position.y + 2,
            player.transform.position.z + player.transform.forward.z * 2);


        Instantiate(myPlayer.GetComponent<Sorts_simple>().Prefabs[1], position, Quaternion.identity);

        Vector3 pos;
        float yRot = transform.rotation.eulerAngles.y;
        Vector3 forwardY = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
        Quaternion rotation = Quaternion.identity;
        GameObject currentPrefabObject = GameObject.Instantiate(myPlayer.GetComponent<Sorts_simple>().Prefabs[0]);
        FireBaseScript currentPrefabScript = currentPrefabObject.GetComponent<FireConstantBaseScript>();

        if (currentPrefabScript == null)
        {
            // temporary effect, like a fireball
            currentPrefabScript = currentPrefabObject.GetComponent<FireBaseScript>();
            if (currentPrefabScript.IsProjectile)
            {
                // set the start point near the player
                rotation = myPlayer.GetComponent<Sorts_simple>().Mycamera.transform.rotation;
                //rotation = transform.rotation;
                pos = new Vector3(transform.position.x + myPlayer.GetComponent<Sorts_simple>().Mycamera.transform.forward.x * 2,
                        transform.position.y + 2,
                        transform.position.z + myPlayer.GetComponent<Sorts_simple>().Mycamera.transform.forward.z * 2); ;
                //pos = transform.position + forward + right + up;
            }
            else
            {
                // set the start point in front of the player a ways
                pos = transform.position + (forwardY * 10.0f);
            }
        }
        else
        {
            // set the start point in front of the player a ways, rotated the same way as the player
            pos = transform.position + (forwardY * 5.0f);
            rotation = transform.rotation;
            pos.y = 0.0f;
        }

        FireProjectileScript projectileScript = currentPrefabObject.GetComponentInChildren<FireProjectileScript>();
        if (projectileScript != null)
        {
            // make sure we don't collide with other friendly layers
            projectileScript.ProjectileCollisionLayers &= (~UnityEngine.LayerMask.NameToLayer("FriendlyLayer"));
        }

        currentPrefabObject.transform.position = pos;
        currentPrefabObject.transform.rotation = rotation;
    }

    [ClientRpc]
    public void RpcMurEole(GameObject myPlayer)
    {
        if (!myPlayer.GetComponent<Sorts_simple>().getIsActivated())
        {
            myPlayer.GetComponent<Sorts_simple>().setMurActif(GameObject.Instantiate(myPlayer.GetComponent<Sorts_simple>().mur));
            myPlayer.GetComponent<Sorts_simple>().getMurActif().transform.position = new Vector3(transform.position.x + myPlayer.GetComponent<Sorts_simple>().Mycamera.transform.forward.x * 2,
                transform.position.y,
                transform.position.z + myPlayer.GetComponent<Sorts_simple>().Mycamera.transform.forward.z * 2);
            myPlayer.GetComponent<Sorts_simple>().getMurActif().transform.rotation = transform.rotation;

        }
        else
        {
            Destroy(myPlayer.GetComponent<Sorts_simple>().getMurActif().gameObject);
        }
        myPlayer.GetComponent<Sorts_simple>().setIsActivated(!myPlayer.GetComponent<Sorts_simple>().getIsActivated());
    }

    [ClientRpc]
    public void RpcBourrasqueInfernale(GameObject myPlayer)
    {
        Vector3 position = new Vector3(transform.position.x + myPlayer.GetComponent<Sorts_simple>().Mycamera.transform.forward.x * 2,
               transform.position.y,
               transform.position.z + myPlayer.GetComponent<Sorts_simple>().Mycamera.transform.forward.z * 2);
        Instantiate(myPlayer.GetComponent<Sorts_simple>().wind, position, Quaternion.identity);
    }
    [ClientRpc]
    public void RpcTornadeEnflammee(GameObject myPlayer)
    {
        Vector3 position = new Vector3(transform.position.x + myPlayer.GetComponent<Sorts_simple>().Mycamera.transform.forward.x * 2,
               transform.position.y,
               transform.position.z + myPlayer.GetComponent<Sorts_simple>().Mycamera.transform.forward.z * 2);
        Instantiate(myPlayer.GetComponent<Sorts_simple>().windfire, position, Quaternion.identity);
    }
    [ClientRpc]
    public void RpcRaz(GameObject myPlayer)
    {
        Vector3 position = new Vector3(transform.position.x,
               transform.position.y,
               transform.position.z);
        Instantiate(myPlayer.GetComponent<Sorts_simple>().wave, position, Quaternion.identity);
    }

    [ClientRpc]
    public void RpcPluieDivine(GameObject myPlayer)
    {
        Vector3 position = new Vector3((transform.position.x + myPlayer.GetComponent<Sorts_simple>().Mycamera.transform.forward.x * 2),
               transform.position.y + 2,
               transform.position.z + myPlayer.GetComponent<Sorts_simple>().Mycamera.transform.forward.z * 2);
        Instantiate(myPlayer.GetComponent<Sorts_simple>().prerain, position, Quaternion.identity);
    }
    [ClientRpc]
    public void RpcPluiedeFeu(GameObject myPlayer)
    {
        Vector3 position = new Vector3((transform.position.x + myPlayer.GetComponent<Sorts_simple>().Mycamera.transform.forward.x * 2),
               transform.position.y + 2,
               transform.position.z + myPlayer.GetComponent<Sorts_simple>().Mycamera.transform.forward.z * 2);
        Instantiate(myPlayer.GetComponent<Sorts_simple>().prefire, position, Quaternion.identity);
    }
    [ClientRpc]
    public void RpcGiboulee(GameObject myPlayer)
    {
        Vector3 position = new Vector3((transform.position.x + myPlayer.GetComponent<Sorts_simple>().Mycamera.transform.forward.x * 2),
               transform.position.y + 2,
               transform.position.z + myPlayer.GetComponent<Sorts_simple>().Mycamera.transform.forward.z * 2);
        Instantiate(myPlayer.GetComponent<Sorts_simple>().pregib, position, Quaternion.identity);
    }
    [ClientRpc]
    public void RpcTyphon(GameObject myPlayer)
    {
        Vector3 position = new Vector3((transform.position.x + myPlayer.GetComponent<Sorts_simple>().Mycamera.transform.forward.x * 2),
               transform.position.y + 2,
               transform.position.z + myPlayer.GetComponent<Sorts_simple>().Mycamera.transform.forward.z * 2);
        Instantiate(myPlayer.GetComponent<Sorts_simple>().pretyphon, position, Quaternion.identity);
    }

    [ClientRpc]
    public void RpcChocAquatique(GameObject myPlayer)
    {
        Vector3 position = new Vector3(myPlayer.GetComponent<Sorts_simple>().pos.position.x,
                myPlayer.GetComponent<Sorts_simple>().pos.position.y,
                myPlayer.GetComponent<Sorts_simple>().pos.position.z);
        Instantiate(myPlayer.GetComponent<Sorts_simple>().trait, position, Quaternion.identity);
    }
    [ClientRpc]
    public void RpcJetObsidienne(GameObject myPlayer)
    {
        Vector3 position = new Vector3(myPlayer.GetComponent<Sorts_simple>().pos.position.x,
                myPlayer.GetComponent<Sorts_simple>().pos.position.y,
                myPlayer.GetComponent<Sorts_simple>().pos.position.z);
        Instantiate(myPlayer.GetComponent<Sorts_simple>().obsidienne, position, Quaternion.identity);
    }
    #endregion
}