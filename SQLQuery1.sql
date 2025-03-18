Create table Articulos(
Codigo int primary key,
Descripcion varchar(50) not null,
Precio float not null
)

insert into Articulos values
(1,'Placa de video',75000),
(2,'Monitor',5000),
(3,'Procesador',2500)

select * from Articulos