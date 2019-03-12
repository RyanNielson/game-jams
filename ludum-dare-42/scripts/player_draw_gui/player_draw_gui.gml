if (room != rm_random) {
    draw_set_font(fnt_pixel);
    draw_set_halign(fa_left);
    draw_set_valign(fa_top);
    draw_text_shadowed(32, 8, "Level " + string(room) + " of 10");;
}
