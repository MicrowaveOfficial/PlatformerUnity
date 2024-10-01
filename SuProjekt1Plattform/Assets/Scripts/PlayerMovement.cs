using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private float ogMoveSpeed;
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] private LayerMask whatIsGround;
    private float horizontalValue;
    private bool isGrounded;
    private bool canMove;
    private float rayDistance = 0.3f;

    public bool canDash;
    [SerializeField] private float dashBoost;
    [SerializeField] private float dashDuration;

    public int startingHealth = 5;
    public int currentHealth = 0;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image healthColor;
    [SerializeField] private Color greenHealth, yellowHealth, redHealth;
    [SerializeField] private AudioClip pickupSound, takeDamage, death;
    [SerializeField] private AudioClip[] jumpSounds;

    public int redGemsCollected = 0;
    [SerializeField] private TMP_Text redGemText;
    [SerializeField] private GameObject gemParticles, dustParticles;

    private Rigidbody2D RigBody;
    private SpriteRenderer sprRend;
    private Animator anim;
    private AudioSource audioSource;
    [SerializeField] private bool reloadSceneWhenKilled = true;
    [SerializeField] private bool startPosition = true;

    void Start()
    {
        ogMoveSpeed = moveSpeed;
        currentHealth = startingHealth;
        redGemText.text = "" + redGemsCollected;
        canMove = true;
        canDash = true;

        RigBody = GetComponent<Rigidbody2D>();
        sprRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if(startPosition == true)
        {
            transform.position = spawnPosition.position;
        }
    }

    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");

        if(horizontalValue < 0)
        {
            FlipSprite(true);
        }
        else if(horizontalValue > 0) 
        {
            FlipSprite(false);
        }

        if (Input.GetButtonDown("Jump") && groundCheck() == true) 
        {
            Jump();
        }

        if (Input.GetKeyDown("space") && canDash == true && !groundCheck())
        {
            Dash();
        }

        /* if(Input.GetKey*/

        anim.SetFloat("MoveSpeed", Mathf.Abs(RigBody.velocity.x));
        anim.SetFloat("VerticalSpeed", RigBody.velocity.y);
        anim.SetBool("IsGrounded", groundCheck());
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }

            RigBody.velocity = new Vector2(horizontalValue * moveSpeed * Time.deltaTime, RigBody.velocity.y);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RedGem"))
        {
            Destroy(other.gameObject);
            redGemsCollected++;
            redGemText.text = "" + redGemsCollected;
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(pickupSound, 0.7f);
            Instantiate(gemParticles, other.transform.position, Quaternion.identity);
        }

        if (other.CompareTag("Health"))
        {
            RestoreHealth(other.gameObject);
        }
    }

    private void FlipSprite(bool direction)
    {
        sprRend.flipX = direction;
    }

    private void Jump()
    {
        RigBody.AddForce(new Vector2(0, jumpForce));
        int randomValue = Random.Range(0, jumpSounds.Length);
        audioSource.PlayOneShot(jumpSounds[randomValue], 0.4f);
        Instantiate(dustParticles, transform.position, dustParticles.transform.localRotation);
    }

    private void Dash()
    {
        canDash = false;
        moveSpeed = moveSpeed * dashBoost;
        Invoke("StopDash", dashDuration);
    }

    public void StopDash()
    {
        moveSpeed = ogMoveSpeed;
    }

    public void EnableDash()
    {
        canDash = true;
    }

    private void Attack()
    {

    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            KillPlayer();
        }
        else
        {
            audioSource.PlayOneShot(takeDamage, 1f);
        }
    }

    public void TakeKnockback(float knockForce, float upwards)
    {
        canMove = false;
        RigBody.AddForce(new Vector2(knockForce, upwards));
        Invoke("CanMoveAgain", 0.25f);
    }

    private void CanMoveAgain()
    {
        canMove = true;
    }

    private void Respawn()
    {
        currentHealth = startingHealth;
        UpdateHealthBar();
        transform.position = spawnPosition.position;
        RigBody.velocity = Vector2.zero;
    }

    public void UpdateHealthBar()
    {

        healthSlider.value = currentHealth;

        if(currentHealth >= 4)
        {
            healthColor.color = Color.green;
        }
        else if(currentHealth == 3)
        {
            healthColor.color = yellowHealth;
        }
        else
        {
            healthColor.color = redHealth;
        }
    }

    private void RestoreHealth(GameObject healthPickup)
    {
        if (currentHealth >= startingHealth)
        {
            return;
        }
        else
        {
            audioSource.PlayOneShot(pickupSound, 1f);
            int healthToRestore = healthPickup.GetComponent<HealthPickup>().healthAmount;
            currentHealth += healthToRestore;
            UpdateHealthBar();
            Destroy(healthPickup);

            if(currentHealth > startingHealth)
            {
                currentHealth = startingHealth;
            }
        }
    }

    private bool groundCheck()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, rayDistance, whatIsGround);
        //Debug.DrawRay(leftFoot.position, Vector2.down * rayDistance, Color.blue, 0.25f);

        if (leftHit.collider != null && leftHit.collider.CompareTag("Ground") || rightHit.collider != null && rightHit.collider.CompareTag("Ground"))
        {
            canDash = true;
            return true;          
        }

        else
        {
            return false;
        }
    }

    public void KillPlayer()
    {
        audioSource.PlayOneShot(death, 1f);

        if (reloadSceneWhenKilled)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Respawn();
        }
    }
}
