using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum RPSType
{
	Rock,
	Paper,
	Scissors
}

public class RPSItem : MonoBehaviour
{
	public float speed;
	public float targetRotationSpeed;
	public RPSType type;

	private GameManager _gameManager;
	private Camera _cam;
	private SpriteRenderer _spriteRenderer;
	private Rigidbody2D _rb;
	private Vector3 _direction;
	private Vector3 _targetDirection;

    void Awake()
    {
		_gameManager = FindObjectOfType<GameManager>();
		_cam = Camera.main;
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_rb = GetComponent<Rigidbody2D>();
    }

	private void Start()
	{
		_direction = Vector3.up;
		_targetDirection = Random.insideUnitCircle;
	}

	void Update()
    {
		//float speed = this.speed * Random.Range(-1f, 1f);
		//Vector3 tpos = Random.insideUnitCircle * speed * Time.deltaTime;
		//transform.Translate(tpos);

		// Recalculate rot anchor sometimes
		if (Random.value < .05f)
		{
			_targetDirection = Random.insideUnitCircle;
		}

		_direction = Vector3.RotateTowards(_direction, _targetDirection, targetRotationSpeed * Time.deltaTime, 0f);
		transform.Translate(_direction * speed * Time.deltaTime);

		// Draw a ray pointing at our target
		Debug.DrawRay(transform.position, _targetDirection.normalized, Color.red);
		Debug.DrawRay(transform.position, _direction.normalized, Color.blue);

		// Correct border pos
		Vector3 maxBounds = _cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		Vector3 minBounds = _cam.ScreenToWorldPoint(Vector3.zero);
		Vector3 pos = transform.position;
		pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
		pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);
		transform.position = pos;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		switch (collider.GetComponent<RPSItem>().type)
		{
			case RPSType.Rock:
				if (type == RPSType.Scissors)
					SetType(RPSType.Rock);
				break;
			case RPSType.Paper:
				if (type == RPSType.Rock)
					SetType(RPSType.Paper);
				break;
			case RPSType.Scissors:
				if (type == RPSType.Paper)
					SetType(RPSType.Scissors);
				break;
			default:
				break;
		}
	}

	public void SetType(RPSType type)
	{
		switch (type)
		{
			case RPSType.Rock:
				_spriteRenderer.sprite = _gameManager.rockSprite;
				this.type = RPSType.Rock;
				break;
			case RPSType.Paper:
				_spriteRenderer.sprite = _gameManager.paperSprite;
				this.type = RPSType.Paper;
				break;
			case RPSType.Scissors:
				_spriteRenderer.sprite = _gameManager.scissorsSprite;
				this.type = RPSType.Scissors;
				break;
			default:
				break;
		}
	}
}
