var position = argument0;
var negative = argument1;

if (negative) {
    return position - MARGIN;
}

return position + MARGIN;