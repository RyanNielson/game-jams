if (keyboard_check_pressed(ord("F"))) {
    window_set_fullscreen(!window_get_fullscreen());   
}
else if (keyboard_check_pressed(vk_escape)) {
    room_goto(rm_menu);
}