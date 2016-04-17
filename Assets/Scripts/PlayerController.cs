using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]


public class PlayerController : MonoBehaviour

{
    public bool wizardsRules = true;
    public float animSpeed = 1.5f;
    public bool useCurves = true;

    public float timerGameOver = 5;

    public float velocidadeMaxima;
    public float velocidadeInicial;

    public float velocidadePlayer = 1f;

    public float useCurvesHeight = 0.5f;

    public float forwardSpeed = 7.0f;
    public float backwardSpeed = 2.0f;
    public float rotateSpeed = 2.0f;
    public float maxCurve = 0.3f;

    public float coolDownShapeShift;

    private CapsuleCollider col;
    private Rigidbody rb;
    private Vector3 velocity;

    private GameObject mage;
    private GameObject monster;
    private GameObject player;
    private float h;

    public float coolDown = 0f;

    private GameObject gManagerObject;
    private GameManager gManager;

    private int alinhamento;

    public GameObject gameOverScreen;
    public GameObject HUDScreen;

    private AudioSource passos;

    private bool isAlive = true;

    public AudioClip mageTurn;
    public AudioClip monsterTurn;
    public AudioClip stepSound;


    public GameObject jenkyll;
    public GameObject hyde;

    private Animation animationJenkyll;
    private Animation animationHyde;



    void Start()
    {
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        player = this.gameObject;

        monster = player.transform.Find("Hyde").gameObject;
        mage    = player.transform.Find("Jekyll").gameObject;
        gManagerObject = GameObject.Find("GameController");
        gManager = gManagerObject.GetComponent<GameManager>();
        coolDown = coolDownShapeShift;

        animationJenkyll = jenkyll.GetComponent<Animation>();
        animationHyde = hyde.GetComponent<Animation>();

    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            AudioSource som = GetComponent<AudioSource>();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gManager.exitGame();
            }

            if (Input.GetKey(KeyCode.Space) && coolDown <= 0)
            {
                wizardsRules = !wizardsRules;
                coolDown = coolDownShapeShift;

                if (wizardsRules)
                    som.clip = mageTurn;
                else
                    som.clip = monsterTurn;
                som.Play();
               
                

            }

            if (wizardsRules)
            {


                monster.SetActive(false);
                mage.SetActive(true);
            }
            else {

                monster.SetActive(true);
                mage.SetActive(false);
            }

            /*
            if (Input.GetKey(KeyCode.X))
            {
                AhhMorreuPoOlhaAi();
            }
            if (Input.GetKey(KeyCode.Z))
            {
                gManager.restartGame();
            }
            */

            coolDown -= Time.deltaTime;

            bool direita = (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));
            bool esquerda = (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));

            if (direita && player.transform.rotation.y <= -maxCurve)
            {
                h = 0f;
            }
            else if (esquerda && player.transform.rotation.y >= maxCurve)
            {
                h = 0f;
            }
            else if ((player.transform.rotation.y >= -maxCurve && direita) || (esquerda && player.transform.rotation.y <= maxCurve))
            {
                h = Input.GetAxis("Horizontal");
                transform.Rotate(0, h * rotateSpeed, 0);
            }

            velocidadePlayer = velocidadeInicial + (transform.position.z / 300);

            if (velocidadePlayer >= velocidadeMaxima)
            {
                velocidadePlayer = velocidadeMaxima;
            }

            float v = velocidadePlayer;
            rb.useGravity = true;

            velocity = new Vector3(0, 0, v);
            velocity = transform.TransformDirection(velocity);
            if (v > 0.1)
            {
                velocity *= forwardSpeed;
            }
            else if (v < -0.1)
            {
                velocity *= backwardSpeed;
            }

            transform.localPosition += velocity * Time.fixedDeltaTime;
        }
        else {
            timerGameOver -= Time.deltaTime;
            gManager.CountDown(timerGameOver);
            if (timerGameOver < 0)
            {
                gManager.restartGame();
            }
        }
        
    }

    void AhhMorreuPoOlhaAi()
    {
        if (wizardsRules)
            animationJenkyll.Play("tripping");
        else
            animationHyde.Play("tripping");


        isAlive = false;
        velocidadePlayer = 0f;
        HUDScreen.SetActive(false);
        gameOverScreen.SetActive(true);
        gManager.setScore();
        if (wizardsRules) { 
            passos = GetComponent<AudioSource>();
            passos.Stop();
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Vibes") {
            Destroy(collision.gameObject);
            gManager.getOrb();
            if (wizardsRules)
            {
                alinhamento++;
                GameObject sli = GameObject.Find("Slider");
                if(alinhamento<=9)
                    sli.transform.position += new Vector3(20f, 0, 0);
                else
                    AhhMorreuPoOlhaAi();
            }
            else {
                alinhamento--;
                GameObject sli = GameObject.Find("Slider");
                if (alinhamento >= -9)
                    sli.transform.position -= new Vector3(20f, 0, 0);
                else
                    AhhMorreuPoOlhaAi();
            }
        }
      
    }
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "CoolZone")
        {
            Debug.Log("Colidiu");
            if (wizardsRules)
            {

            }
            else {
                AhhMorreuPoOlhaAi();
            }

        }
    
        else if (col.gameObject.tag == "DangerZone")
        {
            Debug.Log("Colidiu");
            if (wizardsRules)
            {
                AhhMorreuPoOlhaAi();
            }
            else {
                    
            }
        }
    }

}
