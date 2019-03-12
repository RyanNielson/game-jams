randomize();
max_width = 16;
max_height = 8;
left = 2;
top = 2;
right = 17;
bottom = 9;
moves = irandom_range(30, 90);
moves_taken = 0;

tilemap = layer_tilemap_get_id("Tiles");

start_x = irandom_range(left + 1, right - 1);
start_y = irandom_range(top + 1, bottom - 1);

var current_x = start_x;
var current_y = start_y;
var loops = 0;
var min_x = start_x;
var max_x = start_x;
var min_y = start_y;
var max_y = start_y;

// TODO: Force stop after certain amount of loops to prevent infinite loops.
while (moves_taken < moves) {
    tilemap_set(tilemap, Tiles.FLOOR, current_x, current_y);
    // Take a step;
    var dir = irandom_range(0, 3);
    
    var new_current_x = current_x;
    var new_current_y = current_y;
    
    if (dir == 0) {
        new_current_x++;
        // Step right   
    }
    else if (dir == 1) {
        new_current_y++;
        // Step down   
    }
    else if (dir == 2) {
        // Step left
        new_current_x--;
    }
    else {
        // Step up
        new_current_y--;
    }
    
    new_current_x = clamp(new_current_x, left, right);
    new_current_y = clamp(new_current_y, top, bottom);
    
    // Only set current_* if it hasn't already been visited.
    if (!tilemap_get(tilemap, new_current_x, new_current_y)) {
        current_x = new_current_x;
        current_y = new_current_y;
        moves_taken++;
        
        if (current_x < min_x) {
            min_x = current_x;   
        }
        
        if (current_x > max_x) {
            max_x = current_x;   
        }
        
        if (current_y < min_y) {
            min_y = current_y;   
        }
        
        if (current_y > max_y) {
            max_y = current_y;   
        }
    }
    
    loops++;
    
    if (loops > 1000) {
        break;   
    }
}

var tilemap_width = tilemap_get_width(tilemap);
var tilemap_height = tilemap_get_height(tilemap);

for (var xx = 0; xx < tilemap_width; xx++) {
    for (var yy = 0; yy < tilemap_height; yy++) {
        // if the tile is empty and has a neighbouring floor, make it a wall.
        if (!tilemap_get(tilemap, xx, yy) && (
            tilemap_get(tilemap, xx + 1, yy) == Tiles.FLOOR ||
            tilemap_get(tilemap, xx - 1, yy) == Tiles.FLOOR ||
            tilemap_get(tilemap, xx, yy + 1) == Tiles.FLOOR ||
            tilemap_get(tilemap, xx, yy - 1) == Tiles.FLOOR ||
            tilemap_get(tilemap, xx + 1, yy + 1) == Tiles.FLOOR ||
            tilemap_get(tilemap, xx - 1, yy - 1) == Tiles.FLOOR ||
            tilemap_get(tilemap, xx + 1, yy - 1) == Tiles.FLOOR ||
            tilemap_get(tilemap, xx - 1, yy + 1) == Tiles.FLOOR)) {
                
            tilemap_set(tilemap, Tiles.WALL, xx, yy);    
        }
    }
}

//camera_set_view_pos(view_camera, 100, 100);
instance_create_layer(start_x * TILE_SIZE, start_y * TILE_SIZE, "Instances", obj_player);

var x_diff = max_x - min_x + 1;
var right_border = tilemap_width - max_x;
var move_x = ceil((min_x - right_border) / 2);

var y_diff = max_y - min_y + 1;
var bottom_border = tilemap_height - max_y;
var move_y = ceil((min_y - bottom_border) / 2);

camera_set_view_pos(view_camera, move_x * TILE_SIZE, move_y * TILE_SIZE);
