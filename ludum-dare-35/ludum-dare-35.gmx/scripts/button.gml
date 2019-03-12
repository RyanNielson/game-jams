#define button


#define button_create
image_speed = 0;
text = "Button";
action = noone;
pressable = false;
hovered = false;

#define button_draw_gui
draw_sprite(sprite_index, image_index, x, y);

draw_set_font(fnt_buttons);
draw_set_halign(fa_center);
draw_set_valign(fa_middle);

draw_text_colour(x, y, text, c_black, c_black, c_black, c_black, 1);

#define button_mouse_enter
hovered = true;
//image_index = 1;

#define button_mouse_leave
//image_index = 0;
pressable = false;
hovered = false;

#define button_left_released
if (pressable && action) {
    script_execute(action);
}