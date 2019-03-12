var difficulty = argument0;
var words = argument1;

grid_width = 4;
grid_height = 4;

if (difficulty == 0) {
   grid_width = 4;
   grid_height = 4;
}
else if (difficulty == 1) {
   grid_width = 6;
   grid_height = 6;
}
else if (difficulty == 2) {
   grid_width = 8;
   grid_height = 8;
}


var possible_letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

grid = ds_grid_create(grid_width, grid_height);

// Generate random grid;
for (var xx = 0; xx < grid_width; xx++) {
    for (var yy = 0; yy < grid_height; yy++) {
        grid[# xx, yy] = string_char_at(possible_letters, irandom(string_length(possible_letters) - 1));
    }
}

// Add words;
word_count = array_length_1d(words);
for (var i = 0; i < word_count; i++) {  
    var word = words[i];
    var word_length = string_length(word);
    
    for (var j = 0; j < word_length; j++) {
        var letter = string_char_at(word, j + 1);   
        grid[# j, i] = letter;
        //show_debug_message(letter);
    }
}

// Shuffle Puzzle
var shuffle_number = 20;
if (difficulty == 0) {
   shuffle_number = 100;
}
else if (difficulty == 1) {
   shuffle_number = 200;
}
else if (difficulty == 2) {
   shuffle_number = 400;
}

for (var i = 0; i < shuffle_number; i++) {
    var start_x = irandom(grid_width - 1);
    var start_y = irandom(grid_height - 1);
    var movement_direction = random_movement_direction();
    
    var swap_x = start_x;
    var swap_y = start_y;
    
    if (movement_direction == Direction.Right) {
        swap_x++;
    }
    else if (movement_direction == Direction.Up) {
        swap_y--;
    }
    else if (movement_direction == Direction.Left) {
        swap_x--;
    }
    else if (movement_direction == Direction.Down) {
        swap_y++;
    }
    
    swap_x = clamp(swap_x, 0, grid_width - 1);
    swap_y = clamp(swap_y, 0, grid_height - 1);
    
    start_letter = grid[# start_x, start_y];
    swap_letter = grid[# swap_x, swap_y];
    
    grid[# start_x, start_y] = swap_letter;
    grid[# swap_x, swap_y] = start_letter;
}

return grid;