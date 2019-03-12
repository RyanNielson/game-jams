ini_open("gamedata.ini");

ini_write_real("highscores", "score", 0.0);

ini_close();

if (instance_exists(obj_main_menu)) {
    obj_main_menu.high_score = get_highscore();
}
