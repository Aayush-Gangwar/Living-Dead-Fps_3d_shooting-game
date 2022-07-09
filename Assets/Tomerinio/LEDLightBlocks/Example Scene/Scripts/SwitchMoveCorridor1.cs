using UnityEngine;

public class SwitchMoveCorridor1 : MonoBehaviour
{
    // Switch control.
    [HideInInspector] public bool isTriggered { get; set; } = false;


    // Attached corridor.
    [SerializeField] private GameObject corridor;
    // Attached player.
    [SerializeField] private GameObject player;
    // Attached twin switch.
    [SerializeField] private GameObject switch2;

    // Attached objects' components.
    private Transform corridorTr;
    private Transform playerTr;
    private CharacterController charCtrl;
    private SwitchMoveCorridor2 switch2Ref;


    private void Awake()
    {
        // Init components.
        corridorTr = corridor.transform;
        playerTr = player.transform;
        charCtrl = player.GetComponent<CharacterController>();
        switch2Ref = switch2.GetComponent<SwitchMoveCorridor2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player has triggered the switch.
        if (other.name == "player" && isTriggered == false)
        {
            // Disable second-time triggering of current switch.
            isTriggered = true;
            // Enable the twin switch.
            switch2.gameObject.SetActive(true);
            switch2Ref.isTriggered = false;

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
        corridorTr.localPosition = new Vector3(12.4f, 111.6f, -77.9f);
        corridorTr.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        playerTr.SetParent(corridorTr);
    }
}
