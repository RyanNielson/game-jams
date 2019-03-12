using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float rotationSpeed = 1f;

	public float speed = 10f;

    public RuntimeAnimatorController player1Animator;

    public RuntimeAnimatorController player2Animator;

	private Vector2 direction = Vector2.zero;
	
    private Animator animator;

    private GameController gameController;

    private void Awake()
    {
        animator = transform.FindChild("Sprite").GetComponent<Animator>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        if (!GameData.playerAnimatorController)
        {
            if (Random.Range(0, 2) == 0)
            {
                GameData.playerAnimatorController = player1Animator;
            }
            else
            {
                GameData.playerAnimatorController = player2Animator;
            }
        }

        animator.runtimeAnimatorController = GameData.playerAnimatorController;
    }

	private void Update()
	{
        if (gameController.started)
        {
		    HandleMovement();
        }
	}
	
	private void FixedUpdate()
	{
		rigidbody2D.MovePosition((Vector2)transform.position + direction * speed * Time.deltaTime);
	}
	
	private void HandleMovement()
	{
        float xDirection = Input.GetAxis("Horizontal");
		float yDirection = Input.GetAxis("Vertical");

        animator.SetBool("Running", xDirection != 0 || yDirection != 0);
     
		direction = (new Vector2(xDirection, yDirection)).normalized;
	}
	
	private void HandleRotation()
	{
		Vector3 position = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 rotationDirection = Input.mousePosition - position;
		float angle = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg;
		
		if (angle > 90 || angle < -90)
		{
			transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
		}
		else
		{
			transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
		}
	}
}