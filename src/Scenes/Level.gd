extends Node


onready var bird_animation_player : AnimationPlayer = get_node("Bird/AnimationPlayer")

func _ready() -> void:
	bird_animation_player.stop(true)
