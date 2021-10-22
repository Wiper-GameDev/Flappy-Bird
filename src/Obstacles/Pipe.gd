extends StaticBody2D

func disable_collision() -> void:
	if not $CollisionShape2D.disabled:
		$CollisionShape2D.disabled = true
