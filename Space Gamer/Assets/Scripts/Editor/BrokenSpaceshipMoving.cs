using System.Collections;
using UnityEngine;

public class BrokenSpaceshipMoving : MonoBehaviour
{
    private Rigidbody rb;
    private bool mW, mS, mA, mD, mSpace, mC;
    private bool rA, rD, rSpace, rC;
    private bool isBraking, isReversing;
    private bool isRotatedA, isRotatedD, isRotatedSpace, isRotatedC;
    private short movingSpeed, rotationSpeed;
    private short rAngle;
    private short oA, oD, oSpace, oC;
    private readonly GameObject[] trails = new GameObject[4];
    private GameObject cameraObject;
    private IEnumerator rotateCoroutineH, rotateCoroutineV;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        movingSpeed = 1500;
        rotationSpeed = 3000;
        rAngle = 30;
        for (int i = 0; i < 4; i++)
        {
            trails[i] = transform.GetChild(i).gameObject;
        }
        cameraObject = transform.GetChild(6).gameObject;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            mW = true;
            isBraking = false;
            ParticleChanging(true, 4);
        }
        if (Input.GetKey(KeyCode.S))
        {
            mS = true;
            isBraking = false;
            isReversing = true;
            ParticleChanging(false, 4);
        }
        if (Input.GetKey(KeyCode.A))
        {
            mA = true;
            isBraking = false;
            if (!isReversing)
                ParticleChanging(true, 2);
        }
        if (Input.GetKey(KeyCode.D))
        {
            mD = true;
            isBraking = false;
            if (!isReversing)
                ParticleChanging(true, 2);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            mSpace = true;
            isBraking = false;
            if (!isReversing)
                ParticleChanging(true, 2);
        }
        if (Input.GetKey(KeyCode.C))
        {
            mC = true;
            isBraking = false;
            if (!isReversing)
                ParticleChanging(true, 2);
        }
        if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.Space) ||
            Input.GetKeyUp(KeyCode.C)) && !isBraking)
        {
            isBraking = true;
            ParticleChanging(false, 4);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rA = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rD = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rSpace = true;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            rC = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            oA = rAngle;
            if (isRotatedA)
            {
                Fix();
            }
            rotateCoroutineH = RotateHorizontal(false);
            StartCoroutine(rotateCoroutineH);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            oD = rAngle;
            if (isRotatedD)
            {
                Fix();
            }
            rotateCoroutineH = RotateHorizontal(true);
            StartCoroutine(rotateCoroutineH);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            oSpace = rAngle;
            if (isRotatedSpace)
            {
                Fix();
            }
            rotateCoroutineV = RotateVertical(true);
            StartCoroutine(rotateCoroutineV);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            oC = rAngle;
            if (isRotatedC)
            {
                Fix();
            }
            rotateCoroutineV = RotateVertical(false);
            StartCoroutine(rotateCoroutineV);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            isReversing = false;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            cameraObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
    private void FixedUpdate()
    {
        if (mW)
        {
            rb.AddRelativeForce(new Vector3(1 * movingSpeed, 0, 0) * Time.fixedDeltaTime);
            mW = false;
        }
        if (mS)
        {
            rb.AddRelativeForce(new Vector3(-0.7f * movingSpeed, 0, 0) * Time.fixedDeltaTime);
            mS = false;
        }
        if (mA)
        {
            rb.AddRelativeForce(new Vector3(0, 0, 0.6f * movingSpeed) * Time.fixedDeltaTime);
            mA = false;
        }
        if (mD)
        {
            rb.AddRelativeForce(new Vector3(0, 0, -0.6f * movingSpeed) * Time.fixedDeltaTime);
            mD = false;
        }
        if (mSpace)
        {
            rb.AddRelativeForce(new Vector3(0, 0.6f * movingSpeed, 0) * Time.fixedDeltaTime);
            mSpace = false;
        }
        if (mC)
        {
            rb.AddRelativeForce(new Vector3(0, -0.6f * movingSpeed, 0) * Time.fixedDeltaTime);
            mC = false;
        }

        if (rA && !isRotatedA)
        {
            oA = rAngle;
            isRotatedA = true;
            rotateCoroutineH = RotateHorizontal(true);
            StartCoroutine(rotateCoroutineH);
        }
        if (rD && !isRotatedD)
        {
            oD = rAngle;
            isRotatedD = true;
            rotateCoroutineH = RotateHorizontal(false);
            StartCoroutine(rotateCoroutineH);
        }
        if (rSpace && !isRotatedSpace)
        {
            oSpace = rAngle;
            isRotatedSpace = true;
            rotateCoroutineV = RotateVertical(false);
            StartCoroutine(rotateCoroutineV);
        }
        if (rC && !isRotatedC)
        {
            oC = rAngle;
            isRotatedC = true;
            rotateCoroutineV = RotateVertical(true);
            StartCoroutine(rotateCoroutineV);
        }

        rb.angularVelocity -= (rb.angularVelocity * 1.8f / (1 / Time.fixedDeltaTime));
        rb.velocity -= (rb.velocity / (1 / Time.fixedDeltaTime));

        if (rb.angularVelocity.y < 1.4f && rb.angularVelocity.y > -1.4f)
            rb.AddRelativeTorque(0, Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime, 0);
        if (rb.angularVelocity.z < 1.4f && rb.angularVelocity.z > -1.4f)
            rb.AddRelativeTorque(0, 0, Input.GetAxis("Mouse Y") * rotationSpeed * Time.fixedDeltaTime);
    }
    private void ParticleChanging(bool ifEnable, short howMany)
    {
        if (ifEnable)
        {
            for (int i = 0; i < howMany; i++)
            {
                trails[i].GetComponent<SpaceshipTrails>().EnableTrail();
            }
        }
        else
        {
            for (int i = 0; i < howMany; i++)
            {
                trails[i].GetComponent<SpaceshipTrails>().DisableTrail();
            }
        }
    }
    private IEnumerator RotateHorizontal(bool isA)
    {
        while (oA > 0 || oD > 0)
        {
            if (isA)
            {
                transform.Rotate(Vector3.right * Time.fixedDeltaTime * 40, Space.Self);
                cameraObject.transform.Rotate(Vector3.left * Time.fixedDeltaTime * 40, Space.Self);
            }
            else
            {
                transform.Rotate(Vector3.left * Time.fixedDeltaTime * 40, Space.Self);
                cameraObject.transform.Rotate(Vector3.right * Time.fixedDeltaTime * 40, Space.Self);
            }
            oA -= 1;
            oD -= 1;
            yield return new WaitForSecondsRealtime(0.001f);
        }
        isRotatedA = false;
        isRotatedD = false;
        rA = false;
        rD = false;
    }
    private IEnumerator RotateVertical(bool isC)
    {
        while (oSpace > 0 || oC > 0)
        {
            if (isC)
            {
                transform.Rotate(Vector3.back * Time.fixedDeltaTime * 40, Space.Self);
                cameraObject.transform.Rotate(Vector3.forward * Time.fixedDeltaTime * 40, Space.Self);
            }
            else
            {
                transform.Rotate(Vector3.forward * Time.fixedDeltaTime * 40, Space.Self);
                cameraObject.transform.Rotate(Vector3.back * Time.fixedDeltaTime * 40, Space.Self);
            }
            oSpace -= 1;
            oC -= 1;
            yield return new WaitForSecondsRealtime(0.001f);
        }
        isRotatedSpace = false;
        isRotatedC = false;
        rSpace = false;
        rC = false;
    }
    private void Fix()
    {
        cameraObject.transform.localEulerAngles = Vector3.Slerp(cameraObject.transform.localEulerAngles, new Vector3(0, 0, 0), 1);
    }
}
