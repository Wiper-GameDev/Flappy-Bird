extends Node

signal score_update


var score := 0 setget set_score
var highscore := 0
const save_file  = "user://score.dat"

func _ready() -> void:
	pass

func set_score(value: int) -> void:
	score = value
	emit_signal("score_update")
	

func load_high_score() -> void:
	var file = File.new()
	if file.file_exists(save_file):
		var err = file.open(save_file, File.READ)
		if err == OK:
			var player_data = file.get_var()
			if player_data != null:
				highscore = player_data['highscore']
			file.close()
	
	
func save_high_score() -> void:
	if score > highscore:
		highscore = score
		var data = {
			"highscore": score
		}
		
		var file = File.new()
		var err = file.open(save_file, File.WRITE)
		if err == OK:
			file.store_var(data)
			file.close()

func reset() -> void:
	score = 0
	
