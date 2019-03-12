game_title = "[SNAKEfill]";

selected_item = 0;
items = ["new game", "continue game", "endless random levels", "credits", "exit"];
total_items = array_length_1d(items);

ini_open("savedata.ini");
continue_level = ini_read_real("save", "level", noone);
ini_close();
