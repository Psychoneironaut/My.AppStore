SET IDENTITY_INSERT Countries ON

INSERT INTO Countries(ID, Name, Abbreviation) VALUES
(1, 'United States of America', 'USA'),
(2, 'Canada', 'CAN')

SET IDENTITY_INSERT Countries OFF

INSERT INTO States(CountryID, Abbreviation, Name) VALUES

(1, 'AL', 'Alabama'),
(1, 'AK', 'Alaska'),
(1, 'AZ', 'Arizona'),
(1, 'AR', 'Arkansas'),
(1, 'CA', 'California'),
(1, 'CO', 'Colorado'),
(1, 'CT', 'Connecticut'),
(1, 'DE', 'Delaware'),
(1, 'DC', 'District Of Columbia'),
(1, 'FL', 'Florida'),
(1, 'GA', 'Georgia'),
(1, 'HI', 'Hawaii'),
(1, 'ID', 'Idaho'),
(1, 'IL', 'Illinois'),
(1, 'IN', 'Indiana'),
(1, 'IA', 'Iowa'),
(1, 'KS', 'Kansas'),
(1, 'KY', 'Kentucky'),
(1, 'LA', 'Louisiana'),
(1, 'ME', 'Maine'),
(1, 'MD', 'Maryland'),
(1, 'MA', 'Massachusetts'),
(1, 'MI', 'Michigan'),
(1, 'MN', 'Minnesota'),
(1, 'MS', 'Mississippi'),
(1, 'MO', 'Missouri'),
(1, 'MT', 'Montana'),
(1, 'NE', 'Nebraska'),
(1, 'NV', 'Nevada'),
(1, 'NH', 'New Hampshire'),
(1, 'NJ', 'New Jersey'),
(1, 'NM', 'New Mexico'),
(1, 'NY', 'New York'),
(1, 'NC', 'North Carolina'),
(1, 'ND', 'North Dakota'),
(1, 'OH', 'Ohio'),
(1, 'OK', 'Oklahoma'),
(1, 'OR', 'Oregon'),
(1, 'PA', 'Pennsylvania'),
(1, 'RI', 'Rhode Island'),
(1, 'SC', 'South Carolina'),
(1, 'SD', 'South Dakota'),
(1, 'TN', 'Tennessee'),
(1, 'TX', 'Texas'),
(1, 'UT', 'Utah'),
(1, 'VT', 'Vermont'),
(1, 'VA', 'Virginia'),
(1, 'WA', 'Washington'),
(1, 'WV', 'West Virginia'),
(1, 'WI', 'Wisconsin'),
(1, 'WY', 'Wyoming'),
(2, 'AB', 'Alberta'),
(2, 'BC', 'British Columbia'),
(2, 'MB', 'Manitoba'),
(2, 'NB', 'New Brunswick'),
(2, 'NL', 'Newfoundland and Labrador'),
(2, 'NS', 'Nova Scotia'),
(2, 'NT', 'Northwest Territories'),
(2, 'NU', 'Nunavut'),
(2, 'ON', 'Ontario'),
(2, 'PE', 'Prince Edward Island'),
(2, 'QC', 'Quebec'),
(2, 'SK', 'Saskatchewan'),
(2, 'YT', 'Yukon')

INSERT INTO Products(Name, Price, [Description]) VALUES
('Dimensia', 9.99, 'The first video game I ever made.'),
('Mutagen', 4.99, 'A text-based sci-fi browser game.'),
('The Labyrinth', 19.99, 'Unfinished first-person VR maze game.')

INSERT INTO ProductImages([FilePath], ProductID, AltText) VALUES
('/content/images/dimensia.png', (SELECT TOP 1 ID FROM Products WHERE Name = 'Dimensia'), 'Dimensia'),
('/content/images/mutagen.png', (SELECT TOP 1 ID FROM Products WHERE Name = 'Mutagen'), 'Mutagen'),
('/content/images/maze.jpg', (SELECT TOP 1 ID FROM Products WHERE Name = 'The Labyrinth'), 'The Labyrinth')

INSERT INTO Categories(Name, [Description]) VALUES
('Application', 'Any app whose purpose is anything besides play.'),
('Game', 'Any app whose purpose is play.')

INSERT INTO CategoriesProducts(CategoryID, ProductID) VALUES
((SELECT TOP 1 ID FROM Categories WHERE Name = 'Applications'), (SELECT TOP 1 ID FROM Products WHERE Name = 'New Game')),
((SELECT TOP 1 ID FROM Categories WHERE Name = 'Games'), (SELECT TOP 1 ID FROM Products WHERE Name = 'Dimensia'))

SELECT Name, Price, [Description], COUNT(*)
FROM Products
GROUP BY Name, Price, [Description]
HAVING COUNT(*) > 1;

SELECT * FROM ProductImages