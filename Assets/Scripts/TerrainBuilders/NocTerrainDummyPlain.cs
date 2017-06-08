using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NocTerrainDummyPlain : NOCTerrainBuilder {

	// blocks
	public GameObject protoBlock;
	public GameObject invisibleBlock;


	public override void SetupScene() {
		base.SetupScene();

		SetupGround();
	}

	protected override void SetupGround() {
		base.SetupGround();

		// build ground
		CreateYPlane(0, nx, px, nz, pz, protoBlock);

		// build wall invisible
		CreateXPlane(nx - 1, 1, 3, nz, pz, invisibleBlock);
		CreateXPlane(px + 1, 1, 3, nz, pz, invisibleBlock);
		CreateZPlane(nz - 1, nx, px, 1, 3, invisibleBlock);
		CreateZPlane(pz + 1, nx, px, 1, 3, invisibleBlock);
	}

	protected override void SetupObstacle() {
		base.SetupObstacle();
	}

	/*
	private NOCGeometryTest ReadGeometryFromFile(string fileName)
	{
		string serializedObject = NOCFileManager.ReadJSON(fileName);
		return JsonUtility.FromJson<NOCGeometryTest> (serializedObject);
	}

	// Unit Tests //
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
	*/
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
