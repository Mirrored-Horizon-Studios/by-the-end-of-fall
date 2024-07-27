using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornDropper : MonoBehaviour {
	[SerializeField] int minDropAmmount, maxDropAmmount;

	[SerializeField] float minSecsBetweenDrops, maxSecsBetweenDrops, minDropOffset, maxDropOffset;
	[SerializeField] Transform[] dropPoints;
	[SerializeField] GameObject acorn;

	private bool hasStarted = false;
	private float waitTimeToDrop;

	private GameObject acornParent;
	private const string ACORN_PARENT_NAME = "Acorns";

	// Use this for initialization
	void Start () {
		waitTimeToDrop = Random.Range(minSecsBetweenDrops, maxSecsBetweenDrops);
		createAcornContainer();
	}
	
	// Update is called once per frame
	void Update () {
		if(hasStarted){
			waitTimeToDrop -= Time.deltaTime;

			if(waitTimeToDrop <= 0){
				int ammountToDrop = Random.Range(minDropAmmount, maxDropAmmount);
				
				for(int i = 0; i < ammountToDrop; i++){
					StartCoroutine(dropAcorn());
				}

				waitTimeToDrop = Random.Range(minSecsBetweenDrops, maxSecsBetweenDrops);
			}
		}
	}

	IEnumerator dropAcorn(){
		float dropDelay = Random.Range(minDropOffset, maxDropOffset);

		yield return new WaitForSeconds(dropDelay);

		spawnAcorn();
	}

	private void spawnAcorn(){
		int dropPointIndex = Random.Range(0, dropPoints.Length);
		GameObject acornDuplicate = Instantiate(acorn, dropPoints[dropPointIndex].position, Quaternion.identity);
		acornDuplicate.transform.parent = acornParent.transform;
	}

	public void startSpawning(){
		hasStarted = true;
	}

	public void stopSpawning(){
		hasStarted = false;
	}

	private void createAcornContainer(){
		if( !acornParent && !GameObject.Find(ACORN_PARENT_NAME)){
			acornParent = new GameObject(ACORN_PARENT_NAME);
		}
	}
}
