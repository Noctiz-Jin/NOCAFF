using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCUtility : MonoBehaviour {
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
