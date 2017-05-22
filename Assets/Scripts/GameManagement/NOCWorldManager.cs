using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCWorldManager : MonoBehaviour {

	private NOCTerrainBuilder terrainBuilder;

	public void BluePrint() {
		terrainBuilder = GetComponent<NOCTerrainBuilder> ();
	}

	public void SetupScene() {
		terrainBuilder.SetupScene ();
	}

}
