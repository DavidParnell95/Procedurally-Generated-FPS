using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int sizeX, sizeZ;
    public MazeCell cellPrefab;

    private MazeCell[,] cells;

    public float generationStepDelay;

    public IntVector2 size;

    public MazeCell GetCell(IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.z];
    }

    // Coroutine version, slows down generation so it can be observed
    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[sizeX, sizeZ];

        List<MazeCell> activateCells = new List<MazeCell>();
        DoFirstGenerationStep(activateCells);

        IntVector2 coordinates = RandomCoordinates;

        while(activateCells.Count >0)
        {
            yield return delay;
            DoNextGenerationStep(activateCells);
        }
    }

    /***** Instant Version, non coroutine ******
    public void Generate()
    {
        
        cells = new MazeCell[sizeX,sizeZ];

        for(int x =0; x<sizeX;x++)
        {
            for(int z = 0; z<sizeZ;z++)
            {
                CreateCell(x, z);
            }
        }
     }
     ********************************************/



    

    private MazeCell CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;

        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell" + coordinates.x + "," + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - sizeX * .5f + .5f,
            0f,
            coordinates.z - sizeZ * .5f + .5f);
        return newCell;
    }

    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(RandomCoordinates.Range(0, size.x), Random.Range(0,size.z));
        }
    }

    public bool ContainsCoordinates(IntVector2 coordinates)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }

    private void DoFirstGenerationStep(List<MazeCell> activateCells)
    {
        activateCells.Add(CreateCell(RandomCoordinates));
    }

    private void DoNextGenerationStep(List<MazeCell> activateCells)
    {
        int currentIndex = activateCells.Count - 1;
        MazeCell currentCell = activateCells[currentIndex];
        MazeDirection direction = MazeDirections.RandomValue;

        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();

        if (ContainsCoordinates(coordinates) && GetCell(coordinates) == null)
        {
            activateCells.Add(CreateCell(coordinates));
        }
        else
        {
            activateCells.RemoveAt(currentIndex);
        }
    }
}
