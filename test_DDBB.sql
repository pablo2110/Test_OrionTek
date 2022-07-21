TestTrabajo.dbo.Empresa definition

-- Drop table

-- DROP TABLE TestTrabajo.dbo.Empresa;

CREATE TABLE TestTrabajo.dbo.Empresa (
	empId int IDENTITY(0,1) NOT NULL,
	empNombre varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	empDireccion varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	empTelefono varchar(11) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	empLogoPatch varchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT Empresa_PK PRIMARY KEY (empId)
);


-- TestTrabajo.dbo.Clientes definition

-- Drop table

-- DROP TABLE TestTrabajo.dbo.Clientes;

CREATE TABLE TestTrabajo.dbo.Clientes (
	cliId int IDENTITY(0,1) NOT NULL,
	cliNombre varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	cliApellidos varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	cliCodigo varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	empId int DEFAULT 0 NOT NULL,
	cliFechaCreacion datetime NOT NULL,
	CONSTRAINT Clientes_PK PRIMARY KEY (cliId),
	CONSTRAINT Clientes_UN UNIQUE (cliCodigo),
	CONSTRAINT Clientes_FK FOREIGN KEY (empId) REFERENCES TestTrabajo.dbo.Empresa(empId)
);
CREATE UNIQUE NONCLUSTERED INDEX Clientes_UN ON TestTrabajo.dbo.Clientes (cliCodigo);


-- TestTrabajo.dbo.Usuarios definition

-- Drop table

-- DROP TABLE TestTrabajo.dbo.Usuarios;

CREATE TABLE TestTrabajo.dbo.Usuarios (
	usuId int IDENTITY(0,1) NOT NULL,
	usuSesion varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	usuContrasena varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	usuNombre varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	empId int NOT NULL,
	CONSTRAINT Usuarios_PK PRIMARY KEY (usuId),
	CONSTRAINT Usuarios_FK FOREIGN KEY (empId) REFERENCES TestTrabajo.dbo.Empresa(empId)
);


-- TestTrabajo.dbo.ClienteDirecciones definition

-- Drop table

-- DROP TABLE TestTrabajo.dbo.ClienteDirecciones;

CREATE TABLE TestTrabajo.dbo.ClienteDirecciones (
	cliDirId int IDENTITY(0,1) NOT NULL,
	cliId int NOT NULL,
	cliDirDireccion varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	cliDirSector varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	cliDirCiudad varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	cliDirPais varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	cliDirPrincipal bit DEFAULT 0 NOT NULL,
	CONSTRAINT ClienteDirecciones_PK PRIMARY KEY (cliDirId),
	CONSTRAINT ClienteDirecciones_FK FOREIGN KEY (cliId) REFERENCES TestTrabajo.dbo.Clientes(cliId)
);