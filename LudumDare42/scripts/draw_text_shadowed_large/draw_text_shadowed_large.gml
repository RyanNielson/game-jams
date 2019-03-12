var xx = argument0;
var yy = argument1;
var text = argument2;

draw_set_color(c_black);
draw_text(xx + 2, yy, text);
draw_text(xx, yy + 2, text);
draw_text(xx + 2, yy + 2, text);
draw_set_color(c_white);
draw_text(xx, yy, text);