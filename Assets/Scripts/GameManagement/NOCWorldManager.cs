using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCWorldManager : MonoBehaviour {

	public bool SetScriptedWorld = false;
	private NocTerrainDummyPlain terrainBuilder;

	public void BluePrint() {
		terrainBuilder = GetComponent<NocTerrainDummyPlain> ();
	}

	public void SetupScene() {
		if (SetScriptedWorld == true)
		{
			terrainBuilder.SetupScene ();
		}
	}

}
