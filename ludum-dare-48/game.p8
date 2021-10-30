pico-8 cartridge // http://www.pico-8.com
version 32
__lua__
-- manic miner
-- by ryan nielson

CELL_SIZE = 8
GRID_CELL_OFFSET = 5
TILE_COLOR = 0
-- BACKGROUND_COLOR = 13
offset=0

background_colors = {1, 2, 3, 4, 5, 6, 8, 9, 10, 11, 12, 13, 14, 15 }
background_color_index = rnd({3, 11, 12})
current_level = 1

function lerp(tar, pos, perc)
    return (1-perc)*tar + perc*pos;
end

Game = function() 
    local self = {}

    local player = {}
    local grid = {}
    local title = "Manic Miner"
    local energy_drain_rate = 0.5
    local panel_left_x = CELL_SIZE * GRID_CELL_OFFSET - 1
    local panel_right_x = 127 - CELL_SIZE * GRID_CELL_OFFSET + 1
    local panels_closed = false
    local low_energy_warning = 40
    local high_score = 0
    local low_energy_warning_enabled = true
    local frames_since_low_energy_warning = 0
    local depth_since_last_air_block = 0

    local new_block = function(cell_x, cell_y, type)
        local block = {}
        block.cell_x = cell_x
        block.cell_y = cell_y
        block.type = type
        block.health = 1
        
        if type == 0 then
            block.sprite = 2
        elseif type == 1 then
            block.sprite = 3
            block.health = 2
        elseif type == 2 then
            block.sprite = 4
        end

        block.on_hit = function()
            if block.type == 1 and block.health == 2 then
                sfx(1)
            end

            block.health = block.health - 1
        end

        block.on_destroyed = function(player) 
            if block.type == 2 then
                player.add_energy(25)
                sfx(2)
            else
                sfx(0)
            end
        end

        block.get_sprite = function()
            if block.type == 1 and block.health == 1 then
                return 6
            end
                
            return block.sprite
        end

        return block
    end

    local new_player = function(cell_x, cell_y, sprite_number)
        local player = {}
        player.cell_x = cell_x
        player.cell_y = cell_y
        player.sprite = sprite_number
        player.flip = false
        player.depth = 0 -- Store this outside player?
        player.energy = 100
        player.energy_max = 100
        player.alive = true

        player.add_energy = function(amount)
            player.energy = mid(0, player.energy + amount, player.energy_max)
        end
        return player
    end

    local generate_block = function(x, y)
        local rand = flr(rnd(100))

        local block_type = 0
        
        if player.depth < 50 then
            -- 15% chance of air, 15% chance of stone, otherwise normal
            if rand < 15 then 
                block_type = 2
            elseif rand < 30 then
                block_type = 1
            else
                block_type = 0
            end
        elseif player.depth < 100 then
             -- 12% chance of air, 25% chance of stone, otherwise normal
            if rand < 12 then 
                block_type = 2
            elseif rand < 37 then
                block_type = 1
            else
                block_type = 0
            end
        elseif player.depth < 150 then
            -- 9% chance of air, 35% chance of stone, otherwise normal
            if rand < 9 then 
                block_type = 2
            elseif rand < 44 then
                block_type = 1
            else
                block_type = 0
            end
        elseif player.depth < 200 then
            -- 6% chance of air, 35% chance of stone, otherwise normal
            if rand < 6 then 
                block_type = 2
            elseif rand < 41 then
                block_type = 1
            else
                block_type = 0
            end
        else
            -- 3% chance of air, 40% chance of stone, otherwise normal
            if rand < 3 then 
                block_type = 2
            elseif rand < 43 then
                block_type = 1
            else
                block_type = 0
            end
        end

        if block_type != 2 and depth_since_last_air_block >= 17 then
            block_type = 2
        end

        if block_type == 2 then
            depth_since_last_air_block = 0
        end

        local index = 6 * y + x

        if block_type == 0 then
            grid[index] = new_block(x, y, 0)
        elseif block_type == 1 then
            grid[index] = new_block(x, y, 1)
        elseif block_type == 2 then
            grid[index] = new_block(x, y, 2)
        end
    end

    local update_map = function()
        for x = 0, 5 do
            for y = 0, 15 do
                mset(x, y, 0)
            end
        end

        for x = 0, 5 do
            for y = 0, 15 do
                local index = 6 * y + x
                local block = grid[index]
        
                if (block != nil) then
                    mset(x, y, block.get_sprite())
                end
            end
        end
    
        -- mset(player.cell_x, player.cell_y, player.sprite)
    end

    local screen_shake = function()
        local fade = 0.95
        local offset_x=16-rnd(32)
        local offset_y=16-rnd(32)
        offset_x*=offset
        offset_y*=offset
        
        camera(offset_x,offset_y)
        offset*=fade
        if offset<0.05 then
            offset=0
        end
    end

    local draw_player = function()
        spr(player.sprite, player.cell_x * CELL_SIZE + CELL_SIZE * GRID_CELL_OFFSET, player.cell_y * CELL_SIZE, 1, 1, player.flip)
    end

    local draw_ui = function()
        if not panels_closed then
            local energy_ratio = mid(0, player.energy / player.energy_max, 1)
            local bar_left = CELL_SIZE * GRID_CELL_OFFSET + 2
            local bar_right = bar_left + CELL_SIZE * 6 - 5
            local max_bar_width = (bar_right - bar_left) * energy_ratio
            local bar_top = 127 - 9

            rectfill(bar_left - 1, bar_top, bar_right + 1, bar_top + 7, 7)
            rectfill(bar_left, bar_top - 1, bar_right, bar_top + 7 + 1, 7)
            rectfill(bar_left, bar_top, bar_right, bar_top + 7, 0)
            if player.alive then
                if (player.energy <= low_energy_warning) then
                    if (background_colors[background_color_index] == 8) then
                        rectfill(bar_left, bar_top, bar_left + max_bar_width, bar_top + 7, 12)
                    else
                        rectfill(bar_left, bar_top, bar_left + max_bar_width, bar_top + 7, 8)
                    end
                else
                    rectfill(bar_left, bar_top, bar_left + max_bar_width, bar_top + 7, background_colors[background_color_index]) -- fill
                end
            end

            print("air", bar_left + 8, bar_top + 1, 7)

            palt(0, false)
            palt(7, true)
            pal(0, 7)
            spr(4, bar_left, bar_top)
            pal()
        end

        rectfill(0, 0, panel_left_x, 127, 0)
        rectfill(panel_right_x, 0, 127, 127, 0)

        if not panels_closed then
            line(panel_right_x, 0, panel_right_x, 127, 7)
            line(panel_left_x, 0, panel_left_x, 127, 7)

            local depth_right_x = 40 - 2
            local x_right_aligned = depth_right_x - 5 * 4
            print("depth", x_right_aligned, 1, 7)

            local x_right_aligned = depth_right_x - #tostr(player.depth) * 4

            print(player.depth, x_right_aligned, 1 + 6, 7)

            local text_left = 127 - CELL_SIZE * GRID_CELL_OFFSET + 5 - 1
            print("highscore", text_left, 1, 7)
            print(high_score)
        end

        if panels_closed then
            -- Instructions and score
            local depth_string = "depth"

            local h_center = 64 - #depth_string * 2 -- 5 is number of letters per line
            local v_center = 64 - 6 * 2
            print(depth_string, h_center, v_center, 7)

            -- TODO: Add text if new high score

            local score_string = tostr(player.depth)
            h_center = 64 - #score_string * 2 -- 5 is number of letters per line
            v_center = 64 - 6 * 2
            print(score_string, h_center, v_center + 6, background_colors[background_color_index])

            local reset_string = "❎ reset" -- 🅾️
            h_center = 64 - #reset_string * 2 -- 5 is number of letters per line
            v_center = 64 - 6 * 2
            print(reset_string, h_center, v_center + 18, 7)

            local menu_string = "p/enter pause menu"
            h_center = 64 - #menu_string * 2 -- 5 is number of letters per line
            v_center = 64 - 6 * 2
            print(menu_string, h_center, v_center + 26, 7)
        end

        if player.depth <= 0 then
            local rules_text_x = 127 - CELL_SIZE * GRID_CELL_OFFSET + 5 - 1
            local rules_text_y = 92

            spr(1, rules_text_x + 1 - 1, rules_text_y)
            print("dig", rules_text_x + 1 + 9, rules_text_y + 2)

            spr(2, rules_text_x + 1, rules_text_y + 8 + 1)
            print("1 hit", rules_text_x + 1 + 9, rules_text_y + 8 + 2)

            spr(3, rules_text_x + 1, rules_text_y + 16 + 2)
            print("2 hits", rules_text_x + 1 + 9, rules_text_y + 16 + 3)

            spr(4, rules_text_x + 1, rules_text_y + 24 + 3)
            print("air", rules_text_x + 1 + 9, rules_text_y + 24 + 4)
        end
    end
    
    local move_map = function()
        -- music(-1, 500)
        for x = 0, 5 do
            for y = 0, 15 do
                local index = 6 * y + x
                local new_index = 6 * (y - 1) + x
                local block = grid[index]

                if y == 0 then
                    grid[index] = nil
                elseif (block != nil and new_index >= 0) then
                    grid[new_index] = block
                    grid[index] = nil
                end
            end
        end

        depth_since_last_air_block = depth_since_last_air_block + 1

        -- Generate new bottom row
        for x = 0, 5 do
            generate_block(x, 15)
        end
    end

    local move_down = function()
        -- music(-1, 500)
        local index = 6 * (player.cell_y + 1) + player.cell_x
        if (grid[index] == nil) then
        else
            -- TODO: Extract this stuff out
            local block = grid[index]

            -- block.health = block.health - 1

            block.on_hit()

            if (block.health <= 0) then
                block.on_destroyed(player)
                grid[index] = nil
                player.depth = player.depth + 1
                -- offset = 0.05 -- Set to 0 to stop screenshake

                move_map()
            end
        end
    end

    local move_horizontal = function(direction)
        local new_x = player.cell_x + direction

        if (new_x >= 0 and new_x <= 5) then
            local index = 6 * (player.cell_y) + new_x
            
            -- TODO: Extract this
            -- TODO: Custom sfx per block type?
            if (grid[index] != nil) then
                local block = grid[index]
                block.on_hit()
                -- block.health = block.health - 1

                if (block.health <= 0) then
                    block.on_destroyed(player)
                    grid[index] = nil
                    -- offset = 0.05 -- Set to 0 to stop screenshake
                    
                end
            end

            local block = grid[index]
            if (block == nil) then
                player.cell_x = new_x;
                if (direction < 0) then
                    player.flip = true
                else
                    player.flip = false
                end
            end
        end
    end

    local draw_title = function() 
        local h_center = 64 - 11 * 2 -- 5 is number of letters per line
        local v_center = 16 - player.depth * CELL_SIZE
        -- print(title, h_center + 1, v_center + 1, 0)
        -- print(title, h_center + 1, v_center, 0)
        -- print(title, h_center, v_center + 1, 0)
        print(title, h_center, v_center, 7)

        local author_string = "by ryan n"
        local h_center = 64 - #author_string * 2 -- 5 is number of letters per line
        local v_center = 16 + 8 - player.depth * CELL_SIZE
        -- print(author_string, h_center + 1, v_center + 1, 0)
        -- print(author_string, h_center + 1, v_center, 0)
        -- print(title, h_center, v_center + 1, 0)
        print(author_string, h_center, v_center, 7)

        local move_string = "⬅️⬇️➡️ dig"
        local palette_string = "❎ color"-- "⬆️ 🅾️ color"

        local h_center = 58 - #move_string * 2
        local h_center_palette = 62 - #palette_string * 2
        -- print(move_string, h_center + 1, 32 + 8 - player.depth * CELL_SIZE + 1, 0)
        -- print(move_string, h_center + 1, 32 + 8 - player.depth * CELL_SIZE, 0)
        print(move_string, h_center, 32 + 8 - player.depth * CELL_SIZE, 7)
        -- print(palette_string, h_center_palette + 1 - 2, 40 + 8 - player.depth * CELL_SIZE + 1, 0)
        -- print(palette_string, h_center_palette + 1 - 2, 40 + 8 - player.depth * CELL_SIZE, 0)
        print(palette_string, h_center_palette, 40 + 8 - player.depth * CELL_SIZE, 7)
    end

    local adjust_background_color = function() 
        background_color_index = (background_color_index % #background_colors) + 1
    end

    self.init = function() 
        player = new_player(2, 7, 1)
        grid = {}
        energy_drain_rate = 0.5
        panel_left_x = CELL_SIZE * GRID_CELL_OFFSET - 1
        panel_right_x = 127 - CELL_SIZE * GRID_CELL_OFFSET + 1
        panels_closed = false
        low_energy_warning_enabled = true
        frames_since_low_energy_warning = 0
        depth_since_last_air_block = 0

        -- GENERATE LEVEL
        for x = 0, 5 do
            for y = 0, 15 do
                local index = 6 * y + x
                if (y == 8) then
                    grid[index] = new_block(x, y, 0)
                elseif (y > 8) then
                    generate_block(x, y)
                else
                    grid[index] = nil
                end
            end
        end

        -- music(0)

        update_map()
    end

    self.update = function()
        if (player.alive) then
            if (btnp(0)) then -- LEFT
                move_horizontal(-1)
            elseif (btnp(1)) then -- RIGHT
                move_horizontal(1)
            elseif (btnp(3)) then -- DOWN
                move_down()
            elseif (btnp(5)) then -- TODO: Maybe change this to a non-arrow
                adjust_background_color()
            end
        end

        update_map()

        if (player.depth > 0) then
            player.energy = player.energy - energy_drain_rate

            if player.alive and player.energy <= low_energy_warning and low_energy_warning_enabled then
                sfx(4)
                low_energy_warning_enabled = false
            end

            if not low_energy_warning_enabled and frames_since_low_energy_warning > 30 then
                low_energy_warning_enabled = true
                frames_since_low_energy_warning = 0

            end

            if not low_energy_warning_enabled then
                frames_since_low_energy_warning = frames_since_low_energy_warning + 1
            end
        end

        if player.alive and player.energy <= 0 then
            player.alive = false
            sfx(3)

            if player.depth > high_score then
                high_score = player.depth
            end
        end

        if not player.alive then
            panel_left_x = lerp(128/2 + 2, panel_left_x, 0.8)
            panel_right_x = lerp(128/2 - 2, panel_right_x, 0.8)

            panel_left_x = mid(0, panel_left_x, 128/2)
            panel_right_x = mid(128/2, panel_right_x, 128)

            if (panel_right_x == 128/2) then
                panels_closed = true
            end

            if panels_closed and (btnp(5)) then
                levels[current_level].init()
            end
        end
    end

    self.draw = function() 
    	cls(background_colors[background_color_index])
    	-- rectfill(0, 0, 127, 127, background_color)

        screen_shake()

        draw_title()

        pal(7, TILE_COLOR)
        draw_player()

        map(0, 0, 40, 0, 6, 16)
        pal()

        camera()

        draw_ui()
    end

    return self
end

levels = {
    Game()
}


function _init()
    -- cartdata("manic_miner")
    levels[current_level].init()
end

function _update()
    levels[current_level].update()
end

function _draw()
    levels[current_level].draw()
end

__gfx__
00000000000000007777777777777777777777777777777777777777000000000000000000000000000000000000000000000000000000000000000000000000
00000000000770007777777770077007777007777007700770077707000000000000000000000000000000000000000000000000000000000000000000000000
00700700000007007777777770077707770770777007000777777707000000000000000000000000000000000000000000000000000000000000000000000000
00077000077070707777777777770777707077077700007777777777000000000000000000000000000000000000000000000000000000000000000000000000
00077000077700707777777770700777707777077000007777707777000000000000000000000000000000000000000000000000000000000000000000000000
00700700077700007777777770777007770770777000000770777777000000000000000000000000000000000000000000000000000000000000000000000000
00000000077700007777777777070007777007777707000777070007000000000000000000000000000000000000000000000000000000000000000000000000
00000000070700007777777777777777777777777777777777777777000000000000000000000000000000000000000000000000000000000000000000000000
__sfx__
000100000160003610066200a6300b6300b6300a630066200461005600006000060000600006000060003600036000c6000d6000d6000d6000d6000c6000c6000e6000c6000f6000c6000d600106000d60000000
0001000024410284202b4302d4302d4302b4302942026410364002f40024400024000240000400004000040000400014000240002400034000340004400044000440005400044000340000400004000000000000
000200000a5200f520165201d52025520345203a520235200b5200052000520005200050000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
0004000010150181501d1502015024150261502715027150261502515024150211501f1501d15016150111500b1500d1500e1501015010150111501215013150121501215011150101500e1400c1300a12005110
000a000033030002003f200002003f200002000020000200002000010000100001000010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
001000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
001000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
001000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
001000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
001000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
001e00082462000620246200062024620246202462000620006000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
000f00102472000020247200002024720000202472000720247200002024720007202472000020247200002000720000000000000000000000000000000000000000000000000000000000000000000000000000
__music__
03 0a0b4344

