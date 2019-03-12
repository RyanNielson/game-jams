var left = argument0;
var top = argument1;
var width = argument2 - 1;
var height = argument3 - 1;
var color = argument4;

draw_rectangle_color(left, top, left + width, top + height, color, color, color, color, false);
