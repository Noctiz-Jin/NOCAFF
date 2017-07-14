using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
	GameObject groundLevel;
	GameObject obstacleLevel;

	//Layer Enum
	enum NOCTerrainLayer
	{
		NOCGroundLayer = 0,
		NOCObstacleLayer = 1
	}

	NOCTerrainLayer terrainLayer;
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
	void CreateStaticBlock(int x, int y, int z, GameObject material, string blockFace, string blockYSpin, GameObject parent){
		CreateBlock (x, y, z, material, blockFace, blockYSpin, parent, true, "Static");
	}

	void CreateObstacleBlock(int x, int y, int z, GameObject material, string blockFace, string blockYSpin, GameObject parent){
		CreateBlock (x, y, z, material, blockFace, blockYSpin, parent, false, "Obstacle");
	}

	void CreateBlock(int x, int y, int z, GameObject material, string blockFace, string blockYSpin, GameObject parent, bool isStatic, string tag) {
		Quaternion blockQuaternion = Quaternion.identity;

		if (blockFace != null)
		{
			if (blockFace == "Random")
			{
				blockQuaternion = Quaternion.Euler(blockQuaternion.eulerAngles + BlockFaceSelect[BlockFaceSelect.Keys.ToArray()[Random.Range(0, 6)]]);
			} else {
				blockQuaternion = Quaternion.Euler(blockQuaternion.eulerAngles + BlockFaceSelect[blockFace]);
			}
		}

		if (blockYSpin != null)
		{
			if (blockYSpin == "Random")
			{
				blockQuaternion = Quaternion.Euler(blockQuaternion.eulerAngles + BlockYSpin[BlockYSpin.Keys.ToArray()[Random.Range(0, 4)]]);
			} else {
				blockQuaternion = Quaternion.Euler(blockQuaternion.eulerAngles + BlockYSpin[blockYSpin]);
			}
		}

		GameObject block = Instantiate (material, new Vector3 (x, y, z), blockQuaternion);
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

	protected void CreateXBar (int y, int z, int nx, int px, GameObject block, string blockFace = "AFace", string blockYSpin = "None")
	{
		CreateRect(nx, px, y, y, z, z, block, blockFace, blockYSpin);
	}

	protected void CreateYBar (int x, int z, int ny, int py, GameObject block, string blockFace = "AFace", string blockYSpin = "None")
	{
		CreateRect(x, x, ny, py, z, z, block, blockFace, blockYSpin);
	}

	protected void CreateZBar (int x, int y, int nz, int pz, GameObject block, string blockFace = "AFace", string blockYSpin = "None")
	{
		CreateRect(x, x, y, y, nz, pz, block, blockFace, blockYSpin);
	}

	protected void CreateXPlane (int x, int ny, int py, int nz, int pz, GameObject block, string blockFace = "AFace", string blockYSpin = "None")
	{
		CreateRect(x, x, ny, py, nz, pz, block, blockFace, blockYSpin);
	}

	protected void CreateYPlane (int y, int nx, int px, int nz, int pz, GameObject block, string blockFace = "AFace", string blockYSpin = "None")
	{
		CreateRect(nx, px, y, y, nz, pz, block, blockFace, blockYSpin);
	}

	protected void CreateZPlane (int z, int nx, int px, int ny, int py, GameObject block, string blockFace = "AFace", string blockYSpin = "None")
	{
		CreateRect(nx, px, ny, py, z, z, block, blockFace, blockYSpin);
	}

	protected void CreateRect (int nx, int px, int ny, int py, int nz, int pz, GameObject block, string blockFace = "AFace", string blockYSpin = "None")
	{
		for (int x = nx; x < px + 1; x++) {
			for (int y = ny; y < py + 1; y++) {
				for (int z = nz; z < pz + 1; z++) {
					if (terrainLayer == NOCTerrainLayer.NOCGroundLayer) {
						CreateStaticBlock (x, y, z, block, blockFace, blockYSpin, groundLevel);
					} else if (terrainLayer == NOCTerrainLayer.NOCObstacleLayer) {
						CreateObstacleBlock (x, y, z, block, blockFace, blockYSpin, obstacleLevel);
					} else {
						Debug.Log("*** Unknown NOCTerrainLayer Enum ***");
					}
				}
			}
		}
	}

	public static Dictionary<string, Vector3> BlockFaceSelect = new Dictionary<string, Vector3>
    {
        {"AFace", new Vector3(0, 180, 0)},
        {"BFace", new Vector3(0, 90, 90)},
        {"CFace", new Vector3(-90, 180, 0)},
        {"DFace", new Vector3(0, -90, -90)},
        {"EFace", new Vector3(90, 0, 0)},
        {"FFace", new Vector3(0, 0, 180)}
    };


    public static Dictionary<string, Vector3> BlockYSpin = new Dictionary<string, Vector3>
    {
    	// None Spin block will head its up face letter to Z+ axis
    	{"None", new Vector3(0, 0, 0)},
        {"Left", new Vector3(0, -90, 0)},
        {"Right", new Vector3(0, 90, 0)},
        {"Reverse", new Vector3(0, 180, 0)}
    };
}