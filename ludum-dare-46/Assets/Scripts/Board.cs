using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Board : MonoBehaviour
{
    private Dictionary<Vector3Int, Piece> pieces;

    private RectInt bounds;

    void Start()
    {
        PopulatePieces();
    }

    void PopulatePieces()
    {
        Piece[] allPieces = FindObjectsOfType<Piece>();

        pieces = new Dictionary<Vector3Int, Piece>(allPieces.Length);

        foreach (Piece piece in allPieces)
        {
            Vector3 position = piece.transform.position;
            Vector3Int logicalPosition = new Vector3Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y), Mathf.FloorToInt(position.z));
            piece.logicalPosition = logicalPosition;
            piece.board = this;
            pieces.Add(logicalPosition, piece);
        }
    }

    private bool MovePiece(Piece piece, Vector3Int target)
    {
        // Debug.Log("Move requested: " + piece.name + " from " + piece.logicalPosition + " to " + target);
        Vector3Int previousPosition = piece.logicalPosition;

        Piece pieceAtTarget = null;
        if (pieces.TryGetValue(target, out pieceAtTarget))
        {
            // There is a piece.
        }
        else
        {
            Piece pieceBelowPrevious = null;
            if (pieces.TryGetValue(previousPosition + new Vector3Int(0, -1, 0), out pieceBelowPrevious))
            {
                // pieceBelowPrevious.movedOff.Invoke(piece);
            }

            Piece pieceBelowTarget = null;
            if (pieces.TryGetValue(target + new Vector3Int(0, -1, 0), out pieceBelowTarget))
            {
                // There is a piece.
                pieces.Remove(previousPosition);
                piece.logicalPosition = target;
                pieces.Add(target, piece);
                pieceBelowTarget.movedOn.Invoke(piece);
                pieceBelowPrevious.movedOff.Invoke(piece);

                // piece.transform.position = target;

                if (piece.movementType == MovementType.Teleport)
                {
                    piece.transform.position = target;
                }
                else if (piece.movementType == MovementType.Slide)
                {
                    piece.transform.DOMove(target, .25f).SetEase(Ease.OutExpo);
                }

                return true;
            }
        }

        return false;
    }

    public bool MovePieces(List<Piece> groupedPieces, Vector3Int amount)
    {
        bool somethingMoved = false;
        bool movedDuringIteration = true;
        while (movedDuringIteration && groupedPieces.Count != 0)
        {
            movedDuringIteration = false;
            for (int i = groupedPieces.Count - 1; i >= 0; i--)
            {
                Piece piece = groupedPieces[i];
                bool moved = MovePiece(piece, piece.logicalPosition + amount);

                if (moved)
                {
                    groupedPieces.RemoveAt(i);
                    movedDuringIteration = true;
                    somethingMoved = true;
                }
            }
        }

        return somethingMoved;
    }

    public List<Piece> PushablePiecesInDirection(Piece piece, Direction direction)
    {
        List<Piece> piecesInDirection = new List<Piece>();
        Vector3Int startPosition = piece.logicalPosition;

        // Right now it's going to check out a fixed amount but this can probably be improved
        // by keeping track of board bounds.
        if (direction == Direction.Forward)
        {
            for (int i = 1; i < 20; i++)
            {
                Vector3Int checkPosition = startPosition + new Vector3Int(0, 0, i);
                Piece checkedPiece = null;
                if (pieces.TryGetValue(checkPosition, out checkedPiece))
                {
                    if (checkedPiece.pushable)
                    {
                        piecesInDirection.Add(checkedPiece);
                    }
                }
            }
        }
        else if (direction == Direction.Backward)
        {
            for (int i = 1; i < 20; i++)
            {
                Vector3Int checkPosition = startPosition + new Vector3Int(0, 0, -i);
                Piece checkedPiece = null;
                if (pieces.TryGetValue(checkPosition, out checkedPiece))
                {
                    if (checkedPiece.pushable)
                    {
                        piecesInDirection.Add(checkedPiece);
                    }
                }
            }
        }
        else if (direction == Direction.Right)
        {
            for (int i = 1; i < 20; i++)
            {
                Vector3Int checkPosition = startPosition + new Vector3Int(i, 0, 0);
                Piece checkedPiece = null;
                if (pieces.TryGetValue(checkPosition, out checkedPiece))
                {
                    if (checkedPiece.pushable)
                    {
                        piecesInDirection.Add(checkedPiece);
                    }
                }
            }
        }
        else if (direction == Direction.Left)
        {
            for (int i = 1; i < 20; i++)
            {
                Vector3Int checkPosition = startPosition + new Vector3Int(-i, 0, 0);
                Piece checkedPiece = null;
                if (pieces.TryGetValue(checkPosition, out checkedPiece))
                {
                    if (checkedPiece.pushable)
                    {
                        piecesInDirection.Add(checkedPiece);
                    }
                }
            }
        }

        return piecesInDirection;
    }
}

public enum Direction
{
    Forward,
    Backward,
    Right,
    Left
}

public enum MovementType
{
    Teleport,
    Slide
}
