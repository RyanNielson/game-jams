/// fire_weapon(obj_weapon, direction, team, damage)
var dir = argument0;
var team = argument1;
var damage2 = argument2;
var fire_rate = argument3;
var accuracy2 = argument4;
var projectile = argument5;

if (weapon.fireable)
{
    //audio_play_sound(snd_shoot, 100, false);

    with instance_create(x, y, projectile)
    {
        var accuracy_range = clamp(accuracy2, 0, 10)
        direction = dir + irandom_range(-10 + accuracy_range, 10 - accuracy_range);
        team = team;
        damage = damage2;
    }
    
    weapon.fireable = false;
    weapon.alarm[0] = room_speed / fire_rate;
    
    obj_camera.should_shake = true;
}
