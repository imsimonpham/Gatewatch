using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] bool BillboardX = true;
    [SerializeField] bool BillboardY = true;
    [SerializeField] bool BillboardZ = true;
    [SerializeField] float OffsetToCamera;
    protected Vector3 localStartPosition;

    // Use this for initialization
    void Start()
    {
        localStartPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.CompareTag("TowerBaseUI"))
        {
            transform.LookAt(Camera.main.transform, Vector3.up);
        }
        else
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
                Camera.main.transform.rotation * Vector3.up);
            if(!BillboardX || !BillboardY || !BillboardZ)
                transform.rotation = Quaternion.Euler(BillboardX ? transform.rotation.eulerAngles.x : 0f, BillboardY ? transform.rotation.eulerAngles.y : 0f, BillboardZ ? transform.rotation.eulerAngles.z : 0f);
            transform.localPosition = localStartPosition;
            transform.position = transform.position + transform.rotation * Vector3.forward * OffsetToCamera;
        }
    }
}