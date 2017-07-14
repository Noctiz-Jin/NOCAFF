using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NOCFileManager : MonoBehaviour {

	static string filePath = Application.streamingAssetsPath;

	public static string ReadJSON (string fileName)
	{
		return File.ReadAllText(filePath + "/" + fileName + ".json");
	}
}
