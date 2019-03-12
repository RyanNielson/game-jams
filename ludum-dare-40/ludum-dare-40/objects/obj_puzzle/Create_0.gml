randomize();
random_get_seed()
difficulty = 2;
selected_x = 0;
selected_y = 0;
offset = 48;
draw_set_font(fnt_bold);
char_width = string_width("W");
char_height = string_height("W");
swap_mode = false;
moves = 0;
time = 0;
time_start = noone;
all_words = load_lines();
has_swapped = false;
var size_of_three_words = ds_list_size(all_words[0]);
var size_of_four_words = ds_list_size(all_words[1]);
var size_of_five_words = ds_list_size(all_words[2]);

words = make_words_list();
show_debug_message(words);

for (i = 0; i < array_length_1d(words); i++) {
    found_words[i] = false;
}

grid = generate_puzzle_grid(difficulty, words);

