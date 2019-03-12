draw_set_font(fnt_pixel_large);
draw_set_halign(fa_center);
draw_set_valign(fa_middle);
draw_text_shadowed_large(HALF_WIDTH, HALF_HEIGHT - 128, GAME_NAME);

draw_set_font(fnt_pixel);
for (var i = 0; i < total_items; i++) {
    var menu_item = i == selected_item ? "> " + items[i] + " <" : items[i];

    if (i == 1 && continue_level == noone) {
        draw_set_color(c_gray);
        draw_text(HALF_WIDTH, HALF_HEIGHT + (i * 16), menu_item);
    }
    else if (i == 1) {
        // TODO: Clean this up, it can probably be brought outside this conditional to share code.
        menu_item = i == selected_item ? "> " + items[i] + " [Level " + string(continue_level) + "]" + " <" : items[i] + " [Level " + string(continue_level) + "]";
        draw_set_color(c_white);
        draw_text(HALF_WIDTH, HALF_HEIGHT + (i * 16), menu_item);
    }
    else {
        draw_set_color(c_white);
        draw_text(HALF_WIDTH, HALF_HEIGHT + (i * 16), menu_item);
    }
}

draw_set_font(fnt_pixel);
draw_set_halign(fa_left);
draw_set_valign(fa_bottom);
draw_text(32, GAME_HEIGHT - 60, "arrows to move, enter to select");
draw_text(32, GAME_HEIGHT - 50, "f to toggle fullscreen");
draw_text(32, GAME_HEIGHT - 40, "esc to go back to the main menu at any time");

draw_set_halign(fa_right);
draw_set_valign(fa_bottom);
draw_text(GAME_WIDTH - 32, GAME_HEIGHT - 10, "a game by ryan nielson for ludum dare 42");