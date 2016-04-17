using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class GameManager : MonoBehaviour
{
    public GameObject pontosText;
    public int pontosPorOrb;
    public GameObject gameOverScreen;
    public GameObject HUDScreen;

    public Sprite caraJenkyl;
    public Sprite caraHyde;

    private AudioSource sound;
    private Text txt;
    private int pontuacao;

    private Text[] textos;

    public GameObject Player;
    private PlayerController pController;

    public Image faceTime;
    // Use this for initialization
    void Start()
    {
        txt = pontosText.GetComponent<Text>();
        sound = GetComponent<AudioSource>();
        Player = GameObject.Find("Player");
        pController = Player.GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        txt.text = pontuacao + "pts";

        if (pController.wizardsRules)
            faceTime.sprite = caraJenkyl;
        else
            faceTime.sprite = caraHyde;
            



    }

    public void getOrb()
    {
        pontuacao += pontosPorOrb;
        sound.Play();
    }

    public void setScore()
    {
        textos = gameOverScreen.GetComponentsInChildren<Text>();

        foreach (Text t in textos)
        {
            if (t.name == "Pontuacao")
            {
                t.text = "Your Score: " + pontuacao + "pts";
            }
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void exitGame()
    {
        Application.Quit();
    }

    public void CountDown(float i) {
        textos = gameOverScreen.GetComponentsInChildren<Text>();

        foreach (Text t in textos)
        {
            if (t.name == "CountDown")
            {
                t.text = "Restart in: "+ Mathf.Round(i);
            }
        }
    }

}
