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