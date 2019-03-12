#define game
enum GameState {
    MainMenu,
    Game,
    Dead,
    Retry,
    StartGame
}

enum InputState {
    MouseAndKeyboard,
    Gamepad
}

#define game_create
if (instance_number(obj_game) > 1) {
    instance_destroy();
}

input_state = InputState.MouseAndKeyboard;

last_mouse_x = mouse_x;
last_mouse_y = mouse_y;


randomize();

room_speed = 60;

display_set_gui_size(480, 270);

prev_state = noone;
state = GameState.MainMenu;

player = instance_create(view_wview / 8, view_hview / 2, obj_player);
star_field = instance_create(view_wview, view_yview, obj_star_field);
camera = instance_create(0, 0, obj_camera);

audio_stop_sound(snd_bg_firstplay);
audio_stop_sound(snd_bg_retry);
audio_stop_sound(snd_title);

if (instance_exists(obj_retryer)) {
    //destroy(obj_retryer);
    state = GameState.Game;
    audio_play_sound(snd_bg_retry, 1000, true);
}
else {
    instance_create(0, 0, obj_main_menu);
}

time = 0.0;
flash_alpha = 0;




#define game_step
if (state != prev_state) {
    // Transitioned to Game state.
    if (state == GameState.StartGame) {
        alarm[1] = room_speed * 2;
        audio_stop_sound(snd_title);
        audio_play_sound(snd_bg_firstplay, 1000, true);
    }
    if (state == GameState.Game) {
        window_set_cursor(cr_none);
        alarm[0] = 1;
        star_field.leave_warp = true;
        destroy(obj_main_menu);
        instance_create(0, 0, obj_spawner);
        retrying = false;
        time = 0;
    }
    else if (state == GameState.MainMenu) {
        audio_play_sound(snd_title, 1000, true);
        
        destroy(obj_spawner);
        retrying = false;
    }
    else if (state == GameState.Dead) {
        save_highscore(time);
        highscore_add("HighScore",string(time));
        with (instance_create(0, 0, obj_died_menu)) {
            time = other.time;
        }
        
        destroy(obj_spawner);
        retrying = false;
    }
    else if (state == GameState.Retry) {
        instance_create(0, 0, obj_retryer);
        room_goto(rm_game);
    }

    prev_state = state;
}

if (state == GameState.MainMenu) {
    
}
else if (state == GameState.Game) {
    time += 1 / room_speed;
}
else if (state == GameState.StartGame) {
    flash_alpha += 1 / (room_speed * 1.75);
}

menu_selected = 1;

var last_mouse_x_diff = abs(last_mouse_x - mouse_x);
var last_mouse_y_diff = abs(last_mouse_y - mouse_y);
last_mouse_x = mouse_x;
last_mouse_y = mouse_y;
//if (last_mouse_x != mouse_x || last_mouse_y != mouse_y) {
if (last_mouse_x_diff >= 3 || last_mouse_y_diff >= 3) {
    //last_mouse_x = mouse_x;
    //last_mouse_y = mouse_y;
    input_state = InputState.MouseAndKeyboard;
}
else if (gamepad_used()) {
    input_state = InputState.Gamepad;
}

if (input_state == InputState.Gamepad || state == GameState.Game) {
    window_set_cursor(cr_none);
}
else {
    window_set_cursor(cr_default);
}





#define game_draw_gui
if (state == GameState.Game) {
    draw_text_extended(string(time) + "s", view_wview / 2, 1, fnt_buttons, c_white, fa_center, fa_top)
}
else if (state == GameState.StartGame) {
    //flash_alpha = lerp(0, 1, flash_lerp);
    draw_set_colour(c_white);
    draw_set_alpha(flash_alpha);
    draw_rectangle(0, 0, view_wview, view_hview, false);
    draw_set_alpha(1);
}