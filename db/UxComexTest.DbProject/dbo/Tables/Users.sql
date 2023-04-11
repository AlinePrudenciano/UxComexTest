create table Users (
    [Id]             int           not null identity (1,1),
    [Name]           varchar(max)  not null,
    [Phone]          varchar(13)   not null,
    [Cpf]            varchar(11)   not null,

    primary key (Id)
)