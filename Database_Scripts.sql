/* =============================================================
   BikeStore - Script de creacion de base de datos
   Motor: SQL Server
   ============================================================= */

IF DB_ID(N'BikeStore') IS NULL
BEGIN
    CREATE DATABASE BikeStore;
END
GO

USE BikeStore;
GO

/* =============================================================
   Elimina las tablas existentes respetando el orden de las FK
   (tablas hijas antes que tablas padre) para permitir reejecutar
   este script sin errores.
   ============================================================= */
IF OBJECT_ID(N'dbo.DetalleVenta', N'U') IS NOT NULL DROP TABLE dbo.DetalleVenta;
IF OBJECT_ID(N'dbo.Venta', N'U') IS NOT NULL DROP TABLE dbo.Venta;
IF OBJECT_ID(N'dbo.Bicicleta', N'U') IS NOT NULL DROP TABLE dbo.Bicicleta;
IF OBJECT_ID(N'dbo.Cliente', N'U') IS NOT NULL DROP TABLE dbo.Cliente;
IF OBJECT_ID(N'dbo.Categoria', N'U') IS NOT NULL DROP TABLE dbo.Categoria;
GO

/* =============================================================
   TABLA: Categoria
   ============================================================= */
CREATE TABLE dbo.Categoria
(
    IdCategoria    INT IDENTITY(1,1)      NOT NULL,
    Nombre         NVARCHAR(100)          NOT NULL,
    Descripcion    NVARCHAR(255)          NULL,
    Activo         BIT                    NOT NULL CONSTRAINT DF_Categoria_Activo DEFAULT (1),
    CONSTRAINT PK_Categoria PRIMARY KEY CLUSTERED (IdCategoria)
);
GO

/* =============================================================
   TABLA: Bicicleta
   ============================================================= */
CREATE TABLE dbo.Bicicleta
(
    IdBicicleta    INT IDENTITY(1,1)      NOT NULL,
    IdCategoria    INT                    NOT NULL,
    Marca          NVARCHAR(50)           NOT NULL,
    Modelo         NVARCHAR(100)          NOT NULL,
    Precio         DECIMAL(10,2)          NOT NULL,
    Stock          INT                    NOT NULL CONSTRAINT DF_Bicicleta_Stock DEFAULT (0),
    Estado         NVARCHAR(20)           NOT NULL CONSTRAINT DF_Bicicleta_Estado DEFAULT ('Activo'),
    CONSTRAINT PK_Bicicleta PRIMARY KEY CLUSTERED (IdBicicleta),
    CONSTRAINT FK_Bicicleta_Categoria FOREIGN KEY (IdCategoria) REFERENCES dbo.Categoria (IdCategoria),
    CONSTRAINT CK_Bicicleta_Precio CHECK (Precio >= 0),
    CONSTRAINT CK_Bicicleta_Stock CHECK (Stock >= 0),
    CONSTRAINT CK_Bicicleta_Estado CHECK (Estado IN ('Activo', 'Descontinuado'))
);
GO

/* =============================================================
   TABLA: Cliente
   ============================================================= */
CREATE TABLE dbo.Cliente
(
    IdCliente      INT IDENTITY(1,1)      NOT NULL,
    Cedula         VARCHAR(20)            NOT NULL,
    Nombres        NVARCHAR(100)          NOT NULL,
    Apellidos      NVARCHAR(100)          NOT NULL,
    Telefono       VARCHAR(20)            NULL,
    Correo         VARCHAR(150)           NULL,
    CONSTRAINT PK_Cliente PRIMARY KEY CLUSTERED (IdCliente),
    CONSTRAINT UQ_Cliente_Cedula UNIQUE (Cedula)
);
GO

/* =============================================================
   TABLA: Venta
   ============================================================= */
CREATE TABLE dbo.Venta
(
    IdVenta        INT IDENTITY(1,1)      NOT NULL,
    Fecha          DATETIME               NOT NULL CONSTRAINT DF_Venta_Fecha DEFAULT (GETDATE()),
    IdCliente      INT                    NOT NULL,
    Total          DECIMAL(10,2)          NOT NULL,
    CONSTRAINT PK_Venta PRIMARY KEY CLUSTERED (IdVenta),
    CONSTRAINT FK_Venta_Cliente FOREIGN KEY (IdCliente) REFERENCES dbo.Cliente (IdCliente),
    CONSTRAINT CK_Venta_Total CHECK (Total >= 0)
);
GO

/* =============================================================
   TABLA: DetalleVenta
   ============================================================= */
CREATE TABLE dbo.DetalleVenta
(
    IdDetalle      INT IDENTITY(1,1)      NOT NULL,
    IdVenta        INT                    NOT NULL,
    IdBicicleta    INT                    NOT NULL,
    Cantidad       INT                    NOT NULL,
    Precio         DECIMAL(10,2)          NOT NULL,
    Subtotal       DECIMAL(10,2)          NOT NULL,
    CONSTRAINT PK_DetalleVenta PRIMARY KEY CLUSTERED (IdDetalle),
    CONSTRAINT FK_DetalleVenta_Venta FOREIGN KEY (IdVenta) REFERENCES dbo.Venta (IdVenta) ON DELETE CASCADE,
    CONSTRAINT FK_DetalleVenta_Bicicleta FOREIGN KEY (IdBicicleta) REFERENCES dbo.Bicicleta (IdBicicleta),
    CONSTRAINT CK_DetalleVenta_Cantidad CHECK (Cantidad > 0),
    CONSTRAINT CK_DetalleVenta_Precio CHECK (Precio >= 0),
    CONSTRAINT CK_DetalleVenta_Subtotal CHECK (Subtotal >= 0)
);
GO

/* =============================================================
   DATOS DE PRUEBA: Categoria (5)
   ============================================================= */
INSERT INTO dbo.Categoria (Nombre, Descripcion, Activo) VALUES
    (N'Montaña',   N'Bicicletas para terreno montañoso y trails',        1),
    (N'Ruta',      N'Bicicletas de carretera orientadas a velocidad',     1),
    (N'Urbana',    N'Bicicletas para uso diario en ciudad',               1),
    (N'BMX',       N'Bicicletas para acrobacias y freestyle',             1),
    (N'Eléctrica', N'Bicicletas con motor de asistencia eléctrica',       1);
GO

/* =============================================================
   DATOS DE PRUEBA: Bicicleta (10)
   ============================================================= */
INSERT INTO dbo.Bicicleta (IdCategoria, Marca, Modelo, Precio, Stock, Estado) VALUES
    (1, N'Trek',         N'Marlin 7',           899.99, 15, 'Activo'),
    (1, N'Giant',        N'Talon 3',            749.50,  3, 'Activo'),
    (2, N'Specialized',  N'Allez Sport',       1450.00,  8, 'Activo'),
    (2, N'Cannondale',   N'CAAD13',            2200.00,  0, 'Activo'),
    (3, N'Trek',         N'FX 2 Disc',          599.99, 20, 'Activo'),
    (3, N'Giant',        N'Escape 3',           549.00, 12, 'Activo'),
    (4, N'GT',           N'Performer',          429.99,  5, 'Activo'),
    (4, N'Mongoose',     N'Legion L60',         389.99,  2, 'Activo'),
    (5, N'Cube',         N'Reaction Hybrid',   2650.00,  6, 'Activo'),
    (5, N'Cannondale',   N'Tesoro Neo',        2899.99,  0, 'Activo');
GO

/* =============================================================
   DATOS DE PRUEBA: Cliente (3)
   ============================================================= */
INSERT INTO dbo.Cliente (Cedula, Nombres, Apellidos, Telefono, Correo) VALUES
    ('1723456789', N'Juan Carlos',    N'Pérez Salazar',   '0991234567', 'juan.perez@example.com'),
    ('1789456123', N'María Fernanda', N'López Ortiz',     '0987654321', 'maria.lopez@example.com'),
    ('1712345678', N'Pedro José',     N'Ramírez Torres',  '0998765432', 'pedro.ramirez@example.com');
GO

/* =============================================================
   DATOS DE PRUEBA: Venta + DetalleVenta (2 ventas, IVA 15%)
   ============================================================= */

-- Venta 1: Cliente 1 -> Bicicleta 1 (x1) + Bicicleta 5 (x1)
-- Subtotal = 899.99 + 599.99 = 1499.98 | IVA 15% = 224.997 | Total = 1724.98
INSERT INTO dbo.Venta (Fecha, IdCliente, Total) VALUES
    ('2026-07-15T10:30:00', 1, 1724.98);

INSERT INTO dbo.DetalleVenta (IdVenta, IdBicicleta, Cantidad, Precio, Subtotal) VALUES
    (1, 1, 1, 899.99, 899.99),
    (1, 5, 1, 599.99, 599.99);

-- Venta 2: Cliente 2 -> Bicicleta 3 (x1) + Bicicleta 6 (x2)
-- Subtotal = 1450.00 + (2 * 549.00) = 2548.00 | IVA 15% = 382.20 | Total = 2930.20
INSERT INTO dbo.Venta (Fecha, IdCliente, Total) VALUES
    ('2026-07-20T15:45:00', 2, 2930.20);

INSERT INTO dbo.DetalleVenta (IdVenta, IdBicicleta, Cantidad, Precio, Subtotal) VALUES
    (2, 3, 1, 1450.00, 1450.00),
    (2, 6, 2,  549.00, 1098.00);
GO
