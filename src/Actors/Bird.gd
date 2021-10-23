extends RigidBody2D

class_name Bird

signal died
signal bird_collided_base

export var flap_impulse := 300.0

# Called when the node enters the scene tree for the first time.
onready var animated_sprite : AnimatedSprite = get_node("AnimatedSprite")
onready var animation_player : AnimationPlayer = get_node("AnimationPlayer")
onready var wing_sound: AudioStreamPlayer2D = get_node("WingSound")
onready var hit_sound: AudioStreamPlayer2D = get_node("HitSound")
onready var die_sound: AudioStreamPlayer2D = get_node("DieSound")


const ROTATION_MULTIPLIER = 5
var alive := true
var touched_base := false
var notified_bird_collided_base := false


var fall_velocity := 0
var gravity := 1000

var started := false


func _ready() -> void:
	animated_sprite.play("flap")
	animation_player.play("idle")
	set_use_custom_integrator(true)
	
func start() -> void:
	set_use_custom_integrator(false)
	animation_player.play("flap")
	started = true
	
	
func _play_sound(sound) -> void:
	if sound.playing:
		sound.stop()
	sound.play()

	
func _process(delta: float) -> void:
	if Input.is_action_just_pressed("flap") && alive && started:
		linear_velocity.y = -flap_impulse
		_play_sound(wing_sound)
		
	animated_sprite.rotation_degrees = get_rotation_degrees()
	
		
	if touched_base and animated_sprite.rotation_degrees != 90:
		animated_sprite.rotation_degrees = 90
	
	
	
func get_rotation_degrees():
	return linear_velocity.y * get_physics_process_delta_time() * ROTATION_MULTIPLIER


func _integrate_forces( body_state ) -> void:
	if is_using_custom_integrator() && started:
		fall_velocity += 1000 * get_physics_process_delta_time()
		body_state.set_linear_velocity(Vector2(0, fall_velocity))
		
	if touched_base and not notified_bird_collided_base:
		emit_signal("bird_collided_base")
		notified_bird_collided_base = true
	
	
func die():
	if alive:
		set_use_custom_integrator(true)
		emit_signal("died")
		alive = false
		animated_sprite.stop()
		_play_sound(hit_sound)
		_play_sound(die_sound)
	

