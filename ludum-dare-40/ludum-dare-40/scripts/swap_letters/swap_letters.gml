var start_x = argument0;
var start_y = argument1;
var movement_direction = argument2;

var swap_x = start_x;
var swap_y = start_y;
    
if (movement_direction == Direction.Right) {
    swap_x++;
}
else if (movement_direction == Direction.Up) {
    swap_y--;
}
else if (movement_direction == Direction.Left) {
    swap_x--;
}
else if (movement_direction == Direction.Down) {
    swap_y++;
}
    
swap_x = clamp(swap_x, 0, grid_width - 1);
swap_y = clamp(swap_y, 0, grid_height - 1);

selected_x = swap_x;
selected_y = swap_y;
    
var start_letter = grid[# start_x, start_y];
var swap_letter = grid[# swap_x, swap_y];
    
grid[# start_x, start_y] = swap_letter;
grid[# swap_x, swap_y] = start_letter;