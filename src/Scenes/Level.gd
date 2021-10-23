extends Node


onready var bird_animation_player : AnimationPlayer = get_node("Bird/AnimationPlayer")

var started := false

func _ready() -> void:
	$Bird.connect("died", self, "flash_screen")
	$HUD/GameOver.visible = false
	$HUD/Score.visible = false

func start_game() -> void:
	$GetReady.visible = false
	$ObstacleSpawner.start()
	$HUD/Score.visible = true
	started = true

func _process(delta: float) -> void:
	if Input.is_action_just_pressed("flap") and not started:
		$Bird.start()
		start_game()

func _on_DangerArea_body_entered(body: Node) -> void:
	if body is Bird:
		body.touched_base = true
		body.die()


func flash_screen() -> void:
	$ScreenFlash/ColorRect.visible = true
	yield (get_tree().create_timer(0.1), "timeout")
	$ScreenFlash/ColorRect.visible = false



func _on_Bird_bird_collided_base() -> void:
	get_tree().paused = true
	$HUD.show_game_over()

