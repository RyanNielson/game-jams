if (room == rm_end) {
    file_delete("savedata.ini");
}
else if (room != 0) {
    if (room != rm_random && room != rm_credits) {
        ini_open("savedata.ini");
        ini_write_real("save", "level", room);
        ini_close();
    }
}

