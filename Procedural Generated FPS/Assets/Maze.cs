using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour
{
    public IntVector2 size;
    public MazeCell cellPrefab;
    public float generationStepDelay;
    public MazePassage passagePrefab;
    public MazeWall[] wallprefabs;
    public MazeDoor doorPrefab;
    public MazeRoomSettings[] roomSettings;

    [Range(0f, 1f)]
    public float doorProbability;

    private MazeCell[,] cells;
    private List<MazeRoom> rooms = new List<MazeRoom>();

    //Get random coordinates
    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
        }
    }

    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }

    //get cell at coorinates
    public MazeCell GetCell(IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.z];
    }

    //generate maze
    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);//create starting cell to build from

        //After 1st cell is create
        while (activeCells.Count > 0)
        {
            yield return delay;
            DoNextGenerationStep(activeCells);//Build next cel 
        }

        //hide extra rooms
        for(int i=0; i<rooms.Count; i++)
        {
            rooms[i].Hide();
        }
    }

    //Creates first cell to build from
    private void DoFirstGenerationStep(List<MazeCell> activeCells)
    {
        MazeCell newCell = CreateCell(RandomCoordinates);//Picks random place to start from 
        newCell.Initialize(CreateRoom(-1));
        activeCells.Add(newCell);//Add cell
    }

    //Create next cell
    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];

        //If current cell is already complete
        if (currentCell.IsFullyInitialized)
        {
            activeCells.RemoveAt(currentIndex);//Remove the new cell created
            return;
        }

        MazeDirection direction = currentCell.RandomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();

        if (ContainsCoordinates(coordinates))
        {
            MazeCell neighbor = GetCell(coordinates);

            //If neighbour is empty
            if (neighbor == null)
            {
                neighbor = CreateCell(coordinates);//Create cell in empty neighbour
                CreatePassage(currentCell, neighbor, direction);//Create passage between current cell and neighbour
                activeCells.Add(neighbor);//Add neighbour cell
            }

            //Check if 2 rooms have the same settings
            else if(currentCell.room.settingsIndex == neighbor.room.settingsIndex)
            {
                CreatePassageInSameRoom(currentCell, neighbor, direction);
            }

            else
            {
                CreateWall(currentCell, neighbor, direction);
                
            }
        }
        else
        {
            CreateWall(currentCell, null, direction);
        }
    }

    //Create Cell
    private MazeCell CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;//Instantiate mazeCell object
        cells[coordinates.x, coordinates.z] = newCell;

        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;//Rename with coordinates
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);

        return newCell;
    }


    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage prefab = Random.value < doorProbability ? doorPrefab : passagePrefab;
        MazePassage passage = Instantiate(prefab) as MazePassage;

        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(prefab) as MazePassage;//Instantiate passage

        if(passage is MazeDoor)
        {
            otherCell.Initialize(CreateRoom(cell.room.settingsIndex));
        }
        else
        {
            otherCell.Initialize(cell.room);
        }

        passage.Initialize(otherCell, cell, direction.GetOpposite());//Initialize passage in opposite direction
    }

    //Create passage between rooms, ensurinh that theres no door
    private void CreatePassageInSameRoom(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());

        // Check if rooms are connecting and assimilates them if they are
        if(cell.room != otherCell.room)
        {
            MazeRoom roomToAss = otherCell.room;
            cell.room.Assimilate(roomToAss);
            rooms.Remove(roomToAss);
            Destroy(roomToAss);
        }
    }

    //Create Wall, instantiates wall prefab
    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeWall wall = Instantiate(wallprefabs[Random.Range(0, wallprefabs.Length)]) as MazeWall;
        wall.Initialize(cell, otherCell, direction);

        if (otherCell != null)
        {
            wall = Instantiate(wallprefabs[Random.Range(0, wallprefabs.Length)]) as MazeWall;//instantiate wall prefab
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

    //Generates Rooms
    private MazeRoom CreateRoom(int indextToExclude)
    {
        MazeRoom newRoom = ScriptableObject.CreateInstance<MazeRoom>();
        newRoom.settingsIndex = Random.Range(0, roomSettings.Length);

        if(newRoom.settingsIndex == indextToExclude)
        {
            newRoom.settingsIndex = (newRoom.settingsIndex + 1) % roomSettings.Length;
        }

        newRoom.settings = roomSettings[newRoom.settingsIndex];
        rooms.Add(newRoom);
        return newRoom;
    }
}