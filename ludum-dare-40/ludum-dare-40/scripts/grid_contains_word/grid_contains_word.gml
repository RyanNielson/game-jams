var word = argument0;
var word_length = string_length(word);
//var first_letter = string_char_at(word, 1);

for (var xx = 0; xx < ds_grid_width(grid); xx++) {
    for (var yy = 0; yy < ds_grid_height(grid); yy++) {
        // Check Horizontal
        for (var i = 0; i < word_length; i++) {
            if (xx + i < ds_grid_width(grid)) {
                var grid_letter = ds_grid_get(grid, xx + i, yy);
                var word_letter = string_char_at(word, i + 1);
                if (grid_letter == 0) {
                    break;   
                }
            
                if (grid_letter == word_letter) {
                    if (i == word_length -1) {
                        return true;   
                    }
                    continue;
                }
                break;
            }
            else {
                
                break;
            }
        } 
        
        // Check vertical
        for (var i = 0; i < word_length; i++) {
            if (yy + i < ds_grid_height(grid)) {
                var grid_letter = ds_grid_get(grid, xx, yy + i);
                var word_letter = string_char_at(word, i + 1);
                if (grid_letter == 0) {
                    break;   
                }
            
                if (grid_letter == word_letter) {
                    if (i == word_length -1) {
                        return true;   
                    }
                    continue;
                }
                break;
            }
            else {
                break;
            }
        } 
    }
}

return false;
