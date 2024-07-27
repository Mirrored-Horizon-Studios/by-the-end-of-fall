using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {
	[Header("Ground Assets")]
	[SerializeField] GameObject groundTop;
	[SerializeField] GameObject groundCenter;

	[Header("Hazard Assets")]
	[SerializeField] GameObject stickWall;
	[SerializeField] GameObject stoneWall;
	[SerializeField] GameObject builtWoodenFence;

	[SerializeField] GameObject[] backDropElements;

	[SerializeField] int playerSpawnSize = 6;
	[SerializeField] int minPlatformSize = 1;
	[SerializeField] int maxPlatformSize = 10;
	[SerializeField] int masHazardSize = 3;
	[SerializeField] int maxHeight = 3;
	[SerializeField] int maxDrop = -3;
	[SerializeField] int platforms = 100;

	[SerializeField] int buildUnitsBelowSurface = 4;

	[SerializeField] float hazardChance = .1f;

	[SerializeField] float stickWallChance = .1f;
	[SerializeField] float stoneWallChance = .05f;
	[SerializeField] float builtWoodenFenceChance = .05f;



	float backDropElementsSpawnChance = .05f;

	int blockHeight;

	private int blockNumber = 0;
	private Vector2 lastBlockPosition = new Vector2(0,0);
	private Vector2 firstBlockPosition;

	private bool canGenerateHazard = true;

	// Use this for initialization
	void Start () {
		GeneratePlayerSpawnPoint();
		LoadMap();
	}

	// Platform Generator Test Vars
	bool hasGeneratedPlatform = false;
	[SerializeField] PlatformGenerator platformGenerator;
	float platformChance = .02f;
	int lastBlockHeight = 0;

	public void LoadMap(){
		blockHeight = Mathf.RoundToInt(lastBlockPosition.y);
		//Instantiate(groundTop, lastBlockPosition, Quaternion.identity);
		//BuildDown();
		

		for(int platform = 0; platform < platforms; platform++){
			int platformSize = Mathf.RoundToInt(Random.Range(minPlatformSize, maxPlatformSize));
			blockHeight = blockHeight + Random.Range(maxDrop, maxHeight);

			for(int tiles = 0; tiles < platformSize; tiles++){
				lastBlockPosition = new Vector2(blockNumber, blockHeight);

				Instantiate(groundTop, lastBlockPosition, Quaternion.identity);

				if(lastBlockHeight - blockHeight >= 2){
					if(!hasGeneratedPlatform){
						if(Random.value < platformChance){
							platformGenerator.SetStartingPoint(new Vector2(blockNumber + 2, lastBlockHeight + 1));
							platformGenerator.SetChunks(Random.Range(5,20));
							platformGenerator.Build();
							hasGeneratedPlatform = true;
						}
					}

				}

				GenerateHazard();
				GenerateBackDropItem();
				
				BuildDown();
				blockNumber++;
				lastBlockHeight = blockHeight;
			}	

			if(hasGeneratedPlatform)
				hasGeneratedPlatform = false;
		}
	}

	private void BuildDown(){
		int yindex = GetBuildDirection();
				
		for(int c = 0; c < buildUnitsBelowSurface; c++){
			Instantiate(groundCenter, new Vector2(blockNumber, yindex), Quaternion.identity);
			yindex--;
		}

	}

	private int GetBuildDirection(){
		int buildHeight = 0;
		float upChance = Random.Range(0, 1);
		float downChance = Random.Range(0,1);		

		if(upChance > downChance){
			buildHeight = blockHeight + 1;
		}
		else{
			buildHeight = blockHeight -1;
		}

		return buildHeight;
	}

	private void GeneratePlayerSpawnPoint(){	
		for(int x = 0; x < playerSpawnSize; x++){
			Instantiate(groundTop, new Vector2(x, 0), Quaternion.identity);

			int yIndex = -1;
			
			for(int y = 0; y < buildUnitsBelowSurface; y++){
				Instantiate(groundCenter, new Vector2(x, yIndex), Quaternion.identity);
				yIndex--;
			}

			blockNumber++;
		}
	}

	private void GenerateHazard(){
		if(canGenerateHazard){
					if(Random.value < hazardChance){
						
						if(Random.value < stickWallChance){
							Instantiate(stickWall, new Vector2(blockNumber, blockHeight + 1), Quaternion.identity);
							canGenerateHazard = false;
						}

						else if(Random.value < stoneWallChance){
							Instantiate(stoneWall, new Vector2(blockNumber, blockHeight + 1), Quaternion.identity);
							canGenerateHazard = false;
						}

						else if(Random.value < builtWoodenFenceChance){
							Instantiate(builtWoodenFence, new Vector2(blockNumber, blockHeight + 1.5f), Quaternion.identity);
							canGenerateHazard = false;
						}

					}
				}
				else{
					canGenerateHazard = true;
				}
	}

	private void GenerateBackDropItem(){
		if(Random.value < backDropElementsSpawnChance && backDropElements.Length > 0){
			int element = Random.Range(0, backDropElements.Length);
			Instantiate(backDropElements[element], new Vector3(blockNumber, blockHeight + 1, 5), Quaternion.identity);
		}
	}

	public Vector2 GetLastBlock(){
		return lastBlockPosition;
	}

	public Vector2 GetFirstBlock(){
		return firstBlockPosition;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
