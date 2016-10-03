using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 3.0f;
	public float spawnDelay = 0.5f;
	
	private bool movingRight = true;
	private float xmax;
	private float xmin;
	
	void SpawnEnemies()
	{
		foreach( Transform child in transform )
		{
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	
	void SpawnUntilFull()
	{
		Transform freePosition = NextFreePosition();
		if (freePosition)
		{
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if(NextFreePosition())
		{
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}
	
	// Use this for initialization
	void Start () {
	
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distanceToCamera));
		xmax = rightBoundary.x;
		xmin = leftBoundary.x;
		
		SpawnUntilFull();
	}
	
	public void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
	
	// Update is called once per frame
	void Update () {
		if(movingRight)
		{
			//transform.position += Vector3.right * speed * Time.deltaTime;
			transform.position += new Vector3(speed*Time.deltaTime,0);
		}
		else
		{
			transform.position += new Vector3(-speed*Time.deltaTime,0);
		}
		float rightEdgeOfFormation = transform.position.x + (0.5f*width);
		float leftEdgeOfFormation = transform.position.x - (0.5f*width);
		if(rightEdgeOfFormation > xmax)
		{
			movingRight = false;
		}
		else if (leftEdgeOfFormation < xmin)
		{
			movingRight = true;
		}
		
		if (AllMembersDead())
		{
			Debug.Log ("Empty Formation");
			SpawnUntilFull();
		}
	}
	
	Transform NextFreePosition()
	{
		foreach(Transform childPositionGameObject in transform)
		{
			if (childPositionGameObject.childCount == 0)
			{
				return childPositionGameObject;
			}
		}
		return null;
	}
	
	
	bool AllMembersDead()
	{
		foreach(Transform childPositionGameObject in transform)
		{
			if (childPositionGameObject.childCount > 0)
			{
				return false;
			}
		}
		return true;
	}
}
