using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleInputNamespace;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.EventSystems;

public class PlayerLogic : MonoBehaviour
{

    public static PlayerLogic instance;

    public FixedJoystick joystick;

    public float moveSpeed, jumpPower, gravityModifier,runSpeed = 12f;
    public CharacterController charChon;
    private Vector3 moveInput;

    public Transform cameraTrans;

    public float mouseSentivity;
    public bool invertX, invertY;
    private bool canJump, canDoubleJump;

    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    public GameObject muzzleFlash;

    private float bounceAmount;
    private bool bounce;

    public Transform firePoint;
    public Gun activeGun;
    public List<Gun> allGuns = new List<Gun>();
    public List<Gun> unlockableGuns = new List<Gun>();
    public int currentGun;


    public Transform adsPoint, gunHolder;
    private Vector3 gunStartPos;
    public float adsSpeed = 2f;

    void Start()
    {
        //currentGun--;
        //SwitchGun();
        activeGun = allGuns[PlayerPrefs.GetInt("LastSelectedGun") - 1];
        activeGun.gameObject.SetActive(true);


        gunStartPos = gunHolder.localPosition;
    }

    void Update()
    {
        //player movement

        //store y velocity
        float yStore = moveInput.y;

        Vector3 vertMove = transform.forward * joystick.Vertical ;
        Vector3 horiMove = transform.right * joystick.Horizontal;

        moveInput = horiMove + vertMove;
        moveInput.Normalize();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveInput = moveInput * runSpeed;
        }
        else
        {
            moveInput = moveInput * moveSpeed;
        }

        moveInput.y = yStore;

        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if (charChon.isGrounded)
        {
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        canJump = Physics.OverlapSphere(groundCheckPoint.position, .25f, whatIsGround).Length > 0;

        if (canJump)
        {
            canDoubleJump = false;
        }

        //Handle Jumping
        if (/*Input.GetKeyDown(KeyCode.Space)*/ CrossPlatformInputManager.GetButton("Jump") && canJump)
        {
            moveInput.y = jumpPower;

            canDoubleJump = true;

            //AudioManager.instance.PlaySFX(8);
        }
        else if (canDoubleJump && /*Input.GetKeyDown(KeyCode.Space)*/ CrossPlatformInputManager.GetButton("Jump"))
        {
            moveInput.y = jumpPower;

            canDoubleJump = false;

            //AudioManager.instance.PlaySFX(8);
        }

        charChon.Move(moveInput * Time.deltaTime);
        //camera rotation
        Vector2 mouseInput = new Vector2(); //= new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        if (Input.touchCount > 0 && Input.GetTouch(0).phase==TouchPhase.Moved)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;
            mouseInput.x = Input.GetTouch(0).deltaPosition.x;
            mouseInput.y = Input.GetTouch(0).deltaPosition.y;
        }
        if(invertX)
        {
            mouseInput.x = -mouseInput.x;
        }
        if(invertY)
        {
            mouseInput.y = -mouseInput.y;
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x*mouseSentivity, transform.rotation.eulerAngles.z);
        cameraTrans.rotation = Quaternion.Euler(cameraTrans.transform.rotation.eulerAngles + new Vector3(-mouseInput.y*mouseSentivity, 0f, 0f));

        muzzleFlash.SetActive(false);

        //Handle Shooting
        //single shots
        bool isShooting = CrossPlatformInputManager.GetButton("Shoot");
        if (/*Input.GetMouseButtonDown(0)*/ isShooting && activeGun.fireCounter <= 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraTrans.position, cameraTrans.forward, out hit, 50f))
            {
                if (Vector3.Distance(cameraTrans.position, hit.point) > 2f)
                {
                    firePoint.LookAt(hit.point);
                }
            }
            else
            {
                firePoint.LookAt(cameraTrans.position + (cameraTrans.forward * 30f));
            }
            //Instantiate(bullet, firePoint.position, firePoint.rotation);
            FireShot();
        }

        //repeating shots
        if (/*Input.GetMouseButtonDown(0)*/ isShooting && activeGun.canAutoFire)
        {
            if (activeGun.fireCounter <= 0)
            {
                FireShot();
            }
        }

        if (CrossPlatformInputManager.GetButtonDown("Switch Gun"))
        {
            SwitchGun();
        }


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchGun();
        }
        bool isScoping = CrossPlatformInputManager.GetButton("Scope");
        if (isScoping)//Input.GetMouseButtonDown(1))
        {

            CameraLogic.instance.ZoomIn(activeGun.zoomAmount);
        }

        if (isScoping)//Input.GetMouseButtonDown(1))Input.GetMouseButton(1))
        {
            gunHolder.position = Vector3.MoveTowards(gunHolder.position, adsPoint.position, adsSpeed * Time.deltaTime);
        }
        else
        {
            gunHolder.localPosition = Vector3.MoveTowards(gunHolder.localPosition, gunStartPos, adsSpeed * Time.deltaTime);
        }

        if (!isScoping)//Input.GetMouseButtonDown(1))Input.GetMouseButtonUp(1))
        {
            CameraLogic.instance.ZoomOut();
        }
    }
    public void FireShot()
    {
        if (activeGun.currentAmmo > 0)
        {

            activeGun.currentAmmo--;

            Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation);

            activeGun.fireCounter = activeGun.fireRate;

            //UIController.instance.ammoText.text = "AMMO: " + activeGun.currentAmmo;

            muzzleFlash.SetActive(true);
        }
    }



    public void SwitchGun()
    {
        activeGun.gameObject.SetActive(false);

        currentGun++;

        if (currentGun >= allGuns.Count)
        {
            currentGun = 0;
        }

        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);

        //UIController.instance.ammoText.text = "AMMO: " + activeGun.currentAmmo;

        firePoint.position = activeGun.firepoint.position;
    }
}
