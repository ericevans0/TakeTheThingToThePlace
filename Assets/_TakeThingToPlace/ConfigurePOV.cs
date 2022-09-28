using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConfigurePOV : MonoBehaviour
{
    public enum CameraShot { WideShot, FollowClose, FirstPerson };

    [SerializeField] private CameraShot chosenShot = CameraShot.FollowClose;

//    private CinemachineVirtualCamera sceneCam;
    private CinemachineVirtualCamera followCam;
//    private CinemachineVirtualCamera firstPersonCam;

    private bool ControlledHere;

    void Start()
    {
        Debug.Log("Reached ConfigurePOV.Start");

        // The scene may be loaded without Photon for quick editor experiments. This should not happen in a regular build.
        // When we are in a PhotonNetwork room, we control when photonView.IsMine. Otherwise, it is up to another client to control.
        //       ControlledHere = !PhotonNetwork.InRoom || photonView.IsMine;
        ControlledHere = true; //Single-player prototype

        if (ControlledHere)
        {
            GetComponent<PlayerInput>().enabled = true;
            SetUpCamsToFollowThisCharacter();
            //ActivateChosenPOV();
            ActivateThirdPerson(); //Single-POV prototype
        }
        else
        {
            // This isn't my character, so I don't want to control their movement.
            //            GetComponent<StarterAssets.FirstPersonController>().enabled = false;
            GetComponent<StarterAssets.ThirdPersonController>().enabled = false;
        }
    }

    private void SetUpCamsToFollowThisCharacter()
    {
        Debug.Log("Reached AssignRootTransformToCams");
        followCam = GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>();
 //       firstPersonCam = GameObject.Find("FirstPersonCamera").GetComponent<CinemachineVirtualCamera>();
//        sceneCam = GameObject.Find("SceneOverviewCamera").GetComponent<CinemachineVirtualCamera>();

        Transform rootTransform = transform.Find("PlayerCameraRoot");
        followCam.Follow = rootTransform;
//        firstPersonCam.Follow = rootTransform;
        // sceneCam is static, so it doesn't follow anything.
    }

    public void ActivateThirdPerson()
    {
        //Debug.Log("CameraControls.ActivateThirdPerson for photonView.ViewID = " + photonView.ViewID);

//        sceneCam.enabled = false;
        followCam.enabled = true;
 //       firstPersonCam.enabled = false;

        //Reverse whatever is hidden for first-person    //    face.Show();

        // Activate the third-person controller
        //        GetComponent<StarterAssets.FirstPersonController>().enabled = false;
        GetComponent<StarterAssets.ThirdPersonController>().enabled = true;
    }

}
