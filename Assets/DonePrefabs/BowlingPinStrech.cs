using UnityEngine;

public class BowlingPinStrech : MonoBehaviour
{
    public float scaleStep = 1.1f;   // Scale multiplier per click
    public float minScale = 0.3f;
    public float maxScale = 3f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        ScaleObject(scaleStep);
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        ScaleObject(1f / scaleStep);
                    }
                }
            }
        }
    }

    void ScaleObject(float scaleFactor)
    {
        float newScale = transform.localScale.x * scaleFactor;
        newScale = Mathf.Clamp(newScale, minScale, maxScale);
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }
}
