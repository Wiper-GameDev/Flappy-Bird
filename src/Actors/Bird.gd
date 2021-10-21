extends RigidBody2D

export var flap_impulse := 400.0

# Called when the node enters the scene tree for the first time.
onready var animated_sprite : AnimatedSprite = get_node("AnimatedSprite")
onready var animation_player : AnimationPlayer = get_node("AnimationPlayer")
onready var wing_sound: AudioStreamPlayer2D = get_node("WingSound")


const ROTATION_MULTIPLIER = 5


var _velocity := Vector2.ZERO
var gravity  := 800.0

func _ready() -> void:
	animated_sprite.play("flap")
	

	
func _process(delta: float) -> void:
	if Input.is_action_pressed("flap"):
		_velocity.y = -flap_impulse
		if wing_sound.playing:
			wing_sound.stop()
		wing_sound.play()
		
	animated_sprite.rotation_degrees = get_rotation_degrees()


func _physics_process(delta: float) -> void:
	_velocity.y += gravity * delta
	position += _velocity * delta
	
	
	
func get_rotation_degrees():
	return _velocity.y * get_physics_process_delta_time() * ROTATION_MULTIPLIER

	
	
