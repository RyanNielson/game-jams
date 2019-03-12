ds_grid_destroy(grid);

var three_letter_words = all_words[0];
var four_letter_words = all_words[1];
var five_letter_words = all_words[2];

ds_list_destroy(three_letter_words);
ds_list_destroy(four_letter_words);
ds_list_destroy(five_letter_words);