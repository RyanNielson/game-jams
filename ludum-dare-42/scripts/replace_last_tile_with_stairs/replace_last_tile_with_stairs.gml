var tilemap = argument0;

var tilemap_width = tilemap_get_width(tilemap);
var tilemap_height = tilemap_get_height(tilemap);
//var player_position = position_to_cell(tilemap, obj_player.current_cell_x, obj_player

for (xx = 0; xx < tilemap_width; xx++) {
    for (yy = 0; yy < tilemap_height; yy++) {
        if (tilemap_get(tilemap, xx, yy) == Tiles.FLOOR) {
            if (!(obj_player.current_cell_x == xx && obj_player.current_cell_y == yy)) {
                
                if (room == rm_10) {
                    tilemap_set(tilemap, Tiles.CHEST,xx, yy);
                }
                else {
                    tilemap_set(tilemap, Tiles.STAIRS,xx, yy);
                }
            }
        }
    }
}
