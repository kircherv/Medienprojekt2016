
using UnityEngine;
using System.Collections;

public class ZungenGrapplingHook : MonoBehaviour
{

    public LineRenderer line;
    DistanceJoint2D joint;
    Vector3 targetPos;
    RaycastHit2D hit;
    public float distance = 10f;
    public LayerMask mask;
    public float step = 0.2f;

    // Use this for initialization
    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (joint.distance > .5f)
        {
            Debug.Log("in first if");
            joint.distance -= step;
        }
        else
        {
            Debug.Log("in first else");

            joint.enabled = false;
        }



        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;

            hit = Physics2D.Raycast(transform.position, targetPos - transform.position, distance, mask);
            //Debug.DrawLine(hit.transform.position, targetPos - transform.position);

            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {

                Debug.Log("in if input");
                joint.enabled = true;
                //	Debug.Log (hit.point - new Vector2(hit.collider.transform.position.x,hit.collider.transform.position.y);
                Vector2 connectPoint = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);
                connectPoint.x = connectPoint.x / hit.collider.transform.localScale.x;
                connectPoint.y = connectPoint.y / hit.collider.transform.localScale.y;
                Debug.Log(connectPoint);
                joint.connectedAnchor = connectPoint;

                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                //		joint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x,hit.collider.transform.position.y);
                joint.distance = Vector2.Distance(transform.position, hit.point);

                line.enabled = true;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, hit.point);

                line.GetComponent<ropeRatio>().grabPos = hit.point;

            }

        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("in E");
            joint.enabled = false;
        }
    }
}

