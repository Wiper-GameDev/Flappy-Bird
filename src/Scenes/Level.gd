extends Node


onready var bird_animation_player : AnimationPlayer = get_node("Bird/AnimationPlayer")

func _ready() -> void:
	bird_animation_player.play("flap")
	$Bird.connect("died", self, "flash_screen")


func _on_DangerArea_body_entered(body: Node) -> void:
	if body is Bird:
		body.touched_base = true
		body.die()


func flash_screen() -> void:
	$ScreenFlash/ColorRect.visible = true
	yield (get_tree().create_timer(0.1), "timeout")
	$ScreenFlash/ColorRect.visible = false
