using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
	public projectile lazerPrefab;
	public float speed = 5.0f;
	private bool _lazerActive;
	
    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			this.transform.position += Vector3.left * this.speed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			this.transform.position += Vector3.right * this.speed * Time.deltaTime;
		}
		
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
			Shoot();
		}
    }
	
	private void Shoot() {
		if (!_lazerActive) {
			projectile lazer = Instantiate(this.lazerPrefab, this.transform.position, Quaternion.identity);
			lazer.destroyed += LazerDestroyed;
			_lazerActive = true;
		}
		
	}
	
	private void LazerDestroyed() {
		_lazerActive = false;
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Invader")
			|| other.gameObject.layer == LayerMask.NameToLayer("Missile")) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
