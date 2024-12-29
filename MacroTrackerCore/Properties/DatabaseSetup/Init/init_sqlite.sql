-- Table: foods
CREATE TABLE IF NOT EXISTS foods (
    name TEXT PRIMARY KEY,
    calories_per_100g REAL,
    protein_per_100g REAL,
    carbs_per_100g REAL,
    fat_per_100g REAL,
    icon_file_name TEXT
);

-- Table: exercises
CREATE TABLE IF NOT EXISTS exercises (
    name TEXT PRIMARY KEY,
    calories_per_minute REAL,
    icon_file_name TEXT
);

-- Table: users
CREATE TABLE IF NOT EXISTS users (
    username TEXT PRIMARY KEY,
    encrypted_password TEXT
);

-- Table: goal
CREATE TABLE IF NOT EXISTS goal (
    calories INTEGER,
    protein INTEGER, 
    carbs INTEGER,
    fat INTEGER
);

-- Table: logs
CREATE TABLE IF NOT EXISTS logs (
    log_id INTEGER PRIMARY KEY AUTOINCREMENT, 
    log_date TEXT,            
    total_calories REAL DEFAULT 0
);

-- Table: log_food_items
CREATE TABLE IF NOT EXISTS log_food_items (
    log_food_id INTEGER PRIMARY KEY AUTOINCREMENT,
    log_id INTEGER,
    food_name TEXT,
    number_of_servings REAL,
    total_calories REAL,
    FOREIGN KEY (log_id) REFERENCES logs(log_id) ON DELETE CASCADE,
    FOREIGN KEY (food_name) REFERENCES foods(name) ON DELETE CASCADE
);

-- Table: log_exercise_items
CREATE TABLE IF NOT EXISTS log_exercise_items (
    log_exercise_id INTEGER PRIMARY KEY AUTOINCREMENT,
    log_id INTEGER,
    exercise_name TEXT,
    duration REAL,
    total_calories REAL,
    FOREIGN KEY (log_id) REFERENCES logs(log_id) ON DELETE CASCADE,
    FOREIGN KEY (exercise_name) REFERENCES exercises(name) ON DELETE CASCADE
);

-- Insert into exercises
INSERT INTO exercises (name, calories_per_minute, icon_file_name)
VALUES
    ('Basketball', 1, 'basketball.png'),
    ('Martial Arts', 2, 'martialarts.png'),
    ('Running', 1.5, 'running.png'),
    ('Swimming', 0.7, 'swimming.png'),
    ('Pickle Ball', 1.2, 'pickleball.png');
    
-- Insert into foods
INSERT INTO foods (name, calories_per_100g, protein_per_100g, carbs_per_100g, fat_per_100g, icon_file_name)
VALUES
    ('Chicken breast', 120, 22.5, 0, 2.6, 'chicken_breast.png'),
    ('White rice', 129, 2.7, 27.6, 0.3, 'white_rice.png'),
    ('Pork Chops', 115, 20.3, 0.9, 4, 'pork_chops.png'),
    ('Oat meal', 379, 13.1, 57.6, 6.5, 'oat_meal.png'),
    ('Beef', 152, 20.5, 0, 7.1, 'beef.png');

-- Insert into goal
INSERT INTO goal (calories, protein, carbs, fat)
VALUES (2500, 156, 313, 69);

-- Insert into users
INSERT INTO users (username, encrypted_password)
VALUES ('admin', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3');

-- Insert into logs
INSERT INTO logs (log_date, total_calories)
VALUES
    ('2024-12-18', 0),
    ('2024-12-19', 0),
    ('2024-12-20', 0),
    ('2024-12-21', 0),
    ('2024-12-22', 0);

-- Insert into log_food_items
INSERT INTO log_food_items (log_id, food_name, number_of_servings, total_calories)
VALUES
    (1, 'Chicken breast', 1.5, 180),
    (1, 'White rice', 2, 258),
    (2, 'Pork Chops', 1, 115),
    (2, 'Oat meal', 2, 758),
    (3, 'Beef', 1.2, 182.4),
    (3, 'White rice', 1.5, 193.5),
    (4, 'Chicken breast', 2, 240),
    (4, 'Pork Chops', 1, 115),
    (5, 'Oat meal', 1.5, 568.5),
    (5, 'Beef', 1, 152);

-- Insert into log_exercise_items
INSERT INTO log_exercise_items (log_id, exercise_name, duration, total_calories)
VALUES
    (1, 'Basketball', 30, 30),
    (1, 'Running', 20, 30),
    (2, 'Martial Arts', 40, 80),
    (2, 'Swimming', 25, 17.5),
    (3, 'Pickle Ball', 50, 60),
    (3, 'Running', 30, 45),
    (4, 'Swimming', 15, 10.5),
    (4, 'Pickle Ball', 30, 36),
    (5, 'Martial Arts', 25, 50),
    (5, 'Basketball', 20, 20);

-- Update logs (requires refactoring since SQLite does not support IFNULL or subqueries in the same way)
UPDATE logs
SET total_calories = (
    SELECT COALESCE(SUM(lfi.total_calories), 0) - COALESCE(SUM(lei.total_calories), 0)
    FROM log_food_items lfi
    LEFT JOIN log_exercise_items lei ON lfi.log_id = lei.log_id
    WHERE logs.log_id = lfi.log_id
);
