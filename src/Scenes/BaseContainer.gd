extends Node

const BASE_TEXTURE = preload("res://src/core/Base.tscn")


func _process(delta: float) -> void:
	if get_child_count() < 2:
		var base_texture = BASE_TEXTURE.instance()
		var pos = Vector2.ZERO
		var viewport_size = get_viewport().get_visible_rect().size
		var base_height = base_texture.get_global_rect().size.y
		pos.x = viewport_size.x
		pos.y = viewport_size.y - base_height
		base_texture.rect_position = pos
		add_child(base_texture)
		
