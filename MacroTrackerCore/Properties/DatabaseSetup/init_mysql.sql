-- Tao bang foods
create table foods (
	name nvarchar(255) primary key,
    calories_per_100g double,
    protein_per_100g double,
    carbs_per_100g double,
    fat_per_100g double,
    icon_file_name nvarchar(255) 
);

-- Tao bang exercises
create table exercises (
	name nvarchar(255) primary key,
    calories_per_minute double,
	icon_file_name nvarchar(255) 
);

-- Tao bang users
create table users (
	username nvarchar(255) primary key,
    encrypted_password nvarchar(255) 
);

-- Tao bang goal
create table goal (
	calories int,
    protein int, 
    carbs int,
    fat int
);

-- Tao bang log
create table logs (
    log_id int primary key auto_increment, 
    log_date date,            
    total_calories double default 0        
);

-- Tao bang log_food_items
create table log_food_items (
	log_food_id int primary key auto_increment,
    log_id int,
    food_name nvarchar(255),
    number_of_servings double,
    total_calories double,
    foreign key (log_id) references logs(log_id) on delete cascade,
    foreign key (food_name) references foods(name) on delete cascade
);

-- Tao bang exercise_food_items
create table log_exercise_items (
	log_exercise_id int primary key auto_increment,
    log_id int,
    exercise_name nvarchar(255),
    duration double,
    total_calories double,
    foreign key (log_id) references logs(log_id) on delete cascade,
    foreign key (exercise_name) references exercises(name) on delete cascade
);



-- Them data vao bang exercises
insert into exercises (name, calories_per_minute, icon_file_name)
values
	('Basketball', 1, 'basketball.png'),
    ('Martial Arts', 2, 'martialarts.png'),
    ('Running', 1.5, 'running.png'),
    ('Swimming', 0.7, 'swimming.png'),
    ('Pickle Ball', 1.2, 'pickleball.png');
    
-- Them data vao bang foods
insert into foods (name, calories_per_100g, protein_per_100g, carbs_per_100g, fat_per_100g, icon_file_name)
values
	('Chicken breast', 120, 22.5, 0, 2.6, 'chicken_breast.png'),
    ('White rice', 129, 2.7, 27.6, 0.3, 'white_rice.png'),
    ('Pork Chops', 115, 20.3, 0.9, 4, 'pork_chops.png'),
    ('Oat meal', 379, 13.1, 57.6, 6.5, 'oat_meal.png'),
    ('Beef', 152, 20.5, 0, 7.1, 'beef.png');

-- Them data vao bang goal
insert into goal (calories, protein, carbs, fat)
values (2500, 156, 313, 69);

-- Them data vao bang user
insert into users (username, encrypted_password)
values ('admin', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3' );

-- Them data vao cac logs
-- Thêm dữ liệu vào bảng logs
INSERT INTO logs (log_date, total_calories)
VALUES
    ('2024-12-18', 0),
    ('2024-12-19', 0),
    ('2024-12-20', 0),
    ('2024-12-21', 0),
    ('2024-12-22', 0);

-- Thêm dữ liệu vào bảng log_food_items
INSERT INTO log_food_items (log_id, food_name, number_of_servings, total_calories)
VALUES
    (1, 'Chicken breast', 1.5, 180),      -- Log 1: Chicken breast 1.5 servings
    (1, 'White rice', 2, 258),             -- Log 1: White rice 2 servings
    (2, 'Pork Chops', 1, 115),             -- Log 2: Pork Chops 1 serving
    (2, 'Oat meal', 2, 758),              -- Log 2: Oat meal 2 servings
    (3, 'Beef', 1.2, 182.4),              -- Log 3: Beef 1.2 servings
    (3, 'White rice', 1.5, 193.5),        -- Log 3: White rice 1.5 servings
    (4, 'Chicken breast', 2, 240),        -- Log 4: Chicken breast 2 servings
    (4, 'Pork Chops', 1, 115),             -- Log 4: Pork Chops 1 serving
    (5, 'Oat meal', 1.5, 568.5),          -- Log 5: Oat meal 1.5 servings
    (5, 'Beef', 1, 152);                  -- Log 5: Beef 1 serving

-- Thêm dữ liệu vào bảng log_exercise_items
INSERT INTO log_exercise_items (log_id, exercise_name, duration, total_calories)
VALUES
    (1, 'Basketball', 30, 30),        -- Log 1: Basketball 30 minutes
    (1, 'Running', 20, 30),           -- Log 1: Running 20 minutes
    (2, 'Martial Arts', 40, 80),      -- Log 2: Martial Arts 40 minutes
    (2, 'Swimming', 25, 17.5),        -- Log 2: Swimming 25 minutes
    (3, 'Pickle Ball', 50, 60),       -- Log 3: Pickle Ball 50 minutes
    (3, 'Running', 30, 45),           -- Log 3: Running 30 minutes
    (4, 'Swimming', 15, 10.5),        -- Log 4: Swimming 15 minutes
    (4, 'Pickle Ball', 30, 36),       -- Log 4: Pickle Ball 30 minutes
    (5, 'Martial Arts', 25, 50),      -- Log 5: Martial Arts 25 minutes
    (5, 'Basketball', 20, 20);        -- Log 5: Basketball 20 minutes

-- Dieu chinh tong calories cua log
UPDATE logs
SET total_calories = (
    SELECT 
        IFNULL(SUM(log_food_items.total_calories), 0) - IFNULL(SUM(log_exercise_items.total_calories), 0)
    FROM log_food_items
    LEFT JOIN log_exercise_items
        ON log_food_items.log_id = log_exercise_items.log_id
    WHERE logs.log_id = log_food_items.log_id
    GROUP BY logs.log_id
)
WHERE EXISTS (
    SELECT 1 
    FROM log_food_items
    WHERE logs.log_id = log_food_items.log_id
);