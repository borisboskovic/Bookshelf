/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2016                    */
/* Created on:     3/20/2021 10:33:14 PM                        */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Book') and o.name = 'FK_BOOK_WRITEN BY_AUTHOR')
alter table Book
   drop constraint "FK_BOOK_WRITEN BY_AUTHOR"
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Author')
            and   type = 'U')
   drop table Author
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Book')
            and   name  = 'Writen By_FK'
            and   indid > 0
            and   indid < 255)
   drop index Book."Writen By_FK"
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Book')
            and   type = 'U')
   drop table Book
go

/*==============================================================*/
/* Table: Author                                                */
/*==============================================================*/
create table Author (
   Id                   int                  identity,
   Name                 varchar(64)          not null,
   Surname              varchar(128)         not null,
   constraint PK_AUTHOR primary key (Id)
)
go

/*==============================================================*/
/* Table: Book                                                  */
/*==============================================================*/
create table Book (
   Id                   int                  identity,
   Aut_Id               int                  not null,
   Title                varchar(256)         not null,
   ISBN                 varchar(10)          null,
   ISBN13               varchar(13)          null,
   constraint PK_BOOK primary key (Id)
)
go

/*==============================================================*/
/* Index: "Writen By_FK"                                        */
/*==============================================================*/




create nonclustered index "Writen By_FK" on Book (Aut_Id ASC)
go

alter table Book
   add constraint "FK_BOOK_WRITEN BY_AUTHOR" foreign key (Aut_Id)
      references Author (Id)
go

