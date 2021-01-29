using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flipperScript : MonoBehaviour
{
    private HingeJoint hinge;
    private JointSpring jointSpring;
    private DragonGameManager mngr;

    public float restPosition; //Aantal graden dat wordt toegevoegd aan de oorspronkelijke rotatie van de Y-as als de flipper niet in gebruik is
    public float activePosition; //Aantal graden dat wordt toegevoegd aan de oorspronkelijke rotatie van de Y-as als de flipper in gebruik is
    public float flipperForce; //Kracht waarmee de spring de flipper voortstuwd
    public float flipperDamper; //Kracht die de veer versloomt

    public string inputName; //Naam van de Flipper. FlipperL / FlipperR (tijdelijk)

    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponent<HingeJoint>();


        mngr = GameObject.FindGameObjectWithTag("GameController").GetComponent<DragonGameManager>();
        jointSpring.spring = flipperForce; //Kracht van de veer wordt bepaald door hitStrenght
        jointSpring.damper = flipperDamper; //Versloomt de beweging van de veer
    }

    // Update is called once per frame
    void Update()
    {
        FlipperMovement();
    }

    void FlipperMovement()
    {
        hinge.spring = jointSpring;

        if (Input.GetAxisRaw(inputName) == 1)
        {
            jointSpring.targetPosition = activePosition;
        }
        else
        {
            jointSpring.targetPosition = restPosition;
        }

        //sounds + GameManager link
        if(Input.GetButtonDown(inputName))
        {
            bool isLeft = inputName.Contains("Left");
            mngr.Flipper(isLeft);
            AudioManager.Instance.Play("flipperUp");

        } else if(Input.GetButtonUp(inputName))
        {
            AudioManager.Instance.Play("flipperDown");
        }
    }
}
