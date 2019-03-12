var grid = argument0;
var offset = argument1;
var grid_width = ds_grid_width(grid);
var grid_height = ds_grid_height(grid);

draw_set_font(fnt_bold);
for (xx = 0; xx < grid_width; xx++) {
    for (yy = 0; yy < grid_height; yy++) {
        draw_set_halign(fa_center);
        draw_set_valign(fa_top);
        draw_text(pos_with_margin(xx * char_width, false), pos_with_margin(yy * char_height, false), grid[# xx, yy]);
        //draw_text(xx * offset + offset, yy * offset + offset, grid[# xx, yy]);
    }
}