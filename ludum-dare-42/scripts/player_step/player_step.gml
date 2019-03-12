if (!level_completed) {
    if (keyboard_check_released(vk_anykey)) {
        if (keyboard_check_released(ord("Z"))) {
            if (can_undo && !ds_stack_empty(move_stack)) {
                var last_cell = ds_stack_pop(move_stack);
                current_cell_x = last_cell[0];
                current_cell_y = last_cell[1];
                //tilemap_set(tilemap, 0, current_cell_x, current_cell_y); // TODO: Clean this up and don't use tiles?
                tilemap_set(tilemap, Tiles.FLOOR, current_cell_x, current_cell_y); // TODO: Clean this up and don't use tiles?
                var new_position = cell_to_position(tilemap, current_cell_x, current_cell_y);
                x = new_position[0];
                y = new_position[1];
                audio_play_sound(snd_undo, 100, false);
                replace_stairs_with_floor(tilemap)
                stairs_appeared = false;
            }
        }
        else {
            var requested_movement_x = 0;
            var requested_movement_y = 0;
            if (keyboard_check_released(vk_right)) {
                requested_movement_x++;     
            }
            else if (keyboard_check_released(vk_left)) {
                requested_movement_x--;
            }
            else if (keyboard_check_released(vk_down)) {
                requested_movement_y++;  
            }
            else if (keyboard_check_released(vk_up)) {
                requested_movement_y--;   
            }
        
            if (requested_movement_x != 0 || requested_movement_y != 0) {
                var possible_next_cell_x = current_cell_x + requested_movement_x;
                var possible_next_cell_y = current_cell_y + requested_movement_y;
                var possible_next_tile = tilemap_get(tilemap, possible_next_cell_x, possible_next_cell_y);
        
                if (possible_next_tile == Tiles.FLOOR || possible_next_tile == Tiles.STAIRS || possible_next_tile == Tiles.CHEST) {
                    ds_stack_push(move_stack, [current_cell_x, current_cell_y]);
                    //tilemap_set(tilemap, 2, current_cell_x, current_cell_y);  // TODO: Clean this up and don't blank tiles?
                    tilemap_set(tilemap, Tiles.LAVA, current_cell_x, current_cell_y);  // TODO: Clean this up and don't blank tiles?

                    current_cell_x += requested_movement_x;
                    current_cell_y += requested_movement_y;

                    var new_position = cell_to_position(tilemap, current_cell_x, current_cell_y);
                    x = new_position[0];
                    y = new_position[1];
                    
                    audio_play_sound(snd_step, 100, false);
                }
            }
        }
    }

    covered_tiles = ds_stack_size(move_stack) + 1;

    if (covered_tiles == total_tiles) {
        level_completed = true;
        
        if (room == rm_10) {
            audio_play_sound(snd_chest, 100, false); 
        }
        else {
            audio_play_sound(snd_complete, 100, false);
        }
        alarm_set(0, 30);
    }
    else if (!stairs_appeared && covered_tiles == total_tiles - 1) {
        replace_last_tile_with_stairs(tilemap);
        audio_play_sound(snd_stairwell_appears, 100, false);
        stairs_appeared = true;
    }
}