using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCWorldManager : MonoBehaviour {

	private NocTerrainDummyPlain terrainBuilder;

	public void BluePrint() {
		terrainBuilder = GetComponent<NocTerrainDummyPlain> ();
	}

	public void SetupScene() {
		terrainBuilder.SetupScene ();
	}

}
