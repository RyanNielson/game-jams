draw_set_font(fnt_pixel_large);
draw_set_halign(fa_center);
draw_set_valign(fa_middle);
draw_text_shadowed_large(HALF_WIDTH, HALF_HEIGHT - 128, "CREDITS");

draw_set_font(fnt_pixel);
draw_set_halign(fa_left);
draw_set_valign(fa_top);
draw_text(x, y, "created by ryan nielson");
draw_text(x, y + 10, "- https://nielson.io");
draw_text(x, y + 20, "- https://github.com/ryannielson");
draw_text(x, y + 30, "- https://ryannielson.itch.io");
draw_text(x, y + 64, "audio by kkira");
draw_text(x, y + 74, "- https://opengameart.org/content/byte-man-sfx-1");
draw_text(x, y + 96, "graphics by RLTiles");
draw_text(x, y + 106, "- https://opengameart.org/content/dungeon-crawl-32x32-tiles");


draw_set_halign(fa_left);
draw_set_valign(fa_bottom);
draw_text(x, GAME_HEIGHT - 40, "esc to go back to the main menu");