using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private int jumpForce = 600;
    public float gravityModifier = 1;
    private bool isOnGround = false;
    public bool gameOver = false;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioSource audioPlayer;
    private bool _isSecondJumped = false;
    private bool _isDashed = false;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
        SpawnManager.StartDash += Dash;
        SpawnManager.StartDash += EndDash;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isSecondJumped && !gameOver)
        {
            if (!isOnGround)
            {
                _isSecondJumped = true;
            }
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            audioPlayer.PlayOneShot(jumpSound);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            SpawnManager.StartDash?.Invoke();
            _isDashed = true;
        }
        else if (_isDashed)
        {
            SpawnManager.EndDash?.Invoke();
            _isDashed = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {

            Debug.Log("Game Over");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            audioPlayer.PlayOneShot(crashSound);
        }
        else if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            isOnGround = true;
            _isSecondJumped = false;
            dirtParticle.Play();
        }

    }
    private void Dash()
    {
        playerAnim.SetFloat("Speed_f", 2f);
    }
    private void EndDash()
    {
        playerAnim.SetFloat("Speed_f", 1f);
    }

    private void OnDestroy()
    {
        SpawnManager.StartDash -= Dash;
        SpawnManager.StartDash -= EndDash;
    }
}
