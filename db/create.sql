use master
go

create database UxComexTestAlineOliveira
go

use UxComexTestAlineOliveira
go

create table Users (
    [Id]             int           not null identity (1,1),
    [Name]           varchar(max)  not null,
    [Phone]          varchar(13)   not null,
    [Cpf]            varchar(11)   not null,

    primary key (Id)
)
go

create table Addresses (
    [Id]             int           not null identity (1,1),
    [AddressName]    varchar(max)  not null,
    [Cep]            varchar(8)    not null,
    [City]           varchar(max)  not null,
    [State]          varchar(max)  not null,
    
    [UserId]         int           not null,

    primary key (Id),
    foreign key (UserId) references Users (Id)
)
go