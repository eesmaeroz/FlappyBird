using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    public float jumpForce = 5f;
    public Text scoreText;
    public GameObject gameOverPanel;

    [Header("Sounds")]
    public AudioClip swooshSound;
    public AudioClip flapSound;
    public AudioClip pointSound;
    public AudioClip collisionSound;
    public AudioClip dieSound;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private bool isDead = false;
    private int score = 0;

    void Start()
    {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        PlaySound(swooshSound);
    }

    void Update()
    {
        if (isDead) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
            PlaySound(flapSound);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        isDead = true;
        PlaySound(collisionSound);
        PlaySound(dieSound);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        score++;
        scoreText.text = score.ToString();
        PlaySound(pointSound);
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}