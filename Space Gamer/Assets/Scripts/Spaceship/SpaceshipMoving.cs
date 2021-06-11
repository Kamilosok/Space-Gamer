using UnityEngine;

public class SpaceshipMoving : MonoBehaviour
{
    private Rigidbody rb;
    private bool mW, mS, mA, mD, mSpace, mC;
    private bool isBraking, isReversing;
    private short movingSpeed, rotationSpeed;
    private readonly GameObject[] trails = new GameObject[4];
    private readonly GameObject[] particles = new GameObject[4];
    private readonly GameObject[] lights = new GameObject[2];
    private void Start()
    {
        //Getting all of the needed references and setting default values

        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();

        for (int i = 0; i < 4; i++)
        {
            trails[i] = transform.GetChild(0).GetChild(i).gameObject;
            particles[i] = transform.GetChild(1).GetChild(i).gameObject;
            particles[i].GetComponent<ParticleSystem>().Stop();
        }

        for (int i = 0; i < 2; i++)
        {
            lights[i] = transform.GetChild(2).GetChild(i).gameObject;
        }

        movingSpeed = 1500;
        rotationSpeed = 1000;
    }
    private void Update()
    {
        //Based on key pressed/held, manage trails, particles and lights

        if (Input.GetKey(KeyCode.W))
        {
            mW = true;
            isBraking = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            mS = true;
            isBraking = false;
            isReversing = true;
            ParticleChanging(false, 4);
            ParticleChanging(false, 2);
            TrailChanging(false, 4);
        }

        if (Input.GetKey(KeyCode.A))
        {
            mA = true;
            isBraking = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            mD = true;
            isBraking = true;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            mSpace = true;
            isBraking = true;
        }

        if (Input.GetKey(KeyCode.C))
        {
            mC = true;
            isBraking = true;
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            TrailChanging(true, 4);
            ParticleChanging(true, 4);
            LightChanging(true);
        }

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Space) ||
           Input.GetKeyDown(KeyCode.C)))
        {
            isBraking = false;
            if (!isReversing)
            {
                TrailChanging(true, 2);
                ParticleChanging(true, 2);
            }
        }

        //Based on key up, manage trails, particles and lights

        if (Input.GetKeyUp(KeyCode.W))
        {
            if (!isBraking)
            {
                ParticleChanging(false, 2);
            }
            TrailChanging(false, 2);
            LightChanging(false);
            ParticleChanging(false, 4);
            isBraking = true;
        }

        if(Input.GetKeyUp(KeyCode.S))
        {
            isReversing = false;
        }

        if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.Space) ||
            Input.GetKeyUp(KeyCode.C))&&!isBraking)
        {
            isBraking = true;
            ParticleChanging(false, 2);
            TrailChanging(false, 4);
        }
    }
    private void FixedUpdate()
    {
        //Based on bools set during last update, move the object using relative forces

        if (mW)
        {
            rb.AddRelativeForce(new Vector3(1 * movingSpeed, 0, 0) * Time.fixedDeltaTime);
            mW = false;
        }

        if (mS)
        {
            rb.AddRelativeForce(new Vector3(-0.4f * movingSpeed, 0, 0) * Time.fixedDeltaTime);
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

        //If the rotation speed isn't too high, rotate object using relative torque

        if (rb.angularVelocity.y < 1.4f && rb.angularVelocity.y > -1.4f)
            rb.AddRelativeTorque(0, Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime, 0);
        if (rb.angularVelocity.z < 1.4f && rb.angularVelocity.z > -1.4f)
            rb.AddRelativeTorque(0, 0, Input.GetAxis("Mouse Y") * rotationSpeed * Time.fixedDeltaTime);

        //Make the object slow down in both velocity and angular velocity
        rb.angularVelocity -= (rb.angularVelocity * 1.8f / (1 / Time.fixedDeltaTime));
        rb.velocity -= (rb.velocity / (1.3f / Time.fixedDeltaTime));
    }

    private void TrailChanging(bool ifEnable, byte howMany)
    {
        //If the trails are supposed to be enabled - enable as much as is set in Update
        if (ifEnable)
        {
            for (int i = 0; i < howMany; i++)
            {
                trails[i].GetComponent<SpaceshipTrails>().EnableTrail();
            }
        }

        //Else - disable as much as is set in Update
        else
        {
            for (int i = 0; i < howMany; i++)
            {
                trails[i].GetComponent<SpaceshipTrails>().DisableTrail();
            }
        }
    }

    private void ParticleChanging(bool isEnable, byte howMany)
    {
        //If the particles are supposed to be enabled - enable as much as is set in Update
        if (isEnable)
        { 
            for (int i = 0; i < howMany; i++)
            {
                particles[i].GetComponent<ParticleSystem>().Play();
            }
        }

        //Else - disable as much as is set in Update
        else
        {
            for (int i = howMany - 1; i > howMany - 3; i--)
            {
                particles[i].GetComponent<ParticleSystem>().Stop();
            }
        }
    }
    private void LightChanging(bool isEnable)
    {
        //If the lights are supposed to be enabled - make them brighter
        if (isEnable)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].GetComponent<Light>().intensity = 20;
            }
        }

        //Else - make them dimmer
        else
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].GetComponent<Light>().intensity = 10;
            }
        }
    }
}
