#define main_menu

#define main_menu_create
play_button = instance_create(view_wview / 2, view_hview / 2, obj_button_play);
about_button = instance_create(view_wview / 2, view_hview / 2 + 24, obj_button_about);
exit_button = instance_create(view_wview / 2, view_hview / 2 + 48, obj_button_exit);
reset_button = instance_create(view_wview / 2, view_hview / 2 + 96, obj_button_reset);

high_score = get_highscore();

selected_button = 0;
gamepad_input_delay = 0;
input_state = obj_game.input_state;

#define main_menu_show_about
instance_create(0, 0, obj_about);
with (obj_main_menu) {
    instance_destroy();
}


#define main_menu_show_main
obj_main_menu.play_button = instance_create(view_wview / 2, view_hview / 2, obj_button_play);
obj_main_menu.about_button = instance_create(view_wview / 2, view_hview / 2 + 24, obj_button_about);
obj_main_menu.exit_button = instance_create(view_wview / 2, view_hview / 2 + 48, obj_button_exit);

#define main_menu_destroy
destroy(play_button);
destroy(about_button);
destroy(exit_button);
destroy(reset_button);


#define main_menu_draw_gui
var title_bg_colour = make_colour_rgb(105, 215, 255);

draw_rectangle_colour(view_wview / 2 - 126, view_hview / 2 - 60, view_wview / 2 + 121, view_hview / 2 - 32, title_bg_colour, title_bg_colour, title_bg_colour, title_bg_colour, false);
draw_text_shadowed("MODE_SHIFT", view_wview / 2, view_hview / 2 - 32, fnt_title, c_white, fa_center, fa_bottom); 
draw_text_shadowed("High Score: " + string(high_score) + "s", view_wview / 2, view_hview / 2 + 72, fnt_buttons, c_white, fa_center, fa_middle); 