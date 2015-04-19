using UnityEngine;
using System.Collections;

public class SimpleDoor : MonoBehaviour {

	public float fullyOpenYAngle = -90, doorSwingsSmoothingTime = 0.5f, doorSwingMaxSpeed = 90;
    public bool isNegativeDirection = false;
	public AudioClip openDoor, closeDoor;

	private float fullyClosedYAngle;
    private float currentYAngleOffset = 0f;

    private bool isRunning_CoOpenDoor;
    private IEnumerator coOpenDoor;

	private AudioSource source;

    private bool isOpen = false;

	private float alpha = 0f;

	private float touchRange = 3.5f;

	private float currentAngularVelocity, currentAngle, targetAngle;


    void OnDisable()
    {
        isRunning_CoOpenDoor = false;
    }

    void Start()
    {
		source = GetComponent<AudioSource> ();

		fullyOpenYAngle = -90 + this.transform.rotation.y * (float)(180.0 / Mathf.PI) * 2.3f;
		fullyClosedYAngle = 0 + this.transform.rotation.y * (float)(180.0 / Mathf.PI) * 2.3f;
		targetAngle = -90 + this.transform.rotation.y * (float)(180.0 / Mathf.PI) * 2.3f;
    }

	void Update()
	{
		UpdateAngel();
		UpdateRotation();
	}

	void UpdateAngel()
	{
		currentAngle = Mathf.SmoothDamp(currentAngle, 
		                                targetAngle, 
		                                ref currentAngularVelocity,
		                                doorSwingsSmoothingTime,
		                                doorSwingMaxSpeed);
	}

	void UpdateRotation()
	{
		transform.localRotation = Quaternion.AngleAxis (currentAngle, Vector3.up);
	}

    protected virtual void OnTriggerEnter(Collider other)
    {

    }

	protected virtual void OnMouseOver() 
	{
		float distance = Vector3.Distance(this.transform.position,  Camera.main.transform.position);

		if (distance <= touchRange) {
			if(Input.GetKeyDown(KeyCode.E)){
				if(isOpen)
				{
					Open();
					source.PlayOneShot(closeDoor);
				}
				else
				{
					Close();
					source.PlayOneShot(openDoor);
				}
			}

			alpha = 0.8f;
		}
	}

	protected virtual void OnMouseExit() {
		alpha = 0f;
	}
	
	public virtual void OnGUI()
	{
		GUIStyle style;
		style = GUI.skin.label;
		style.fontSize = 20; // Set size of text to 20.
		style.normal.textColor = new Color (1, 1, 1, alpha); // Set color to white w/alpha.
		GUI.Label (new Rect (Screen.width * 0.5f - 50f, Screen.height * 0.5f - 10f, 200f, 40f), "Press 'E' to open", style);
	}
	
    public void Open()
    {
		isOpen = false;
		targetAngle = fullyOpenYAngle;
    }

	public void Close()
	{
		isOpen = true;
		targetAngle = fullyClosedYAngle;
	}

    private IEnumerator CoOpenDoor()
    {
        isRunning_CoOpenDoor = true;

        while (currentYAngleOffset < fullyOpenYAngle)
        {
            if (currentYAngleOffset < fullyOpenYAngle - 1f)
            {
                currentYAngleOffset = Mathf.Lerp(currentYAngleOffset, fullyOpenYAngle, .1f);
            }
            else
            {
                currentYAngleOffset = fullyOpenYAngle;
            }
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, ((isNegativeDirection ? -currentYAngleOffset : currentYAngleOffset) + fullyClosedYAngle), transform.localRotation.z);

            yield return null;
        }

        isOpen = true;
        isRunning_CoOpenDoor = false;
    }

	private IEnumerator CoCloseDoor()
	{
		isRunning_CoOpenDoor = true;
		
		while (currentYAngleOffset < fullyOpenYAngle)
		{
			if (currentYAngleOffset < fullyOpenYAngle - 1f)
			{
				currentYAngleOffset = Mathf.Lerp(currentYAngleOffset, fullyOpenYAngle, .1f);
			}
			else
			{
				currentYAngleOffset = fullyOpenYAngle;
			}
			transform.localRotation = Quaternion.Euler(transform.localRotation.x, ((isNegativeDirection ? -currentYAngleOffset : currentYAngleOffset) + fullyClosedYAngle), transform.localRotation.z);
			
			yield return null;
		}
		
		isOpen = true;
		isRunning_CoOpenDoor = false;
	}
}
