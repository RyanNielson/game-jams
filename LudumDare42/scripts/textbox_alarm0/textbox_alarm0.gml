//character = string_char_at(text, counter);
counter++;

drawable_text = string_copy(text, 0, counter + 1);

if (counter < string_length(text)) {
    alarm_set(0, frames_per_character);
}
