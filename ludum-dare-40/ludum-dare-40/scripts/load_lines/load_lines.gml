var file = file_text_open_read("allwords.txt");

var three_letters = ds_list_create();
var four_letters = ds_list_create();
var five_letters = ds_list_create();

while (!file_text_eof(file)) {
    var word = file_text_read_string(file);
    var word_length = string_length(word);
    
    if (word_length == 3) {
        ds_list_add(three_letters, word);
    }
    else if (word_length == 4) {
        ds_list_add(four_letters, word);
    }
    else if (word_length == 5) {
        ds_list_add(five_letters, word);
    }
    
    file_text_readln(file);
}

file_text_close(file);

return [three_letters, four_letters, five_letters];