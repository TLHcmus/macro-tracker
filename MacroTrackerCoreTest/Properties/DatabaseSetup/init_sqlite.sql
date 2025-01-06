-- Tao bang foods
create table foods (
	food_id int primary key auto_increment,
	name nvarchar(255),
    calories_per_100g double,
    protein_per_100g double,
    carbs_per_100g double,
    fat_per_100g double,
    image longblob 
);

-- Tao bang exercises
create table exercises (
	exercise_id int primary key auto_increment,
	name nvarchar(255),
    calories_per_minute double,
	image longblob  
);

-- Tao bang users
create table users (
	user_id int primary key auto_increment,
	username nvarchar(255),
    encrypted_password nvarchar(255) 
);

-- Tao bang goal
create table goal (
	goal_id int primary key auto_increment,
	calories int,
    protein int, 
    carbs int,
    fat int,
    user_id int,
    foreign key (user_id) references users(user_id) on delete cascade
);

-- Tao bang log
create table logs (
    log_id int primary key auto_increment, 
    log_date date,            
    total_calories double default 0,
    user_id int,
    foreign key (user_id) references users(user_id) on delete cascade
);

-- Tao bang log_food_items
create table log_food_items (
	log_food_id int primary key auto_increment,
    log_id int,
    food_id int,
    number_of_servings double,
    total_calories double,
    foreign key (log_id) references logs(log_id) on delete cascade,
    foreign key (food_id) references foods(food_id) on delete cascade
);

-- Tao bang exercise_food_items
create table log_exercise_items (
	log_exercise_id int primary key auto_increment,
    log_id int,
    exercise_id int,
    duration double,
    total_calories double,
    foreign key (log_id) references logs(log_id) on delete cascade,
    foreign key (exercise_id) references exercises(exercise_id) on delete cascade
);



-- Them data vao bang exercises
insert into exercises (name, calories_per_minute, image)
values
	('Basketball', 10, LOAD_FILE('C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/MacroTracker/Assets/Exercises/basketball.png')),
    ('Martial Arts', 20, LOAD_FILE('C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/MacroTracker/Assets/Exercises/martial-arts.png')),
    ('Running', 15, LOAD_FILE('C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/MacroTracker/Assets/Exercises/running.png')),
    ('Swimming', 7, LOAD_FILE('C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/MacroTracker/Assets/Exercises/swimming.png')),
    ('Pickle Ball', 12, LOAD_FILE('C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/MacroTracker/Assets/Exercises/pickle-ball.png'));
    
-- Them data vao bang foods
insert into foods (name, calories_per_100g, protein_per_100g, carbs_per_100g, fat_per_100g, image)
values
	('Chicken breast', 120, 22.5, 0, 2.6, LOAD_FILE('C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/MacroTracker/Assets/Foods/chicken-breast.png')),
    ('White rice', 129, 2.7, 27.6, 0.3, LOAD_FILE('C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/MacroTracker/Assets/Foods/white-rice.png')),
    ('Pork Chops', 115, 20.3, 0.9, 4, LOAD_FILE('C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/MacroTracker/Assets/Foods/pork-chops.png')),
    ('Oat meal', 379, 13.1, 57.6, 6.5, LOAD_FILE('C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/MacroTracker/Assets/Foods/oat-meal.png')),
    ('Beef', 152, 20.5, 0, 7.1, LOAD_FILE('C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/MacroTracker/Assets/Foods/beef.png'));
    
-- Them data vao bang user
insert into users (username, encrypted_password)
values ('admin', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3' );

insert into users (username, encrypted_password)
values ('user', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92' );

insert into users (username, encrypted_password)
values ('tule', 'ce6835d490575fd7788b91618a9e393e7f17f994ba3ecd7c92ff71ebd66071c9' );


-- Them data vao bang goal
insert into goal (calories, protein, carbs, fat, user_id)
values
    (2500, 156, 313, 69, 1),  -- goal cho user_id = 1
    (2200, 140, 280, 60, 2),  -- goal cho user_id = 2
    (2400, 150, 300, 70, 3);  -- goal cho user_id = 3


-- Them data vao cac logs
-- Thêm d? li?u vào b?ng logs (10 logs)
INSERT INTO logs (log_date, total_calories, user_id)
VALUES
    ('2024-12-23', 0, 1),
    ('2024-12-24', 0, 1),
    ('2024-12-25', 0, 2),
    ('2024-12-26', 0, 2),
    ('2024-12-27', 0, 3),
    ('2024-12-28', 0, 3),
    ('2024-12-29', 0, 1),
    ('2024-12-30', 0, 1),
    ('2024-12-31', 0, 2),
    ('2025-01-01', 0, 3);

-- Thêm d? li?u vào b?ng log_food_items (m?i log có 5 món ãn)
INSERT INTO log_food_items (log_id, food_id, number_of_servings, total_calories)
VALUES
    -- Log 1
    (1, 1, 150, 180),  -- Chicken breast 150g
    (1, 2, 200, 258),  -- White rice 200g
    (1, 4, 200, 758),  -- Oat meal 200g
    (1, 3, 100, 115),  -- Pork Chops 100g
    (1, 5, 150, 228),  -- Beef 150g
    
    -- Log 2
    (2, 1, 200, 240),  -- Chicken breast 200g
    (2, 2, 150, 193.5),-- White rice 150g
    (2, 4, 100, 379),  -- Oat meal 100g
    (2, 3, 200, 230),  -- Pork Chops 200g
    (2, 5, 100, 152),  -- Beef 100g
    
    -- Log 3
    (3, 1, 100, 120),  -- Chicken breast 100g
    (3, 2, 120, 154.8),-- White rice 120g
    (3, 3, 150, 172.5),-- Pork Chops 150g
    (3, 4, 150, 568.5),-- Oat meal 150g
    (3, 5, 200, 304),  -- Beef 200g

    -- Log 4
    (4, 1, 180, 216),  -- Chicken breast 180g
    (4, 2, 100, 129),  -- White rice 100g
    (4, 4, 200, 758),  -- Oat meal 200g
    (4, 3, 150, 172.5),-- Pork Chops 150g
    (4, 5, 150, 228),  -- Beef 150g
    
    -- Log 5
    (5, 1, 120, 144),  -- Chicken breast 120g
    (5, 2, 200, 258),  -- White rice 200g
    (5, 3, 100, 115),  -- Pork Chops 100g
    (5, 4, 100, 379),  -- Oat meal 100g
    (5, 5, 150, 228),  -- Beef 150g
    
    -- Log 6
    (6, 1, 150, 180),  -- Chicken breast 150g
    (6, 2, 200, 258),  -- White rice 200g
    (6, 4, 250, 948),  -- Oat meal 250g
    (6, 3, 150, 172.5),-- Pork Chops 150g
    (6, 5, 120, 182.4),-- Beef 120g
    
    -- Log 7
    (7, 2, 100, 129),  -- White rice 100g
    (7, 3, 150, 172.5),-- Pork Chops 150g
    (7, 5, 200, 304),  -- Beef 200g
    (7, 4, 100, 379),  -- Oat meal 100g
    (7, 1, 200, 240),  -- Chicken breast 200g
    
    -- Log 8
    (8, 5, 150, 228),  -- Beef 150g
    (8, 3, 100, 115),  -- Pork Chops 100g
    (8, 2, 120, 154.8),-- White rice 120g
    (8, 4, 200, 758),  -- Oat meal 200g
    (8, 1, 100, 120),  -- Chicken breast 100g
    
    -- Log 9
    (9, 1, 150, 180),  -- Chicken breast 150g
    (9, 2, 200, 258),  -- White rice 200g
    (9, 4, 250, 948),  -- Oat meal 250g
    (9, 3, 100, 115),  -- Pork Chops 100g
    (9, 5, 150, 228);  -- Beef 150g

-- Thêm d? li?u vào b?ng log_exercise_items (m?i log có 2 bài t?p)
INSERT INTO log_exercise_items (log_id, exercise_id, duration, total_calories)
VALUES
    -- Log 1
    (1, 1, 10, 100),  -- Basketball 10 minutes
    (1, 3, 10, 150),  -- Running 10 minutes
    
    -- Log 2
    (2, 2, 8, 160),   -- Martial Arts 8 minutes
    (2, 4, 6, 42),    -- Swimming 6 minutes
    
    -- Log 3
    (3, 5, 12, 144),  -- Pickle Ball 12 minutes
    (3, 3, 7, 105),   -- Running 7 minutes
    
    -- Log 4
    (4, 1, 6, 60),    -- Basketball 6 minutes
    (4, 2, 15, 300),  -- Martial Arts 15 minutes
    
    -- Log 5
    (5, 3, 12, 180),  -- Running 12 minutes
    (5, 4, 8, 56),    -- Swimming 8 minutes
    
    -- Log 6
    (6, 1, 15, 150),  -- Basketball 15 minutes
    (6, 2, 10, 200),  -- Martial Arts 10 minutes
    
    -- Log 7
    (7, 5, 7, 84),    -- Pickle Ball 7 minutes
    (7, 4, 10, 70),   -- Swimming 10 minutes
    
    -- Log 8
    (8, 3, 8, 120),   -- Running 8 minutes
    (8, 5, 10, 120),  -- Pickle Ball 10 minutes
    
    -- Log 9
    (9, 2, 6, 120),   -- Martial Arts 6 minutes
    (9, 4, 10, 70);   -- Swimming 10 minutes

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

