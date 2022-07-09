using UnityEngine;

public class SwitchMoveCorridor2 : MonoBehaviour
{
    // Switch control.
    [HideInInspector] public bool isTriggered { get; set; } = false;


    // Attached corridor.
    [SerializeField] private GameObject corridor;
    // Attached player.
    [SerializeField] private GameObject player;
    // Attached twin switch.
    [SerializeField] private GameObject switch1;

    // Attached objects' components.
    private Transform corridorTr;
    private Transform playerTr;
    private CharacterController charCtrl;
    private SwitchMoveCorridor1 switch1Ref;


    private void Awake()
    {
        // Init components.
        corridorTr = corridor.transform;
        playerTr = player.transform;
        charCtrl = player.GetComponent<CharacterController>();
        switch1Ref = switch1.GetComponent<SwitchMoveCorridor1>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player has triggered the switch.
        if (other.name == "player" && isTriggered == false)
        {
            // Disable second-time triggering of current switch.
            isTriggered = true;
            // Enable the twin switch.
            switch1.gameObject.SetActive(true);
            switch1Ref.isTriggered = false;

            // Move the GameObject to the desired location.
            MoveCorridor();

            // Disable the current switch.
            this.gameObject.SetActive(false);
        }
    }

    // Move the GameObject to the desired location.
    private void MoveCorridor()
    {
        // Set the corridor's attributes, and the player's attribute accordingly.
        charCtrl.enabled = false;
        corridorTr.localPosition = new Vector3(78.086f, 204.56f, -50.707f);
        corridorTr.eulerAngles = new Vector3(0.0f, -90.0f, 0.0f);
        charCtrl.enabled = true;
        playerTr.SetParent(null);
    }
}
