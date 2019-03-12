ini_open("gamedata.ini");

var current_highscore = ini_read_real("highscores", "score", 0.0);

ini_close();

return current_highscore;
