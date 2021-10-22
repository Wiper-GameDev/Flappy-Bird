extends Node


onready var score_sound : AudioStreamPlayer2D = get_node("ScoreSound")


func _ready() -> void:
	PlayerData.connect("score_update", self, "_on_Score_update")
	

func _on_Score_update() -> void:
	$Score.text = "%s" % PlayerData.score
	score_sound.play()
	
