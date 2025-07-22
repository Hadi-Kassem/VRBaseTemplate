using UnityEngine;

public class CylinderStretch : MonoBehaviour
{
    public float stretchSpeed = 1f;
    public float rotationSpeed = 90f;
    public float minScale = 0.1f;
    public float maxScale = 10f;

    private bool isSelected = false;
    private SelectableVisual visual;

    void Start()
    {
        visual = GetComponent<SelectableVisual>();
    }

    void Update()
    {
        HandleSelection();

        if (!isSelected) return;

        Vector3 scale = transform.localScale;

        if (Input.GetKey(KeyCode.UpArrow))
            scale.y += stretchSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.DownArrow))
            scale.y -= stretchSpeed * Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            scale.x += stretchSpeed * Time.deltaTime;
            scale.z += stretchSpeed * Time.deltaTime;
        }
        if (Input.GetMouseButton(1))
        {
            scale.x -= stretchSpeed * Time.deltaTime;
            scale.z -= stretchSpeed * Time.deltaTime;
        }

        scale.x = Mathf.Clamp(scale.x, minScale, maxScale);
        scale.y = Mathf.Clamp(scale.y, minScale, maxScale);
        scale.z = Mathf.Clamp(scale.z, minScale, maxScale);

        transform.localScale = scale;

        Vector2 rotationInput = Vector2.zero;
        if (Input.GetKey(KeyCode.Alpha6) || Input.GetKey(KeyCode.Keypad6))
            rotationInput.x += 1;
        if (Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Keypad4))
            rotationInput.x -= 1;
        if (Input.GetKey(KeyCode.Alpha8) || Input.GetKey(KeyCode.Keypad8))
            rotationInput.y += 1;
        if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))
            rotationInput.y -= 1;

        transform.Rotate(Vector3.up, rotationInput.x * rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.right, rotationInput.y * rotationSpeed * Time.deltaTime);
    }

    void HandleSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    isSelected = true;
                    if (visual != null) visual.OnSelect();
                }
                else
                {
                    Deselect();
                }
            }
            else
            {
                Deselect();
            }
        }
    }

    void Deselect()
    {
        isSelected = false;
        if (visual != null) visual.OnDeselect();
    }
}
