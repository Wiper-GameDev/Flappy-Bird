extends RigidBody2D

class_name Bird

signal died

export var flap_impulse := 350.0

# Called when the node enters the scene tree for the first time.
onready var animated_sprite : AnimatedSprite = get_node("AnimatedSprite")
onready var animation_player : AnimationPlayer = get_node("AnimationPlayer")
onready var wing_sound: AudioStreamPlayer2D = get_node("WingSound")
onready var hit_sound: AudioStreamPlayer2D = get_node("HitSound")
onready var die_sound: AudioStreamPlayer2D = get_node("DieSound")


const ROTATION_MULTIPLIER = 5
var alive := true
var touched_base := false


func _ready() -> void:
	animated_sprite.play("flap")
	
	
	
func _play_sound(sound) -> void:
	if sound.playing:
		sound.stop()
	sound.play()

	
func _process(delta: float) -> void:
	if Input.is_action_pressed("flap") && alive:
		linear_velocity.y = -flap_impulse
		_play_sound(wing_sound)
		
	animated_sprite.rotation_degrees = get_rotation_degrees()
	
		
	if touched_base:
		animated_sprite.rotation_degrees = 90
	
	
	
func get_rotation_degrees():
	return linear_velocity.y * get_physics_process_delta_time() * ROTATION_MULTIPLIER

	
	
func die():
	if alive:
		set_physics_process(false)
		emit_signal("died")
		alive = false
		animated_sprite.stop()
		_play_sound(hit_sound)
		_play_sound(die_sound)
	

