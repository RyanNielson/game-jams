camera = camera_create_view(0, 0, TARGET_WIDTH, TARGET_HEIGHT, 0, -1, -1, -1, 0, 0);

view_enabled = true;
view_set_visible(0, true);
view_set_camera(0, camera);
display_set_gui_size(TARGET_WIDTH, TARGET_HEIGHT);
