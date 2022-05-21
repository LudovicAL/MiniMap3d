using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Dome : MonoBehaviour {

	// Start is called before the first frame update
	void Start() {
		Mesh mesh = MeshGenerator.instance.GenerateBellMesh();
		GetComponent<MeshFilter>().mesh = mesh;
		GameObject.Find("MiniDome").GetComponent<MeshFilter>().mesh = mesh;
	}
}
