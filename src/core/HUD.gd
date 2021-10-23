extends Node


onready var score_sound : AudioStreamPlayer2D = get_node("ScoreSound")


func _ready() -> void:
	PlayerData.connect("score_update", self, "_on_Score_update")
	
	
func get_score_text() -> String:
	return "%s" % PlayerData.score

func _on_Score_update() -> void:
	$Score.text = get_score_text()
	score_sound.play()
	
func show_game_over() -> void:
	$Score.visible = false
	$GameOver.visible = true
	PlayerData.load_high_score()
	PlayerData.save_high_score()
	$GameOver/TextureRect/Score.text = get_score_text()
	$GameOver/TextureRect/Best.text = "%s" % PlayerData.highscore
	


func _on_RestartButton_button_up() -> void:
	PlayerData.reset()
	get_tree().paused = false
	get_tree().reload_current_scene()
