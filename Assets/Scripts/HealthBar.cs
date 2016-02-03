using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public enum GameState { playing, gameover, pause };


public class HealthBar : NetworkBehaviour
{

    public static HealthBar HPBar;
    public static int score;

    private int bestScore = 0;
    public static GameState gameState;

    float curHP = 100.0f;
    float maxHP = 100.0f;
    public GUISkin myskin;
    public Texture2D HpBarTexture;
    public float HpBarLenght;
    public int HpBarX = 160;
    public int HpBarY = 10;
    public int HpBarHeight = 15;
    float PercentOfHP;

    void Awake()
    {
        HPBar = this;
        StartGame();
    }

    void StartGame()
    {
        gameState = GameState.playing;
    }



    void OnGUI()
    {
        GUI.skin = myskin;
        GUILayout.Space(3);

        if (curHP > 0)
        {
            GUI.DrawTexture(new Rect(HpBarX, HpBarY, HpBarLenght, HpBarHeight), HpBarTexture);
        }

        if (gameState == GameState.gameover)
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            GUILayout.Label("Game over!");


            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();

        }
    }

    public float getCurHP()
    {
        return this.curHP;
    }

    public void setCurHP(float curHP)
    {
        this.curHP = curHP;
    }


    void GameOver()
    {
        //Time.timeScale = 0.0f; //Pause the game
        gameState = GameState.gameover;
    }

    void Pause()
    {
        Time.timeScale = 0.0f; //Pause the game
    }

    void Update()
    {

        //auto regen
        curHP += Time.deltaTime * 5;


        if (curHP > maxHP)
        {
            curHP = maxHP;
        }

        PercentOfHP = curHP / maxHP;
        HpBarLenght = PercentOfHP * 130;

        //if hp = 0
        if (HealthBar.HPBar.getCurHP() <= 0)
        {
            CmdDeadPlayer(this.gameObject);
            this.GameOver();
        }

        /*
        if (Input.GetKeyDown("y"))
        {
            HealthBar.SP.setCurHP(HealthBar.SP.getCurHP() - 15.0f);
            Debug.Log("retire 15 hp");
    }
    */    

    }


    [Command]
    void CmdDeadPlayer(GameObject myPlayer)
    {
        myPlayer.GetComponent<NetworkedPlayerScript>().RpcResolveDead();
    }

}
