/// fire_weapon(obj_weapon, direction, team, damage)

var weapon = argument0;
var dir = argument1;
var team2 = argument2;
var damage2 = argument3;
var fire_rate2 = argument4;
var accuracy2 = argument5;

if (weapon.fireable)
{
    audio_play_sound(snd_shoot, 100, false);

    with instance_create(weapon.x, weapon.y, weapon.projectile)
    {
        var accuracy_range = clamp(accuracy2, 0, 10)
        direction = dir + irandom_range(-10 + accuracy_range, 10 - accuracy_range);
        team = team2;
        damage = damage2;
    }
    
    weapon.fireable = false;
    weapon.alarm[0] = room_speed / fire_rate2;
    
    obj_camera.should_shake = true;
}
