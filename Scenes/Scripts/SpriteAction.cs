using UnityEngine;
using System.Collections;

public class SpriteAction : MonoBehaviour
{
	static int hashSpeed = Animator.StringToHash ("Speed");
	static int hashFallSpeed = Animator.StringToHash ("FallSpeed");
	static int hashGroundDistance = Animator.StringToHash ("GroundDistance");
	static int hashIsCrouch = Animator.StringToHash ("IsCrouch");

	static int hashDamage = Animator.StringToHash ("Damage");
	static int hashIsDead = Animator.StringToHash ("IsDead");

	private Vector2 fromPos;
	private Vector2 toPos;

	[SerializeField] private float characterHeightOffset = 0.2f;
	[SerializeField] LayerMask groundMask;

	[SerializeField, HideInInspector] Animator animator;
	[SerializeField, HideInInspector]SpriteRenderer spriteRenderer;
	[SerializeField, HideInInspector]Rigidbody2D rig2d;

	public int hp = 4;
	public GameObject Arrow;

	void Awake ()
	{
		animator = GetComponent<Animator> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		rig2d = GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		float axis = Input.GetAxisRaw ("Horizontal");
		// bool isDown = Input.GetAxisRaw ("Vertical") < 0;
		bool isDown = Input.GetMouseButton(0);

		// if (Input.GetButtonDown ("Jump")) {
		// 	rig2d.velocity = new Vector2 (rig2d.velocity.x, 5);
		// }

		var distanceFromGround = Physics2D.Raycast (transform.position, Vector3.down, 1, groundMask);

		// update animator parameters
		animator.SetBool (hashIsCrouch, isDown);
		animator.SetFloat (hashGroundDistance, distanceFromGround.distance == 0 ? 99 : distanceFromGround.distance - characterHeightOffset);
		animator.SetFloat (hashFallSpeed, rig2d.velocity.y);
		animator.SetFloat (hashSpeed, Mathf.Abs (axis));

		// flip sprite
		if (axis != 0)
			spriteRenderer.flipX = axis < 0;

		if ( Input.GetMouseButtonDown(0) ) {
			fromPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		// Unityちゃんの上に表示する矢印
			//Arrow.gameObject.SetActive(true);
		}

		if (Input.GetMouseButton(0)){
				// Debug.Log( this.gameObject.transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition.x));
				if( this.gameObject.transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x > 0)spriteRenderer.flipX = false;
				else spriteRenderer.flipX = true;
		}	

		if ( Input.GetMouseButtonUp(0) ){
			toPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			// rig2d.velocity = new Vector2 (rig2d.velocity.x, 5);	
			rig2d.velocity = new Vector2 (-(toPos.x - fromPos.x), -(toPos.y - fromPos.y)*5);	
		}

	}
}
