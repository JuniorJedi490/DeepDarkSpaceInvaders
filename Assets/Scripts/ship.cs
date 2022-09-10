using UnityEngine;

public class ship : MonoBehaviour
{
	public float spawnAttemptTime;
	public float speed;
	private Vector3 _dir;
	private int _timeTillNext;
	
	private void Awake()
	{
		RandomizeDirection();
	}
	
    private void Start()
    {
        InvokeRepeating(nameof(TrySpawnShip), this.spawnAttemptTime, this.spawnAttemptTime);
    }
	
	private void Update()
    {
        this.transform.position += _dir * this.speed * Time.deltaTime;
    }
	
	private void TrySpawnShip()
	{
		--_timeTillNext;
		if (_timeTillNext <= 0) SpawnShip();
	}
	
	private void SpawnShip()
	{
		RandomizeDirection();
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
		if (_dir == Vector3.right)
		{
			this.transform.position = new Vector3(leftEdge.x - 1.0f, this.transform.position.y, this.transform.position.z);
		}
		else
		{
			this.transform.position = new Vector3(rightEdge.x + 1.0f, this.transform.position.y, this.transform.position.z);
		}
		this.gameObject.SetActive(true);
	}
	
	private void RandomizeDirection()
	{
		if (Random.value < 0.5)
		{
			_dir = Vector2.right;
		} else {
			_dir = Vector2.left;
		}
		_timeTillNext = 2 + (int) (Random.value * 10.0f);
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Lazer")) {
			this.gameObject.SetActive(false);
		}
	}
}
