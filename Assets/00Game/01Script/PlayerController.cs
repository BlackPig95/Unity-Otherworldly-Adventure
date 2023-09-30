using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum PlayerState
{
    Idle,
    Run,
    DoubleJump,
    WallSlide,
    Jump,
}
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigi;
    [SerializeField] Collider2D colli;
    [SerializeField] float speed;
    [SerializeField] LayerMask playerLayerMask;
    Vector2 movement = Vector2.zero;
    [SerializeField] float jumpForce;
    [SerializeField]
    bool canDoubleJump = false, isGrounded = true,
        isJumping = false, isDoubleJumping = false, isWallSliding = false;
    float wallJumpCounter = 0f;
    [SerializeField] PlayerState playerState = PlayerState.Idle;
    AnimationController playerAnimationController;

    // Start is called before the first frame update
    void Start()
    {
        rigi = this.GetComponent<Rigidbody2D>();
        colli = this.GetComponent<Collider2D>();
        playerAnimationController = this.GetComponentInChildren<AnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        Jump();
        isWallSliding = WallSlide();
        if (DetectWall())
        {
            WallJump();
        }
        wallJumpCounter -= Time.deltaTime;
        MovePlayer();
        if (wallJumpCounter > 0f) //Avoid bug player can jump twice due to raycast
            canDoubleJump = false;
        SetPlayerState();
        playerAnimationController.UpdatePlayerAnim(playerState);
    }
    private void FixedUpdate()
    {
        rigi.velocity = movement;
    }
    void SetPlayerState()
    {
        if (isGrounded)
        {
            isJumping = false;
        }

        if (isJumping)
        {
            if (isDoubleJumping)
                playerState = PlayerState.DoubleJump;
            else
                playerState = PlayerState.Jump;

            if (rigi.velocity.y < 0f)
                isDoubleJumping = false;

            playerAnimationController.UpdatePlayerAnim(rigi.velocity.y);
        }
        if (isWallSliding)
        {
            playerState = PlayerState.WallSlide;
            return;
        }

        if (!isGrounded)
            return;

        if (Input.GetAxisRaw(CONSTANT.horizontalInput) != 0)
        {
            playerState = PlayerState.Run;
            return;
        }

        playerState = PlayerState.Idle;
    }
    void MovePlayer()
    {
        if (wallJumpCounter > 0) // Give some time to finish wall jump
        {
            movement.x = 0f;
            movement.y = rigi.velocity.y;
            rigi.velocity = movement;
            return;
        }
        movement.x = Input.GetAxisRaw(CONSTANT.horizontalInput) * speed;
        movement.y = rigi.velocity.y;
    }
    float RotatePlayer()
    {
        Vector2 scale = Vector2.one;
        if (Input.GetAxisRaw(CONSTANT.horizontalInput) != 0)
            scale.x *= Input.GetAxisRaw(CONSTANT.horizontalInput);
        this.transform.localScale = scale;
        return this.transform.localScale.x;
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Seperate if statement to control second jump velocity easily
            if (isGrounded)
            {
                rigi.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
                canDoubleJump = true;
                isGrounded = false;
                isJumping = true;
                return;
            }
            if (canDoubleJump)
            {
                this.rigi.velocity = Vector2.zero;
                canDoubleJump = false;
                rigi.AddForce(jumpForce * Vector2.up * 0.8f, ForceMode2D.Impulse);
                isDoubleJumping = true;
                return;
            }
        }
    }
    void WallJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigi.AddForce(new Vector2(1500f * -RotatePlayer(), 550f)); //Force mode Impulse can't work??
            wallJumpCounter = 0.2f;
            isJumping = true;
            return;
        }
    }
    private void OnCollisionStay2D(Collision2D collision) //Collision Enter will cause bug 3 times jump when in corner
    {
        GroundCheck();
    }
    bool DetectWall()
    {
        if (isGrounded) //Only cast ray if player if not grounded
            return false;

        if (Input.GetAxisRaw(CONSTANT.horizontalInput) == 0) //No need to cast ray if player if not wall sliding
            return false;
        // Bug in corner due to raycast
        int rayCount = 5;
        float outOfBound = 0.02f; //Distance of the raycast outside of player collider
        float castRangeY = colli.bounds.size.x / 2 + outOfBound;
        float stepY = this.colli.bounds.size.y / (rayCount - 1);
        float yPos = this.transform.position.y - colli.bounds.size.y / 2;
        for (int i = 1; i < rayCount - 1; i++)
        {
            Vector2 castPosY = new Vector2(this.transform.position.x, yPos + i * stepY);
            //Raycast to the right
            RaycastHit2D rightRay = Physics2D.Raycast(castPosY, Vector2.right, castRangeY, playerLayerMask);

            //Raycast to the left
            RaycastHit2D leftRay = Physics2D.Raycast(castPosY, Vector2.left, castRangeY, playerLayerMask);

            //Wall slide

            if (rightRay.collider != null)
            {
                canDoubleJump = true;
                return true;
            }
            if (leftRay.collider != null) // Bug fall fast when press key to opposite direction
            {
                canDoubleJump = true;
                return true;
            }
        }
        return false;
    }
    void GroundCheck()
    {
        float castRangeGround = colli.bounds.size.y / 2 + 0.02f;
        int rayCount = 5;
        float stepX = this.colli.bounds.size.x / (rayCount - 1);
        float xPos = this.transform.position.x - colli.bounds.size.x / 2;
        for (int i = 0; i < rayCount; i++)
        {
            Vector2 castPosX = new Vector2(xPos + i * stepX, this.transform.position.y);
            RaycastHit2D groundCheck = Physics2D.Raycast(castPosX, Vector2.down, castRangeGround, playerLayerMask);
            if (groundCheck.collider == null)
            {
                isGrounded = false; //Avoid bug stuck to wall when fall off platform
                isJumping = true;
                return;
            } // Avoid colliding with player null ref exception
            if (groundCheck.collider.gameObject.CompareTag(CONSTANT.groundTag))
            {
                isGrounded = true;
                canDoubleJump = true; //Allow Player jump once when fall off platform
            }
        }
    }
    bool WallSlide()
    {
        if (!DetectWall())
            return false;
      
        rigi.velocity = new Vector2(0f, -4f);
        return true;

    }
    void OnDrawGizmosSelected()
    {
        int rayCount = 5;
        float outOfBound = 0.1f;
        for (int i = 1; i < rayCount - 1; i++)
        {
            //Show Raycast Horizontally
            float stepY = this.colli.bounds.size.y / (rayCount - 1);
            float yPos = this.transform.position.y - colli.bounds.size.y / 2;
            float castRangeY = colli.bounds.size.x / 2 + outOfBound;
            Vector2 castPosY = new Vector2(this.transform.position.x, yPos + i * stepY);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(castPosY, Vector2.right * castRangeY);
            Gizmos.DrawRay(castPosY, Vector2.left * castRangeY);

        }

        // Show Raycast Vertically
        for (int i = 0; i < rayCount; i++)
        {
            float castRangeGround = colli.bounds.size.y / 2 + 0.02f;
            float stepX = this.colli.bounds.size.x / (rayCount - 1);
            float xPos = this.transform.position.x - colli.bounds.size.x / 2;
            Vector2 castPosX = new Vector2(xPos + i * stepX, this.transform.position.y);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(castPosX, Vector2.down * castRangeGround);
        }
    }
}
