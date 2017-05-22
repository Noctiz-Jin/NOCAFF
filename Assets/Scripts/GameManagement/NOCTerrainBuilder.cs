using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCTerrainBuilder : MonoBehaviour {

	// 3 dimensions of the box room
	public int lx;
	public int ly;
	public int lz;

	// blocks
	public GameObject protoBlock;
	public GameObject invisibleBlock;
	public GameObject obstacleHolder;

	// boundary of the scene
	private int nx;
	private int px;
	private int nz;
	private int pz;

	//Parent to all GameObjects
	private GameObject groundLevel;
	private GameObject obstacleLevel;

	public void SetupScene() {
		nx = -lx;
		px = lx;
		nz = -lz;
		pz = lz;

		groundLevel = new GameObject ("GroundLevel");
		obstacleLevel = new GameObject ("ObstacleLevel");

		SetupGround ();
		//SetupObstacle (obs1, dirt);
		//SetupObstacle (obs2, grass);
	}

	private void SetupGround() {
		// build ground
		for (int x = nx; x < px + 1; x++) {
			for (int z = nz; z < pz + 1; z++) {
				CreateStaticBlock (x, 0, z, protoBlock, groundLevel);
			}
		}

		// build wall invisible
		for (int x = nx; x < px + 1; x++) {
			CreateStaticBlock (x, 1, nz - 1, invisibleBlock, obstacleLevel);
			CreateStaticBlock (x, 1, pz + 1, invisibleBlock, obstacleLevel);
			CreateStaticBlock (x, 2, nz - 1, invisibleBlock, groundLevel);
			CreateStaticBlock (x, 2, pz + 1, invisibleBlock, groundLevel);
			CreateStaticBlock (x, 3, nz - 1, invisibleBlock, groundLevel);
			CreateStaticBlock (x, 3, pz + 1, invisibleBlock, groundLevel);
		}
		for (int z = nz; z < pz + 1; z++) {
			CreateStaticBlock (nx - 1, 1, z, invisibleBlock, obstacleLevel);
			CreateStaticBlock (px + 1, 1, z, invisibleBlock, obstacleLevel);
			CreateStaticBlock (nx - 1, 2, z, invisibleBlock, groundLevel);
			CreateStaticBlock (px + 1, 2, z, invisibleBlock, groundLevel);
			CreateStaticBlock (nx - 1, 3, z, invisibleBlock, groundLevel);
			CreateStaticBlock (px + 1, 3, z, invisibleBlock, groundLevel);
		}
	}

	private void CreateStaticBlock(int x, int y, int z, GameObject material, GameObject parent){
		CreateBlock (x, y, z, material, parent, true, "Static");
	}

	private void CreateObstacleBlock(int x, int y, int z, GameObject material, GameObject parent){
		CreateBlock (x, y, z, material, parent, false, "Obstacle");
	}

	private void CreateBlock(int x, int y, int z, GameObject material, GameObject parent, bool isStatic, string tag) {
		GameObject block = Instantiate (material, new Vector3 (x, y, z), Quaternion.identity);
		block.isStatic = isStatic;
		block.tag = tag;

		if (isStatic == true) {
			block.transform.SetParent (parent.transform);
		} else {
			GameObject obstacleGO = Instantiate (obstacleHolder, new Vector3 (x, y, z), Quaternion.identity);
			obstacleGO.tag = tag;
			block.transform.SetParent (obstacleGO.transform);
			obstacleGO.transform.SetParent (parent.transform);
		}
	}
}
