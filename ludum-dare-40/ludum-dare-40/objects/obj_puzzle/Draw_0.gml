draw_puzzle_grid(grid, offset);
draw_sprite(spr_cursor, 0, pos_with_margin(selected_x * char_width, false), pos_with_margin(selected_y * char_height, false));

if (swap_mode) {
    var grid_width = ds_grid_width(grid);
    var grid_height = ds_grid_height(grid);
    
    if (selected_x + 1 < grid_width) {
        draw_sprite(spr_cursor, 1, pos_with_margin((selected_x + 1) * char_width, false), pos_with_margin(selected_y * char_height, false));
    }
    
    if (selected_y - 1 >= 0) {
        draw_sprite(spr_cursor, 1, pos_with_margin(selected_x * char_width, false), pos_with_margin((selected_y - 1) * char_height, false));
    }
    
    if (selected_x - 1 >= 0) {
        draw_sprite(spr_cursor, 1, pos_with_margin((selected_x - 1) * char_width, false), pos_with_margin(selected_y * char_height, false));
    }
    
    if (selected_y + 1 < grid_height) {
        draw_sprite(spr_cursor, 1, pos_with_margin(selected_x * char_width, false), pos_with_margin((selected_y + 1) * char_height, false));
    }
}

draw_set_font(fnt_bold);
draw_set_halign(fa_right);
draw_set_valign(fa_top);
draw_text(pos_with_margin(TARGET_WIDTH, true), pos_with_margin(0, false), "Moves: " + string(moves));
draw_text(pos_with_margin(TARGET_WIDTH, true), pos_with_margin(64, false), "Time: " + string(floor(time)));

draw_set_halign(fa_right);
draw_set_valign(fa_bottom);
for (var i = 0; i < array_length_1d(words); i++) {
    var word = words[i];   
    
    if (found_words[i]) {
        draw_set_color(c_red);   
    }
    else {
        draw_set_color(c_white);
    }
    
    draw_text(pos_with_margin(TARGET_WIDTH, true), pos_with_margin(TARGET_HEIGHT - i * char_height, true), word);
    draw_set_color(c_white);
}
