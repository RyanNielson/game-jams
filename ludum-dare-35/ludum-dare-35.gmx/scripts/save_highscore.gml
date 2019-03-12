var possible_highscore = argument0;

ini_open("gamedata.ini");

var current_highscore = ini_read_real("highscores", "score", 0.0);

if (possible_highscore > current_highscore) {
    score = ini_write_real("highscores", "score", possible_highscore);
}

ini_close();

