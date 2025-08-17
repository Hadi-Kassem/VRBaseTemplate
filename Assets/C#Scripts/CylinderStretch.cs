using UnityEngine;

public class CylinderStretch : MonoBehaviour
{
    public float stretchSpeed = 1f;
    public float rotationSpeed = 90f;
    [Range(0.1f, 10f)] public float minScale = 0.5f;
    [Range(0.1f, 10f)] public float maxScale = 2f;

    private bool isSelected = false;
    private SelectableVisual visual;
    private Vector3 initialScale;

    void Start()
    {
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<MeshCollider>().convex = true;
        }
        visual = GetComponent<SelectableVisual>();
        initialScale = transform.localScale;
    }

    void Update()
    {
        HandleSelection();

        if (!isSelected) return;

        Vector3 scale = transform.localScale;

        // Vertical stretch
        if (Input.GetKey(KeyCode.UpArrow))
            scale.y += stretchSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.DownArrow))
            scale.y -= stretchSpeed * Time.deltaTime;

        // Uniform width scaling with mouse
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

        // Clamp to safe relative scale range
        scale.x = Mathf.Clamp(scale.x, initialScale.x * minScale, initialScale.x * maxScale);
        scale.y = Mathf.Clamp(scale.y, initialScale.y * minScale, initialScale.y * maxScale);
        scale.z = Mathf.Clamp(scale.z, initialScale.z * minScale, initialScale.z * maxScale);

        transform.localScale = scale;

        // Manual rotation via number keys
        Vector2 rotationInput = Vector2.zero;
        if (Input.GetKey(KeyCode.Alpha6) || Input.GetKey(KeyCode.Keypad6))
            rotationInput.x += 1;
        if (Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Keypad4))
            rotationInput.x -= 1;
        if (Input.GetKey(KeyCode.Alpha8) || Input.GetKey(KeyCode.Keypad8))
            rotationInput.y += 1;
        if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))
            rotationInput.y -= 1;

        transform.Rotate(Vector3.up, rotationInput.x * rotationSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.right, rotationInput.y * rotationSpeed * Time.deltaTime, Space.World);
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
