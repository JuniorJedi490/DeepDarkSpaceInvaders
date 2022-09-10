using UnityEngine;

public class invader : MonoBehaviour
{
	public Sprite[] animationSprites;
	public float animationTime;
	public System.Action killed;
	private SpriteRenderer _spriteRenderer;
	private int _animFrame;
	
	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }
	
	private void AnimateSprite()
	{
		_animFrame++;
		if (_animFrame >= this.animationSprites.Length) {
			_animFrame = 0;
		}
		
		_spriteRenderer.sprite = this.animationSprites[_animFrame];
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Lazer")) {
			this.gameObject.SetActive(false);
			this.killed.Invoke();
		}
	}
}
