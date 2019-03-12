var three_letter_words = all_words[0];
var four_letter_words = all_words[1];
var five_letter_words = all_words[2];

var size_of_three_words = ds_list_size(three_letter_words);
var size_of_four_words  = ds_list_size(four_letter_words);
var size_of_five_words  = ds_list_size(five_letter_words);

var words = [];
if (difficulty == 0) {
    words[0] = string_upper(ds_list_find_value(three_letter_words, irandom(size_of_three_words - 1)));
    words[1] = string_upper(ds_list_find_value(three_letter_words, irandom(size_of_three_words - 1)));
    words[2] = string_upper(ds_list_find_value(four_letter_words, irandom(size_of_four_words - 1)));
}
else if (difficulty == 1) {
    words[0] = string_upper(ds_list_find_value(four_letter_words, irandom(size_of_four_words - 1)));
    words[1] = string_upper(ds_list_find_value(four_letter_words, irandom(size_of_four_words - 1)));
    words[2] = string_upper(ds_list_find_value(four_letter_words, irandom(size_of_four_words - 1)));
    words[3] = string_upper(ds_list_find_value(five_letter_words, irandom(size_of_five_words - 1)));
}
else if (difficulty == 2) {
    words[0] = string_upper(ds_list_find_value(four_letter_words, irandom(size_of_four_words - 1)));
    words[1] = string_upper(ds_list_find_value(four_letter_words, irandom(size_of_four_words - 1)));
    words[2] = string_upper(ds_list_find_value(five_letter_words, irandom(size_of_five_words - 1)));
    words[3] = string_upper(ds_list_find_value(five_letter_words, irandom(size_of_five_words - 1)));
    words[4] = string_upper(ds_list_find_value(five_letter_words, irandom(size_of_five_words - 1)));
}

return words;