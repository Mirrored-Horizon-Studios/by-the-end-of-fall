using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

	Vector2 startingPoint;
	[SerializeField] GameObject groundTop;

	[SerializeField] int minChunkSize = 1;
	[SerializeField] int maxChunkSize = 10;

	[SerializeField] int maxHeight = 1;
	[SerializeField] int maxDrop = -1;

	[SerializeField] int chunks = 20;

	int blockHeight;
	int blockNumber;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetStartingPoint(Vector2 startingPoint){this.startingPoint = startingPoint;}
	public void SetChunks(int chunks){this.chunks = chunks;}

	public void Build(){
		blockHeight = Mathf.RoundToInt(startingPoint.y);
		blockNumber = Mathf.RoundToInt(startingPoint.x);

		Instantiate(groundTop, startingPoint, Quaternion.identity);

		blockNumber++;

		for(int platform = 0; platform < chunks; platform++){
			int platformSize = Mathf.RoundToInt(Random.Range(minChunkSize, maxChunkSize));
			blockHeight = blockHeight + Random.Range(maxDrop, maxHeight);

			for(int tiles = 0; tiles < platformSize; tiles++){
				Vector2 nextPos = new Vector2(blockNumber, blockHeight);

				Instantiate(groundTop, nextPos, Quaternion.identity);

				blockNumber++;
			}	
		}
	}
}
