extends StaticBody2D

func disable_collision() -> void:
	if not $CollisionShape2D.disabled:
		$CollisionShape2D.disabled = true
		


func _on_PipeArea_body_entered(body: Node) -> void:
	if body is Bird:
		body.die()
