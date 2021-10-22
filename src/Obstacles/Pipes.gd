extends Node2D


# Declare member variables here. Examples:
# var a: int = 2
export var speed := 200.0


func _process(delta: float) -> void:
	global_position.x -= speed * delta
	


func _on_screen_exited() -> void:
	queue_free()


func _on_ScoreArea_body_entered(body: Node) -> void:
	if body is Bird:
		if body.alive:
			PlayerData.score += 1
