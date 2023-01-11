import pyglet


class Window(pyglet.window.Window):
    def __init__(self):
        super(Window, self).__init__(width=7 * 64, height=9 * 64)
        self.batch = pyglet.graphics.Batch()

        self.img_dict = {}

        self.create_slots()

    def on_draw(self):
        self.clear()
        self.batch.draw()

    def center_anchor(self, image):
        image.anchor_x = image.width // 2
        image.anchor_y = image.height // 2

    def load_image(self, key, fname):
        self.img_dict[key] = pyglet.image.load(fname)
        self.center_anchor(self.img_dict[key])

    def create_slots(self, label=True):
        self.load_image("button", "frame64.png")

        self.slot_info = [
            {"name": "headband", "file": "headband.png",
                "x_rel": -1, "y_rel": -2},
            {"name": "head", "file": "head.png",
                "x_rel": 0, "y_rel": -2},
            {"name": "eyes", "file": "eyes.png",
                "x_rel": +1, "y_rel": -2},

            {"name": "neck", "file": "neck.png",
                "x_rel": -1, "y_rel": -1},
            {"name": "face", "file": "face.png",
                "x_rel": 0, "y_rel": -1},
            {"name": "shoulders", "file": "shoulders.png",
                "x_rel": +1, "y_rel": -1},

            {"name": "armor", "file": "armor.png",
                "x_rel": -1, "y_rel": 0},
            {"name": "chest", "file": "chest.png",
                "x_rel": 0, "y_rel": 0},
            {"name": "body", "file": "body.png",
                "x_rel": +1, "y_rel": 0},

            {"name": "mainhand", "file": "mainhand.png",
                "x_rel": -2, "y_rel": +1},
            {"name": "mainwrist", "file": "mainwrist.png",
                "x_rel": -1, "y_rel": +1},
            {"name": "belt", "file": "belt.png",
                "x_rel": 0, "y_rel": +1},
            {"name": "offwrist", "file": "offwrist.png",
                "x_rel": +1, "y_rel": +1},
            {"name": "offhand", "file": "offhand.png",
                "x_rel": +2, "y_rel": +1},

            {"name": "hands_right", "file": "hands_right.png",
                "x_rel": -1, "y_rel": +2},
            {"name": "trousers", "file": "trousers.png",
                "x_rel": 0, "y_rel": +2},
            {"name": "hands_left", "file": "hands_left.png",
                "x_rel": +1, "y_rel": +2},

            {"name": "ring_r", "file": "ring.png",
                "x_rel": -1, "y_rel": +3},
            {"name": "feet", "file": "feet.png",
                "x_rel": -0, "y_rel": +3},
            {"name": "ring_l", "file": "ring.png",
                "x_rel": +1, "y_rel": +3},
        ]

        self.map = {"frame": {}, "icon": {}, "label": {}}

        for slot in self.slot_info:
            self.map["frame"][slot["name"]] = pyglet.sprite.Sprite(
                self.img_dict["button"],
                y=self.height // 2 - slot["y_rel"] * 64,
                x=self.width // 2 + slot["x_rel"] * 64,
                batch=self.batch,
                group=pyglet.graphics.OrderedGroup(0))

            self.load_image(slot["name"], slot["file"])
            self.map["icon"][slot["name"]] = pyglet.sprite.Sprite(
                self.img_dict[slot["name"]],
                y=self.height // 2 - slot["y_rel"] * 64,
                x=self.width // 2 + slot["x_rel"] * 64,
                batch=self.batch,
                group=pyglet.graphics.OrderedGroup(1))

            if label:
                self.map["label"][slot["name"]] = pyglet.text.Label(
                    slot["name"],
                    font_name='Fontin',
                    font_size=8,
                    color=(255, 255, 255, 191),
                    y=self.height // 2 - slot["y_rel"] * 64,
                    x=self.width // 2 + slot["x_rel"] * 64,
                    anchor_y='center',
                    anchor_x='center',
                    align="baseline",
                    batch=self.batch,
                    group=pyglet.graphics.OrderedGroup(2))


if __name__ == '__main__':
    window = Window()
    pyglet.app.run()
