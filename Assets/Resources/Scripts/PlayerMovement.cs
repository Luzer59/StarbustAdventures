using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerMovement : MonoBehaviour
{
    //public float dashDistance;
    public float dashSpeed;
    public float dashDuration;
    public float speed;
    public Boundary boundary;

    private bool dashActive = false;
    private float dashTimer = 0f;
    private float dashHorizontal = 0f;
    private float dashVertical = 0f;

    private SpriteRenderer sprite;
    private PlayerController playerController;

    void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        playerController = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerController>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(1))
        {
            if (!dashActive && playerController.dash >= 1f)
            {
                if (moveHorizontal == 0f && moveVertical == 0f)
                {
                    // empty
                }
                else
                {
                    playerController.dash = 0f;
                    sprite.color = new Color(255f, 255f, 0f);
                    GetComponent<GameObjectPublicValueHandler>().invincible = true;
                    dashActive = true;
                    dashHorizontal = moveHorizontal;
                    dashVertical = moveVertical;
                }
            }
        }

        Vector3 movement;
        if (!dashActive)
        {
            movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
            transform.Translate(movement * speed);
        }
        else
        {
            movement = new Vector3(dashHorizontal, dashVertical, 0.0f);
            transform.Translate(movement * dashSpeed);
            dashTimer += Time.deltaTime;
            if (dashTimer >= dashDuration)
            {
                sprite.color = new Color(255f, 255f, 255f);
                GetComponent<GameObjectPublicValueHandler>().invincible = false;
                dashActive = false;
                dashTimer = 0f;
            }
        }
        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax), // x
            Mathf.Clamp(transform.position.y, boundary.yMin, boundary.yMax), // y
            0.0f // z
        );
    }
}
