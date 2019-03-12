if (!swap_mode) {
    if (keyboard_check_pressed(vk_right)) {
        selected_x++;   
    }
    if (keyboard_check_pressed(vk_left)) {
        selected_x--;   
    }
    if (keyboard_check_pressed(vk_up)) {
        selected_y--;   
    }
    if (keyboard_check_pressed(vk_down)) {
        selected_y++;   
    }

    selected_x = clamp(selected_x, 0, ds_grid_width(grid) - 1);
    selected_y = clamp(selected_y, 0, ds_grid_height(grid) - 1);
}
else {
    if (keyboard_check_pressed(vk_right)) {
        audio_play_sound(snd_letter_swap_short, 100, false);
        swap_letters(selected_x, selected_y, Direction.Right);
        swap_mode = false;
        moves++;
        has_swapped = true;
    }
    if (keyboard_check_pressed(vk_left)) {
        audio_play_sound(snd_letter_swap_short, 100, false);
        swap_letters(selected_x, selected_y, Direction.Left);    
        swap_mode = false;
        moves++;
        has_swapped = true;
    }
    if (keyboard_check_pressed(vk_up)) {
        audio_play_sound(snd_letter_swap_short, 100, false);
        swap_letters(selected_x, selected_y, Direction.Up);     
        swap_mode = false;
        moves++;
        has_swapped = true;
    }
    if (keyboard_check_pressed(vk_down)) {
        audio_play_sound(snd_letter_swap_short, 100, false);
        swap_letters(selected_x, selected_y, Direction.Down);  
        swap_mode = false;
        moves++;
        has_swapped = true;
    }
}

if (keyboard_check_pressed(vk_space)) {
    swap_mode = !swap_mode;   
}

if (has_swapped && time_start == noone) {
    time_start = date_current_datetime();
}

if (time_start) {
    time = date_second_span(time_start, date_current_datetime());
}
for (var i = 0; i < array_length_1d(words); i++) {
    found_words[i] = grid_contains_word(words[i]);
}