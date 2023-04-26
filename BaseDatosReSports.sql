use resports;

CREATE TABLE Usuario (
  id INT PRIMARY KEY AUTO_INCREMENT,
  nombreUsuario VARCHAR(50) NOT NULL,
  contrasena VARCHAR(50) NOT NULL,
  email VARCHAR(50) NOT NULL,
  nombreCompleto VARCHAR(50) NOT NULL,
  genero VARCHAR(50) NOT NULL,
  edad INT NOT NULL,
  experiencia VARCHAR(50) NULL,
  posicion VARCHAR(50) NULL,
  rol INT NOT NULL,
  equipo_id INT
);
##equipos no debe tener foreing key
CREATE TABLE Equipos (
  id INT PRIMARY KEY AUTO_INCREMENT,
  nombre VARCHAR(50) NOT NULL,
  genero int NOT NULL
);

##Agregar las llaves foraneas

ALTER TABLE Usuario ADD CONSTRAINT fk_usuario_roles
	FOREIGN KEY (rol) REFERENCES RolUsuario(id);

ALTER TABLE Usuario ADD CONSTRAINT fk_usuario_equipos
  FOREIGN KEY (equipo_id) REFERENCES Equipos(id);

CREATE TABLE Publicacion (
  id INT PRIMARY KEY AUTO_INCREMENT,
  mensaje VARCHAR(255) NOT NULL,
  fecha_publicacion TIMESTAMP NOT NULL,
  usuario_id INT,
  FOREIGN KEY (usuario_id) REFERENCES Usuario(id)
);

CREATE TABLE Asistencia (
  id INT PRIMARY KEY AUTO_INCREMENT,
  fecha_asistencia DATE NOT NULL,
  asistio BIT NOT NULL,
  razonFalta VARCHAR(255) NULL,
  usuario_id INT,
  FOREIGN KEY (usuario_id) REFERENCES Usuario(id)
);

CREATE TABLE log_Reporte (
  id INT PRIMARY KEY AUTO_INCREMENT,
  fecha DATE NOT NULL,
  fallo BIT NOT NULL,
  descripccion VARCHAR(255)
);

CREATE TABLE rolUsuario (
  id INT PRIMARY KEY AUTO_INCREMENT,
  nombre varchar(255) NOT NULL
);


#Regex posiciones mockaroo ((delantero (central|lateral))|(portero)|(defensa (central|lateral))|(medio (central | intero))|(libero)|(armador)|(rematador)|(bloqueador(central|lateral))|(pivot)|(base)| (escolta)|(alero)){1}