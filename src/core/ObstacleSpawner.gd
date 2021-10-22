extends Node


var Obstacle = preload("res://src/Obstacles/Pipes.tscn")

func _ready() -> void:
	randomize()
	$Timer.start()
	
	
var collision_disabled := false

func spawn_obstacle() -> void:
	var obstacle = Obstacle.instance()
	add_child(obstacle)
	obstacle.global_position.y += randi() % 121 - 100
	obstacle.global_position.x = get_viewport().get_visible_rect().size.x + 100
	

func _on_Timer_timeout() -> void:
	spawn_obstacle()


func _process(delta: float) -> void:
	if collision_disabled:
		get_tree().call_group("pipes", "disable_collision")

func _on_Bird_died() -> void:
	collision_disabled = true
