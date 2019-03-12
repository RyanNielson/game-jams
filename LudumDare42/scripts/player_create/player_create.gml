level_completed = false;
stairs_appeared = false;
can_undo = room >= 4 || room == rm_random;

move_stack = ds_stack_create();

tilemap = layer_tilemap_get_id("Tiles");
current_cell = position_to_cell(tilemap, x, y);
current_cell_x = current_cell[0];
current_cell_y = current_cell[1];

var tilemap_width = tilemap_get_width(tilemap);
var tilemap_height = tilemap_get_height(tilemap);

total_tiles = 0;
covered_tiles = 1;


for (xx = 0; xx < tilemap_width; xx++) {
    for (yy = 0; yy < tilemap_height; yy++) {
        if (tilemap_get(tilemap, xx, yy) == Tiles.FLOOR) {
            total_tiles++;
        }
    }
}