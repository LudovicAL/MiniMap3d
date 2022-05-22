using UnityEngine;

public class MiniMap : MonoBehaviour {

	[SerializeField]
	private Vector3 minPosition;
	[SerializeField]
	private Vector3 maxPosition;
	[SerializeField]
	private float positionSpeed;
	[SerializeField]
	private Vector3 minScale;
	[SerializeField]
	private Vector3 maxScale;
	[SerializeField]
	private float scaleSpeed;

	private Transform miniMapScalePositionTransform;
	private Transform miniMapVerticalTransform;
	private Transform miniMapHorizontalTransform;
	private Transform playerTransform;
	private Transform miniPlayerTransform;
	private Transform cameraTransform;
	private GameObject textInstructionGo;

	// Start is called before the first frame update
	void Start() {
		miniMapScalePositionTransform = GameObject.Find("MiniMapScalePosition").transform;
		miniMapVerticalTransform = GameObject.Find("MiniMapVertical").transform;
		miniMapHorizontalTransform = GameObject.Find("MiniMapHorizontal").transform;
		playerTransform = GameObject.Find("Player").transform;
		miniPlayerTransform = GameObject.Find("MiniPlayer").transform;
		cameraTransform = Camera.main.transform;
		textInstructionGo = GameObject.Find("TextInstruction");
	}

	// Update is called once per frame
	void Update() {
		RotateMiniMap();
		if (Input.GetKey(KeyCode.Tab)) {
			ZoomOnMiniMap();
		} else {
			ZoomOffMiniMap();
		}
	}

	//Rotates the MiniMap
	private void RotateMiniMap() {
		miniPlayerTransform.localPosition = playerTransform.position;
		miniPlayerTransform.localRotation = playerTransform.rotation;
		Vector3 verticalRotation = new Vector3(-cameraTransform.rotation.eulerAngles.x, 0f, 0f);
		miniMapVerticalTransform.localRotation = Quaternion.Euler(verticalRotation);
		//miniMapVerticalTransform.localRotation = cameraTransform.localRotation;
		Vector3 horizontalRotation = new Vector3(0f, -playerTransform.rotation.eulerAngles.y, 0f);
		miniMapHorizontalTransform.localRotation = Quaternion.Euler(horizontalRotation);
	}

	//Zooms on to the MiniMap
	private void ZoomOnMiniMap() {
		textInstructionGo.SetActive(false);
		miniMapScalePositionTransform.localPosition = Vector3.MoveTowards(miniMapScalePositionTransform.localPosition, maxPosition, positionSpeed * Time.deltaTime);
		miniMapScalePositionTransform.localScale = Vector3.MoveTowards(miniMapScalePositionTransform.localScale, maxScale, scaleSpeed * Time.deltaTime);
	}

	//Zooms away from the MiniMap
	private void ZoomOffMiniMap() {
		textInstructionGo.SetActive(true);
		miniMapScalePositionTransform.localPosition = Vector3.MoveTowards(miniMapScalePositionTransform.localPosition, minPosition, positionSpeed * Time.deltaTime);
		miniMapScalePositionTransform.localScale = Vector3.MoveTowards(miniMapScalePositionTransform.localScale, minScale, scaleSpeed * Time.deltaTime);
	}
}
