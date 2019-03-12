/// add_wall(position_x, position_y, check_tile)

var wall_position_x = argument0;
var wall_position_y = argument1;
var check_tile = argument2;

if (tile_layer_find(WALL_DEPTH, wall_position_x, wall_position_y) == -1 && check_tile == -1)
{
    if (irandom(5) == 0)
    {
        var decal_choice = choose(TILE_SIZE * 10, TILE_SIZE * 11, TILE_SIZE * 12, TILE_SIZE * 13, TILE_SIZE * 14);
        tile_add(bg_cave, decal_choice, 0, TILE_SIZE, TILE_SIZE, wall_position_x, wall_position_y, WALL_DEPTH);
    }
    else
    {
        tile_add(bg_cave, TILE_SIZE * 9, 0, TILE_SIZE, TILE_SIZE, wall_position_x, wall_position_y, WALL_DEPTH);
    }

    instance_create(wall_position_x, wall_position_y, obj_wall);
}
