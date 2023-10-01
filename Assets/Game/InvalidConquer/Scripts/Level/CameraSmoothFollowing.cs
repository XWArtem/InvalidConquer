using UnityEngine;

public class CameraSmoothFollowing : MonoBehaviour
{
    [SerializeField] private Transform targetRect;
    public Vector3 offset;
    [SerializeField] private float dumping;
    [SerializeField] private Transform leftMaxDistance;
    [SerializeField] private Transform rightMaxDistance;
    public bool IsFixed = false;

    private Vector3 velocity = Vector3.zero;

    private void OnEnable()
    {
        StaticActions.OnMainHeroGrabbed += SetFixed;
    }

    private void OnDisable()
    {
        StaticActions.OnMainHeroGrabbed -= SetFixed;
    }

    private void SetFixed(bool isFixed)
    {
        IsFixed = isFixed;
    }

    private void LateUpdate()
    {
        if (IsFixed) return;

        if (targetRect.position.x > leftMaxDistance.position.x && targetRect.position.x < rightMaxDistance.position.x) 
        {
            Vector3 movePos = new Vector3(targetRect.position.x, 0f, targetRect.position.z) + offset;
            transform.position = Vector3.SmoothDamp(new Vector3(transform.position.x, 0, -10f), movePos, ref velocity, dumping);
        }
    }
}
