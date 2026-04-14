"""
Automation script for "The Farmer Was Replaced".

Designed for the in-game Python-like runtime API:
- get_world_size, move, till, plant, harvest, can_harvest, get_water, use_item, unlock
- Entities, Items, Unlocks, and direction constants (North/South/East/West)
"""

# Unlocks are attempted in this order whenever possible.
UNLOCK_PRIORITY = [
    Unlocks.Speed,
    Unlocks.Expand,
    Unlocks.MegaFarm,
]

# Watering thresholds by crop (%).
WATER_THRESHOLD = {
    Entities.Tree: 50,
    Entities.Pumpkin: 60,
}

# Crops that should be planted on already-tilled cells.
REQUIRES_TILL = {
    Entities.Carrot,
    Entities.Tree,
    Entities.Sunflower,
    Entities.Pumpkin,
    Entities.Grass,
}


current_x = 0
current_y = 0
known_world_size = get_world_size()
initialized_cells = set()


def retry(action, attempts=3):
    attempt = 0
    while attempt < attempts:
        result = action()
        # In-game commands may return None; treat that as non-failure.
        if result is None or result:
            return True
        attempt += 1
    return False


def normalize_water(value):
    # Some APIs expose water as [0..1], others as [0..100].
    if value <= 1:
        return value * 100
    return value


def target_entity_for_cell(x, y, size):
    # Top-right 2x2 override for Pumpkin.
    if x >= size - 2 and y >= size - 2:
        return Entities.Pumpkin

    # Dynamic column layout:
    # x is 0-based, while the requirement's column labels are 1-based:
    # x 0-1 => columns 1-2, x 2-3 => columns 3-4, x 4-5 => columns 5-6, x >= 6 => columns 7+.
    if x <= 1:
        return Entities.Carrot
    if x <= 3:
        return Entities.Tree
    if x <= 5:
        return Entities.Sunflower
    return Entities.Grass


def move_one_step(direction):
    global current_x, current_y

    if retry(lambda: move(direction), 2):
        if direction == East:
            current_x += 1
        elif direction == West:
            current_x -= 1
        elif direction == North:
            current_y += 1
        elif direction == South:
            current_y -= 1
        return True

    return False


def move_to(x, y):
    while current_x < x:
        move_one_step(East)
    while current_x > x:
        move_one_step(West)
    while current_y < y:
        move_one_step(North)
    while current_y > y:
        move_one_step(South)


def attempt_unlocks():
    for unlock_type in UNLOCK_PRIORITY:
        retry(lambda: unlock(unlock_type), 2)


def initialize_cell_if_needed(x, y, size):
    key = (x, y)
    if key in initialized_cells:
        return

    entity = target_entity_for_cell(x, y, size)

    if entity in REQUIRES_TILL:
        retry(till, 2)

    retry(lambda: plant(entity), 2)
    initialized_cells.add(key)


def maintain_cell(x, y, size):
    entity = target_entity_for_cell(x, y, size)

    threshold = WATER_THRESHOLD.get(entity)
    if threshold is not None:
        water_pct = normalize_water(get_water())
        if water_pct < threshold:
            retry(lambda: use_item(Items.Water), 2)

    if can_harvest():
        retry(harvest, 2)
        retry(lambda: plant(entity), 2)


def initialize_new_cells(old_size, new_size):
    # Add only newly unlocked rows/columns.
    y = 0
    while y < new_size:
        x = 0
        while x < new_size:
            if x >= old_size or y >= old_size:
                move_to(x, y)
                initialize_cell_if_needed(x, y, new_size)
            x += 1
        y += 1


def traverse_and_maintain(size):
    y = 0
    while y < size:
        if y % 2 == 0:
            x = 0
            while x < size:
                move_to(x, y)
                initialize_cell_if_needed(x, y, size)
                maintain_cell(x, y, size)
                x += 1
        else:
            x = size - 1
            while x >= 0:
                move_to(x, y)
                initialize_cell_if_needed(x, y, size)
                maintain_cell(x, y, size)
                x -= 1
        y += 1


def main():
    global known_world_size

    while True:
        current_size = get_world_size()

        if current_size > known_world_size:
            initialize_new_cells(known_world_size, current_size)
            known_world_size = current_size

        attempt_unlocks()
        traverse_and_maintain(current_size)


main()
