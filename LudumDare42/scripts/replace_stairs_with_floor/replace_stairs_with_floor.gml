var tilemap = argument0;

var tilemap_width = tilemap_get_width(tilemap);
var tilemap_height = tilemap_get_height(tilemap);

for (xx = 0; xx < tilemap_width; xx++) {
    for (yy = 0; yy < tilemap_height; yy++) {
        if (tilemap_get(tilemap, xx, yy) == Tiles.STAIRS || tilemap_get(tilemap, xx, yy) == Tiles.CHEST) {
            tilemap_set(tilemap, Tiles.FLOOR,xx, yy);
        }
    }
}
