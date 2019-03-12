var xx = argument0;
var yy = argument1;
var text = argument2;

draw_set_color(c_black);
draw_text(xx + 1, yy, text);
draw_text(xx, yy + 1, text);
draw_text(xx + 1, yy + 1, text);
draw_set_color(c_white);
draw_text(xx, yy, text);