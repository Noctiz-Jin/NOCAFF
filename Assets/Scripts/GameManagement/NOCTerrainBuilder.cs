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
		CreateYPlane(0, nx, px, nz, pz, protoBlock);

		// build wall invisible
		CreateXPlane(nx - 1, 1, 3, nz, pz, invisibleBlock);
		CreateXPlane(px + 1, 1, 3, nz, pz, invisibleBlock);
		CreateZPlane(nz - 1, nx, px, 1, 3, invisibleBlock);
		CreateZPlane(pz + 1, nx, px, 1, 3, invisibleBlock);

	}

	////// Helper Methods //////
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



	private void CreateXPlane (int x, int ny, int py, int nz, int pz, GameObject block)
	{
		CreateRect(x, x, ny, py, nz, pz, block);
	}

	private void CreateYPlane (int y, int nx, int px, int nz, int pz, GameObject block)
	{
		CreateRect(nx, px, y, y, nz, pz, block);
	}

	private void CreateZPlane (int z, int nx, int px, int ny, int py, GameObject block)
	{
		CreateRect(nx, px, ny, py, z, z, block);
	}

	private void CreateRect (int nx, int px, int ny, int py, int nz, int pz, GameObject block)
	{
		for (int x = nx; x < px + 1; x++) {
			for (int y = ny; y < py + 1; y++) {
				for (int z = nz; z < pz + 1; z++) {
					CreateStaticBlock (x, y, z, block, groundLevel);
				}
			}
		}
	}
}