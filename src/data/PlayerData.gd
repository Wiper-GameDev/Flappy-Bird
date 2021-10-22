extends Node

signal score_update


var score := 0 setget set_score


func _ready() -> void:
	pass

func set_score(value: int) -> void:
	score = value
	emit_signal("score_update")
	
