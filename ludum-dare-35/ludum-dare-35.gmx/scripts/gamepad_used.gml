if (gamepad_is_connected(0)) {
    return gamepad_button_check(0, gp_face1) 
    || gamepad_button_check(0, gp_face2) 
    || gamepad_button_check(0, gp_face3) 
    || gamepad_button_check(0, gp_face4)
    || gamepad_axis_value(0, gp_axislh) > 0.25
    || gamepad_axis_value(0, gp_axislh) < -0.25
    || gamepad_axis_value(0, gp_axislv) > 0.25
    || gamepad_axis_value(0, gp_axislv) < -0.25;
    
}
