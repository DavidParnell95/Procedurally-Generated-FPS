using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDoor : MazePassage
{
    //Rotation for door hing (180 degrees) 
    private static Quaternion
        normalRotation = Quaternion.Euler(0f, -90f, 0f),
        mirroredRotation = Quaternion.Euler(0f, 90f, 0f);

    public Transform hinge;
    public bool isMirrored;

    private MazeDoor OtherSideOfDoor
    {
        get
        {
            return otherCell.GetEdge(direction.GetOpposite()) as MazeDoor;
        }
    }

    public override void Initialize(MazeCell Primary, MazeCell other, MazeDirection direction)
    {
        base.Initialize(Primary, other, direction);

        //if other side of the door isnt empty
        if(OtherSideOfDoor != null)
        {
            isMirrored = true;//reverse direction 
            hinge.localScale = new Vector3(-1f, 1f, 1f);
            Vector3 p = hinge.localPosition;
            p.x = -p.x;
            hinge.localPosition = p;
        }

        for(int i =0; i< transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if(child != hinge)
            {
                child.GetComponent<Renderer>().material = cell.room.settings.wallMaterial;
            }
        }

    }

    //When player enters, show room
    public override void OnPlayerEntered()
    {
        OtherSideOfDoor.hinge.localRotation = hinge.localRotation = isMirrored ? mirroredRotation : normalRotation;
        OtherSideOfDoor.cell.room.Show();
    }

    //When player exits, hide room
    public override void OnPlayerExited()
    {
        OtherSideOfDoor.hinge.localRotation = hinge.localRotation = Quaternion.identity;
        OtherSideOfDoor.cell.room.Hide();
    }

}