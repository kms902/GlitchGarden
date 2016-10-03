using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 15.0f;
	public float padding = 0.5f;
	public GameObject projectile;
	public float projectileSpeed;
	public float firingRate = 0.2f;
	public float health = 500f;
	public AudioClip fireSound;
	public LevelManager man;

	float xmin;
	float xmax;
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log ("Player collided with missile");
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile)
		{
			Debug.Log ("Player collided with missile");
			health -= missile.GetDamage();
			missile.Hit ();
			if (health <= 0)
			{
				Die ();
			}
		}
	}
	
	void Die()
	{
		//LevelManager man = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel ("Win Screen");
		Destroy (gameObject);
	}
	
	void Start()
	{
		// Use Camera.ViewportToWorldPoint() to work out the boundaries of the playspace
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}
	
	void Fire()
	{
		Vector3 offset = new Vector3(0, 0.5f, 0);
		GameObject beam = Instantiate(projectile, transform.position + offset, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
		
		// Play Fire sound
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}
		if (Input.GetKeyUp (KeyCode.Space))
		{
			CancelInvoke("Fire");
		}
		
		if (Input.GetKey (KeyCode.D))
		{
			// transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else if (Input.GetKey (KeyCode.A))
		{		
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		
		// restrict the player to the gamespace
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
}
