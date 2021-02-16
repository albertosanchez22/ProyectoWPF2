-- Table: peliculas
DROP TABLE IF EXISTS peliculas;

CREATE TABLE peliculas (
    idPelicula   INTEGER PRIMARY KEY,
    titulo       TEXT,
    cartel       TEXT,
    año          INTEGER,
    genero       TEXT,
    calificacion TEXT
);


-- Table: salas
DROP TABLE IF EXISTS salas;

CREATE TABLE salas (
    idSala     INTEGER PRIMARY KEY AUTOINCREMENT,
    numero     TEXT,
    capacidad  INTEGER,
    disponible BOOLEAN DEFAULT (true) 
);


-- Table: sesiones
DROP TABLE IF EXISTS sesiones;

CREATE TABLE sesiones (
    idSesion INTEGER PRIMARY KEY AUTOINCREMENT,
    pelicula INTEGER REFERENCES peliculas (idPelicula),
    sala     INTEGER REFERENCES salas (idSala),
    hora     TEXT
);


-- Table: ventas
DROP TABLE IF EXISTS ventas;

CREATE TABLE ventas (
    idVenta  INTEGER PRIMARY KEY AUTOINCREMENT,
    sesion   INTEGER REFERENCES sesiones (idSesion),
    cantidad INTEGER,
    pago     TEXT
);
