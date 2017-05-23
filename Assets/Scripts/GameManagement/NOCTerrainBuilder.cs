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


	////// Helper Methods --- Geometry Construction //////
	private NOCGeometryTest ReadGeometryFromFile(string fileName)
	{
		string serializedObject = NOCFileManager.ReadJSON(fileName);
		return JsonUtility.FromJson<NOCGeometryTest> (serializedObject);
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

	private void CreateXBar (int y, int z, int nx, int px, GameObject block)
	{
		CreateRect(nx, px, y, y, z, z, block);
	}

	private void CreateYBar (int x, int z, int ny, int py, GameObject block)
	{
		CreateRect(x, x, ny, py, z, z, block);
	}

	private void CreateZBar (int x, int y, int nz, int pz, GameObject block)
	{
		CreateRect(x, x, y, y, nz, pz, block);
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

	private void TestGeometryDemoBuild ()
	{
		NOCGeometryTest geo = ReadGeometryFromFile("NOCJSONDemo");

		CreateXBar(geo.XBars[0], geo.XBars[1], geo.XBars[2], geo.XBars[3], protoBlock);
		CreateYBar(geo.YBars[0], geo.YBars[1], geo.YBars[2], geo.YBars[3], protoBlock);
		CreateZBar(geo.ZBars[0], geo.ZBars[1], geo.ZBars[2], geo.ZBars[3], protoBlock);

		CreateXPlane(geo.XPlanes[0], geo.XPlanes[1], geo.XPlanes[2], geo.XPlanes[3], geo.XPlanes[4], protoBlock);
		CreateYPlane(geo.YPlanes[0], geo.YPlanes[1], geo.YPlanes[2], geo.YPlanes[3], geo.YPlanes[4], protoBlock);
		CreateZPlane(geo.ZPlanes[0], geo.ZPlanes[1], geo.ZPlanes[2], geo.ZPlanes[3], geo.ZPlanes[4], protoBlock);

		CreateRect(geo.Rects[0], geo.Rects[1], geo.Rects[2], geo.Rects[3], geo.Rects[4], geo.Rects[5], protoBlock);
	}
}


////// Helper Struct --- NOCGeometryTest //////
[System.Serializable]
public class NOCGeometryTest
{
	public int XBar;
	public int[] XBars;
	public int YBar;
	public int[] YBars;
	public int ZBar;
	public int[] ZBars;

	public int XPlane;
	public int[] XPlanes;
	public int YPlane;
	public int[] YPlanes;
	public int ZPlane;
	public int[] ZPlanes;

	public int Rect;
	public int[] Rects;
}