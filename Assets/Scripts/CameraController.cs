using UnityEngine;

public class CameraController : MonoBehaviour {

	private bool doMovement = true;

	public float panSpeed = 30f;
	public float panBorderThickness = 10f; //будет определять расстояние от границы экрана
    public float maxX = 72f;
    public float minX = 0f;
    public float maxZ = -10f;
    public float minZ = -85f;

    public float scrollSpeed = 5f;
	public float minY = 10f;
	public float maxY = 80f;

	// Update is called once per frame
	void Update () {

        if (GameManager.GameIsOver) //блокирует камеру при окончании игры
        {
            this.enabled = false;
            return;
        }

		if (Input.GetKeyDown(KeyCode.Escape)) //нажатие Esc блокирует управление камерой
            doMovement = !doMovement;

		if (!doMovement)
			return;

        if (Input.GetKey("w") && transform.position.z < maxZ || Input.mousePosition.y >= Screen.height - panBorderThickness && transform.position.z < maxZ)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") && transform.position.z > minZ || Input.mousePosition.y <= panBorderThickness && transform.position.z > minZ)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") && transform.position.x < maxX || Input.mousePosition.x >= Screen.width - panBorderThickness && transform.position.x < maxX)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") && transform.position.x > minX || Input.mousePosition.x <= panBorderThickness && transform.position.x > minX)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

		Vector3 pos = transform.position; //переменная позиции масштаба

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
		pos.y = Mathf.Clamp(pos.y, minY, maxY);

		transform.position = pos;

	}
}
