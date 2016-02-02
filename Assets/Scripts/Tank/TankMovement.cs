using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public float m_Speed = 12f;
    public float m_TurnSpeed = 180f;
    public AudioSource m_MovementAudio;
    public AudioClip m_EngineIdling;
    public AudioClip m_EngineDriving;
    public float m_PitchRange = 0.2f;
    public float audioPitchDampTime = 0.2f;


    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;
    private float m_TurnInputValue;
    private float m_OriginalPitch;

    private float instantAudioPitch;
    private float velocityTemp;


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

        m_OriginalPitch = instantAudioPitch = m_MovementAudio.pitch;


        //m_MovementAudio.Play();
    }


    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

        EngineAudio();
    }


    private void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
        if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
        {
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                m_MovementAudio.clip = m_EngineIdling;
                instantAudioPitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch);
                m_MovementAudio.Play();
            }
        }
        else
        {
            if (m_MovementAudio.clip == m_EngineIdling)
            {
                m_MovementAudio.clip = m_EngineDriving;
                instantAudioPitch = Random.Range(m_OriginalPitch, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
        //m_MovementAudio.pitch=Mathf.SmoothDamp(m_MovementAudio.pitch,instantAudioPitch,ref velocityTemp,audioPitchDampTime);
        m_MovementAudio.pitch = Mathf.Lerp(m_MovementAudio.pitch, instantAudioPitch, 0.25f);
    }


    private void FixedUpdate()
    {
        // Move and turn the tank.
        Move();
        Turn();
    }


    private void Move()
    {
        // Adjust the position of the tank based on the player's input.
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        //m_Rigidbody.velocity = m_Rigidbody.velocity+movement;
        //Debug.Log(m_Rigidbody.velocity);
    }


    private void Turn()
    {
        // Adjust the rotation of the tank based on the player's input.
        Quaternion turn = Quaternion.Euler(new Vector3(0f, m_TurnInputValue, 0f) * m_TurnSpeed * Time.deltaTime);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turn);
    }
}