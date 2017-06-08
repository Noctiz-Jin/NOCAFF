using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NOCTerrainBuilder : MonoBehaviour {

	// 3 dimensions of the box room
	public int lx;
	public int ly;
	public int lz;

	// blocks
	//private GameObject protoBlock;
	public GameObject obstacleHolder;

	// boundary of the scene
	protected int nx;
	protected int px;
	protected int nz;
	protected int pz;

	//Parent to all GameObjects
	private GameObject groundLevel;
	private GameObject obstacleLevel;

	//Layer Enum
	private enum NOCTerrainLayer
	{
		NOCGroundLayer = 0,
		NOCObstacleLayer = 1
	}

	private NOCTerrainLayer terrainLayer;
	// Subclass Guide: Call base.SetupScene() before building terrain
	public virtual void SetupScene() {
		nx = -lx;
		px = lx;
		nz = -lz;
		pz = lz;

		groundLevel = new GameObject ("GroundLevel");
		obstacleLevel = new GameObject ("ObstacleLevel");
	}

	protected virtual void SetupGround() {
		terrainLayer = NOCTerrainLayer.NOCGroundLayer;
	}

	protected virtual void SetupObstacle() {
		terrainLayer = NOCTerrainLayer.NOCObstacleLayer;
	}

	////// Helper Methods --- Geometry Construction //////
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

	protected void CreateXBar (int y, int z, int nx, int px, GameObject block)
	{
		CreateRect(nx, px, y, y, z, z, block);
	}

	protected void CreateYBar (int x, int z, int ny, int py, GameObject block)
	{
		CreateRect(x, x, ny, py, z, z, block);
	}

	protected void CreateZBar (int x, int y, int nz, int pz, GameObject block)
	{
		CreateRect(x, x, y, y, nz, pz, block);
	}

	protected void CreateXPlane (int x, int ny, int py, int nz, int pz, GameObject block)
	{
		CreateRect(x, x, ny, py, nz, pz, block);
	}

	protected void CreateYPlane (int y, int nx, int px, int nz, int pz, GameObject block)
	{
		CreateRect(nx, px, y, y, nz, pz, block);
	}

	protected void CreateZPlane (int z, int nx, int px, int ny, int py, GameObject block)
	{
		CreateRect(nx, px, ny, py, z, z, block);
	}

	protected void CreateRect (int nx, int px, int ny, int py, int nz, int pz, GameObject block)
	{
		for (int x = nx; x < px + 1; x++) {
			for (int y = ny; y < py + 1; y++) {
				for (int z = nz; z < pz + 1; z++) {
					if (terrainLayer == NOCTerrainLayer.NOCGroundLayer) {
						CreateStaticBlock (x, y, z, block, groundLevel);
					} else if (terrainLayer == NOCTerrainLayer.NOCObstacleLayer) {
						CreateObstacleBlock (x, y, z, block, obstacleLevel);
					} else {
						Debug.Log("*** Unknown NOCTerrainLayer Enum ***");
					}
				}
			}
		}
	}
}