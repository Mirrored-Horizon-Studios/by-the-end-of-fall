using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
[Header("Ground Assets")]
[SerializeField] GameObject groundTop, groundTopEdgeRight, groundTopEdgeLeft;

[Header("Segment Configuration")]
[SerializeField] float  startY, startX, groundAssetWidth;
[SerializeField] int mapSegmentLength, startZoneLength, groundChunkMin, groundChunkMax;
[SerializeField] float difficultyFactor = 1.0f;

[SerializeField] GameObject thorns;

[Header("Pit Configuration")]
[SerializeField] float pitSpawnChance, thornSpawnChance, masterMaxHazardChanceCap;
[SerializeField] int pitMinLength, pitMaxLength, pitMinLengthCap, pitMaxLengthCap;

[Header("Test Parameters")]
[SerializeField] bool thornsEnabled, pitsEnabled = true;

private GameObject groundParent;
const string GROUND_PARENT_NAME = "Ground";

private GameObject hazardParent;
const string HAZARD_PARENT_NAME = "Hazard";

private float lastBlockX;

private bool justSpawnedHazard = false;

private Vector2 lastGroundPosition;
	// Use this for initialization
	void Start () {
		createGroundContainer();
		createHazardContainer();
		generateStartZone();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void generateStartZone(){
		lastBlockX = startX;
		
		for(int i = 0; i < startZoneLength; i++){
			GameObject groundBlock = Instantiate(groundTop, new Vector2(lastBlockX, startY), Quaternion.identity);
			groundBlock.transform.parent = groundParent.transform;
			lastBlockX += groundAssetWidth ;
		}
		
	}

	public void generateSegment(){
		for(int i = 0; i < mapSegmentLength; i++){
			
			int groundChunkWidth = Random.Range(groundChunkMin, groundChunkMax);
			
			for(int c = 0; c < groundChunkWidth; c++){	
				GameObject groundBlock = Instantiate(groundTop, new Vector2(lastBlockX, startY), Quaternion.identity);
				groundBlock.transform.parent = groundParent.transform;
				lastGroundPosition = new Vector2(lastBlockX, startY);

				if(thornsEnabled){
					if(justSpawnedHazard == false){
						if(Random.value < thornSpawnChance){
							GameObject thornBlock = Instantiate(thorns, new Vector2(lastBlockX, startY), Quaternion.identity);
							thornBlock.transform.parent = hazardParent.transform;
						}

						justSpawnedHazard = true;
					}
					else{
						justSpawnedHazard = false;
					}
				}

					lastBlockX += groundAssetWidth;

					
			}

			if(pitsEnabled){
				if(Random.value <= pitSpawnChance){
					generatePit();
				}
			}
		}

		increaseDifficulty();
		
	}

	public void generatePit(){
		int pitLength = Random.Range(pitMinLength, pitMaxLength);

		//Generate Start Edge Block
		GameObject groundEdgeBlockRight =  Instantiate(groundTopEdgeRight, new Vector2(lastBlockX, startY), Quaternion.identity);
		groundEdgeBlockRight.transform.parent = groundParent.transform;

		lastBlockX += groundAssetWidth;
		//lastBlockX += pitLength;
		for(int i = 0; i < pitLength; i++){
			GameObject thornBlock = Instantiate(thorns, new Vector2(lastBlockX, startY - 0.56f), Quaternion.identity);
			thornBlock.transform.parent = hazardParent.transform;
			if(i + 1 < pitLength){
				lastBlockX += groundAssetWidth;
			}
		}

		
		//Generate End Edge Block
		GameObject groundEdgeBlockLeft = Instantiate(groundTopEdgeLeft, new Vector2(lastBlockX, startY), Quaternion.identity);
		groundEdgeBlockLeft.transform.parent = groundParent.transform;

		lastBlockX += groundAssetWidth;
	}

	public Vector2 getLastGroundPosition(){
		return lastGroundPosition;
	}

	public void increaseDifficulty(){
		if(thornSpawnChance < masterMaxHazardChanceCap && pitSpawnChance < masterMaxHazardChanceCap){
			thornSpawnChance = thornSpawnChance * difficultyFactor;
			pitSpawnChance = pitSpawnChance * difficultyFactor;
		}

		if(pitMinLength < pitMinLengthCap){
			pitMinLength++;
		}
		if(pitMaxLength < pitMaxLengthCap){
			pitMaxLength++;
		}
	}

	private void createGroundContainer(){
		groundParent = GameObject.Find(GROUND_PARENT_NAME);

		if(!groundParent){
			groundParent = new GameObject(GROUND_PARENT_NAME);
		}
	}

	private void createHazardContainer(){
		hazardParent = GameObject.Find(HAZARD_PARENT_NAME);

		if(!hazardParent){
			hazardParent = new GameObject(HAZARD_PARENT_NAME);
		}
	}
}
