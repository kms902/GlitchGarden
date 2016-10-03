using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	
	public float health = 150;
	public GameObject projectile;
	public float projectileSpeed = 5;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 150;
	public AudioClip fireSound;
	public AudioClip deathSound;
	
	private ScoreKeeper scoreKeeper;
	
	void Start()
	{
		// old code:
		// scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
		// this code wouldn't work, but the below code does
		// Unity community says to never use GameObject.Find() as it is slow and
		// computationally expensive
		scoreKeeper = FindObjectOfType<ScoreKeeper>();
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile)
		{
			health -= missile.GetDamage();
			missile.Hit ();
			if (health <= 0)
			{
				Die ();
			}
			Debug.Log ("Hit by a projectile");
		}
	}
	
	void Die()
	{
		Destroy (gameObject);
		scoreKeeper.Score(scoreValue);
		
		// Play Death Sound
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
	}
	
	
	void Fire()
	{
		Vector3 startPosition = transform.position + new Vector3(0, -0.5f,0);
		GameObject missile = Instantiate (projectile, startPosition, Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
		
		// Play Fire Sound
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	
	
	void Update()
	{
		float probability = Time.deltaTime * shotsPerSecond;
		if(Random.value < probability)
		{
			Fire();
		}
	}
}
