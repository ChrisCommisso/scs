using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementComponent : MonoBehaviour
{
    RaycastHit hitInfoGround;
    RaycastHit hitInfoRight;
    RaycastHit hitInfoLeft;
    RaycastHit hitInfoUp;
    RaycastHit hitInfoDown;
    float _groundDist;
    public void DoMovement(Vector3 Movement) 
    {
        float xToMove = Movement.x;
        float yToMove = Movement.y;
        while (xToMove != 0 && yToMove != 0) 
        {
            float xMovement = Mathf.Clamp(xToMove, -Width / 2, Width / 2);
            float yMovement = Mathf.Clamp(yToMove, -Height / 2, Height / 2);
            TryMove(new Vector3(xMovement, yMovement, 0));
            xToMove -= xMovement;
            yToMove -= yMovement;
        }
    }

    
    public void TryMove(Vector3 micromovement) 
    {
        transform.position += micromovement;
        if (Physics.Raycast(transform.position + new Vector3(0, Height, 0), Vector3.up * -1f, out hitInfoDown, Height, 0, QueryTriggerInteraction.Ignore)) 
        {
            if (hitInfoDown.distance <= Height) 
            {
                float delta = (Height - hitInfoDown.distance);
                transform.position += new Vector3(0, delta, 0);
            }
        }
        if (Physics.Raycast(transform.position, Vector3.up, out hitInfoUp, Height, 0, QueryTriggerInteraction.Ignore))
        {
            if (hitInfoUp.distance <= Height)
            {
                float delta = (Height - hitInfoDown.distance);
                transform.position += new Vector3(0, -delta, 0);
            }
        }
        if (Physics.Raycast(transform.position + new Vector3(-Width / 2f, Height / 2f, 0), Vector3.right, out hitInfoRight, Width, 0, QueryTriggerInteraction.Ignore))
        {
            if (hitInfoRight.distance <= Width)
            {
                float delta = (Width - hitInfoDown.distance);
                transform.position += new Vector3(-delta, 0, 0);
            }
        }
        if (Physics.Raycast(transform.position + new Vector3(Width / 2f, Height / 2f, 0), -Vector3.right, out hitInfoLeft, Width, 0, QueryTriggerInteraction.Ignore))
        {
            if (hitInfoLeft.distance <= Width)
            {
                float delta = (Width - hitInfoLeft.distance);
                transform.position += new Vector3(delta, 0, 0);
            }
        }
    }
    public float groundDist 
    {
        get { Physics.Raycast(transform.position + new Vector3(0, Height, 0), Vector3.up * -1f, out hitInfoGround, float.MaxValue, 0, QueryTriggerInteraction.Ignore);
            _groundDist = hitInfoGround.distance;
            return _groundDist; 
        }
    }
    public bool onGround 
    {
        get {
            Physics.Raycast(transform.position + new Vector3(0, Height, 0), Vector3.up * -1f, out hitInfoGround, float.MaxValue, 0, QueryTriggerInteraction.Ignore);
            _groundDist = hitInfoGround.distance;
            return ((_groundDist / Height) <= .99f);
        }
    }
    BoxCollider2D spaceBox;
    float _widthpixels;
    public void setDimensions(float width,float height,float offsetPixelsX,float offsetPixelsY) 
    {
        _widthpixels = width;
        _heightpixels = height;
        if (spaceBox == null) 
        {
            spaceBox = gameObject.AddComponent<BoxCollider2D>();
        }
        spaceBox.offset = new Vector2((offsetPixelsX* transform.localScale.x)/100f, (offsetPixelsY * transform.localScale.y)/100f);
        spaceBox.size = new Vector2(Width/100f, Height/100f);
    }
    public float Width 
    {
        get { return _widthpixels*transform.localScale.x; }
    }
    float _heightpixels;
    public float Height
    {
        get { return _widthpixels * transform.localScale.y; }
    }
}
