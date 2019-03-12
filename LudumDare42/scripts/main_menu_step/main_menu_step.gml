if (keyboard_check_pressed(vk_down)) {
    selected_item++; 
    audio_play_sound(snd_menu_move, 100, false);
}
else if (keyboard_check_pressed(vk_up)) {
    selected_item--;
    audio_play_sound(snd_menu_move, 100, false);
}

selected_item = clamp(selected_item, 0, total_items - 1);

if (keyboard_check_pressed(vk_enter)) {    
    audio_play_sound(snd_menu_select, 100, false);
    switch (selected_item) {
        case 0:
            room_goto_next();
            break;
        case 1:
            if (continue_level != noone) {
                room_goto(continue_level);
            }
            break;
        case 2:
            room_goto(rm_random);
            break;
        case 3:
            room_goto(rm_credits);
            break;
        default:
            game_end();
            break;
    }
    
}