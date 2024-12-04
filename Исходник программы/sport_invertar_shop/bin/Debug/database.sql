create database sport_invertar_shop
go
use sport_invertar_shop
go

-- Роли
create table [roles]
(
	[id] int identity primary key,
	[name] varchar(50),
	[description] varchar(200) default 'Типичное описание роли',
);
-- Пользователи
create table [users]
(
	[id] int identity primary key,
	[login] varchar(40) UNIQUE not null,
	[password] varchar(40) not null default '123456',
	[surname] varchar(100) not null default 'Не указано',
	[name] varchar(100) not null default 'Не указано',
	[lastname] varchar(100) not null default 'Не указано',
	[created] datetime default sysdatetime(),
	[role] int foreign key references [roles]([id]) not null default '1' ,
	[money] money default '0',
	[block] bit not null default '0'
);
-- Поставщики
create table [providers]
(
	[id] int identity primary key,
	[name] varchar(100),
	[description] varchar(800) default 'Бригадир еще не добавил описание о себе',
	[created] datetime default sysdatetime(),
	[user] int foreign key references [users]([id]) not null,
);
-- Категории товаров
create table [categories]
(
	[id] int identity primary key,
	[name] varchar(50) default 'Наименование спецификации',
	[description] varchar(200) default 'Нет описания спецификации',
);
-- Товары
create table [products]
(
	[id] int identity primary key,
	[name] varchar(100) default 'Бригада',
	[description] varchar(200) default 'Описание бригады ещё не добавлено',
	[provider] int foreign key references [providers]([id]) default '1',
	[categoria] int foreign key references [categories]([id]) default '1',
	[img] varchar(max) not null default '/img/none.png',
	[added] datetime default sysdatetime(),
	[coste] money default '0',
	[availability] bit default '0',
);
-- Корзины товаров
create table [baskets]
(
	[id] int identity primary key,
	[user] int foreign key references [users]([id]) ON DELETE CASCADE,
	[product] int foreign key references [products]([id]) ON DELETE CASCADE,
	[quantity] int default '0',
	[added] datetime default sysdatetime(),
	[execution] bit default '0'
);

insert into [roles]([name],[description])
values('Заказчик','Совершайте найм и довольствуйтесь качеством работ'),
('Бригадир','Добавляйте бригады для агенства'),
('Адмнистратор','Производите редактирование и контроллирование программы')
go

insert into [users]([login],[password],[role],[money])
values('user','user','1','100'),
('postav','postav','2','2000'),
('admin','admin','3','10000')
go

insert into [providers]([name],[description],[user])
values('ИП Бригада','Всё для ремонта, сделал сам','2')
go

insert into [categories]([name],[description])
values('Все спецификации',' - '), 
('Штукатуры','Бригада высокой квалификации по штукатурным работам'), 
('Плиточники','Бригада для професиональной укладки плитки'), 
('Электрики','Бригада по работам с электроприборами'), 
('Сантехники','Бригада для работы с сантехникой'), 
('Потолочники','Специализируются на работе с потолками'), 
('Чернорабочие','Бригада для выполнения черновых работ')
go


insert into [products]([name])
values('Бригада 1'),
('Бригада 2'),
('Бригада 3')
go

insert into [baskets]([user],[product])
values('1', '1'),
('1', '2'),
('1', '3')
go



-- Процедура авторизации
create procedure [login]
@login varchar(30), @password varchar(30) as
begin
if exists(select * from [users] where [login] = @login)
begin
select * from [users] where [login] = @login and [password] = @password
end
else
begin
print 'Аккаунт не найден'
end
end
go

-- процедура регистрации
create procedure [signup]
@login varchar(30), @password varchar(30) as
begin
if exists(select * from [users] where [login] = @login)
begin
print 'Такой логин занят'
end
else
begin
insert into[users]([login],[password])
values (@login,@password)
end
end
go

create procedure [check_login]
@login varchar(30) as
begin
	select [login] from [users] where [login] = @login
end
go

create procedure [allproduct] as
begin
	select * from [products]
end
go

create procedure [allcategories] as
begin
	select * from [categories]
end
go




create procedure [product]
@id int
as
begin
	select * from [products] where [id] = @id
end
go

create procedure [get_user] 
@id int
as
begin
	select * from [users] where [id] = @id
end
go

create procedure [get_search] 
@search varchar(max), @code int,  @sort int
as
if(@code != 1)
begin
	select * from [products] where [name] like @search and [categoria] = @code
end
else
begin
	select * from [products] where [name] like @search
end
go

create procedure [get_basket]
as
begin
	select * from [baskets]
end
go

exec [get_basket]
go



create view [my_basket] 
([id], [user], [product], [img], [description], [coste], [quantity], [execution]) as 
select [b].[id], [u].[id], [p].[name], [p].[img], [p].[description],[p].[coste], [b].[quantity], [b].[execution]
from [baskets] [b] inner join [users] [u] on [b].[user] = [u].[id] inner join [products] [p] on [b].[product] = [p].[id]
where [execution] != 1
go

create view [info_baskets] 
([id], [user], [product], [img], [description], [coste], [quantity], [execution]) as 
select [b].[id], [u].[id], [p].[name], [p].[img], [p].[description],[p].[coste], [b].[quantity], [b].[execution]
from [baskets] [b] inner join [users] [u] on [b].[user] = [u].[id] inner join [products] [p] on [b].[product] = [p].[id]
go

create procedure [info_basket]
@id int
as
begin
	select *
	from [info_baskets]
	where [id] = @id
end
go


create procedure [get_my_basket]
@id int
as
begin
	select *
	from [my_basket]
	where [user] = @id
end
go

create procedure [update_account]
@id int, @password varchar(30), @name varchar(50), @surname varchar(50), @firstname varchar(50)
as
begin
update [users] set [password] = replace(@password, '',''),
[surname] = replace(@name, '',''),
[name] = replace(@surname, '',''),
[lastname] = replace(@firstname, '','')
where [id] = @id;
end
go

create procedure [update_product]
@id int, @name varchar(100),@description varchar(200),@provider int, @categoria int,@img varchar(max),@coste money,@availability bit
as
begin
update [products] set [name] = replace(@name, '',''),
[description] = replace(@description, '',''),
[provider] = replace(@provider, '',''),
[categoria] = replace(@categoria, '',''),
[img] = replace(@img, '',''),
[coste] = replace(@coste, '',''),
[availability] = replace(@availability, '','')
where [id] = @id;
end
go



create procedure [me_providers]
@id int
as
begin
	select *
	from [providers]
	where [user] = @id
end
go

create procedure [providers_prodo]
@id int
as
begin
	select *
	from [products]
	where [provider] = @id
end
go


create procedure [drop_product]
@id int
as
begin
	select *
	from [products]
	where [provider] = @id
end
go


create procedure [pay]
@user int, @product int, @money1 money, @money2 money
as
begin
select @money1 = [money]
from [users]
where [id] = @user
select @money2 =[coste]
from [products]
where [id] = @product;
if(@money1 > @money2)
begin
print 'Недостаточно средств'
end
else
begin
update [users] set [money] = replace(@money1 - @money2, ' ',' ')
where [id] = @user;
delete from [baskets] where [id] = @product;
end
end
go
