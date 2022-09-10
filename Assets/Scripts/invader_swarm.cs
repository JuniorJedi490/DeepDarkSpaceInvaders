using UnityEngine;
using UnityEngine.SceneManagement;

public class invader_swarm : MonoBehaviour
{
	public int rows = 5;
	public int cols = 11;
	public invader[] prefabs;
	
	// Mathematical formula, not solely useful for animation
	public AnimationCurve speed;
	
	public float missileAttackRate;
	public projectile missilePrefab;
	
	public int amtKilled { get; private set; }
	public int totalInvaders => this.rows * this.cols;
	public float pctKilled => (float) this.amtKilled / (float) this.totalInvaders;
	
	private Vector3 _dir = Vector2.right;
	
	private void Awake()
	{
		for (int i = 0; i < rows; ++i) {
			float wid = 2.0f * (this.cols - 1);
			float high = 2.0f * (this.rows - 1);
			Vector2 centering = new Vector2(-wid/2, -high/2);
			Vector3 rowPosition = new Vector3(centering.x, centering.y + i * 2.0f, 0.0f);
			for (int j = 0; j < cols; ++j) {
				invader invaderz = Instantiate(this.prefabs[i], this.transform);
				invaderz.killed = InvaderKilled;
				Vector3 position = rowPosition;
				position.x += j * 2.0f;
				invaderz.transform.localPosition = position;
			}
		}
	}
	
	private void Start()
	{
		InvokeRepeating(nameof(MissileAttack), this.missileAttackRate, this.missileAttackRate);
	}
	
	private void Update()
	{
		this.transform.position += _dir * this.speed.Evaluate(this.pctKilled) * Time.deltaTime;
		
		// Screen edges
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
		
		foreach(Transform invaderz in this.transform)
		{
			if (!invaderz.gameObject.activeInHierarchy) {
				continue;
			}
			if ((_dir == Vector3.right && invaderz.position.x >= (rightEdge.x - 1.0f))
				|| (_dir == Vector3.left && invaderz.position.x <= (leftEdge.x + 1.0f))) {
				AdvanceRow();
			}
		}
	}
	
	private void AdvanceRow()
	{
		_dir.x *= -1.0f;
		Vector3 position = this.transform.position;
		position.y -= 1.0f;
		this.transform.position = position;
	}
	
	private void MissileAttack()
	{
		foreach(Transform invaderz in this.transform)
		{
			if (!invaderz.gameObject.activeInHierarchy) {
				continue;
			}
			
			if (Random.value < (1.0f / (float) (this.totalInvaders + 1 - this.amtKilled)))
			{
				Instantiate(this.missilePrefab, invaderz.position, Quaternion.identity);
				break;
			}
				
			//
		}
	}
	
	private void InvaderKilled()
	{
		++this.amtKilled;
		if (this.amtKilled >= this.totalInvaders) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
	
}
