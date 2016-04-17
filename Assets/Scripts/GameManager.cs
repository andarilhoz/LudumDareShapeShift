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

    private AudioSource sound;
    private Text txt;
    private int pontuacao;

    private Text[] textos;


    // Use this for initialization
    void Start()
    {
        txt = pontosText.GetComponent<Text>();
        sound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        txt.text = pontuacao + "pts";

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
        Debug.Log("Era pra sair");
        Application.Quit();
    }
}
