var movement_direction = irandom(3);

if (movement_direction == 0) {
    return Direction.Right;
}
else if (movement_direction == 1) {
    return Direction.Up;
}
else if (movement_direction == 2) {
    return Direction.Left;
}

return Direction.Down;
