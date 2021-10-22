extends Node


onready var bird_animation_player : AnimationPlayer = get_node("Bird/AnimationPlayer")

func _ready() -> void:
	bird_animation_player.play("flap")


func _on_DangerArea_body_entered(body: Node) -> void:
	if body is Bird:
		body.die()
