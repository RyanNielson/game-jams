var spawn_x = view_wview + 32;
var spawn_y = irandom_range(view_yview + 16, view_hview - 16);


var ufo = instance_create(view_wview + 32, irandom_range(view_yview + 16, view_hview - 16), obj_ufo);

// TODO: Ideas for delaying spawning.

/*with (instance_create(x, y, obj_timed_deactivator)) {
    target = instance_create(spawn_x, spawn_y, obj_ufo);
    activation_delay = 0;
}

with (instance_create(x, y, obj_timed_deactivator)) {
    target = instance_create(spawn_x, spawn_y, obj_ufo);
    activation_delay = room_speed;
}

with (instance_create(x, y, obj_timed_deactivator)) {
    target = instance_create(spawn_x, spawn_y, obj_ufo);
    activation_delay = 2 * room_speed;
}

*/
