other.can_undo = true;

audio_play_sound(snd_pendant, 100, false);

with (instance_create_layer(32, 320, "Instances", obj_textbox)) {
    text = "The pendant allows you to undo your previous moves, one at a time, by pressing the z key.";
    image_xscale = 18;
}

instance_destroy();