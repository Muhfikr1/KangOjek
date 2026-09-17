using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMotorCinemachineCamera : MonoBehaviour
{
    private const string TargetSceneName = "MenuMotor";
    private const string RigName = "MenuMotor Cinemachine Camera";

    [SerializeField] private Vector3 cameraPosition = new Vector3(98.05f, 70.71f, -10f);
    [SerializeField] private float referenceAspect = 2400f / 1080f;
    [SerializeField] private float referenceOrthographicSize = 5f;
    [SerializeField] private float maxOrthographicSize = 6.2f;

    private Camera unityCamera;
    private CinemachineCamera cinemachineCamera;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Bootstrap()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
        ConfigureScene(SceneManager.GetActiveScene());
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ConfigureScene(scene);
    }

    private static void ConfigureScene(Scene scene)
    {
        if (scene.name != TargetSceneName) return;

        GameObject rig = GameObject.Find(RigName);
        if (rig == null)
        {
            rig = new GameObject(RigName);
        }

        MenuMotorCinemachineCamera controller = rig.GetComponent<MenuMotorCinemachineCamera>();
        if (controller == null)
        {
            controller = rig.AddComponent<MenuMotorCinemachineCamera>();
        }

        controller.Configure();
    }

    private void Configure()
    {
        unityCamera = Camera.main;
        if (unityCamera == null) return;

        unityCamera.orthographic = true;
        unityCamera.transform.position = cameraPosition;
        unityCamera.transform.rotation = Quaternion.identity;

        CinemachineBrain brain = unityCamera.GetComponent<CinemachineBrain>();
        if (brain == null)
        {
            brain = unityCamera.gameObject.AddComponent<CinemachineBrain>();
        }

        brain.LensModeOverride = new CinemachineBrain.LensModeOverrideSettings
        {
            Enabled = true,
            DefaultMode = LensSettings.OverrideModes.Orthographic
        };
        brain.DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Styles.Cut, 0f);

        cinemachineCamera = GetComponent<CinemachineCamera>();
        if (cinemachineCamera == null)
        {
            cinemachineCamera = gameObject.AddComponent<CinemachineCamera>();
        }

        cinemachineCamera.Priority.Value = 100;
        ApplyCameraSettings();
    }

    private void LateUpdate()
    {
        ApplyCameraSettings();
    }

    private void ApplyCameraSettings()
    {
        if (unityCamera == null)
        {
            unityCamera = Camera.main;
        }

        if (cinemachineCamera == null || unityCamera == null) return;

        transform.position = cameraPosition;
        transform.rotation = Quaternion.identity;

        LensSettings lens = cinemachineCamera.Lens;
        lens.ModeOverride = LensSettings.OverrideModes.Orthographic;
        lens.OrthographicSize = CalculateOrthographicSize();
        lens.NearClipPlane = 0.1f;
        lens.FarClipPlane = 1000f;
        cinemachineCamera.Lens = lens;
    }

    private float CalculateOrthographicSize()
    {
        float aspect = unityCamera.aspect;
        if (aspect <= 0f) return referenceOrthographicSize;

        float size = referenceOrthographicSize;
        if (aspect < referenceAspect)
        {
            size *= referenceAspect / aspect;
        }

        return Mathf.Min(size, maxOrthographicSize);
    }
}
