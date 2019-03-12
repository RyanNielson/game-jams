var tilemap = argument0;
var xx = argument1;
var yy = argument2;

return [tilemap_get_cell_x_at_pixel(tilemap, xx, yy), tilemap_get_cell_y_at_pixel(tilemap, xx, yy)];