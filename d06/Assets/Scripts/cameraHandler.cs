using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cameraHandler : MonoBehaviour
{
    public float mainSpeed = 100.0f;
    public float shiftAdd = 250.0f;
    public float maxShift = 1000.0f;
    public float camSens = 0.25f;
    private Vector3 lastMouse = new Vector3(255, 255, 255);
    private float totalRun = 1.0f;

    private Rigidbody rigidBodyCam;

    public AudioSource panicMusic;

    public AudioSource normalMusic;

    public AudioSource winMusic;

    public AudioSource doorOpened;

    public AudioSource accesDenied;

    public AudioSource grabCard;

    public AudioSource footStep;

    public Text screenText;

    private bool hasCard = false;

    public GameObject card;

    public GameObject door;

    private IEnumerator routine = null;

    private bool routineIsRunning = false;

    private bool doorOpen = false;

    void Start()
    {
        rigidBodyCam = GetComponent<Rigidbody>();
        normalMusic.loop = true;
        panicMusic.loop = true;
        screenText.text = "Infiltrade the base\nand find the documents.";
        StartCoroutine(FadeTextToFullAlpha(1f, screenText));
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        routineIsRunning = true;

        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        yield return new WaitForSeconds(3);
        StartCoroutine(FadeTextToZeroAlpha(1f, screenText));
        routineIsRunning = false;
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    void FixedUpdate()
    {
        if (progressBar.detectionPercent >= 100 && !routineIsRunning)
        {
            screenText.text = "DETECTED.\nYou loose.";
            StartCoroutine(FadeTextToFullAlpha(1f, screenText));
        }
        if (!panicMusic.isPlaying && progressBar.detectionPercent >= 70)
        {
            normalMusic.Stop();
            panicMusic.Play();
        }
        else if (!normalMusic.isPlaying && progressBar.detectionPercent < 70)
        {
            normalMusic.Play();
            panicMusic.Stop();
        }
        lastMouse = Input.mousePosition - lastMouse;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse = Input.mousePosition;

        Vector3 p = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            totalRun += Time.deltaTime;
            p = p * totalRun * shiftAdd;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
        }
        else
        {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p = p * mainSpeed;
        }

        p = p * Time.deltaTime;
        Vector3 newPosition = transform.position;
        transform.Translate(p);
        newPosition.x = transform.position.x;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
        rigidBodyCam.velocity = Vector3.zero;
        rigidBodyCam.angularVelocity = Vector3.zero;
    }

    private Vector3 GetBaseInput()
    {
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            if (!footStep.isPlaying)
                footStep.Play();
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (!footStep.isPlaying)
                footStep.Play();
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (!footStep.isPlaying)
                footStep.Play();
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (!footStep.isPlaying)
                footStep.Play();
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }

    void OnCollisionEnter(Collision col)
    {
        transform.Translate(Vector3.zero);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "prop_keycard" && !routineIsRunning && !hasCard)
        {
            screenText.text = "Press 'E' to grab the card.";
            routine = FadeTextToFullAlpha(1f, screenText);
            StartCoroutine(routine);
        }
        else if (col.gameObject.name == "prop_switchUnit" && !routineIsRunning && !hasCard && !doorOpen)
        {
            screenText.text = "You need to find the acces card.";
            accesDenied.Play();
            routine = FadeTextToFullAlpha(1f, screenText);
            StartCoroutine(routine);
        }
        else if (col.gameObject.name == "prop_switchUnit" && !routineIsRunning && hasCard && !doorOpen)
        {
            screenText.text = "Press 'E' to use the card.";
            routine = FadeTextToFullAlpha(1f, screenText);
            StartCoroutine(routine);
        }
        else if (col.gameObject.name == "prop_television" && !routineIsRunning)
        {
            screenText.text = "Press 'E' to steal the documents (hide in the TV).";
            routine = FadeTextToFullAlpha(1f, screenText);
            StartCoroutine(routine);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "prop_keycard" && !hasCard && Input.GetKeyDown(KeyCode.E))
        {
            card.SetActive(false);
            grabCard.Play();
            hasCard = true;
        }
        else if (col.gameObject.name == "prop_switchUnit" && hasCard && Input.GetKey(KeyCode.E))
        {
            hasCard = false;
            doorOpen = true;
            StopAllCoroutines();
            screenText.text = "The door is open";
            doorOpened.Play();
            routine = FadeTextToFullAlpha(1f, screenText);
            StartCoroutine(routine);
            door.transform.Translate(new Vector3(3, 0, 0));
        }
        else if (col.gameObject.name == "prop_television" && Input.GetKey(KeyCode.E))
        {
            StopAllCoroutines();
            winMusic.Play();
            screenText.text = "You Win !";
            routine = FadeTextToFullAlpha(1f, screenText);
            StartCoroutine(routine);
            StartCoroutine(ReloadLevel());
        }
    }

    public IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
